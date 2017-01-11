using System;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Field flags
    /// </summary>
    [Flags]
    public enum FieldFlags
    {
        /// <summary>
        /// No flags
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Insert
        /// </summary>
        Insert = 0x1,

        /// <summary>
        /// Update
        /// </summary>
        Update = 0x2,

        /// <summary>
        /// Primary key
        /// </summary>
        PrimaryKey = 0x4,

        /// <summary>
        /// Identify
        /// </summary>
        Identity = 0x8,
    }
}
