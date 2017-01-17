using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Linq;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Common
{
    /// <summary>
    /// Query builder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class QueryBuilder<T> : IQueryBuilder<T>
        where T : class, new()
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="engine"></param>
        public QueryBuilder(IDbEngine engine)
        {
            Expect.IsNotNull(engine, nameof(engine));

            this.Engine = engine;
        }

        /// <inheritdoc />
        public IDbEngine Engine { get; private set; }

        /// <inheritdoc />
        public IEntityMapper<T> Mapper { get; set; }

        /// <inheritdoc />
        public OrderCritia<T> OrderBy(Expression<Func<T, object>> selector,
            object value, bool ascending = true)
        {
            Expect.IsNotNull(selector, nameof(selector));

            return new OrderCritia<T>(selector, value, ascending);
        }

        /// <inheritdoc />
        public IQuery<T> GetById(ISession session, object id)
        {
            Expect.IsNotNull(session, nameof(session));
            Expect.IsNotNull(Mapper, nameof(Mapper));
            Expect.IsNotNull(id, nameof(id));

            var pk = Mapper.GetPrimaryKey();
            if (pk.PropertyType != id.GetType())
                throw ExceptionBuilder.NotSupported(FrameworkStrings.ErrorPrimaryKeyTypeUnmatch,
                    typeof(T).Name, pk.PropertyType, id.GetType());

            var fieldNames = string.Join(", ", Mapper.FieldMappers.Select(x => x.FieldName));
            string sql = string.Format("select top 1 {0} from {1} where {2}=@{2}",
                fieldNames, Mapper.TableName, pk.FieldName);
            var query = session.CreateQuery<T>(sql, Mapper);
            query.Set(pk.FieldName, id);
            return query;
        }

        /// <inheritdoc />
        public IQuery<T> QueryAll(ISession session, OrderCritia<T> orderBy)
        {
            Expect.IsNotNull(session, nameof(session));
            Expect.IsNotNull(Mapper, nameof(Mapper));

            var fieldNames = string.Join(", ", Mapper.FieldMappers.Select(x => x.FieldName));
            var sql = string.Format("select {0} from {1}", fieldNames, Mapper.TableName);
            sql = AppendOrder(sql, orderBy);
            return session.CreateQuery(sql, Mapper);
        }

        /// <summary>
        /// Append order by clause if exist
        /// </summary>
        /// <param name="baseSql"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        private string AppendOrder(string baseSql, OrderCritia<T> orderBy)
        {
            Expect.IsNotBlank(baseSql, nameof(baseSql));
            Expect.IsNotNull(Mapper, nameof(Mapper));

            string result = baseSql;
            if (orderBy != null)
            {
                var prop = ExpressionBuilder.GetProperty(orderBy.Selector);
                Expect.IsNotNull(prop, nameof(prop));
                var fieldMapper = Mapper.FieldMappers.FirstOrDefault(x => x.PropertyName == prop.Name);
                Expect.IsNotNull(fieldMapper, nameof(fieldMapper));
                result += string.Format(" order by {0} {1}",
                    fieldMapper.FieldName,
                    orderBy.Ascending ? "asc" : "desc");
            };
            return result;
        }

        /// <inheritdoc />
        public IQuery<T> Count(ISession session)
        {
            Expect.IsNotNull(session, nameof(session));
            Expect.IsNotNull(Mapper, nameof(Mapper));

            string sql = string.Format("select count(*) from {0}", Mapper.TableName);
            return session.CreateQuery<T>(sql, Mapper);
        }

        /// <inheritdoc />
        public IQuery<T> DeleteById(ISession session, object id)
        {
            Expect.IsNotNull(session, nameof(session));
            Expect.IsNotNull(Mapper, nameof(Mapper));
            Expect.IsNotNull(id, nameof(id));

            var pk = Mapper.GetPrimaryKey();
            string sql = string.Format("delete from {0} where {1}=@{1}",
                Mapper.TableName, pk.FieldName);
            var query = session.CreateQuery(sql, Mapper);
            query.Set(pk.FieldName, id);
            return query;
        }

        /// <inheritdoc />
        public IQuery<T> Insert(ISession session, T entity)
        {
            Expect.IsNotNull(session, nameof(session));
            Expect.IsNotNull(Mapper, nameof(Mapper));
            Expect.IsNotNull(entity, nameof(entity));

            var fields = Mapper.FieldMappers.Where(x => x.Insert).ToArray();
            var pk = Mapper.GetPrimaryKey();
            var fieldNames = string.Join(", ", fields.Select(x => x.FieldName));
            var paramNames = string.Join(", ", fields.Select(x => "@" + x.FieldName));
            string sql = string.Format("insert into {0} ({1}) values ({2})",
                Mapper.TableName, fieldNames, paramNames);
            if (pk.Identity)
            {
                sql += string.Format("; select cast(scope_identity() as {0});", Engine.GetDbTypeName(pk.PropertyType));
            }
            var query = session.CreateQuery<T>(sql, Mapper);
            foreach (var field in fields)
                query.Set(field.FieldName, Engine.GetDbType(field.PropertyType), field.GetValue(entity));
            return query;
        }

        /// <inheritdoc />
        public IQuery<T> Update(ISession session, T entity)
        {
            Expect.IsNotNull(session, nameof(session));
            Expect.IsNotNull(Mapper, nameof(Mapper));
            Expect.IsNotNull(entity, nameof(entity));

            var fields = Mapper.FieldMappers.Where(x => x.Update).ToArray();
            return CreateUpdateQuery(session, entity, fields);
        }

        private IQuery<T> CreateUpdateQuery(ISession session, T entity, 
            IEnumerable<IFieldMapper<T>> updateFields)
        {
            Expect.IsNotNull(session, nameof(session));
            Expect.IsNotNull(Mapper, nameof(Mapper));
            Expect.IsNotNull(entity, nameof(entity));
            Expect.IsNotNull(updateFields, nameof(updateFields));

            var pk = Mapper.GetPrimaryKey();
            string sql = string.Format("update {0} set {1} where {2}",
                Mapper.TableName,
                string.Join(", ", updateFields.Select(_ => string.Format("{0}=@{0}", _.FieldName))),
                string.Format("{0}=@{0}", pk.FieldName));
            var query = session.CreateQuery<T>(sql, Mapper);
            foreach (var field in updateFields)
            {
                query.Set(field.FieldName, Engine.GetDbType(field.PropertyType), field.GetValue(entity));
            }
            query.Set(pk.FieldName, pk.GetValue(entity));
            return query;
        }

        /// <inheritdoc />
        public IQuery<T> UpdatePartial(ISession session, T entity, 
            params Expression<Func<T, object>>[] propSelectors)
        {
            Expect.IsNotNull(session, nameof(session));
            Expect.IsNotNull(Mapper, nameof(Mapper));
            Expect.IsNotNull(entity, nameof(entity));
            Expect.IsNotNull(propSelectors, nameof(propSelectors));

            if (propSelectors.Length == 0)
                throw ExceptionBuilder.InvalidOperation(FrameworkStrings.ErrorNoFieldsSepecified);

            var fieldMappers = propSelectors.Select(GetFieldMapper).ToArray();
            return CreateUpdateQuery(session, entity, fieldMappers);
        }

        private IFieldMapper<T> GetFieldMapper(Expression<Func<T, object>> propSelector)
        {
            Expect.IsNotNull(propSelector, nameof(propSelector));

            var prop = ExpressionBuilder.GetProperty(propSelector);
            Expect.IsNotNull(prop, nameof(prop));
            var fieldMapper = Mapper.FieldMappers.FirstOrDefault(x => x.Property == prop);
            Expect.IsNotNull(fieldMapper, nameof(fieldMapper));
            return fieldMapper;
        }

        /// <inheritdoc />
        public abstract Tuple<IQuery<T>, IQuery<T>> CreatePagedQueryTuple(ISession session,
            string baseSql, OrderCritia<T> orderField, QueryDTO qdata);

        /// <inheritdoc />
        public IQuery<T> ListAll(ISession session, OrderCritia<T> orderField)
        {
            var fieldNames = string.Join(", ", Mapper.FieldMappers.Select(x => x.FieldName));
            string sql = string.Format("select {0} from {1}",
                string.Join(", ", fieldNames),
                Mapper.TableName);
            if (orderField != null)
            {
                var orderProp = ExpressionBuilder.GetProperty(orderField.Selector);
                var field = Mapper.FieldMappers.FirstOrDefault(x => x.PropertyName == orderProp.Name);
                Expect.IsNotNull(field, nameof(field));
                sql += string.Format(" order by {0} {1}",
                    field.FieldName,
                    orderField.Ascending ? "asc" : "desc");
            }
            var query = session.CreateQuery<T>(sql);
            return query;
        }
    }
}
