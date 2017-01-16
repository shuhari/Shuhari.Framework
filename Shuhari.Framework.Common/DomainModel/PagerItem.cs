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
        /// <param name="navigate"></param>
        public PagerItem(int page, string displayName, bool navigate)
        {
            this.Page = page;
            this.DisplayName = displayName;
            this.Navigate = navigate;
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
        /// If the page is different than current (thus can navigate to)
        /// </summary>
        public bool Navigate { get; set; }
    }
}
