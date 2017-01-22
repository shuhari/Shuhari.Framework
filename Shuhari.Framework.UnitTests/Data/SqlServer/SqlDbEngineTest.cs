using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using NUnit.Framework;
using Shuhari.Framework.Data.SqlServer;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Data.SqlServer
{
    [TestFixture]
    public class SqlDbEngineTest
    {
        [SetUp]
        public void SetUp()
        {
            _engine = new SqlDbEngine();
        }

        private SqlDbEngine _engine;

        [Test]
        public void CreateSessionFactory()
        {
            var sessionFactory = Fixtures.SqlSessionFactory;

            Assert.IsNotNull(sessionFactory);
            Assert.IsNotNull(sessionFactory.Engine);
        }

        [Test]
        public void CreateConnection_ShouldBeSqlConnection()
        {
            using (var connection = _engine.CreateConnection())
            {
                Assert.IsInstanceOf<SqlConnection>(connection);
            }
        }

        [TestCase(null)]
        [TestCase("")]
        public void CreateParam_ParamNameInvalid_ShouldThrow(string paramName)
        {
            Assert.Throws<ExpectionException>(() => _engine.CreateParameter(paramName, DbType.Int32, 1));
        }

        [TestCase(DbType.Object)]
        [TestCase(DbType.Time)]
        [TestCase(DbType.Currency)]
        [TestCase(DbType.UInt16)]
        [TestCase(DbType.UInt32)]
        [TestCase(DbType.UInt64)]
        [TestCase(DbType.VarNumeric)]
        [TestCase(DbType.Xml)]
        [TestCase(DbType.SByte)]
        public void CreateParam_NotSupportedType_ShouldThrow(DbType dbType)
        {
            Assert.Throws<NotSupportedException>(() => _engine.CreateParameter("param", dbType, 1));
        }

        [TestCase(DbType.Binary, new byte[0], SqlDbType.VarBinary)]
        [TestCase(DbType.Byte, (byte)0, SqlDbType.TinyInt)]
        [TestCase(DbType.Int16, (short)0, SqlDbType.SmallInt)]
        [TestCase(DbType.Int32, 0, SqlDbType.Int)]
        [TestCase(DbType.Int64, 0L, SqlDbType.BigInt)]
        [TestCase(DbType.Single, 0F, SqlDbType.Real)]
        [TestCase(DbType.Double, 1D, SqlDbType.Float)]
        [TestCase(DbType.String, "abc", SqlDbType.NVarChar)]
        [TestCase(DbType.StringFixedLength, "abc", SqlDbType.NVarChar)]
        [TestCase(DbType.AnsiString, "abc", SqlDbType.VarChar)]
        [TestCase(DbType.AnsiStringFixedLength, "abc", SqlDbType.VarChar)]
        [TestCase(DbType.Boolean, true, SqlDbType.Bit)]
        public void CreateParam_PrimitiveType(DbType dbType, object value, SqlDbType sqlDbType)
        {
            var param = _engine.CreateParameter("param", dbType, value);
            AssertParam(param, "@param", sqlDbType, value);
        }

        [TestCase(DbType.Date)]
        [TestCase(DbType.DateTime)]
        [TestCase(DbType.DateTime2)]
        [TestCase(DbType.DateTimeOffset)]
        public void CreateParam_DateTimeTypes(DbType dbType)
        {
            var value = DateTime.Now;
            var param = _engine.CreateParameter("param", dbType, value);
            AssertParam(param, "@param", SqlDbType.DateTime2, value);
        }

        [Test]
        public void CreateParam_Decimal()
        {
            var value = 123m;
            var param = _engine.CreateParameter("param", DbType.Decimal, value);
            AssertParam(param, "@param", SqlDbType.Decimal, value);
        }

        [Test]
        public void CreateParam_Guid()
        {
            var value = Guid.NewGuid();
            var param = _engine.CreateParameter("param", DbType.Guid, value);
            AssertParam(param, "@param", SqlDbType.UniqueIdentifier, value);
        }

        private void AssertParam(DbParameter param, string name, SqlDbType sqlType, object value)
        {
            var sqlParam = (SqlParameter)param;
            Assert.AreEqual(name, sqlParam.ParameterName);
            Assert.AreEqual(sqlType, sqlParam.SqlDbType);
            Assert.AreEqual(value, sqlParam.Value);
        }

        [TestCase(typeof(FileInfo))]
        public void GetDbTypeName_NotSupported_ShouldThrow(Type clrType)
        {
            Assert.Throws<NotSupportedException>(() => _engine.GetDbTypeName(clrType));
        }

        [TestCase(typeof(int), "int")]
        [TestCase(typeof(long), "bigint")]
        [TestCase(typeof(Guid), "uniqueidentifier")]
        public void GetDbTypeName_Supported(Type clrType, string typeName)
        {
            Assert.AreEqual(typeName, _engine.GetDbTypeName(clrType));
        }
    }
}
