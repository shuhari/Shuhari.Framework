using System;
using System.Data;
using System.Data.Common;
using System.IO;
using NUnit.Framework;
using Shuhari.Framework.Data.Common;

namespace Shuhari.Framework.UnitTests.Data.Common
{
    [TestFixture]
    public class DbEngineTest
    {
        class TestEngine : DbEngine
        {
            public override IDbConnection CreateConnection()
            {
                throw new NotImplementedException();
            }

            protected override DbParameter CreateVendorParameter(string paramName, DbType dbType, object value)
            {
                throw new NotImplementedException();
            }
        }

        [SetUp]
        public void SetUp()
        {
            _engine = new TestEngine();
        }

        private  DbEngine _engine;

        [TestCase(typeof(sbyte))]
        [TestCase(typeof(Stream))]
        public void GetDbType_ClrTypeNotSupported_ShouldThrow(Type clrType)
        {
            Assert.Throws<NotSupportedException>(() => _engine.GetDbType(clrType));
        }

        [TestCase(typeof(byte), DbType.Byte)]
        [TestCase(typeof(short), DbType.Int16)]
        [TestCase(typeof(int), DbType.Int32)]
        [TestCase(typeof(long), DbType.Int64)]
        [TestCase(typeof(bool), DbType.Boolean)]
        [TestCase(typeof(float), DbType.Single)]
        [TestCase(typeof(double), DbType.Double)]
        [TestCase(typeof(decimal), DbType.Decimal)]
        [TestCase(typeof(string), DbType.String)]
        [TestCase(typeof(DateTime), DbType.DateTime2)]
        [TestCase(typeof(byte[]), DbType.Binary)]
        [TestCase(typeof(Guid), DbType.Guid)]
        public void GetDbType_PrimitiveClrTypes_ShouldReturnDbType(Type clrType, DbType dbType)
        {
            Assert.AreEqual(dbType, _engine.GetDbType(clrType));
        }

        [TestCase(typeof(byte?), DbType.Byte)]
        [TestCase(typeof(short?), DbType.Int16)]
        [TestCase(typeof(int?), DbType.Int32)]
        [TestCase(typeof(long?), DbType.Int64)]
        [TestCase(typeof(bool?), DbType.Boolean)]
        [TestCase(typeof(float?), DbType.Single)]
        [TestCase(typeof(double?), DbType.Double)]
        [TestCase(typeof(decimal?), DbType.Decimal)]
        [TestCase(typeof(DateTime?), DbType.DateTime2)]
        [TestCase(typeof(Guid?), DbType.Guid)]
        public void GetDbType_NullableClrTypes_ShouldReturnDbType(Type clrType, DbType dbType)
        {
            Assert.AreEqual(dbType, _engine.GetDbType(clrType));
        }

        [TestCase(typeof(FileMode))]
        [TestCase(typeof(FileMode?))]
        public void GetDbType_EnumTypes_ShouldReturnInt(Type clrType)
        {
            Assert.AreEqual(DbType.Int32, _engine.GetDbType(clrType));
        }
    }
}
