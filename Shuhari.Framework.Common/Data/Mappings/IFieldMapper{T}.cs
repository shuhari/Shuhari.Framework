using System.Reflection;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Mapping information for single field
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFieldMapper<T> : IFieldReader<T>
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
        /// Get value from entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        object GetValue(T entity);
    }
}
