using System.Collections.Generic;
using System.Data;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Normally reader and writer are implemented in same class
    /// as there are common infrastructures.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityMapper<T>
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
        /// Fetch field information from <see cref="IDataReader.GetSchemaTable"/>.
        /// The fetched result could be used in <see cref="Map(IDataReader, T, object)"/> method
        /// to speed up mapping. Can simply return null If speed is not concerned.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        object GetSchema(IDataReader reader);

        /// <summary>
        /// Map fields from reader to entity
        /// </summary>
        /// <param name="reader">Reader to fetch record</param>
        /// <param name="entity">Target entity</param>
        /// <param name="schema">Readed schema for mapping</param>
        void Map(IDataReader reader, T entity, object schema);

        /// <summary>
        /// Expect the entity should have one and only primar key.
        /// </summary>
        /// <returns>Primary key</returns>
        /// <exception cref="MappingException">Throw if entity has no or more-then-2 primary keys</exception>
        IFieldMapper<T> GetPrimaryKey();
    }
}
