using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Read schema from data reader
    /// </summary>
    public class SchemaMapping
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public SchemaMapping()
        {
            _columns = new List<SchemaMappingColumn>();
        }

        private readonly List<SchemaMappingColumn> _columns;

        /// <summary>
        /// All columns
        /// </summary>
        public IEnumerable<SchemaMappingColumn> Columns => _columns.AsReadOnly();

        /// <summary>
        /// Add column
        /// </summary>
        /// <param name="column"></param>
        public void AddColumn(SchemaMappingColumn column)
        {
            Expect.IsNotNull(column, nameof(column));

            _columns.Add(column);
        }

        /// <summary>
        /// Find column by name
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public SchemaMappingColumn FindColumn(string columnName)
        {
            Expect.IsNotBlank(columnName, nameof(columnName));

            return _columns.FirstOrDefault(c => c.ColumnName.EqualsNoCase(columnName));
        }

        /// <summary>
        /// Read from data
        /// </summary>
        /// <param name="table"></param>
        public void Load(DataTable table)
        {
            Expect.IsNotNull(table, nameof(table));
            _columns.Clear();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                var column = new SchemaMappingColumn();
                column.Load(row);
                AddColumn(column);
            }
        }

        /// <summary>
        /// Dump schema information
        /// </summary>
        public string Dump()
        {
            return string.Join(Environment.NewLine, _columns);
        }
    }
}
