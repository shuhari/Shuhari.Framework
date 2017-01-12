using System;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Read field from data reader
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFieldReader<T> 
        where T: class
    {
        /// <summary>
        /// Property type
        /// </summary>
        Type PropertyType { get; }

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
    }
}
