using NUnit.Framework;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.UnitTests.DomainModel
{
    [TestFixture]
    public class PagerItemTest
    {
        [Test]
        public void Equals()
        {
            var item1 = new PagerItem(12, "page", true, false);
            var item2 = new PagerItem(12, "page", true, false);

            Assert.AreEqual(item1, item2);
            Assert.AreEqual(item1.GetHashCode(), item2.GetHashCode());
        }
    }
}
