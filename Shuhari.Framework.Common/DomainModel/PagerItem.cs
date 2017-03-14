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
            Page = page;
            DisplayName = displayName;
            IsCurrent = isCurrent;
            Disabled = disabled;
        }

        /// <summary>
        /// Page index
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// Display page
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// If is current page
        /// </summary>
        public bool IsCurrent { get; }

        /// <summary>
        /// Flag for placeholders which is not navigatable
        /// </summary>
        public bool Disabled { get; }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var other = obj as PagerItem;
            if (other == null)
                return false;

            return Page == other.Page &&
                DisplayName == other.DisplayName &&
                IsCurrent == other.IsCurrent &&
                Disabled == other.Disabled;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Page.GetHashCode();
        }
    }
}
