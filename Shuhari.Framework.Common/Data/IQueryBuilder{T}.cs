using System;
using System.Linq.Expressions;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Query builder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueryBuilder<T>
        where T: class, new()
    {
        /// <summary>
        /// Db engine
        /// </summary>
        IDbEngine Engine { get; }

        /// <summary>
        /// Entity mapper
        /// </summary>
        IEntityMapper<T> Mapper { get; }

        /// <summary>
        /// Create order critia
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="value"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        OrderCritia<T> OrderBy(Expression<Func<T, object>> selector, object value, bool ascending = true);

        /// <summary>
        /// Create get by id query
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        IQuery<T> GetById(ISession session, object id);

        /// <summary>
        /// Query all records
        /// </summary>
        /// <param name="session"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        IQuery<T> QueryAll(ISession session, OrderCritia<T> orderBy = null);

        /// <summary>
        /// Count all records
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        IQuery<T> Count(ISession session);

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        IQuery<T> DeleteById(ISession session, object id);

        /// <summary>
        /// Create insert query
        /// </summary>
        /// <param name="session"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        IQuery<T> Insert(ISession session, T entity);

        /// <summary>
        /// Create update query
        /// </summary>
        /// <param name="session"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        IQuery<T> Update(ISession session, T entity);

        /// <summary>
        /// Generate query that update partial fields specified by
        /// <paramref name="propSelectors"/>
        /// </summary>
        /// <param name="session"></param>
        /// <param name="entity"></param>
        /// <param name="propSelectors"></param>
        /// <returns></returns>
        IQuery<T> UpdatePartial(ISession session, T entity, params 
            Expression<Func<T, object>>[] propSelectors);

        /// <summary>
        /// Create paged query
        /// </summary>
        /// <param name="session"></param>
        /// <param name="baseSql"></param>
        /// <param name="orderField"></param>
        /// <param name="qdata"></param>
        /// <returns></returns>
        Tuple<IQuery<T>, IQuery<T>> CreatePagedQueryTuple(ISession session, 
            string baseSql, OrderCritia<T> orderField, QueryDTO qdata);

        /// <summary>
        /// Create list all query
        /// </summary>
        /// <param name="session"></param>
        /// <param name="orderField"></param>
        /// <returns></returns>
        IQuery<T> ListAll(ISession session, OrderCritia<T> orderField);
    }
}
