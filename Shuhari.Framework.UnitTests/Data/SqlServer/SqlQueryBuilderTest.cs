using NUnit.Framework;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.UnitTests.Data.SqlServer
{
    [TestFixture]
    public class SqlQueryBuilderTest
    {
        [Test]
        public void CreatePagedQueryTuple()
        {
            var sessionFactory = Fixtures.SqlSessionFactory;
            using (var session = sessionFactory.OpenSession())
            {
                var builder = session.SessionFactory.GetQueryBuilder<NotNullEntity>();
                string baseSql = @"select * from TNotNullEntity
                    join TNullableEntity on TNotNullEntity.FID=TNullableEntity.FID
                    where TNotNullEntity.FID=1";
                var qdata = new QueryDto();
                qdata.SetPagination(0, 20);
                var tuple = builder.CreatePagedQueryTuple(session, baseSql, 
                    builder.OrderBy(x => x.StringProp, "abc"), qdata);
                Assert.IsNotEmpty(tuple.Item1.Sql);
                Assert.IsNotEmpty(tuple.Item2.Sql);
            }
        }
    }
}
