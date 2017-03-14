using System.Runtime;
using Moq;
using NUnit.Framework;
using Shuhari.Framework.Data;

namespace Shuhari.Framework.UnitTests.Data
{
    [TestFixture]
    public class DbTestBaseTest
    {
        [Test]
        public void ReadOnly_ShouldCreateOnOneTimeSetUp_AndDropOnOneTimeTearDown()
        {
            var mock = new Mock<DbTestBase>(true);
            Assert.IsTrue(mock.Object.ReadOnly);
            mock.Object.OneTimeSetUp();
            mock.Verify(m => m.CreateDatabase());

            mock.Object.SetUp();
            mock.Verify(m => m.OpenSession());
            mock.Verify(m => m.AfterSetUp());

            mock.Object.TearDown();
            Assert.IsNull(mock.Object.Session);

            mock.Object.OneTimeTearDown();
            mock.Verify(m => m.DropDatabase());
        }

        [Test]
        public void ReadWrite_ShouldCreateOnSetUp_AndDropOnTearDown()
        {
            var mock = new Mock<DbTestBase>(false);
            Assert.IsFalse(mock.Object.ReadOnly);
            mock.Object.OneTimeSetUp();
            mock.Verify(m => m.CreateDatabase(), Times.Never());

            mock.Object.SetUp();
            mock.Verify(m => m.CreateDatabase());
            mock.Verify(m => m.OpenSession());
            mock.Verify(m => m.AfterSetUp());

            mock.Object.TearDown();
            Assert.IsNull(mock.Object.Session);
            mock.Verify(m => m.DropDatabase());

            mock.Object.OneTimeTearDown();
        }
    }
}
