using System;
using System.Data;
using Shuhari.Framework.Data.Mappings;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Represent a connection to database.
    /// Caller responsible to release after use.
    /// </summary>
    public interface ISession : IDisposable
    {
        /// <summary>
        /// Owned session factory
        /// </summary>
        ISessionFactory SessionFactory { get; }

        /// <summary>
        /// Connection. It should be initialized the first time this property is visited
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// Create transaction
        /// </summary>
        /// <returns></returns>
        IDbTransaction BeginTransaction();

        /// <summary>
        /// Create generic query
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        IGenericQuery CreateQuery(string sql);

        /// <summary>
        /// Create strongly-typed query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        IQuery<T> CreateQuery<T>(string sql, IEntityMapper<T> mapper = null) where T : class, new();
    }
}
