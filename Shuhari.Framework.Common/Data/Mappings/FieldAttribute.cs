using System;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Field mapping
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class FieldAttribute : Attribute
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="flags"></param>
        public FieldAttribute(string fieldName, FieldFlags flags)
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

        /// <summary>
        /// Default Flags
        /// </summary>
        public const FieldFlags DEFAULT_FLAGS = FieldFlags.Insert | FieldFlags.Update;
    }
}
