using NUnit.Framework;
using Shuhari.Framework.Data.Common;

namespace Shuhari.Framework.UnitTests.Data.Common
{
    [TestFixture]
    public class DbContextFactoryTest
    {
        [Test]
        public void CreateContext()
        {
            var factory = new DbContextFactory<TestDbContext>(Fixtures.SqlSessionFactory,
                typeof(NotNullEntityRepository).Assembly);
            var dbCtx = factory.CreateContext();

            Assert.IsNotNull(dbCtx);
            Assert.IsInstanceOf<NotNullEntityRepository>(dbCtx.NotNullEntityRepository);
            Assert.IsInstanceOf<NullableEntityRepository>(dbCtx.NullableEntityRepository);
        }
    }
}
