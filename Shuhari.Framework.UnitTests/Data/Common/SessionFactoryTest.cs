using NUnit.Framework;
using Shuhari.Framework.Data;

namespace Shuhari.Framework.UnitTests.Data.Common
{
    [TestFixture]
    public class SessionFactoryTest
    {
        [SetUp]
        public void SetUp()
        {
            _sessionFactory = Fixtures.SqlSessionFactory;
        }

        private ISessionFactory _sessionFactory;

        [Test]
        public void OpenSession()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                Assert.IsNotNull(session);
                Assert.AreSame(_sessionFactory, session.SessionFactory);
            }
        }
    }
}
