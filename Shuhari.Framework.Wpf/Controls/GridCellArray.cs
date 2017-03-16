using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Wpf.Controls
{
    /// <summary>
    /// Maintain an array of grid row/columns to calculate layout.
    /// </summary>
    internal class GridCellArray
    {
        public GridCellArray(int rowCount, int columnCount)
        {
            Expect.That(rowCount > 0, "rowCount=" + rowCount);
            Expect.That(columnCount > 0, "columnCount=" + columnCount);

            _columnCount = columnCount;
            _cells = new List<GridCell[]>();
            for (int i = 0; i < rowCount; i++)
            {
                var row = new GridCell[columnCount];
                for (int j = 0; j < columnCount; j++)
                    row[j] = new GridCell(i, j);
                _cells.Add(row);
            }
        }

        private readonly int _columnCount;

        private readonly List<GridCell[]> _cells;

        /// <summary>
        /// Row count
        /// </summary>
        public int RowCount
        {
            get { return _cells.Count; }
        }

        /// <summary>
        /// Place elements
        /// </summary>
        /// <param name="elements"></param>
        public void PlaceElements(IEnumerable<FrameworkElement> elements)
        {
            Expect.IsNotNull(elements, nameof(elements));

            int rowIndex = 0, colIndex = 0;
            foreach (var elem in elements)
            {
                PlaceElement(ref rowIndex, ref colIndex, elem);
            }
            TrimUnusedRows();
        }

        private void PlaceElement(ref int rowIndex, ref int colIndex, FrameworkElement elem)
        {
            Expect.IsNotNull(elem, nameof(elem));
            bool success = false;

            while (true)
            {
                var cell = _cells[rowIndex][colIndex];
                if (cell.Usage == GridCellUsage.None)
                {
                    cell.SetUsage(GridCellUsage.Element, elem);
                    SetCellSpans(rowIndex, colIndex, elem);
                    success = true;
                }
                colIndex++;
                if (colIndex >= _columnCount)
                {
                    rowIndex++;
                    colIndex = 0;
                }
                if (success || rowIndex >= _cells.Count)
                    break;
            }
        }

        private bool IsValidCell(int rowIndex, int colIndex)
        {
            return rowIndex < _cells.Count && colIndex < _columnCount;
        }

        private void SetCellSpans(int rowIndex, int colIndex, FrameworkElement elem)
        {
            int rowSpan = Grid.GetRowSpan(elem);
            int colSpan = Grid.GetColumnSpan(elem);
            for (int i = 0; i < rowSpan; i++)
            {
                int nextRow = rowIndex + i;
                for (int j = 0; j < colSpan; j++)
                {
                    int nextCol = colIndex + j;
                    if ((i > 0 || j > 0) && IsValidCell(nextRow, nextCol))
                        _cells[nextRow][nextCol].SetUsage(GridCellUsage.Span, null);
                }
            }
        }

        /// <summary>
        /// Remove unused rows from end of list
        /// </summary>
        private void TrimUnusedRows()
        {
            while (true)
            {
                var lastRow = _cells.LastOrDefault();
                if (lastRow != null && lastRow.All(x => x.Usage == GridCellUsage.None))
                    _cells.Remove(lastRow);
                else
                    break;
            }
        }

        /// <summary>
        /// Enum all cells
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GridCell> EnumCells()
        {
            return _cells.SelectMany(x => x);
        }
    }
}