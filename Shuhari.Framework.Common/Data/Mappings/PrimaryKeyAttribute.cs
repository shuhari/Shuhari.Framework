using System;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Describe primary key
    /// </summary>
    /// <remarks>Entity property of primery key is always <strong>Id</strong>, but
    /// it's field name and flags (identity or not) can be customized</remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PrimaryKeyAttribute : FieldAttributeBase
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="flags"></param>
        public PrimaryKeyAttribute(string fieldName, FieldFlags flags = DEFAULT_FLAGS)
            : base(fieldName, flags)
        {
        }

        /// <summary>
        /// Default Flags
        /// </summary>
        public const FieldFlags DEFAULT_FLAGS = FieldFlags.None;

        /// <summary>
        /// Primary key always mapped to Id
        /// </summary>
        public const string PROPERTY_NAME = "Id";
    }
}
