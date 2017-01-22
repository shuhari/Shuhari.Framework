using Shuhari.Framework.Data;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Resources;
using Shuhari.Framework.UnitTests.Data;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests
{
    internal static class Fixtures
    {
        internal static AssemblyResource SitemapResource
        {
            get { return typeof(Fixtures).Assembly.GetResource("ResourceFiles/sitemap.xaml"); }
        }

        internal static Sitemap Sitemap
        {
            get { return Sitemap.FromResource(SitemapResource); }
        }

        internal static ISessionFactory SqlSessionFactory
        {
            get
            {
                var engine = DbRegistry.GetEngine(DatabaseType.SqlServer);
                string connStr = @"Data Source=.;Initial Catalog=tempdb; Integrated Security=SSPI;";
                var sessionFactory = engine.CreateSessionFactory(connStr);
                sessionFactory.MapEntitiesWithAnnonations(typeof(NotNullEntity).Assembly);
                return sessionFactory;
            }
        }
    }
}
