using NUnit.Framework;

namespace Shuhari.Framework.IntegrationTests.Data
{
    [TestFixture]
    public class TransactionTest : DbTestBase
    {
        public TransactionTest() 
            : base(false)
        {
        }

        [Test]
        public void ExecuteTransation_Commit_ShouldPersist()
        {
            using (var trans = Session.BeginTransaction())
            {
                Session.CreateQuery("delete from TNotNullEntity where FID=1").ExecNonQuery();
                trans.Commit();
            }

            Assert.AreEqual(0, Session.CreateQuery("select count(*) from TNotNullEntity where FID=1").ExecInt());
        }

        [Test]
        public void ExecuteTransation_Rollback_ShouldNotPersist()
        {
            using (var trans = Session.BeginTransaction())
            {
                Session.CreateQuery("delete from TNotNullEntity where FID=1").ExecNonQuery();
                trans.Rollback();
            }

            Assert.AreEqual(1, Session.CreateQuery("select count(*) from TNotNullEntity where FID=1").ExecInt());
        }
    }
}
