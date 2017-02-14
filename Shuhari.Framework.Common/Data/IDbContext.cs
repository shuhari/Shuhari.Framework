using System;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Interface for db context
    /// </summary>
    public interface IDbContext : IRepositoryContainer
    {
        /// <summary>
        /// Session factory
        /// </summary>
        ISessionFactory SessionFactory { get; }

        /// <summary>
        /// Open session and assign to all repositories
        /// </summary>
        void OpenSession();

        /// <summary>
        /// Close all sessions and set repository session to null
        /// </summary>
        void CloseSession();

        /// <summary>
        /// Execute query in transaction.
        /// If action success (no exception throw), then the transaction will be commit.
        /// If action failed with exception, then the transaction should be rollbacked, and exception throw again.
        /// </summary>
        /// <param name="action"></param>
        void ExecuteTransaction(Action action);

        /// <summary>
        /// Create session scope
        /// </summary>
        /// <returns></returns>
        IDisposable CreateSessionScope();
    }
}
