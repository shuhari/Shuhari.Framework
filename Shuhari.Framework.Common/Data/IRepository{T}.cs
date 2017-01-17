using System;
using System.Linq.Expressions;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Strongly typed repository interface
    /// </summary>
    /// <typeparam name="TID">Type of primary key. Can be int/long/Guid</typeparam>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    public interface IRepository<TID, TEntity> : IRepository
        where TID : struct
        where TEntity : class, IEntity<TID>, new()
    {
        /// <summary>
        /// Create strongly-typed query
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        IQuery<TEntity> CreateQuery(string sql);

        /// <summary>
        /// Count all records
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(TID id);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        int Update(TEntity entity);

        /// <summary>
        /// Update partial fields
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propSelectors"></param>
        int UpdatePartial(TEntity entity, 
            params Expression<Func<TEntity, object>>[] propSelectors);

        /// <summary>
        /// Delete entity by id
        /// </summary>
        /// <param name="id"></param>
        int DeleteById(TID id);

        /// <summary>
        /// List all entity
        /// </summary>
        /// <param name="orderField"></param>
        /// <returns></returns>
        TEntity[] ListAll(OrderCritia<TEntity> orderField = null);

        /// <summary>
        /// Create order critia
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        OrderCritia<TEntity> OrderBy(Expression<Func<TEntity, object>> selector, bool ascending = true);

        /// <summary>
        /// Query data with current page and total record count
        /// </summary>
        /// <param name="baseSql"></param>
        /// <param name="orderField"></param>
        /// <param name="qdata"></param>
        /// <returns></returns>
        PagedCollection<TEntity> QueryPaged(string baseSql, OrderCritia<TEntity> orderField, QueryDTO qdata);
    }
}
