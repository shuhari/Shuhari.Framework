using System;
using System.Collections.Generic;
using System.Data;
using Shuhari.Framework.Data.Mappings;
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

            _mappers = new Dictionary<Type, object>();

            this.Engine = engine;
            this.ConnectionString = connectionString;
        }

        private Dictionary<Type, object> _mappers;

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

        /// <inheritdoc />
        public void RegisterMapper(Type entityType, object mapper)
        {
            Expect.IsNotNull(entityType, nameof(entityType));
            Expect.IsNotNull(mapper, nameof(mapper));

            _mappers[entityType] = mapper;
        }

        /// <inheritdoc />
        public object GetMapper(Type entityType)
        {
            Expect.IsNotNull(entityType, nameof(entityType));

            return _mappers.ContainsKey(entityType) ? _mappers[entityType] : null;
        }

        /// <summary>
        /// Get mapper for entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEntityMapper<T> GetMapper<T>()
            where T : class
        {
            return (IEntityMapper<T>)GetMapper(typeof(T));
        }

        /// <inheritdoc />
        public IQueryBuilder<T> GetQueryBuilder<T>() 
            where T : class, new()
        {
            var mapper = GetMapper<T>();
            Expect.IsNotNull(mapper, nameof(mapper));

            var queryBuilder = Engine.CreateQueryBuilder<T>();
            queryBuilder.Mapper = mapper;
            return queryBuilder;
        }
    }
}
