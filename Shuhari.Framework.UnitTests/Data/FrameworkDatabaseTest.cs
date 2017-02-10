using System.Configuration;
using NUnit.Framework;
using Shuhari.Framework.Data;
using Shuhari.Framework.Data.Common;
using Shuhari.Framework.Data.SqlServer;

namespace Shuhari.Framework.UnitTests.Data
{
    [TestFixture]
    public class FrameworkDatabaseTest
    {
        class TestDatabase : FrameworkDatabase<TestDbContext>
        {
            public TestDatabase(string connName)
                : base(DatabaseType.SqlServer, connName, typeof(NotNullEntity).Assembly, typeof(NotNullEntityRepository).Assembly)
            {
            }
        }

        [SetUp]
        public void SetUp()
        {
            _db = new TestDatabase("tempdb");
        }

        private TestDatabase _db;

        [Test]
        public void SessionFactory_ConnectionNameNotExist_ShouldThrow()
        {
            Assert.Throws<ConfigurationErrorsException>(() => { var obj = new TestDatabase("invalid_conn").SessionFactory; });
        }

        [Test]
        public void Engine_ShouldBeValid()
        {
            Assert.IsInstanceOf<SqlDbEngine>(_db.Engine);
        }

        [Test]
        public void SessionFactory_ShouldBeValid()
        {
            var sessionFactory = _db.SessionFactory;

            Assert.IsNotNull(sessionFactory);
            Assert.IsNotNull(sessionFactory.GetMapper<NotNullEntity>());
        }

        [Test]
        public void CreateDbContext_ShouldBeValid()
        {
            var ctx = _db.CreateDbContext();

            Assert.IsNotNull(ctx);
            Assert.IsNotNull(ctx.NotNullEntityRepository);
        }
    }
}