using System.Collections.ObjectModel;
using NUnit.Framework;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Utils
{
    [TestFixture]
    public class ObservableCollectionUtilTest
    {
        [Test]
        public void AddRange()
        {
            var collection = new ObservableCollection<int>();
            collection.AddRange(new[] { 1, 2, 3 });

            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, collection);
        }

        [Test]
        public void Sort()
        {
            var collection = new ObservableCollection<int>();
            collection.AddRange(new[] { 2, 3, 1 });
            collection.Sort();

            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, collection);
        }
    }
}
