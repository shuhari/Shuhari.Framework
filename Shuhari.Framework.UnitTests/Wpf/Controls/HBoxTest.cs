using System.Threading;
using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;
using Shuhari.Framework.Wpf.Controls;

namespace Shuhari.Framework.UnitTests.Wpf.Controls
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class HBoxTest
    {
        [Test]
        public void ChildMargin()
        {
            var margin = new Thickness(10);
            var box = new HBox {ChildMargin = margin};
            var tb = new TextBlock();
            box.Children.Add(tb);

            Assert.AreEqual(margin, tb.Margin);
        }
    }
}