namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Abstract pagination info
    /// </summary>
    public interface IPagination
    {
        /// <summary>
        /// Current page
        /// </summary>
        int Page { get; set; }

        /// <summary>
        /// Records per page
        /// </summary>
        int PerPage { get; set; }

        /// <summary>
        /// Total record count
        /// </summary>
        int Total { get; set; }

        /// <summary>
        /// Calculate pager result
        /// </summary>
        /// <returns></returns>
        Pager CalculatePager();
    }
}
