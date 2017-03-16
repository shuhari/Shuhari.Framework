using System.Threading;
using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;
using Shuhari.Framework.Wpf.Controls;

namespace Shuhari.Framework.UnitTests.Wpf.Controls
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class FromGridTest
    {
        [Test]
        public void SimpleLayout()
        {
            var margin = new Thickness(4);
            var form = new FormGrid {ColumnCount = 2, ChildMargin = margin};
            var labels = new[] {new Label(), new Label(), new Label(), new Label()};
            var texts = new[] {new TextBox(), new TextBox(), new TextBox(), new TextBox()};
            for (int i = 0; i < 4; i++)
            {
                form.Children.Add(labels[i]);
                form.Children.Add(texts[i]);
            }
            form.RecalcLayout();

            AssertCell(labels[0], 0, 0);
            AssertCell(texts[0], 0, 1);
            AssertCell(labels[1], 0, 2);
            AssertCell(texts[1], 0, 3);
            AssertCell(labels[2], 1, 0);
            AssertCell(texts[2], 1, 1);
            AssertCell(labels[3], 1, 2);
            AssertCell(texts[3], 1, 3);

            Assert.AreEqual(margin, labels[0].Margin);
            Assert.AreEqual(HorizontalAlignment.Right, labels[0].HorizontalAlignment);
        }

        private void AssertCell(FrameworkElement elem, int row, int col)
        {
            Assert.AreEqual(row, Grid.GetRow(elem),
                "Expect {0} row={1}, actual={2}", elem.GetType(), row, Grid.GetRow(elem));
            Assert.AreEqual(col, Grid.GetColumn(elem),
                "Expect {0} colulmn={1}, actual={2}", elem.GetType(), col, Grid.GetColumn(elem));
        }
    }
}