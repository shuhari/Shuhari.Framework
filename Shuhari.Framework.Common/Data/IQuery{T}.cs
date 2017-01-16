using System;
using System.Linq.Expressions;

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
        /// Set query parameter from linq expression
        /// </summary>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="selector"></param>
        /// <param name="value"></param>
        void Set<TProp>(Expression<Func<T, TProp>> selector, TProp value);

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
