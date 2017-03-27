using System.Windows;
using System.Windows.Controls;

namespace Shuhari.Framework.Wpf.Controls
{
    /// <summary>
    /// Vertical grid splitter
    /// </summary>
    public class VGridSplitter : SimpleGridSplitter
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public VGridSplitter()
        {
            HorizontalAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Center;
            Height = DEFAULT_SIZE;
            Grid.SetRow(this, 1);
        }
    }
}
