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
