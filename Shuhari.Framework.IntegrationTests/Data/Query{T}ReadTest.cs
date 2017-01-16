using NUnit.Framework;
using Shuhari.Framework.UnitTests.Data;

namespace Shuhari.Framework.IntegrationTests.Data
{
    [TestFixture]
    public class QueryTReadTest : DbTestBase
    {
        public QueryTReadTest() 
            : base(true)
        {
        }

        [Test]
        public void GetAll()
        {
            var query = Session.CreateQuery<NotNullEntity>("select * from TNotNullEntity where FID=@ID");
            query.Set("ID", 1);
            var result = query.GetAll();

            CollectionAssert.IsNotEmpty(result);
        }

        [Test]
        public void SetByExpression()
        {
            var query = Session.CreateQuery<NotNullEntity>("select * from TNotNullEntity where FID=@FID");
            query.Set(x => x.Id, 1);
            var result = query.GetAll();

            CollectionAssert.IsNotEmpty(result);
        }
    }
}
