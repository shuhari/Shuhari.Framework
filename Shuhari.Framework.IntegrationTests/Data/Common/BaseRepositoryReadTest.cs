using NUnit.Framework;
using Shuhari.Framework.DomainModel;
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

        [Test]
        public void QueryPaged()
        {
            string baseSql = @"select * from TNotNullEntity";
            var qdata = new QueryDTO();
            qdata.SetPagination(0, 20);
            var result = _repository.QueryPaged(baseSql, _repository.OrderBy(x => x.StringProp), qdata);

            Assert.AreEqual(1, result.Total);
            Assert.AreEqual(1, result.Data.Length);
        }

        [Test]
        public void GetAll_NoOrder()
        {
            var result = _repository.GetAll();

            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void GetAll_WithOrder()
        {
            var result = _repository.GetAll(_repository.OrderBy(x => x.Id));

            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void GetBy_Exist_ShouldReturn()
        {
            var e = _repository.GetBy(x => x.StringProp, "abc");
            Assert.IsNotNull(e);
            Assert.AreEqual("abc", e.StringProp);
        }

        [Test]
        public void GetBy_NotExist_ShouldReturnNull()
        {
            Assert.IsNull(_repository.GetBy(x => x.StringProp, "not_prop"));
        }
    }
}
