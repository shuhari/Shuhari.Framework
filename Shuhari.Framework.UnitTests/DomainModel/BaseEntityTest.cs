using System.Collections.Generic;
using NUnit.Framework;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.UnitTests.DomainModel
{
    [TestFixture]
    public class BaseEntityTest
    {
        [SetUp]
        public void SetUp()
        {
            _entity = new BaseEntity<int> { Id = 1 };
        }

        private BaseEntity<int> _entity;

        [Test]
        public void GetProperty_NotSet_ShouldThrow()
        {
            Assert.Throws<KeyNotFoundException>(() => { _entity["Name"].ToString(); });
        }

        [Test]
        public void SetProperty_GetProperty()
        {
            string value = "abc";
            _entity["prop"] = value;

            Assert.AreEqual(value, _entity["prop"]);
            Assert.AreEqual(value, _entity["Prop"]);
        }

        [TestCase("prop", true)]
        [TestCase("Prop", true)]
        [TestCase("PROP", true)]
        [TestCase("NotProp", false)]
        public void HasProperty(string propName, bool result)
        {
            _entity["prop"] = "abc";

            Assert.AreEqual(result, _entity.HasProperty(propName));
        }
    }
}
