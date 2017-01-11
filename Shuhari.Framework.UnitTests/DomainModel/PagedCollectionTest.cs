using NUnit.Framework;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.UnitTests.DomainModel
{
    [TestFixture]
    public class PagedCollectionTest
    {
        [SetUp]
        public void SetUp()
        {
            _collection = new PagedCollection<int>();
        }

        private PagedCollection<int> _collection;

        [Test]
        public void SetPagination()
        {
            _collection.SetPagination(new QueryDTO(1, 20));

            Assert.AreEqual(1, _collection.Page);
            Assert.AreEqual(20, _collection.PerPage);
        }

        [Test]
        public void SetData()
        {
            var data = new[] { 1, 2, 3 };
            _collection.SetData(100, data);

            Assert.AreEqual(100, _collection.Total);
            CollectionAssert.AreEqual(data, _collection.Data);
        }

        [Test]
        public void MapTo()
        {
            _collection.SetData(100, new[] { 1, 2, 3 });
            var mapped = _collection.MapTo(x => x * 2);

            Assert.AreEqual(100, mapped.Total);
            CollectionAssert.AreEqual(new[] { 2, 4, 6 }, mapped.Data);
        }
    }
}
