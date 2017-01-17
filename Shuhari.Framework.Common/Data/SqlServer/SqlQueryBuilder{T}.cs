using System;
using System.Linq;
using System.Text.RegularExpressions;
using Shuhari.Framework.Data.Common;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Linq;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.SqlServer
{
    /// <summary>
    /// Sql server specified query builder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class SqlQueryBuilder<T> : QueryBuilder<T>
        where T : class, new()
    {
        public SqlQueryBuilder(IDbEngine engine) 
            : base(engine)
        {
        }

        /// <inheritdoc />
        public override Tuple<IQuery<T>, IQuery<T>> CreatePagedQueryTuple(ISession session, 
            string baseSql, OrderCritia<T> orderField, QueryDTO qdata)
        {
            var re = new Regex(@"(select\s+)(.+)(\s+from[\s\S]+)", RegexOptions.IgnoreCase);
            var match = re.Match(baseSql);
            if (!match.Success)
                throw new ArgumentException("Unsupport sql: " + baseSql);
            var orderProp = ExpressionBuilder.GetProperty(orderField.Selector);
            var field = Mapper.FieldMappers.FirstOrDefault(x => x.PropertyName == orderProp.Name);
            Expect.IsNotNull(field, nameof(field));

            string countSql = string.Format("{0}count(*){1}",
                match.Groups[1].Value, match.Groups[3].Value);
            var countQuery = session.CreateQuery<T>(countSql);
            qdata.SetQuery(countQuery);

            string dataSql = string.Format("{0} order by {1} {2} offset @offset row fetch next @limit rows only",
                baseSql,
                field.FieldName,
                orderField.Ascending ? "asc" : "desc");
            var dataQuery = session.CreateQuery<T>(dataSql);
            qdata.SetQuery(dataQuery);

            return new Tuple<IQuery<T>, IQuery<T>>(countQuery, dataQuery);
        }
    }
}
