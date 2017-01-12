using System;
using System.Data;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Common
{
    /// <summary>
    /// Strongly-typed query
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Query<T> : Query, IQuery<T>
        where T : class, new()
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sql"></param>
        /// <param name="provided"></param>
        public Query(Session session, string sql, IEntityMapper<T> provided) 
            : base(session, sql)
        {
            _mapper = (IEntityMapper<T>)EnsureMapper(provided);
        }

        private readonly IEntityMapper<T> _mapper;

        /// <inheritdoc />
        IQuery<T> IQuery<T>.Set(string paramName, object value)
        {
            Expect.IsNotBlank(paramName, nameof(paramName));
            Expect.IsNotNull(value, nameof(value));

            SetCore(paramName, value);
            return this;
        }

        /// <inheritdoc />
        IQuery<T> IQuery<T>.Set(string paramName, DbType paramType, object value)
        {
            Expect.IsNotBlank(paramName, nameof(paramName));

            SetCore(paramName, paramType, value);
            return this;
        }

        /// <inheritdoc />
        IQuery<T> IQuery<T>.SetPaginiation(QueryDTO q)
        {
            Expect.IsNotNull(q, nameof(q));

            q.SetQuery(this);
            return this;
        }

        /// <inheritdoc />
        T[] IQuery<T>.GetAll()
        {
            return base.GetAll(_mapper);
        }

        /// <inheritdoc />
        T IQuery<T>.GetFirst()
        {
            return base.GetFirst(_mapper);
        }
    }
}
