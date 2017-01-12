using System;
using System.Data;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// schema column
    /// </summary>
    public class SchemaColumn
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public SchemaColumn()
        {
        }

        /*/// <summary>
        /// Helper ctor to build a valid column
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="dataType"></param>
        /// <param name="allowDbNull"></param>
        public SchemaColumn(string columnName, Type dataType, bool allowDbNull)
        {
            Expect.IsNotBlank(columnName, nameof(columnName));
            Expect.IsNotNull(dataType, nameof(dataType));

            this.ColumnName = columnName;
            this.DataType = dataType;
            this.AllowDBNull = allowDbNull;
        }*/

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

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format("SchemaColumn(ColumnName={0}, DataType={1}, AllowDBNull={2})", ColumnName, DataType, AllowDBNull);
        }
    }
}
