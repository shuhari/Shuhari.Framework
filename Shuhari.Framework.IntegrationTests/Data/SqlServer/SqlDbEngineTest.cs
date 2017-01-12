﻿using System;
using System.Configuration;
using System.Data.SqlClient;
using NUnit.Framework;
using Shuhari.Framework.Data.SqlServer;
using Shuhari.Framework.Text;

namespace Shuhari.Framework.IntegrationTests.Data.SqlServer
{
    [TestFixture]
    public class SqlDbEngineTest
    {
        [SetUp]
        public void SetUp()
        {
            _engine = new SqlDbEngine();
        }

        private SqlDbEngine _engine;

        [Test]
        [Ignore("Manual inspect create output")]
        public void ExecResourceScript_Create()
        {
            DbFixtures.CreateDatabase(null, true);

            AssertDbExist("Shuhari_Framework_TestDb", true);
        }

        [Test]
        [Ignore("Manual inspect create output for test")]
        public void ExecResourceScript_Create_ReplaceDbName()
        {
            DbFixtures.CreateDatabase(new StringReplacer("Shuhari_Framework_TestDb", "Shuhari_Framework_OtherDb"), true);

            AssertDbExist("Shuhari_Framework_OtherDb", true);
        }

        [Test]
        [Ignore("Manual inspect drop output")]
        public void ExecuteResourceScript_Drop()
        {
            DbFixtures.DropDatabase(true);

            AssertDbExist("Shuhari_Framework_TestDb", false);
        }

        private void AssertDbExist(string dbName, bool exist)
        {
            int count = 0;

            string connStr = ConfigurationManager.ConnectionStrings["tempdb"].ConnectionString;
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = string.Format(@"select count(*) from sys.databases where name='{0}'", dbName);
                using (var cmd = new SqlCommand(sql, conn))
                {
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            Assert.AreEqual(exist, count == 1, string.Format("Assert database {0} exist={1} but failed", dbName, exist));
        }
    }
}