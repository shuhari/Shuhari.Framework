using NUnit.Framework;
using Shuhari.Framework.Data;

namespace Shuhari.Framework.UnitTests.Data
{
    [TestFixture]
    public class CritiaListTest
    {
        [SetUp]
        public void SetUp()
        {
            _list = new CritiaList();
        }

        private CritiaList _list;

        [Test]
        public void NoCritia_ShouldToEmptyClause()
        {
            Assert.IsEmpty(_list.ToString());
        }

        [Test]
        public void AddOne_ShouldSetClause()
        {
            _list.Add("Name=@Name");

            Assert.AreEqual("where (Name=@Name)", _list.ToString());
        }

        [TestCase(true, "where (Name=@Name) and (Age=@Age)")]
        [TestCase(false, "where (Name=@Name)")]
        public void AddIf(bool predicate, string result)
        {
            _list.Add("Name=@Name")
                .AddIf(predicate, "Age=@Age");

            Assert.AreEqual(result, _list.ToString());
        }
    }
}
