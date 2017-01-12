using System;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Base class for <see cref="FieldAttribute"/> and <see cref="PrimaryKeyAttribute"/>.
    /// </summary>
    public abstract class FieldAttributeBase : Attribute
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="flags"></param>
        public FieldAttributeBase(string fieldName, FieldFlags flags)
        {
            this.FieldName = fieldName;
            this.Flags = flags;
        }

        /// <summary>
        /// Field name
        /// </summary>
        public string FieldName { get; private set; }

        /// <summary>
        /// Flags
        /// </summary>
        public FieldFlags Flags { get; set; }
    }
}
