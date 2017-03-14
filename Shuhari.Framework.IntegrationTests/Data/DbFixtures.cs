using System;
using System.Configuration;
using System.IO;
using Shuhari.Framework.Data;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.Data.SqlServer;
using Shuhari.Framework.IO;
using Shuhari.Framework.UnitTests.Data;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.IntegrationTests.Data
{
    internal static class DbFixtures
    {
        /// <summary>
        /// Create testbase
        /// </summary>
        public static void CreateDatabase(DbScriptExecuteOptions options)
        {
            ExecuteResourceScript(options, "Data/Scripts/create.sql");
        }

        public static void DropDatabase(DbScriptExecuteOptions options)
        {
            ExecuteResourceScript(options, "Data/Scripts/drop.sql");
        }

        private static void ExecuteResourceScript(DbScriptExecuteOptions options, string scriptPath)
        {
            string filePath = Path.Combine(options.WorkDirectory, Path.GetFileName(scriptPath));
            typeof(DbFixtures).Assembly.GetResource(scriptPath).CopyToFile(filePath);

            var response = new CommandLine("sqlcmd")
                .SetWorkingDirectory(options.WorkDirectory)
                .AddArg("-i")
                .AddArg(Path.GetFileName(scriptPath))
                .AddArg("-v")
                .AddArg(string.Format("db=\"{0}\"", options.DbName))
                .Exec();
            if (options.Output)
                Console.WriteLine(response);
        }

        public static ISession OpenSession()
        {
            string connStr = ConfigurationManager.ConnectionStrings["testdb"].ConnectionString;
            var engine = DbRegistry.GetEngine(DatabaseType.SqlServer);
            var sessionFactory = engine.CreateSessionFactory(connStr);
            sessionFactory.MapEntitiesWithAnnonations(typeof(NotNullEntity).Assembly);
            return sessionFactory.OpenSession();
        }
    }
}
