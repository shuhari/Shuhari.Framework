using System.Windows;
using System.Windows.Controls;

namespace Shuhari.Framework.Wpf.Controls
{
    /// <summary>
    /// Simplify Grid control in following way:
    /// <ul>
    ///   <li>Set Rows/Columns in string instead of lengthy RowDefinitions/ColumnDefinitions</li>
    ///   <li>Set Default margin between children</li>
    /// </ul>
    /// </summary>
    public class SimpleGrid : Grid
    {
        /// <summary>
        /// Define rows in string
        /// </summary>
        public string Rows
        {
            get { return (string)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        /// <summary>
        /// Rows property
        /// </summary>
        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(nameof(Rows), 
            typeof(string), typeof(SimpleGrid), new PropertyMetadata("", OnRowsChanged));

        /// <summary>
        /// Define columns in string
        /// </summary>
        public string Columns
        {
            get { return (string)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        /// <summary>
        /// Columns property
        /// </summary>
        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register(nameof(Columns), 
            typeof(string), typeof(SimpleGrid), new PropertyMetadata("", OnColumnsChanged));

        private static void OnRowsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SimpleGrid)d).SetRows((string)e.NewValue);
        }

        private static void OnColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SimpleGrid)d).SetColumns((string)e.NewValue);
        }

        private void SetRows(string rows)
        {
            var items = GridUtil.ParseAll(rows);

            RowDefinitions.Clear();
            foreach (var item in items)
                RowDefinitions.Add(new RowDefinition { Height = item });
        }

        private void SetColumns(string columns)
        {
            var items = GridUtil.ParseAll(columns);

            ColumnDefinitions.Clear();
            foreach (var item in items)
                ColumnDefinitions.Add(new ColumnDefinition { Width = item });
        }

        /// <summary>
        /// Margin between children
        /// </summary>
        public Thickness ChildMargin
        {
            get { return (Thickness)GetValue(ChildMarginProperty); }
            set { SetValue(ChildMarginProperty, value); }
        }

        /// <summary>
        /// ChildMargin property
        /// </summary>
        public static readonly DependencyProperty ChildMarginProperty = DependencyProperty.Register(nameof(ChildMargin),
                typeof(Thickness), typeof(SimpleGrid),
                new PropertyMetadata(new Thickness(4)));

        /// <summary>
        /// Set default margin
        /// </summary>
        /// <param name="visualAdded"></param>
        /// <param name="visualRemoved"></param>
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);

            var addedElem = visualAdded as FrameworkElement;
            if (addedElem != null && addedElem.ReadLocalValue(MarginProperty) == DependencyProperty.UnsetValue)
                addedElem.Margin = ChildMargin;
        }
    }
}
