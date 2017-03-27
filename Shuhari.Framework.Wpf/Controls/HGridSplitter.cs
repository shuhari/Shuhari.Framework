using System.Windows;
using System.Windows.Controls;

namespace Shuhari.Framework.Wpf.Controls
{
    /// <summary>
    /// Horizontal grid splitter (shape vertical)
    /// </summary>
    public class HGridSplitter : SimpleGridSplitter
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public HGridSplitter()
        {
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Stretch;
            Width = DEFAULT_SIZE;
            Grid.SetColumn(this, 1);
        }
    }
}
