using System;
using System.Text.RegularExpressions;
using Shuhari.Framework.Data.Common;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.Data.SqlServer
{
    /// <summary>
    /// Sql server specified query builder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class SqlQueryBuilder<T> : QueryBuilder<T>
        where T : class, new()
    {
        public SqlQueryBuilder(IDbEngine engine, IEntityMapper<T> mapper) 
            : base(engine, mapper)
        {
        }

        /// <inheritdoc />
        public override Tuple<IQuery<T>, IQuery<T>> CreatePagedQueryTuple(ISession session, 
            string baseSql, OrderCritia<T> orderCritia, QueryDto qdata)
        {
            var re = new Regex(@"(select\s+)([\s\S]+)(\s+from[\s\S]+)", RegexOptions.IgnoreCase);
            var match = re.Match(baseSql);
            if (!match.Success)
                throw new ArgumentException("Unsupport sql: " + baseSql);
            var field = GetOrderField(orderCritia);

            string countSql = string.Format("{0}count(*){1}",
                match.Groups[1].Value, match.Groups[3].Value);
            var countQuery = session.CreateQuery<T>(countSql);
            qdata.SetQuery(countQuery);

            string dataSql = string.Format("{0} order by {1} {2} offset @offset row fetch next @limit rows only",
                baseSql,
                field.FieldName,
                orderCritia.Ascending ? "asc" : "desc");
            var dataQuery = session.CreateQuery<T>(dataSql);
            qdata.SetQuery(dataQuery);

            return new Tuple<IQuery<T>, IQuery<T>>(countQuery, dataQuery);
        }
    }
}
