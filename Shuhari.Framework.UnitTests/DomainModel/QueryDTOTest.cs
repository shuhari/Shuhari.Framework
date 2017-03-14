using System.Data;
using Moq;
using NUnit.Framework;
using Shuhari.Framework.Data;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.DomainModel
{
    [TestFixture]
    public class QueryDtoTest
    {
        [Test]
        public void SetPagination_ShouldSetOffset()
        {
            var q = new QueryDto();
            q.SetPagination(2, 20);

            Assert.AreEqual(2, q.Page);
            Assert.AreEqual(20, q.PerPage);
            Assert.AreEqual(40, q.Offset);
        }

        [Test]
        public void Ctor_PageInvalid_ShouldThrow()
        {
            Assert.Throws<ExpectionException>(() => new QueryDto(-1, -1).ToString());
        }

        [Test]
        public void SetQuery()
        {
            var mockQuery = new Mock<IQueryBase>();
            var q = new QueryDto(2, 20);
            q.SetQuery(mockQuery.Object);

            mockQuery.Verify(m => m.SetParam(QueryDto.PARAM_OFFSET, DbType.Int32, 40));
            mockQuery.Verify(m => m.SetParam(QueryDto.PARAM_LIMIT, DbType.Int32, 20));
        }

        [Test]
        public void ToCritias()
        {
            Assert.IsNotNull(new QueryDto().ToCritias());
        }
    }
}
