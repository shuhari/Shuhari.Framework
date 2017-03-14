using Moq;
using NUnit.Framework;
using Shuhari.Framework.Data;

namespace Shuhari.Framework.UnitTests.Data
{
    [TestFixture]
    public class RepositoryTestBaseTest
    {
        class RepositoryTest : RepositoryTestBase<NotNullEntityRepository>
        {
            public RepositoryTest()
                : base(true)
            {
            }

            protected internal override void CreateDatabase()
            {
            }

            protected internal override void DropDatabase()
            {
            }

            protected internal override ISession OpenSession()
            {
                return null;
            }
        }

        [Test]
        public void AfterSetup_ShouldSetRepository()
        {
            var test = new RepositoryTest();
            test.SetUp();

            Assert.IsNotNull(test.Repository);
        }
    }
}