﻿using System;
using System.Configuration;
using Shuhari.Framework.Data;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.Data.SqlServer;
using Shuhari.Framework.Text;
using Shuhari.Framework.UnitTests.Data;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.IntegrationTests.Data
{
    internal static class DbFixtures
    {
        /// <summary>
        /// Create testbase
        /// </summary>
        public static void CreateDatabase(StringReplacer replacer = null, bool showOutput = false)
        {
            ExecuteResourceScript("Data/Scripts/create.sql", replacer, showOutput);
        }

        public static void DropDatabase(bool showOutput = false)
        {
            ExecuteResourceScript("Data/Scripts/drop.sql", null, showOutput);
        }

        private static void ExecuteResourceScript(string scriptPath, StringReplacer replacer, bool showOutput)
        {
            var engine = new SqlDbEngine();
            var resource = typeof(DbFixtures).Assembly.GetResource(scriptPath);
            var output = engine.ExecuteResourceScript(resource, null, replacer);
            if (showOutput)
                Console.WriteLine(output);
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