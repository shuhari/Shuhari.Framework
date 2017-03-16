using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Shuhari.Framework.Wpf.Controls
{
    /// <summary>
    /// Given element are layout like a form (label left of control),
    /// This custom grid place elements automatically, avoid manually
    /// set Row/Columns, and also apply the common rule:
    /// <ul>
    ///   <li>Labels aligned right, if not manually set;</li>
    ///   <li>ChildMargin applied to each child if not set manually.</li>
    /// </ul>
    /// </summary>
    public class FormGrid : Grid
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public FormGrid()
        {
            Loaded += FormGrid_Loaded;
        }

        /// <summary>
        /// Column count (label and control together are treated as one column)
        /// </summary>
        public int ColumnCount
        {
            get { return (int) GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        /// <summary>
        /// ColumnCount property
        /// </summary>
        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.Register(nameof(ColumnCount),
                typeof(int), typeof(FormGrid),
                new PropertyMetadata(1));

        /// <summary>
        /// Margin between children
        /// </summary>
        public Thickness ChildMargin
        {
            get { return (Thickness) GetValue(ChildMarginProperty); }
            set { SetValue(ChildMarginProperty, value); }
        }

        /// <summary>
        /// ChildMargin property
        /// </summary>
        public static readonly DependencyProperty ChildMarginProperty =
            DependencyProperty.Register(nameof(ChildMargin),
                typeof(Thickness), typeof(FormGrid),
                new PropertyMetadata(new Thickness(4)));

        private void FormGrid_Loaded(object sender, RoutedEventArgs e)
        {
            RecalcLayout();
        }

        /// <summary>
        /// Recalc layout
        /// </summary>
        public void RecalcLayout()
        {
            const int MAX_ROWS = 100;
            var cells = new GridCellArray(MAX_ROWS, ColumnCount * 2);
            var children = Children.OfType<FrameworkElement>().ToArray();
            cells.PlaceElements(children);

            RowDefinitions.Clear();
            ColumnDefinitions.Clear();
            for (int i = 0; i < ColumnCount; i++)
            {
                ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto});
                ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)});
            }
            for (int i = 0; i < cells.RowCount; i++)
            {
                RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
            }

            foreach (var cell in cells.EnumCells().Where(x => x.Element != null))
            {
                var elem = cell.Element;
                SetRow(cell.Element, cell.Row);
                SetColumn(cell.Element, cell.Column);

                elem.SetUserProperty(MarginProperty, ChildMargin);
                if (elem is Label)
                    elem.SetUserProperty(HorizontalAlignmentProperty, HorizontalAlignment.Right);
            }
        }
    }
}