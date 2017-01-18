using System.Linq;
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

        private Pager CalculatePager(int total, int page)
        {
            var q = new QueryDTO(page, 20);
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

            AssertPager(result, 0, 0, 0, 0);
        }

        [Test]
        public void CalculatePager_OnePageOnly()
        {
            var result = CalculatePager(3, 0);

            AssertPager(result, 3, 0, 2, 1);
            AssertPagerItem(result.Items.First(), 0, "1", false);
        }

        [Test]
        public void CalculatePager_Page2()
        {
            var result = CalculatePager(24, 1);

            AssertPager(result, 24, 20, 23, 2);
            var items = result.Items.ToArray();
            AssertPagerItem(items[0], 0, "<<", true);
            AssertPagerItem(items[1], 1, "2", false);
        }

        [Test]
        public void CalculatePager_3Pages_Middle()
        {
            var result = CalculatePager(51, 1);

            AssertPager(result, 51, 20, 39, 3);
            var items = result.Items.ToArray();
            AssertPagerItem(items[0], 0, "<<", true);
            AssertPagerItem(items[1], 1, "2", false);
            AssertPagerItem(items[2], 2, ">>", true);
        }

        [Test]
        public void CalculatePager_100Pages_Middle()
        {
            var result = CalculatePager(1995, 24);

            AssertPager(result, 1995, 480, 499, 13);
            var items = result.Items.ToArray();
            AssertPagerItem(items[0], 0, "<<", true);
            AssertPagerItem(items[1], 1, "2", true);
            AssertPagerItem(items[2], 2, "3", true);
            AssertPagerItem(items[3], -1, "...", false);
            AssertPagerItem(items[4], 22, "23", true);
            AssertPagerItem(items[5], 23, "24", true);
            AssertPagerItem(items[6], 24, "25", false);
            AssertPagerItem(items[7], 25, "26", true);
            AssertPagerItem(items[8], 26, "27", true);
            AssertPagerItem(items[9], -1, "...", false);
            AssertPagerItem(items[10], 97, "98", true);
            AssertPagerItem(items[11], 98, "99", true);
            AssertPagerItem(items[12], 99, ">>", true);
        }

        private void AssertPager(Pager pager, int total, int startIndex, int endIndex, int itemCount)
        {
            Assert.AreEqual(total, pager.Total, "Expected total={0}, got {1}", total, pager.Total);
            Assert.AreEqual(startIndex, pager.StartIndex, "Expect startIndex={0}, got {1}", startIndex, pager.StartIndex);
            Assert.AreEqual(endIndex, pager.EndIndex, "Expect endIndex={0}, got {1}", endIndex, pager.EndIndex);
            Assert.AreEqual(itemCount, pager.Items.Count(), "Expect item count={0}, got {1}", itemCount, pager.Items.Count());
        }

        private void AssertPagerItem(PagerItem item, int page, string text, bool navigate)
        {
            Assert.AreEqual(page, item.Page, "Expect page={0}, got {1}", page, item.Page);
            Assert.AreEqual(text, item.DisplayName);
            Assert.AreEqual(navigate, item.Navigate, "Expect page {0} navigate={1}", text, navigate);
        }
    }
}
