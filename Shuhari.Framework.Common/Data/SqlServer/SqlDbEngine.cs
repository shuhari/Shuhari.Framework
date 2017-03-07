using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Shuhari.Framework.Data.Common;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.SqlServer
{
    /// <summary>
    /// SQL Sever implementation of DbEngine
    /// </summary>
    internal class SqlDbEngine : DbEngine
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public SqlDbEngine()
        {
            _knownDbTypes = new Dictionary<DbType, SqlDbType>
            {
                { DbType.Binary, SqlDbType.VarBinary },
                { DbType.Byte, SqlDbType.TinyInt },
                { DbType.Int16, SqlDbType.SmallInt },
                { DbType.Int32, SqlDbType.Int },
                { DbType.Int64, SqlDbType.BigInt },
                { DbType.Single, SqlDbType.Real },
                { DbType.Double, SqlDbType.Float },
                { DbType.String, SqlDbType.NVarChar },
                { DbType.StringFixedLength, SqlDbType.NVarChar },
                { DbType.AnsiString, SqlDbType.VarChar },
                { DbType.AnsiStringFixedLength, SqlDbType.VarChar },
                { DbType.Boolean, SqlDbType.Bit },
                { DbType.Date, SqlDbType.DateTime2 },
                { DbType.DateTime, SqlDbType.DateTime2 },
                { DbType.DateTime2, SqlDbType.DateTime2 },
                { DbType.DateTimeOffset, SqlDbType.DateTime2 },
                { DbType.Decimal, SqlDbType.Decimal },
                { DbType.Guid, SqlDbType.UniqueIdentifier },
            };
        }

        private Dictionary<DbType, SqlDbType> _knownDbTypes;

        /// <inheritdoc />
        public override IDbConnection CreateConnection()
        {
            return new SqlConnection();
        }

        /// <inheritdoc />
        public override string GetDbTypeName(Type clrType)
        {
            Expect.IsNotNull(clrType, nameof(clrType));

            if (clrType == typeof(int))
                return "int";
            else if (clrType == typeof(long))
                return "bigint";
            else if (clrType == typeof(Guid))
                return "uniqueidentifier";
            else
                return base.GetDbTypeName(clrType);
        }

        /// <inheritdoc />
        protected override DbParameter CreateVendorParameter(string paramName, DbType dbType, object value)
        {
            var sqlType = GetSqlDbType(dbType);
            var param = new SqlParameter(paramName, sqlType);
            param.Value = value;
            return param;
        }

        public SqlDbType GetSqlDbType(DbType dbType)
        {
            if (_knownDbTypes.ContainsKey(dbType))
                return _knownDbTypes[dbType];
            throw ExceptionBuilder.NotSupported(FrameworkStrings.ErrorUnsupportedType, dbType);
        }

        /// <inheritdoc />
        public override IQueryBuilder<T> CreateQueryBuilder<T>(IEntityMapper<T> mapper)
        {
            Expect.IsNotNull(mapper, nameof(mapper));
            return new SqlQueryBuilder<T>(this, mapper);
        }
    }
}
