using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Common
{
    /// <summary>
    /// Implementation of <see cref="IQuery"/> and <see cref="IGenericQuery"/>
    /// </summary>
    internal class Query : IGenericQuery
    {
        public Query(Session session, string sql)
        {
            Expect.IsNotNull(session, nameof(session));
            Expect.IsNotBlank(sql, nameof(sql));

            this.Sql = sql;
            _session = session;
            _parameters = new Dictionary<string, DbParameter>();
        }

        protected readonly Session _session;

        protected Dictionary<string, DbParameter> _parameters;

        /// <inheritdoc />
        public ISession Session
        {
            get { return _session; }
        }

        /// <inheritdoc />
        public string Sql { get; private set; }

        /// <inheritdoc />
        public void Set(string paramName, object value)
        {
            Expect.IsNotBlank(paramName, nameof(paramName));
            Expect.IsNotNull(value, nameof(value));

            var dbType = _session.SessionFactory.Engine.GetDbType(value.GetType());
            Set(paramName, dbType, value);
        }

        /// <inheritdoc />
        public void Set(string paramName, DbType paramType, object value)
        {
            Expect.IsNotBlank(paramName, nameof(paramName));

            var param = _session.SessionFactory.Engine.CreateParameter(paramName, paramType, value);
            _parameters[param.ParameterName] = param;
        }

        /// <inheritdoc />
        public void SetPaginiation(QueryDTO q)
        {
            Expect.IsNotNull(q, nameof(q));

            q.SetQuery(this);
        }

        private IDbCommand CreateCommand()
        {
            var cmd = _session.CreateCommand();
            cmd.CommandText = Sql;
            cmd.CommandType = CommandType.Text;

            foreach (var param in _parameters.Values)
            {
                cmd.Parameters.Add(param);
            }

            return cmd;
        }

        /// <inheritdoc />
        public object ExecScalar()
        {
            using (var cmd = CreateCommand())
            {
                return cmd.ExecuteScalar();
            }
        }

        /// <inheritdoc />
        public int ExecInt()
        {
            return Convert.ToInt32(ExecScalar());
        }

        /// <inheritdoc />
        public int ExecNonQuery()
        {
            using (var cmd = CreateCommand())
            {
                return cmd.ExecuteNonQuery();
            }
        }

        /// <inheritdoc />
        public T[] GetAll<T>(IEntityMapper<T> mapper = null) 
            where T : class, new()
        {
            mapper = EnsureMapper(mapper);

            var result = new List<T>();
            using (var cmd = CreateCommand())
            using (var reader = cmd.ExecuteReader())
            {
                var schema = mapper.GetSchema(reader);
                while (reader.Read())
                {
                    var entity = new T();
                    mapper.Map(reader, entity, schema);
                    result.Add(entity);
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// If mapper is provied, then return the provied value.
        /// Else, get <see cref="ISessionFactory"/>'s registered mapper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="provided"></param>
        /// <returns></returns>
        protected IEntityMapper<T> EnsureMapper<T>(IEntityMapper<T> provided)
            where T : class
        {
            var mapper = provided;
            if (mapper == null)
            {
                mapper = _session.SessionFactory.GetMapper<T>();
                if (mapper == null)
                    throw ExceptionBuilder.Mapping(FrameworkStrings.ErrorMappingNotFound, typeof(T).Name);
            }
            return mapper;
        }

        /// <inheritdoc />
        public T GetFirst<T>(IEntityMapper<T> mapper = null)
            where T : class, new()
        {
            mapper = EnsureMapper(mapper);

            var result = new List<T>();
            using (var cmd = CreateCommand())
            using (var reader = cmd.ExecuteReader())
            {
                var schema = mapper.GetSchema(reader);
                if (reader.Read())
                {
                    var entity = new T();
                    mapper.Map(reader, entity, schema);
                    return entity;
                }
            }

            return null;
        }
    }
}
