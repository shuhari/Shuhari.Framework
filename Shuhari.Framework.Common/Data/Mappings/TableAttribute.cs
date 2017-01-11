using System;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Mark entity as mapping to table
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TableAttribute : Attribute
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="tableName">Table name. If not specified, then use entity name as table name</param>
        public TableAttribute(string tableName = null)
        {
            this.TableName = tableName;
        }

        /// <summary>
        /// Table name. If not specified, then use entity name as table name
        /// </summary>
        public string TableName { get; private set; }
    }
}
