using System;
using System.Data;
using Shuhari.Framework.Data.Mappings;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Factory class to store database information, such as connection string
    /// and entity mappings, and create instance of <see cref="ISession"/>.
    /// </summary>
    public interface ISessionFactory
    {
        /// <summary>
        /// Database engine
        /// </summary>
        IDbEngine Engine { get; }

        /// <summary>
        /// Open session instance
        /// </summary>
        /// <param name="parameters">Additional parametes to create session.
        /// for example, switch connection in read-write-separated scene.
        /// Application with only one database can ignore this parameter.</param>
        /// <returns>Opened session</returns>
        /// <remarks>Caller is responsible for release session after use it.</remarks>
        ISession OpenSession(object parameters = null);

        /// <summary>
        /// Create a data connection, set connection string and open
        /// </summary>
        /// <param name="parameters">Parameters when creating session object</param>
        /// <returns></returns>
        IDbConnection OpenConnection(object parameters = null);

        /// <summary>
        /// Register mapper
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="mapper"></param>
        void RegisterMapper(Type entityType, object mapper);

        /// <summary>
        /// Get mapper, generic version
        /// </summary>
        /// <param name="entityType">Type of entity</param>
        /// <returns></returns>
        object GetMapper(Type entityType);

        /// <summary>
        /// Get registered mapper for entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEntityMapper<T> GetMapper<T>() where T : class;

        /// <summary>
        /// Get query builder
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryBuilder<T> GetQueryBuilder<T>() where T : class, new();
    }
}
