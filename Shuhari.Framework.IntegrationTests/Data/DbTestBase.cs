using Shuhari.Framework.Data;

namespace Shuhari.Framework.IntegrationTests.Data
{
    public class DbTestBase : Shuhari.Framework.Data.DbTestBase
    {
        public DbTestBase(bool readOnly) 
            : base(readOnly)
        {
        }

        protected internal override void CreateDatabase()
        {
            DbFixtures.CreateDatabase();
        }

        protected internal override void DropDatabase()
        {
            DbFixtures.DropDatabase();
        }

        protected internal override ISession OpenSession()
        {
            return DbFixtures.OpenSession();
        }
    }
}
