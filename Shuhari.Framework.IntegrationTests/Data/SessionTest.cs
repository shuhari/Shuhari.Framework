using System.Data;
using NUnit.Framework;

namespace Shuhari.Framework.IntegrationTests.Data
{
    [TestFixture]
    public class SessionTest : DbTestBase
    {
        public SessionTest() 
            : base(true)
        {
        }

        [Test]
        public void SessionFactory_ShouldBeNotNull()
        {
            Assert.IsNotNull(Session.SessionFactory);
        }

        [Test]
        public void Connection_Visited_ShouldBeNotNull()
        {
            Assert.IsNotNull(Session.Connection);
        }

        [Test]
        public void Disposed_ConnectionShouldBeNull()
        {
            var connection = Session.Connection;

            Session.Dispose();
            Assert.AreEqual(ConnectionState.Closed, connection.State);
        }
    }
}
