using System.Collections.Generic;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Normally reader and writer are implemented in same class
    /// as there are common infrastructures.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityMapper<T> : IEntityReader<T>
        where T : class
    {
        /// <summary>
        /// Table name
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// Field mappings
        /// </summary>
        IEnumerable<IFieldMapper<T>> FieldMappers { get; }

        /// <summary>
        /// Expect the entity should have one and only primar key.
        /// </summary>
        /// <returns>Primary key</returns>
        /// <exception cref="MappingException">Throw if entity has no or more-then-2 primary keys</exception>
        IFieldMapper<T> GetPrimaryKey();
    }
}
