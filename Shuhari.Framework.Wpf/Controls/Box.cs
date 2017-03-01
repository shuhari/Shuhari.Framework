using System.Windows;
using System.Windows.Controls;

namespace Shuhari.Framework.Wpf.Controls
{
    /// <summary>
    /// StackPanel element with margin within children
    /// </summary>
    public abstract class Box : StackPanel
    {
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
                typeof(Thickness), typeof(Box), new PropertyMetadata(new Thickness(4)));

        /// <summary>
        /// Set margin for children
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
