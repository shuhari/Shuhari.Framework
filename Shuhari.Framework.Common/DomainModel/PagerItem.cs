namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Pagination item
    /// </summary>
    public class PagerItem
    {
        /// <summary>
        /// Default initialize
        /// </summary>
        public PagerItem()
        {
        }

        /// <summary>
        /// Initialize with properites
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayName"></param>
        /// <param name="isCurrent"></param>
        /// <param name="disabled"></param>
        public PagerItem(int page, string displayName, bool isCurrent, bool disabled)
        {
            this.Page = page;
            this.DisplayName = displayName;
            this.IsCurrent = isCurrent;
            this.Disabled = disabled;
        }

        /// <summary>
        /// Page index
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Display page
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// If is current page
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// Flag for placeholders which is not navigatable
        /// </summary>
        public bool Disabled { get; set; }
    }
}
