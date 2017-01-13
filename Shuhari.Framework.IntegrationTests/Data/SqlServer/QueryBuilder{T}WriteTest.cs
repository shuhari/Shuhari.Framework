using NUnit.Framework;
using Shuhari.Framework.Data;
using Shuhari.Framework.UnitTests.Data;

namespace Shuhari.Framework.IntegrationTests.Data.SqlServer
{
    [TestFixture]
    public class QueryBuilderOfTWriteTest : DbTestBase
    {
        public QueryBuilderOfTWriteTest() 
            : base(false)
        {
        }

        private IQueryBuilder<NotNullEntity> _builder;

        protected internal override void AfterSetUp()
        {
            _builder = Session.SessionFactory.GetQueryBuilder<NotNullEntity>();
        }

        [Test]
        public void DeleteById()
        {
            int id = 1;
            _builder.DeleteById(Session, id).ExecNonQuery();

            Assert.IsNull(_builder.GetById(Session, id).GetFirst());
        }

        [Test]
        public void Insert()
        {
            var entity = new NotNullEntity
            {
                StringProp = "",
                BinaryProp = new byte[0]
            };
            var id = (int)_builder.Insert(Session, entity).ExecScalar();

            Assert.IsTrue(id > 0);
            Assert.IsTrue(id != 1);
        }

        [Test]
        public void Update()
        {
            int id = 1;
            var entity = _builder.GetById(Session, id).GetFirst();
            entity.StringProp = "xyz";
            _builder.Update(Session, entity).ExecNonQuery();

            var updated = _builder.GetById(Session, id).GetFirst();
            Assert.AreEqual("xyz", updated.StringProp);
        }

        [Test]
        public void UpdatePartial()
        {
            int id = 1;
            var entity = _builder.GetById(Session, id).GetFirst();
            entity.StringProp = "xyz";
            _builder.UpdatePartial(Session, entity, x => x.StringProp).ExecNonQuery();

            var updated = _builder.GetById(Session, id).GetFirst();
            Assert.AreEqual("xyz", updated.StringProp);
        }
    }
}
