using System.Data;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Common
{
    /// <summary>
    /// Implementation of <see cref="IQueryBase"/> and <see cref="IQuery"/>
    /// </summary>
    internal class Query : BaseQuery, IQuery
    {
        public Query(Session session, string sql)
            : base(session, sql)
        {
        }

        /// <inheritdoc />
        public IQuery Set(string paramName, object value)
        {
            SetCore(paramName, value);
            return this;
        }

        /// <inheritdoc />
        public IQuery Set(string paramName, DbType paramType, object value)
        {
            SetParam(paramName, paramType, value);
            return this;
        }

        /// <inheritdoc />
        public IQuery SetPaginiation(QueryDTO q)
        {
            Expect.IsNotNull(q, nameof(q));
            q.SetQuery(this);
            return this;
        }

        /// <inheritdoc />
        public T[] GetAll<T>(IEntityMapper<T> mapper = null) 
            where T : class, new()
        {
            return GetAllCore<T>(mapper);
        }

        /// <inheritdoc />
        public T GetFirst<T>(IEntityMapper<T> mapper = null) 
            where T : class, new()
        {
            return GetFirstCore<T>(mapper);
        }
    }
}
