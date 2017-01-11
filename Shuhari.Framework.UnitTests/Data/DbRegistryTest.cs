using System;
using NUnit.Framework;
using Shuhari.Framework.Data;

namespace Shuhari.Framework.UnitTests.Data
{
    [TestFixture]
    public class DbRegistryTest
    {
        [Test]
        public void GetEngine_UnsupportedType_Throw()
        {
            Assert.Throws<NotSupportedException>(() => DbRegistry.GetEngine(DatabaseType.Unknown));
        }

        [TestCase(DatabaseType.SqlServer)]
        public void GetEngine_SupportedType_ShouldReturnEngine(DatabaseType type)
        {
            var engine = DbRegistry.GetEngine(type);
            Assert.IsNotNull(engine);
        }
    }
}
