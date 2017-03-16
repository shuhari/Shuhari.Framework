namespace Shuhari.Framework.Wpf.Controls
{
    /// <summary>
    /// How grid cell is used
    /// </summary>
    enum GridCellUsage
    {
        /// <summary>
        /// Not used
        /// </summary>
        None = 0,

        /// <summary>
        /// Element placed in cell
        /// </summary>
        Element = 1,

        /// <summary>
        /// Not element in it, but still occupied because of RowSpan/ColumnSpan
        /// </summary>
        Span = 2,
    }
}