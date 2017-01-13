using System;
using System.Linq.Expressions;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Common
{
    /// <summary>
    /// Strongly typed Base repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TID">Id type</typeparam>
    public abstract class BaseRepository<TID, TEntity> : BaseRepository, IRepository<TID, TEntity>
        where TID : struct
        where TEntity : class, IEntity<TID>, new()
    {
        /// <inheritdoc />
        public IQuery<TEntity> CreateQuery(string sql)
        {
            Expect.IsNotBlank(sql, nameof(sql));

            return Session.CreateQuery<TEntity>(sql);
        }

        private IQueryBuilder<TEntity> GetQueryBuilder()
        {
            return Session.SessionFactory.GetQueryBuilder<TEntity>();
        }

        private IEntityMapper<TEntity> GetMapper()
        {
            var mapper = Session.SessionFactory.GetMapper<TEntity>();
            Expect.IsNotNull(mapper, nameof(mapper));
            return mapper;
        }

        /// <inheritdoc />
        public int Count()
        {
            return GetQueryBuilder().Count(Session).ExecInt();
        }

        /// <inheritdoc />
        public TEntity GetById(TID id)
        {
            return GetQueryBuilder().GetById(Session, id).GetFirst();
        }

        /// <inheritdoc />
        public void Insert(TEntity entity)
        {
            var pk = GetMapper().GetPrimaryKey();

            var query = GetQueryBuilder().Insert(Session, entity);
            if (pk.Identity)
            {
                var id = query.ExecScalar();
                pk.SetValue(entity, id);
            }
            else
            {
                query.ExecNonQuery();
            }
        }

        /// <inheritdoc />
        public int Update(TEntity entity)
        {
            Expect.IsNotNull(entity, nameof(entity));

            return GetQueryBuilder().Update(Session, entity)
                .ExecNonQuery();
        }

        /// <inheritdoc />
        public int UpdatePartial(TEntity entity, 
            params Expression<Func<TEntity, object>>[] propSelectors)
        {
            Expect.IsNotNull(entity, nameof(entity));

            return GetQueryBuilder().UpdatePartial(Session, entity, propSelectors)
                .ExecNonQuery();
        }

        /// <inheritdoc />
        public int DeleteById(TID id)
        {
            return GetQueryBuilder().DeleteById(Session, id)
                .ExecNonQuery();
        }

        /// <inheritdoc />
        public OrderCritia<TEntity> OrderBy(Expression<Func<TEntity, object>> selector, bool ascending = true)
        {
            Expect.IsNotNull(selector, nameof(selector));

            return new OrderCritia<TEntity>(selector, null, ascending);
        }

        /// <inheritdoc />
        public PagedCollection<TEntity> QueryPaged(string baseSql, OrderCritia<TEntity> orderField, QueryDTO qdata)
        {
            Expect.IsNotBlank(baseSql, nameof(baseSql));
            Expect.IsNotNull(orderField, nameof(orderField));
            Expect.IsNotNull(qdata, nameof(qdata));

            var result = new PagedCollection<TEntity>();
            result.SetPagination(qdata);
            var queryPair = GetQueryBuilder().CreatePagedQueryTuple(Session, baseSql, orderField, qdata);
            result.Total = queryPair.Item1.ExecInt();
            result.Data = queryPair.Item2.GetAll();
            return result;
        }
    }
}
