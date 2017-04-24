using Shuhari.Framework.Utils;

namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Common implementation of <see cref="ISitemapResolver"/>
    /// </summary>
    public class SitemapResolver : ISitemapResolver
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="user"></param>
        protected SitemapResolver(UserInfo user)
        {
            Expect.IsNotNull(user, nameof(user));

            User = user;
        }

        /// <summary>
        /// User info
        /// </summary>
        protected UserInfo User { get; }

        /// <summary>
        /// Given a valid url, return resolved url.
        /// Base implementation only return origin value.
        /// Can be overriden by derived classes.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public virtual string ResolveUrl(string url)
        {
            return url;
        }

        /// <inheritdoc />
        public void Resolve(Sitemap sitemap)
        {
            Expect.IsNotNull(sitemap, nameof(sitemap));

            ResolveRecursive(null, sitemap);
        }

        private void ResolveRecursive(SitemapContainer parent, SitemapContainer child)
        {
            Expect.IsNotNull(child, nameof(child));

            // Use bottom-up method
            if (child.Children != null)
                foreach (var grandChild in child.Children.ToArray())
                    ResolveRecursive(child, grandChild);

            // Resolve node self
            var item = child as SitemapItem;
            if (item != null)
            {
                if (item.Url.IsNotBlank())
                    item.Url = ResolveUrl(item.Url);

                if (item.Permission.IsNotBlank() && !User.HasPermission(item.Permission))
                    item.Visible = false;
                if (item.Url.IsBlank() && item.Children.Count == 0)
                    item.Visible = false;

                if (!item.Visible && parent != null)
                    parent.Children.Remove(item);
            }
        }
    }
}
