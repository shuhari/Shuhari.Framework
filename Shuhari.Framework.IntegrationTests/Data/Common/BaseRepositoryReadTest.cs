using NUnit.Framework;
using Shuhari.Framework.UnitTests.Data;

namespace Shuhari.Framework.IntegrationTests.Data.Common
{
    [TestFixture]
    public class BaseRepositoryReadTest : DbTestBase
    {
        public BaseRepositoryReadTest() 
            : base(true)
        {
        }

        protected internal override void AfterSetUp()
        {
            base.AfterSetUp();

            _repository = new NotNullEntityRepository { Session = Session };
        }

        private NotNullEntityRepository _repository;

        [Test]
        public void CreateQuery()
        {
            Assert.AreEqual(1, _repository.CreateQuery("select count(*) from TNotNullEntity").ExecInt());
        }

        [Test]
        public void Count()
        {
            Assert.AreEqual(1, _repository.Count());
        }

        [Test]
        public void GetById()
        {
            int id = 1;
            var entity = _repository.GetById(id);
            Assert.IsNotNull(entity);
        }
    }
}
