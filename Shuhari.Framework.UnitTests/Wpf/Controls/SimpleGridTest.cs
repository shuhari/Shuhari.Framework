using System.Threading;
using System.Windows;
using NUnit.Framework;
using Shuhari.Framework.Wpf.Controls;

namespace Shuhari.Framework.UnitTests.Wpf.Controls
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class SimpleGridTest
    {
        [Test]
        public void Rows()
        {
            var grid = new SimpleGrid { Rows = "1*, 20" };

            Assert.AreEqual(2, grid.RowDefinitions.Count);
            Assert.AreEqual(new GridLength(1, GridUnitType.Star), grid.RowDefinitions[0].Height);
            Assert.AreEqual(new GridLength(20, GridUnitType.Pixel), grid.RowDefinitions[1].Height);
        }

        [Test]
        public void Columns()
        {
            var grid = new SimpleGrid { Columns = "1*, 20" };

            Assert.AreEqual(2, grid.ColumnDefinitions.Count);
            Assert.AreEqual(new GridLength(1, GridUnitType.Star), grid.ColumnDefinitions[0].Width);
            Assert.AreEqual(new GridLength(20, GridUnitType.Pixel), grid.ColumnDefinitions[1].Width);
        }
    }
}
