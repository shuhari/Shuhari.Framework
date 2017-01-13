using System.Data;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Strongly type query. It's mapper are assigned on initialize rather than on method-level.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQuery<T> : IQuery
        where T: class, new()
    {
        /// <summary>
        /// Get all returned records from query
        /// </summary>
        /// <returns>All matched records</returns>
        T[] GetAll();

        /// <summary>
        /// Get just one record from query, such as GetById()
        /// </summary>
        /// <returns>First matched record, or null if no records to return</returns>
        T GetFirst();
    }
}
