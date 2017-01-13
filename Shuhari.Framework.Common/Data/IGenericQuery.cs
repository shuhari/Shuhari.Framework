using System.Data;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Query with no strongly-type support
    /// </summary>
    public interface IGenericQuery : IQuery
    {
        /// <summary>
        /// Set query parameter. Parameter type are guessed from value type, 
        /// so <strong>value could not be null</strong>.
        /// </summary>
        /// <param name="paramName">Parameter name</param>
        /// <param name="value"></param>
        /// <returns>This query</returns>
        IGenericQuery Set(string paramName, object value);

        /// <summary>
        /// In case value can be null, it is impossible to guess parameter type
        /// from value type, thus caller should pass <paramref name="paramType"/> manually.
        /// </summary>
        /// <param name="paramName">Parameter name</param>
        /// <param name="paramType">Parameter type</param>
        /// <param name="value">Parameter value</param>
        /// <returns>This query</returns>
        IGenericQuery Set(string paramName, DbType paramType, object value);

        /// <summary>
        /// Set query parameter
        /// </summary>
        /// <param name="q">Query parameter</param>
        /// <returns>This query</returns>
        IGenericQuery SetPaginiation(QueryDTO q);

        /// <summary>
        /// Get all returned records from query
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="mapper">Custom entity mapper, or null to use <see cref="ISessionFactory"/> defined mapper</param>
        /// <returns>All matched records</returns>
        T[] GetAll<T>(IEntityMapper<T> mapper = null) where T : class, new();

        /// <summary>
        /// Get just one record from query, such as GetById()
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="mapper">Custom entity mapper, or null to use <see cref="ISessionFactory"/> defined mapper</param>
        /// <returns>First matched record, or null if no records to return</returns>
        T GetFirst<T>(IEntityMapper<T> mapper = null) where T : class, new();
    }
}
