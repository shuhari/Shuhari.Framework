using NUnit.Framework;
using Shuhari.Framework.UnitTests.Data;

namespace Shuhari.Framework.IntegrationTests.Data
{
    [TestFixture]
    public class QueryOfTReadTest : DbTestBase
    {
        public QueryOfTReadTest() 
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
            var result = Session.CreateQuery<NotNullEntity>("select * from TNotNullEntity where FID=@FID")
                .Set(x => x.Id, 1)
                .GetAll();
            CollectionAssert.IsNotEmpty(result);
        }
    }
}
