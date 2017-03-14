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
        protected FieldAttributeBase(string fieldName, FieldFlags flags)
        {
            FieldName = fieldName;
            Flags = flags;
        }

        /// <summary>
        /// Field name
        /// </summary>
        public string FieldName { get; }

        /// <summary>
        /// Flags
        /// </summary>
        public FieldFlags Flags { get; set; }
    }
}
