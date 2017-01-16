using NUnit.Framework;
using Shuhari.Framework.Data;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Data
{
    [TestFixture]
    public class ScriptBookTest
    {
        private ScriptBook _book;

        [SetUp]
        public void SetUp()
        {
            _book = new ScriptBook();
            _book.LoadResource(GetType().Assembly.GetResource("ResourceFiles/scriptBook.sql"));
        }

        [Test]
        public void GetSql_SingleLine()
        {
            var sql = _book["sql1"];

            Assert.IsNotNull(sql);
            StringAssert.Contains("select * from users", sql);
        }

        [Test]
        public void GetSql_MultiLine()
        {
            var sql = _book["sql2"];

            Assert.IsNotNull(sql);
            StringAssert.DoesNotContain("select * from users", sql);
            StringAssert.Contains("insert into users (id, name)", sql);
            StringAssert.Contains("values (1, 'guest')", sql);
        }

    }
}
