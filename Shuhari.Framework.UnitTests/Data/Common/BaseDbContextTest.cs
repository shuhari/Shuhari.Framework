using System;
using System.Data;
using System.Diagnostics;
using Moq;
using NUnit.Framework;
using Shuhari.Framework.Data;

namespace Shuhari.Framework.UnitTests.Data.Common
{
    [TestFixture]
    public class BaseDbContextTest
    {
        [SetUp]
        public void SetUp()
        {
            _mockSessionFactory = new Mock<ISessionFactory>();
            _mockSession = new Mock<ISession>();
            _mockSessionFactory.Setup(m => m.OpenSession(It.IsAny<object>()))
                .Returns(_mockSession.Object);
            _mockTransaction = new Mock<IDbTransaction>();
            _mockSession.Setup(m => m.BeginTransaction()).Returns(_mockTransaction.Object);

            _dbCtx = new TestDbContext(_mockSessionFactory.Object);
            _dbCtx.OpenSession();
        }

        private TestDbContext _dbCtx;
        private Mock<ISessionFactory> _mockSessionFactory;
        private Mock<ISession> _mockSession;
        private Mock<IDbTransaction> _mockTransaction;

        [Test]
        public void GetRepositories()
        {
            CollectionAssert.Contains(_dbCtx.Repositories, _dbCtx.NotNullEntityRepository);
            CollectionAssert.Contains(_dbCtx.Repositories, _dbCtx.NullableEntityRepository);
        }

        [Test]
        public void OpenSession_CloseSession()
        {
            Assert.AreSame(_mockSession.Object, _dbCtx.NotNullEntityRepository.Session);
            Assert.AreSame(_mockSession.Object, _dbCtx.NullableEntityRepository.Session);

            _dbCtx.CloseSession();
            Assert.IsNull(_dbCtx.NotNullEntityRepository.Session);
            Assert.IsNull(_dbCtx.NullableEntityRepository.Session);
            _mockSession.Verify(m => m.Dispose());
        }

        [Test]
        public void ExecuteTransaction_Success_ShouldSubmit()
        {
            _dbCtx.ExecuteTransaction(() => Debug.WriteLine("ok"));

            _mockTransaction.Verify(m => m.Commit());
        }

        [Test]
        public void ExecuteTransaction_Throws_ShouldRollback()
        {
            try
            {
                _dbCtx.ExecuteTransaction(() => { throw new ArgumentException(); });
            }
            catch(ArgumentException)
            {
            }

            _mockTransaction.Verify(m => m.Rollback());
        }

        [Test]
        public void CreateSessionScope()
        {
            using (_dbCtx.CreateSessionScope())
            {
                _mockSessionFactory.Verify(m => m.OpenSession(It.IsAny<object>()));
            }

            _mockSession.Verify(m => m.Dispose());
        }
    }
}
