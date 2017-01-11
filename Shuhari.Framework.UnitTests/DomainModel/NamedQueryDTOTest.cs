using NUnit.Framework;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.UnitTests.DomainModel
{
    [TestFixture]
    public class NamedQueryDTOTest
    {
        [Test]
        public void Ctor_ShouldSetProperties()
        {
            var q = new NamedQueryDTO(1, 20, "n");

            Assert.AreEqual(1, q.Page);
            Assert.AreEqual(20, q.PerPage);
            Assert.AreEqual("n", q.Name);
        }
    }
}
