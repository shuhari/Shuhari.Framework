using NUnit.Framework;
using Shuhari.Framework.UnitTests.Data;

namespace Shuhari.Framework.IntegrationTests.Data.Common
{
    [TestFixture]
    public class BaseRepositoryWriteTest : DbTestBase
    {
        public BaseRepositoryWriteTest() 
            : base(false)
        {
        }

        protected internal override void AfterSetUp()
        {
            base.AfterSetUp();

            _repository = new NotNullEntityRepository { Session = Session };
        }

        private NotNullEntityRepository _repository;

        [Test]
        public void Insert()
        {
            var entity = new NotNullEntity
            {
                StringProp = "",
                BinaryProp = new byte[0]
            };
            _repository.Insert(entity);

            Assert.That(entity.Id > 0);
        }

        [Test]
        public void Update()
        {
            int id = 1;
            var entity = _repository.GetById(id);
            entity.StringProp = "xyz";
            _repository.Update(entity);

            Assert.AreEqual("xyz", _repository.GetById(id).StringProp);
        }

        [Test]
        public void UpdatePartial()
        {
            int id = 1;
            var entity = _repository.GetById(id);
            entity.StringProp = "xyz";
            _repository.UpdatePartial(entity, x => x.StringProp);

            Assert.AreEqual("xyz", _repository.GetById(id).StringProp);
        }

        [Test]
        public void Delete()
        {
            int id = 1;
            _repository.DeleteById(1);

            Assert.IsNull(_repository.GetById(id));
        }
    }
}
