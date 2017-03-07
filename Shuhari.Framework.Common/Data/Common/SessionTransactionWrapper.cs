using System.Data;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Common
{
    /// <summary>
    /// Wrap of db transaction. will notify session when inner transaction disposed
    /// </summary>
    internal class SessionTransactionWrapper : IDbTransaction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="inner"></param>
        public SessionTransactionWrapper(Session session, IDbTransaction inner)
        {
            Expect.IsNotNull(session, nameof(session));
            Expect.IsNotNull(inner, nameof(inner));

            _session = session;
            InnerTransaction = inner;
        }

        private readonly Session _session;

        public IDbTransaction InnerTransaction { get; private set; }

        /// <inheritdoc />
        public IDbConnection Connection
        {
            get { return InnerTransaction.Connection; }
        }

        /// <inheritdoc />
        public IsolationLevel IsolationLevel
        {
            get { return InnerTransaction.IsolationLevel; }
        }

        /// <inheritdoc />
        public void Commit()
        {
            InnerTransaction.Commit();
        }

        /// <inheritdoc />
        public void Rollback()
        {
            InnerTransaction.Rollback();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (InnerTransaction != null)
            {
                InnerTransaction.Dispose();
                InnerTransaction = null;
                _session.OnTransactionDisposed(this);
            }
        }
    }
}
