using System;
using NUnit.Framework;
using Shuhari.Framework.Data;
using Shuhari.Framework.UnitTests.Data;

namespace Shuhari.Framework.IntegrationTests.Data.SqlServer
{
    [TestFixture]
    public class QueryBuilderOfTReadTest : DbTestBase
    {
        public QueryBuilderOfTReadTest() 
            : base(true)
        {
        }

        private IQueryBuilder<NotNullEntity> _builder;

        protected internal override void AfterSetUp()
        {
            _builder = Session.SessionFactory.GetQueryBuilder<NotNullEntity>();
        }

        [Test]
        public void GetById()
        {
            int id = 1;
            var entity = _builder.GetById(Session, 1).GetFirst();

            Assert.IsNotNull(entity);
            Assert.AreEqual(id, entity.Id);
        }

        [Test]
        public void GetById_PkTypeInvalid_ShouldThrow()
        {
            Assert.Throws<NotSupportedException>(() => _builder.GetById(Session, "abc"));
        }

        [Test]
        public void GetAll_NoOrder()
        {
            var result = _builder.QueryAll(Session).GetAll();
            CollectionAssert.IsNotEmpty(result);
        }

        [Test]
        public void GetAll_WithOrder()
        {
            var result = _builder.QueryAll(Session, _builder.OrderBy(x => x.Id, true)).GetAll();
            CollectionAssert.IsNotEmpty(result);
        }

        [Test]
        public void Count()
        {
            Assert.AreEqual(1, _builder.Count(Session).ExecInt());
        }
    }
}
