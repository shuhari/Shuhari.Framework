using System;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Field mapping
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public sealed class FieldAttribute : FieldAttributeBase
    {
        /// <summary>
        /// Initialize with default flags
        /// </summary>
        /// <param name="fieldName"></param>
        public FieldAttribute(string fieldName)
            : base(fieldName, DEFAULT_FLAGS)
        {
        }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="flags"></param>
        public FieldAttribute(string fieldName, FieldFlags flags)
            : base(fieldName, flags)
        {
        }

        /// <summary>
        /// Default Flags
        /// </summary>
        public const FieldFlags DEFAULT_FLAGS = FieldFlags.Insert | FieldFlags.Update;
    }
}
