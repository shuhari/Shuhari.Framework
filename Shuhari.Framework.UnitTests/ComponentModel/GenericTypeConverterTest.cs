using System;
using System.ComponentModel;
using System.Reflection;
using NUnit.Framework;
using Shuhari.Framework.ComponentModel;

namespace Shuhari.Framework.UnitTests.ComponentModel
{
    [TestFixture]
    public class GenericTypeConverterTest
    {
        [TypeConverter(typeof(GenericTypeConverter<ValidClass>))]
        class ValidClass
        {
            public int Value { get; set; }

            public override string ToString()
            {
                return Value.ToString();
            }

            public static ValidClass Parse(string str)
            {
                return new ValidClass { Value = int.Parse(str) };
            }
        }

        [TypeConverter(typeof(GenericTypeConverter<NoParseClass>))]
        class NoParseClass
        {
        }

        private ValidClass _obj;
        private TypeConverter _converter;

        [SetUp]
        public void SetUp()
        {
            _obj = new ValidClass { Value = 123 };
            _converter = TypeDescriptor.GetConverter(_obj);
        }

        [TestCase(typeof(string), true)]
        [TestCase(typeof(int), false)]
        public void CanConvertFrom(Type type, bool result)
        {
            Assert.AreEqual(result, _converter.CanConvertFrom(type));
        }

        [Test]
        public void ConvertFrom_String()
        {
            var converted = (ValidClass)_converter.ConvertFrom("456");
            Assert.AreEqual(456, converted.Value);
        }

        [Test]
        public void ConvertFrom_NotSupportedValue()
        {
            Assert.Throws<NotSupportedException>(() => _converter.ConvertFrom(123m));
        }

        [TestCase(typeof(string), true)]
        [TestCase(typeof(int), false)]
        public void CanConvertTo(Type type, bool result)
        {
            Assert.AreEqual(result, _converter.CanConvertTo(type));
        }

        [Test]
        public void ConvertTo_String()
        {
            Assert.AreEqual("123", _converter.ConvertTo(_obj, typeof(string)));
        }

        [Test]
        public void ConvertTo_NotSupportedType()
        {
            Assert.Throws<NotSupportedException>(() => _converter.ConvertTo(_obj, typeof(decimal)));
        }

        [Test]
        public void NoParseMethod_ShouldThrow()
        {
            var exp = Assert.Throws<TargetInvocationException>(() => TypeDescriptor.GetConverter(new NoParseClass()));
            Assert.IsInstanceOf<NotSupportedException>(exp.InnerException);
        }
    }
}
