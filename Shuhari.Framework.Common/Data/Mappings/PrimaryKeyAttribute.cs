﻿using System;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Describe primary key
    /// </summary>
    /// <remarks>Entity property of primery key is always <strong>Id</strong>, but
    /// it's field name and flags (identity or not) can be customized</remarks>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class PrimaryKeyAttribute : FieldAttributeBase
    {
        /// <summary>
        /// Initialize with default flags
        /// </summary>
        /// <param name="fieldName"></param>
        public PrimaryKeyAttribute(string fieldName)
            : base(fieldName, DEFAULT_FLAGS)
        {
        }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="flags"></param>
        public PrimaryKeyAttribute(string fieldName, FieldFlags flags)
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