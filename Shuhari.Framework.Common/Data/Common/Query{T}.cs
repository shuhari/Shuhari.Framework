using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Linq;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Common
{
    /// <summary>
    /// Strongly-typed query
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Query<T> : BaseQuery, IQuery<T>
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
            _mapper = EnsureMapper(provided);
        }

        private readonly IEntityMapper<T> _mapper;

        /// <inheritdoc />
        public T[] GetAll()
        {
            return GetAllCore(_mapper);
        }

        /// <inheritdoc />
        public T GetFirst()
        {
            return GetFirstCore(_mapper);
        }

        /// <inheritdoc />
        public IQuery<T> Set(string paramName, object value)
        {
            SetCore(paramName, value);
            return this;
        }

        /// <inheritdoc />
        public IQuery<T> Set(string paramName, DbType paramType, object value)
        {
            SetCore(paramName, paramType, value);
            return this;
        }

        /// <inheritdoc />
        public IQuery<T> SetPaginiation(QueryDTO q)
        {
            Expect.IsNotNull(q, nameof(q));
            q.SetQuery(this);
            return this;
        }

        /// <inheritdoc />
        public IQuery<T> Set<TProp>(Expression<Func<T, TProp>> selector, TProp value)
        {
            Expect.IsNotNull(selector, nameof(selector));
            var prop = ExpressionBuilder.GetProperty(selector);
            Expect.IsNotNull(prop, nameof(prop));

            var fieldMapper = _mapper.FieldMappers.FirstOrDefault(x => x.PropertyName == prop.Name);
            Expect.IsNotNull(fieldMapper, nameof(fieldMapper));
            var engine = Session.SessionFactory.Engine;
            SetCore(fieldMapper.FieldName, engine.GetDbType(prop.PropertyType), value);
            return this;
        }
    }
}
