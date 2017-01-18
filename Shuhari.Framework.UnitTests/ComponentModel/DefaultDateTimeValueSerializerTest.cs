using System;
using NUnit.Framework;
using Shuhari.Framework.ComponentModel;

namespace Shuhari.Framework.UnitTests.ComponentModel
{
    [TestFixture]
    public class DefaultDateTimeValueSerializerTest
    {
        [SetUp]
        public void SetUp()
        {
            _serializer = new DefaultDateTimeValueSerializer();
        }

        private DefaultDateTimeValueSerializer _serializer;
        private const string DATE_STER = "2016-12-31 01:02:03";
        private DateTime _dateValue = new DateTime(2016, 12, 31, 1, 2, 3);

        [TestCase("invalid_date", false)]
        [TestCase(DATE_STER, true)]
        public void CanConvertFromString_InvalidFormat_ShouldReturnFalse(string value, bool result)
        {
            Assert.AreEqual(result, _serializer.CanConvertFromString(value, null));
        }

        [Test]
        public void ConvertFromString()
        {
            Assert.AreEqual(_dateValue, _serializer.ConvertFromString(DATE_STER, null));
        }

        [Test]
        public void ConvertFromString_InvalidFormat_ShouldUseBase()
        {
            Assert.Throws<NotSupportedException>(() => _serializer.ConvertFromString("abcd", null));
        }

        [Test]
        public void CanConvertToString_DateTime_ShouldSuccess()
        {
            Assert.IsTrue(_serializer.CanConvertToString(DateTime.Now, null));
        }

        [Test]
        public void CanConvertToString_InvalidType_ShouldNo()
        {
            Assert.IsFalse(_serializer.CanConvertToString(123, null));
        }

        [Test]
        public void ConvertToString()
        {
            Assert.AreEqual(DATE_STER, _serializer.ConvertToString(_dateValue, null));
        }

        [Test]
        public void ConvertToString_InvalidValue_ShouldUseBase()
        {
            Assert.Throws<NotSupportedException>(() => _serializer.ConvertToString(123m, null));
        }
    }
}
