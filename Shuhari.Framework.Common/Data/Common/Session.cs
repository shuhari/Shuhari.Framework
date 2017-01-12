using System;
using System.Data;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Common
{
    /// <summary>
    /// Implementation of <see cref="ISession"/>
    /// </summary>
    internal class Session : ISession
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public Session(SessionFactory sessionFactory, object parameters)
        {
            Expect.IsNotNull(sessionFactory, nameof(sessionFactory));

            _sessionFactory = sessionFactory;
            _parameters = parameters;
        }

        private readonly SessionFactory _sessionFactory;

        private object _parameters;

        private IDbConnection _connection;

        /// <inheritdoc />
        public ISessionFactory SessionFactory
        {
            get { return _sessionFactory; }
        }

        /// <inheritdoc />
        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = _sessionFactory.OpenConnection(_parameters);
                }
                return _connection;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
