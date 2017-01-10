using System.Linq;
using NUnit.Framework;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Utils
{
    [TestFixture]
    public class CollectionUtilTest
    {
        public class TestItem
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

        [Test]
        public void FindBy_Generic()
        {
            Assert.IsNotNull(TestCollection.FindBy(x => x.Id, 1));
            Assert.IsNull(TestCollection.FindBy(x => x.Id, 999));
        }

        [TestCase("item1", false, true)]
        [TestCase("item999", false, false)]
        [TestCase("Item1", false, false)]
        [TestCase("Item1", true, true)]
        public void FindBy_String(string name, bool ignoreCase, bool found)
        {
            var item = TestCollection.FindBy(x => x.Name, name, ignoreCase);
            Assert.AreEqual(found, item != null);
        }
    }
}
