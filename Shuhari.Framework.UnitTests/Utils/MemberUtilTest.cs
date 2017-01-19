using System;
using NUnit.Framework;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Utils
{
    [TestFixture]
    public class MemberUtilTest
    {
        [TestCase(typeof(TestFixtureAttribute), true)]
        [TestCase(typeof(TestAttribute), false)]
        public void HasAttribute(Type attrType, bool result)
        {
            Assert.AreEqual(result, GetType().HasAttribute(attrType));
        }

        [Test]
        public void HasAttributeOfT()
        {
            Assert.IsTrue(GetType().HasAttribute<TestFixtureAttribute>());
            Assert.IsFalse(GetType().HasAttribute<TestAttribute>());
        }
    }
}
