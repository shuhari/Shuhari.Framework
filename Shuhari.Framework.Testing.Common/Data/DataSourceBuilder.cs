using System;
using System.Collections.Generic;
using System.Data;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Build test source data, such as schema mapping, IDataReader, and so on.
    /// </summary>
    public static class DataSourceBuilder
    {
        /// <summary>
        /// Build schema table for mock of IDataReader
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static DataTable BuildSchemaTable(params SchemaMappingColumn[] columns)
        {
            Expect.IsNotNull(columns, nameof(columns));

            var table = new DataTable();
            AddColumn(table, "ColumnName", typeof(string));
            AddColumn(table, "DataType", typeof(Type));
            AddColumn(table, "AllowDBNull", typeof(bool));

            foreach (var column in columns)
                AddRow(table, column);

            return table;
        }

        private static void AddColumn(DataTable table, string columnName, Type type)
        {
            Expect.IsNotNull(table, nameof(table));
            Expect.IsNotBlank(columnName, nameof(columnName));
            Expect.IsNotNull(type, nameof(type));

            table.Columns.Add(new DataColumn { ColumnName = columnName, DataType = type });
        }

        private static void AddRow(DataTable table, SchemaMappingColumn column)
        {
            Expect.IsNotNull(table, nameof(table));
            Expect.IsNotNull(column, nameof(column));

            var row = table.NewRow();
            row["ColumnName"] = column.ColumnName;
            row["DataType"] = column.DataType;
            row["AllowDBNull"] = column.AllowDBNull;
            table.Rows.Add(row);
        }

        /// <summary>
        /// Build <see cref="IDataReader"/> that can return schema table and data
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IDataReader BuildDataReader(SchemaMappingColumn[] columns, object[][] data)
        {
            Expect.IsNotNull(columns, nameof(columns));
            Expect.IsNotNull(data, nameof(data));

            var schemaTable = BuildSchemaTable(columns);
            var reader = new MockDataReader(schemaTable, data);

            return reader;
        }

        /// <summary>
        /// Build schema table from entity mapping
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityMapping"></param>
        /// <param name="additionColumns">Additional columns not defined in mapper</param>
        /// <returns></returns>
        public static DataTable BuildSchemaTableFromEntityMapping<T>(IEntityMapper<T> entityMapping,
            params SchemaMappingColumn[] additionColumns)
            where T: class
        {
            Expect.IsNotNull(entityMapping, nameof(entityMapping));
            var columns = new List<SchemaMappingColumn>();

            foreach (var fieldMapper in entityMapping.FieldMappers)
            {
                Type dataType = fieldMapper.PropertyType;
                bool allowDbNull = !dataType.IsValueType;
                if (dataType.IsNullableType())
                {
                    dataType = dataType.GetNullableBaseType();
                    allowDbNull = !dataType.IsValueType;
                }
                if (dataType.IsEnum)
                    dataType = typeof(int);
                var column = new SchemaMappingColumn(fieldMapper.FieldName, dataType, allowDbNull);
                columns.Add(column);
            }

            if (additionColumns != null)
                columns.AddRange(additionColumns);

            return BuildSchemaTable(columns.ToArray());
        }

        /// <summary>
        /// Build schema mapping from entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityMapping"></param>
        /// <param name="additionColumns"></param>
        /// <returns></returns>
        public static SchemaMapping BuildSchemaMappingFromEntityMapping<T>(IEntityMapper<T> entityMapping,
            params SchemaMappingColumn[] additionColumns)
            where T : class
        {
            Expect.IsNotNull(entityMapping, nameof(entityMapping));
            var schemaTable = BuildSchemaTableFromEntityMapping(entityMapping);
            var schemaMapping = new SchemaMapping();
            schemaMapping.Load(schemaTable);

            if (additionColumns != null)
                foreach (var column in additionColumns)
                    schemaMapping.AddColumn(column);

            return schemaMapping;
        }
    }
}
