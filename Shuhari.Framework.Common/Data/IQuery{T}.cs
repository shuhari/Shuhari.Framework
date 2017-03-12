using System;
using System.Data;
using System.Linq.Expressions;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Strongly type query. It's mapper are assigned on initialize rather than on method-level.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQuery<T> : IQueryBase
        where T: class, new()
    {
        /// <summary>
        /// Set query parameter. Parameter type are guessed from value type, 
        /// so <strong>value could not be null</strong>.
        /// </summary>
        /// <param name="paramName">Parameter name</param>
        /// <param name="value"></param>
        /// <returns>This query</returns>
        IQuery<T> Set(string paramName, object value);

        /// <summary>
        /// In case value can be null, it is impossible to guess parameter type
        /// from value type, thus caller should pass <paramref name="paramType"/> manually.
        /// </summary>
        /// <param name="paramName">Parameter name</param>
        /// <param name="paramType">Parameter type</param>
        /// <param name="value">Parameter value</param>
        /// <returns>This query</returns>
        IQuery<T> Set(string paramName, DbType paramType, object value);

        /// <summary>
        /// Set query parameter
        /// </summary>
        /// <param name="q">Query parameter</param>
        /// <returns>This query</returns>
        IQuery<T> SetPaginiation(QueryDto q);

        /// <summary>
        /// Set query parameter from linq expression
        /// </summary>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="selector"></param>
        /// <param name="value"></param>
        IQuery<T> Set<TProp>(Expression<Func<T, TProp>> selector, TProp value);

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
