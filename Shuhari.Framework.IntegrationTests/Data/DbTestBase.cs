using System.IO;
using Shuhari.Framework.Data;

namespace Shuhari.Framework.IntegrationTests.Data
{
    public class DbTestBase : Shuhari.Framework.Data.DbTestBase
    {
        protected DbTestBase(bool readOnly)
            : base(readOnly)
        {
        }

        private DbScriptExecuteOptions Options
        {
            get
            {
                var workDir = Path.GetDirectoryName(GetType().Assembly.Location);
                return new DbScriptExecuteOptions("Shuhari_Framework_TestDb", workDir, false);
            }
        }

        protected internal override void CreateDatabase()
        {
            DbFixtures.CreateDatabase(Options);
        }

        protected internal override void DropDatabase()
        {
            DbFixtures.DropDatabase(Options);
        }

        protected internal override ISession OpenSession()
        {
            return DbFixtures.OpenSession();
        }
    }
}
