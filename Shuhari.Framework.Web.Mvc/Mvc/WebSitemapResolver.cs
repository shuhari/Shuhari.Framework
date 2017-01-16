using System.Web;
using System.Web.Mvc;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Web.Mvc
{
    /// <summary>
    /// Sitemap resolver in web context
    /// </summary>
    public class WebSitemapResolver : BaseSitemapResolver
    {
        /// <summary>
        /// Initializeo
        /// </summary>
        /// <param name="context"></param>
        /// <param name="user"></param>
        public WebSitemapResolver(HttpContextBase context, UserInfo user)
            : base(user)
        {
            Expect.IsNotNull(context, nameof(context));

            _context = context;
        }

        private HttpContextBase _context;

        /// <inheritdoc />
        public override string ResolveUrl(string url)
        {
            return UrlHelper.GenerateContentUrl(url, _context);
        }
    }
}
