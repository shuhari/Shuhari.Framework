using NUnit.Framework;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.DomainModel
{
    [TestFixture]
    public class QueryDTOTest
    {
        [Test]
        public void SetPagination_ShouldSetOffset()
        {
            var q = new QueryDTO();
            q.SetPagination(2, 20);

            Assert.AreEqual(2, q.Page);
            Assert.AreEqual(20, q.PerPage);
            Assert.AreEqual(40, q.Offset);
        }

        [Test]
        public void Ctor_PageInvalid_ShouldThrow()
        {
            Assert.Throws<ExpectionException>(() => new QueryDTO(-1, -1));
        }
    }
}
