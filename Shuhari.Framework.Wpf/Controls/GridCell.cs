using System.Windows;

namespace Shuhari.Framework.Wpf.Controls
{
    /// <summary>
    /// Describe Grid cell
    /// </summary>
    internal sealed class GridCell
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public GridCell(int row,  int column)
        {
            this.Row = row;
            this.Column = column;
            this.Usage = GridCellUsage.None;
        }

        /// <summary>
        /// Row index
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// Column index
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// Element
        /// </summary>
        public FrameworkElement Element { get; private set; }

        /// <summary>
        /// Usage
        /// </summary>
        public GridCellUsage Usage { get; private set; }

        /// <summary>
        /// Set usage
        /// </summary>
        /// <param name="usage"></param>
        /// <param name="elem"></param>
        public void SetUsage(GridCellUsage usage, FrameworkElement elem = null)
        {
            this.Usage = usage;
            this.Element = elem;
        }
    }
}