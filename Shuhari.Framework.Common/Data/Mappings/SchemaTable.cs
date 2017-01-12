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
    public class SchemaTable
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public SchemaTable()
        {
            _columns = new List<SchemaColumn>();
        }

        private List<SchemaColumn> _columns;

        /// <summary>
        /// All columns
        /// </summary>
        public IEnumerable<SchemaColumn> Columns
        {
            get { return _columns.AsReadOnly(); }
        }

        /// <summary>
        /// Find column by name
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public SchemaColumn FindColumn(string columnName)
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
                var column = new SchemaColumn();
                column.Load(row);
                _columns.Add(column);
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
