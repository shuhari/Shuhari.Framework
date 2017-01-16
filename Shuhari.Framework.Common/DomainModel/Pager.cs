using System.Collections.Generic;

namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Pagination information for result
    /// </summary>
    public class Pager
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public Pager()
        {
            Items = new PagerItem[0];
        }

        /// <summary>
        /// Total count
        /// </summary>
        public int Total { get; internal set; }

        /// <summary>
        /// Start record index
        /// </summary>
        public int StartIndex { get; internal set; }

        /// <summary>
        /// End record index
        /// </summary>
        public int EndIndex { get; internal set; }

        /// <summary>
        /// Items
        /// </summary>
        public IEnumerable<PagerItem> Items { get; internal set; }
    }
}
