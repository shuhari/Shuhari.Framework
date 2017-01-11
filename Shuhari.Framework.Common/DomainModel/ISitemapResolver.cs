namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Resolve sitemap, include:
    /// <ul>
    ///   <li>Resolve url to application/site relative url;</li>
    ///   <li>Truncate node with no permissions;</li>
    /// </ul>
    /// </summary>
    public interface ISitemapResolver
    {
        /// <summary>
        /// Resolve sitemap
        /// </summary>
        /// <param name="sitemap"></param>
        void Resolve(Sitemap sitemap);
    }
}
