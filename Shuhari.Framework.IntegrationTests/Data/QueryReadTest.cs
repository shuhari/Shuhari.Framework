using System;
using System.IO;
using NUnit.Framework;
using Shuhari.Framework.UnitTests.Data;

namespace Shuhari.Framework.IntegrationTests.Data
{
    [TestFixture]
    public class QueryReadTest : DbTestBase
    {
        public QueryReadTest() 
            : base(true)
        {
        }

        [Test]
        public void ExecScalar()
        {
            Assert.IsNotNull(Session.CreateQuery("select count(*) from TNotNullEntity").ExecScalar());
        }

        [Test]
        public void ExecInt()
        {
            Assert.That(Session.CreateQuery("select count(*) from TNotNullEntity").ExecInt() > 0);
        }

        [Test]
        public void ExecNonQuery()
        {
            Assert.AreEqual(0, Session.CreateQuery("update TNotNullEntity set FIntProp=0 where FID<0").ExecNonQuery());
        }

        [Test]
        public void GetAll_NotNull()
        {
            var result = Session.CreateQuery("select * from TNotNullEntity").GetAll<NotNullEntity>();

            CollectionAssert.IsNotEmpty(result);
            var entity = result[0];
            Assert.AreEqual(FileMode.CreateNew, entity.EnumProp);
            Assert.IsNotNull(entity.BinaryProp);
            Assert.IsNotNull(entity.StringProp);
            Assert.AreNotEqual(Guid.Empty, entity.GuidProp);
        }

        [Test]
        public void GetAll_WithParams()
        {
            var result = Session.CreateQuery("select * from TNotNullEntity where FID=@ID")
                .Set("ID", 1)
                .GetAll<NotNullEntity>();
            CollectionAssert.IsNotEmpty(result);
        }

        [Test]
        public void GetFirst_Exist()
        {
            var query = Session.CreateQuery("select * from TNotNullEntity where FID=@ID");
            query.Set("ID", 1);
            var entity = query.GetFirst<NotNullEntity>();

            Assert.IsNotNull(entity);
        }

        [Test]
        public void GetFirst_NotExist_ShouldReturnNull()
        {
            Assert.IsNull(Session.CreateQuery("select * from TNotNullEntity where FID=0").GetFirst<NotNullEntity>());
        }

        [Test]
        public void GetAll_Nullable()
        {
            var result = Session.CreateQuery("select * from TNullableEntity").GetAll<NullableEntity>();

            CollectionAssert.IsNotEmpty(result);
            var entity = result[0];
            Assert.AreEqual(FileMode.CreateNew, entity.EnumProp);
            Assert.AreNotEqual(Guid.Empty, entity.GuidProp);
        }
    }
}
