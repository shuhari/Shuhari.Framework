using System.Threading;
using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;
using Shuhari.Framework.Wpf.Controls;

namespace Shuhari.Framework.UnitTests.Wpf.Controls
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class ElementExtensionsTest
    {
        [Test]
        public void EnableElements()
        {
            var parent = new Window();
            var btn = new Button();
            parent.EnableElements(false, btn);

            Assert.IsFalse(btn.IsEnabled);
        }

        [Test]
        public void Show()
        {
            var btn = new Button();
            btn.Show();

            Assert.AreEqual(Visibility.Visible, btn.Visibility);
        }

        [Test]
        public void Hide()
        {
            var btn = new Button();
            btn.Hide();

            Assert.AreEqual(Visibility.Collapsed, btn.Visibility);
        }
    }
}
