using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Utils
{
    [TestFixture]
    public class CollectionUtilTest
    {
        public class TestItem : INamed
        {
            public TestItem(int id, string name)
            {
                this.Id = id;
                this.Name = name;
            }
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private TestItem[] TestCollection
        {
            get
            {
                return new[]
                {
                    new TestItem(1, "item1"),
                    new TestItem(2, "item2"),
                };
            }
        }

        [Test]
        public void Each()
        {
            var collection = TestCollection;
            collection.Each(x => x.Name += "x");

            CollectionAssert.AreEqual(new[] { "item1x", "item2x" }, collection.Select(x => x.Name));
        }

        [TestCase(1, true)]
        [TestCase(-1, false)]
        public void Find(int id, bool found)
        {
            Assert.AreEqual(found, TestCollection.Find(x => x.Id == id) != null);
        }

        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(999, -1)]
        public void FindIndex(int id, int result)
        {
            Assert.AreEqual(result, TestCollection.FindIndex(x => x.Id == id));
        }

        [Test]
        public void Safe_ParamNull_ShouldReturnEmpty()
        {
            IEnumerable<int> collection = null;
            var safe = collection.Safe();
            Assert.IsNotNull(safe);
            CollectionAssert.IsEmpty(safe);
        }

        [Test]
        public void Safe_ParamNotEmpty_ShouldReturnOrigin()
        {
            IEnumerable<int> collection = new[] {1, 2, 3};
            var safe = collection.Safe();
            Assert.AreSame(collection, safe);
        }
    }
}
