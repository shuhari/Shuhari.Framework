using System.Data;
using Moq;
using NUnit.Framework;
using Shuhari.Framework.Data;
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

        [Test]
        public void SetQuery()
        {
            var mockQuery = new Mock<IQuery>();
            var q = new NamedQueryDTO(1, 20, "n");
            q.SetQuery(mockQuery.Object);

            mockQuery.Verify(m => m.Set(QueryDTO.PARAM_OFFSET, 20));
            mockQuery.Verify(m => m.Set(QueryDTO.PARAM_LIMIT, 20));
            mockQuery.Verify(m => m.Set(NamedQueryDTO.PARAM_NAME, DbType.String, "n"));
        }
    }
}
