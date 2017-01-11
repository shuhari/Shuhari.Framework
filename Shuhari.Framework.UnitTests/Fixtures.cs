using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests
{
    internal static class Fixtures
    {
        internal static Sitemap ResourceSitemap
        {
            get { return Sitemap.FromResource(typeof(Fixtures).Assembly.GetResource("ResourceFiles/sitemap.xaml")); }
        }
    }
}
