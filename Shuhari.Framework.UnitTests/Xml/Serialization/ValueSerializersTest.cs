using System;
using NUnit.Framework;
using Shuhari.Framework.Xml.Serialization;

namespace Shuhari.Framework.UnitTests.Xml.Serialization
{
    [TestFixture]
    public class ValueSerializersTest
    {
        [TestCase(true, "true")]
        [TestCase(false, "false")]
        public void BoolSerializer(bool value, string serialized)
        {
            var serializer = new BoolValueSerializer();
            Assert.AreEqual(serialized, serializer.Serialize(value));
            Assert.AreEqual(value, serializer.Deserialize(serializer.Serialize(value)));
        }

        [TestCase(null, "2016-1-1", "2016-01-01 00:00:00")]
        [TestCase("yy-MM-dd", "2016-1-1", "16-01-01")]
        public void DateTimeSerializer(string format, string valueStr, string serialized)
        {
            var serializer = new DateTimeValueSerializer(format);
            var value = DateTime.Parse(valueStr);
            Assert.AreEqual(serialized, serializer.Serialize(value));
            Assert.AreEqual(value, serializer.Deserialize(serializer.Serialize(value)));
        }
    }
}
