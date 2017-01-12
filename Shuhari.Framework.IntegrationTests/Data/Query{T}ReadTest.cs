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
            var result = Session.CreateQuery<NotNullEntity>("select * from TNotNullEntity where FID=@ID")
                .Set("ID", 1)
                .GetAll();

            CollectionAssert.IsNotEmpty(result);
        }
    }
}
