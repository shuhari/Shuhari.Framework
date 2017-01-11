using System;
using System.Reflection;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Mapping information for single field
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFieldMapper<T>
        where T : class
    {
        /// <summary>
        /// Field name
        /// </summary>
        string FieldName { get; }

        /// <summary>
        /// Property name
        /// </summary>
        string PropertyName { get; }

        /// <summary>
        /// Property
        /// </summary>
        PropertyInfo Property { get; }

        /// <summary>
        /// Property type
        /// </summary>
        Type PropertyType { get; }

        /// <summary>
        /// Include on insert
        /// </summary>
        bool Insert { get; }

        /// <summary>
        /// Include on update
        /// </summary>
        bool Update { get; }

        /// <summary>
        /// Identity
        /// </summary>
        bool Identity { get; }

        /// <summary>
        /// Whether the field is primary key
        /// </summary>
        bool IsPrimaryKey { get; }

        /// <summary>
        /// Check if match column
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        bool Match(SchemaColumn column);

        /// <summary>
        /// Set entity value
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="value"></param>
        void SetValue(T entity, object value);

        /// <summary>
        /// Get value from entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        object GetValue(T entity);
    }
}
