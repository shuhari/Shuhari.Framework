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
    }
}
