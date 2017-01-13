using System;
using System.Collections;
using System.Data;
using System.Linq;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// schema column
    /// </summary>
    public class SchemaMappingColumn
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public SchemaMappingColumn()
        {
        }

        /// <summary>
        /// Initialize properties
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dataType"></param>
        /// <param name="allowDbNull"></param>
        public SchemaMappingColumn(string name, Type dataType, bool allowDbNull)
        {
            Expect.IsNotBlank(name, nameof(name));
            Expect.IsNotNull(dataType, nameof(dataType));

            this.ColumnName = name;
            this.DataType = dataType;
            this.AllowDBNull = allowDbNull;
        }

        /// <summary>
        /// Base column name
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        /// Data type
        /// </summary>
        public Type DataType { get; private set; }

        /// <summary>
        /// Allow DB null
        /// </summary>
        public bool AllowDBNull { get; private set; }

        /// <summary>
        /// Load information from row
        /// </summary>
        /// <param name="row"></param>
        internal void Load(DataRow row)
        {
            Expect.IsNotNull(row, nameof(row));

            ColumnName = (string)row["ColumnName"];
            DataType = (Type)row["DataType"];
            AllowDBNull = (bool)row["AllowDBNull"];
        }

        /// <summary>
        /// Get Clr type for property
        /// </summary>
        public Type ClrType
        {
            get
            {
                var clrType = DataType;
                if (clrType.IsValueType && !clrType.IsNullableType() && AllowDBNull)
                    clrType = clrType.MakeNullableType();
                return clrType;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format("SchemaColumn(ColumnName={0}, DataType={1}, AllowDBNull={2})", ColumnName, DataType, AllowDBNull);
        }

        /// <summary>
        /// Sort columns according by order defined in <paramref name="columnNames"/>
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="columnNames"></param>
        public static void SortByName(SchemaMappingColumn[] columns, params string[] columnNames)
        {
            Expect.IsNotNull(columns, nameof(columns));
            Expect.IsNotNull(columnNames, nameof(columnNames));

            Array.Sort(columns, new ColumnNameComparer(columnNames));
        }
    }

    internal class ColumnNameComparer : IComparer
    {
        public ColumnNameComparer(string[] names)
        {
            Expect.IsNotNull(names, nameof(names));

            _names = names.Select(x => x.ToLowerInvariant()).ToArray();
        }

        private readonly string[] _names;

        public int Compare(object x, object y)
        {
            return GetOrder(x).CompareTo(GetOrder(y));
        }

        private int GetOrder(object obj)
        {
            var column = obj as SchemaMappingColumn;
            Expect.IsNotNull(column, nameof(column));
            string columnName = column.ColumnName.ToLowerInvariant();
            int index = _names.ToList().IndexOf(columnName);
            return index >= 0 ? index : 9999;
        }
    }
}
