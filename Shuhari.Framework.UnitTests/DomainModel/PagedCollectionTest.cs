using System.Collections.Generic;
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
            _collection.SetPagination(new QueryDto(1, 20));

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

        private Pager CalculatePager(int total, int page)
        {
            var q = new QueryDto(page, 20);
            var collection = new PagedCollection<int>();
            collection.SetPagination(q);
            collection.Total = total;
            var pager = collection.CalculatePager();
            return pager;
        }

        [Test]
        public void CalculatePager_NoData()
        {
            var result = CalculatePager(0, 0);

            AssertPager(result, 0, 0, 0, new PagerItem[0]);
        }

        [Test]
        public void CalculatePager_OnePageOnly()
        {
            var result = CalculatePager(3, 0);

            AssertPager(result, 3, 0, 2, new[]
            {
                new PagerItem(0, "1", true, true)
            });
        }

        [Test]
        public void CalculatePager_Page2()
        {
            var result = CalculatePager(24, 1);

            AssertPager(result, 24, 20, 23, new[]
            {
                new PagerItem(0, "1", false, false),
                new PagerItem(1, "2", true, true),
            });
        }

        [Test]
        public void CalculatePager_3Pages_Middle()
        {
            var result = CalculatePager(51, 1);

            AssertPager(result, 51, 20, 39, new[]
            {
                new PagerItem(0, "1", false, false),
                new PagerItem(1, "2", true, true),
                new PagerItem(2, "3", false, false),
            });
        }

        [Test]
        public void CalculatePager_100Pages_Middle()
        {
            var result = CalculatePager(1995, 24);

            AssertPager(result, 1995, 480, 499, new[]
            {
                new PagerItem(0, "1", false, false),
                new PagerItem(1, "2", false, false),
                new PagerItem(2, "3", false, false),
                new PagerItem(-1, "...", false, true),
                new PagerItem(22, "23", false, false),
                new PagerItem(23, "24", false, false),
                new PagerItem(24, "25", true, true),
                new PagerItem(25, "26", false, false),
                new PagerItem(26, "27", false, false),
                new PagerItem(-1, "...", false, true),
                new PagerItem(97, "98", false, false),
                new PagerItem(98, "99", false, false),
                new PagerItem(99, "100", false, false),
            });
        }

        private void AssertPager(Pager pager, int total, int startIndex, int endIndex, IEnumerable<PagerItem> items)
        {
            Assert.AreEqual(total, pager.Total, "Expected total={0}, got {1}", total, pager.Total);
            Assert.AreEqual(startIndex, pager.StartIndex, "Expect startIndex={0}, got {1}", startIndex, pager.StartIndex);
            Assert.AreEqual(endIndex, pager.EndIndex, "Expect endIndex={0}, got {1}", endIndex, pager.EndIndex);
            CollectionAssert.AreEqual(items, pager.Items);
        }
    }
}
