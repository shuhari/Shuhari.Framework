using System.Data;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Read entity from <see cref="IDataReader" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityReader<T>
        where T : class
    {
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
    }
}
