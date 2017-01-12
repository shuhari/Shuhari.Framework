using System;
using System.Data;
using Shuhari.Framework.Data.Mappings;
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

        /// <inheritdoc />
        public IGenericQuery CreateQuery(string sql)
        {
            Expect.IsNotBlank(sql, nameof(sql));

            return new Query(this, sql);
        }

        /// <inheritdoc />
        public IQuery<T> CreateQuery<T>(string sql, IEntityMapper<T> mapper) 
            where T : class, new()
        {
            return new Query<T>(this, sql, mapper);
        }

        internal IDbCommand CreateCommand()
        {
            var command = Connection.CreateCommand();
            // if (_transaction != null)
            //     command.Transaction = _transaction.InnerTransaction;
            return command;
        }
    }
}
