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
            _mapper = EnsureMapper(provided);
        }

        private readonly IEntityMapper<T> _mapper;

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

        /// <inheritdoc />
        public void Set<TProp>(Expression<Func<T, TProp>> selector, TProp value)
        {
            Expect.IsNotNull(selector, nameof(selector));
            var prop = ExpressionBuilder.GetProperty(selector);
            Expect.IsNotNull(prop, nameof(prop));

            var fieldMapper = _mapper.FieldMappers.FirstOrDefault(x => x.PropertyName == prop.Name);
            Expect.IsNotNull(fieldMapper, nameof(fieldMapper));
            var engine = Session.SessionFactory.Engine;
            Set(fieldMapper.FieldName, engine.GetDbType(prop.PropertyType), value);
        }
    }
}
