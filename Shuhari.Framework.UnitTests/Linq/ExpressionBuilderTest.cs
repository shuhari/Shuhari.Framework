using System;
using System.Reflection;
using NUnit.Framework;
using Shuhari.Framework.Linq;

namespace Shuhari.Framework.UnitTests.Linq
{
    [TestFixture]
    public class ExpressionBuilderTest
    {
        class TestEntity
        {
            public int IntProp { get; set; }
            public int? NullableIntProp { get; set; }
            public string StringProp { get; set; }
        }

        private PropertyInfo GetProp(string propName)
        {
            return typeof(TestEntity).GetProperty(propName);
        }

        [Test]
        public void GetProperty_NotPropertyExpression_Throw()
        {
            Assert.Throws<ArgumentException>(() => ExpressionBuilder.GetProperty<int, string>(x => x.ToString()));
        }

        [Test]
        public void GetProperty_IsPropertyExpression_Pass()
        {
            Assert.AreEqual("Length", ExpressionBuilder.GetProperty<string, int>(x => x.Length).Name);
        }

        [Test]
        public void GetProperty_IsPropertyExpressionWrapped_Pass()
        {
            Assert.AreEqual("Length", ExpressionBuilder.GetProperty<string, object>(x => x.Length).Name);
        }

        [TestCase("IntProp", 123)]
        [TestCase("StringProp", "abc")]
        public void BuildSetter(string propName, object value)
        {
            var entity = new TestEntity();
            var prop = typeof(TestEntity).GetProperty(propName);
            var setter = ExpressionBuilder.BuildSetter(prop);
            setter.DynamicInvoke(entity, value);

            Assert.AreEqual(value, prop.GetValue(entity));
        }

        [TestCase("IntProp", 123)]
        [TestCase("StringProp", "abc")]
        public void BuildGetter(string propName, object value)
        {
            var entity = new TestEntity();
            var prop = typeof(TestEntity).GetProperty(propName);
            prop.SetValue(entity, value);
            var getter = ExpressionBuilder.BuildGetter(prop);
            var getterValue = getter.DynamicInvoke(entity);

            Assert.AreEqual(value, getterValue);
        }
    }
}
