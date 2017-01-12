using System;
using System.Data;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Common
{
    /// <summary>
    /// Implementation of <see cref="ISessionFactory"/>
    /// </summary>
    public class SessionFactory : ISessionFactory
    {
        /// <summary>
        /// Create instance
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="connectionString"></param>
        public SessionFactory(IDbEngine engine, string connectionString)
        {
            Expect.IsNotNull(engine, nameof(engine));
            Expect.IsNotBlank(connectionString, nameof(connectionString));

            this.Engine = engine;
            this.ConnectionString = connectionString;
        }

        /// <inheritdoc />
        public IDbEngine Engine { get; private set; }

        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <inheritdoc />
        public ISession OpenSession(object parameters = null)
        {
            return new Session(this, parameters);
        }

        /// <inheritdoc />
        public IDbConnection OpenConnection(object parameters = null)
        {
            var connection = Engine.CreateConnection();
            connection.ConnectionString = this.ConnectionString;
            connection.Open();
            return connection;
        }
    }
}
