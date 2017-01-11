using Shuhari.Framework.Data;
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

        internal static ISessionFactory SqlSessionFactory
        {
            get
            {
                var engine = DbRegistry.GetEngine(DatabaseType.SqlServer);
                string connStr = @"Data Source=.;Initial Catalog=tempdb; Integrated Security=SSPI;";
                return engine.CreateSessionFactory(connStr);
            }
        }
    }
}
