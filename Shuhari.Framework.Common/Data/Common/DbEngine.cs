using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Shuhari.Framework.Data.Utils;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Resources;
using Shuhari.Framework.Text;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Common
{
    /// <summary>
    /// Common implementation of <see cref="IDbEngine"/>
    /// </summary>
    public abstract class DbEngine : IDbEngine
    {
        private static Dictionary<Type, DbType> _knownDbTypes = new Dictionary<Type, DbType>
        {
            { typeof(byte), DbType.Byte },
            { typeof(short), DbType.Int16 },
            { typeof(int), DbType.Int32 },
            { typeof(long), DbType.Int64 },
            { typeof(bool), DbType.Boolean },
            { typeof(float), DbType.Single },
            { typeof(double), DbType.Double },
            { typeof(decimal), DbType.Decimal },
            { typeof(string), DbType.String },
            { typeof(DateTime), DbType.DateTime2 },
            { typeof(byte[]), DbType.Binary },
            { typeof(Guid), DbType.Guid },
        };

        /// <inheritdoc />
        public abstract IDbConnection CreateConnection();

        /// <inheritdoc />
        public ISessionFactory CreateSessionFactory(string connectionString)
        {
            Expect.IsNotBlank(connectionString, nameof(connectionString));

            return new SessionFactory(this, connectionString);
        }

        /// <inheritdoc />
        public virtual DbType GetDbType(Type clrType)
        {
            Expect.IsNotNull(clrType, nameof(clrType));

            if (_knownDbTypes.ContainsKey(clrType))
                return _knownDbTypes[clrType];
            else if (clrType.IsEnum)
                return DbType.Int32;
            else if (clrType.IsNullableType())
                return GetDbType(clrType.GetNullableBaseType());
            
            throw ExceptionBuilder.NotSupported(FrameworkStrings.ErrorUnsupportedType, clrType.FullName);
        }

        /// <inheritdoc />
        public virtual string GetDbTypeName(Type clrType)
        {
            Expect.IsNotNull(clrType, nameof(clrType));

            throw ExceptionBuilder.NotSupported(FrameworkStrings.ErrorUnsupportedType, clrType.FullName);
        }

        /// <inheritdoc />
        public DbParameter CreateParameter(string paramName, DbType dbType, object value)
        {
            Expect.IsNotBlank(paramName, nameof(paramName));

            paramName = NormalizeParamName(paramName);
            var paramValue = ParamConverter.ToDbValue(value);
            return CreateVendorParameter(paramName, dbType, paramValue);
        }

        /// <summary>
        /// Normalize parameter name. Most database support @name as parameter format.
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        protected virtual string NormalizeParamName(string paramName)
        {
            Expect.IsNotBlank(paramName, nameof(paramName));

            return paramName.StartsWith("@") ? paramName : "@" + paramName;
        }

        /// <summary>
        /// Create vendor-specified parameter
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected abstract DbParameter CreateVendorParameter(string paramName, DbType dbType, object value);

        /// <inheritdoc />
        public abstract string ExecuteCommand(DbManagementCommandOptions options);

        /// <inheritdoc />
        public string ExecuteResourceScript(AssemblyResource resource, DbManagementCommandOptions options = null,
            StringReplacer replacer = null)
        {
            Expect.IsNotNull(resource, nameof(resource));
            options = options ?? DbManagementCommandOptions.GetDefault();

            string filePath = resource.CopyToBaseDirectory();
            if (replacer != null)
                replacer.ApplyToFile(filePath, options.FileEncoding);

            options.FileName = filePath;
            return ExecuteCommand(options);
        }

        /// <summary>
        /// Helper method to execute database management command and return standard output.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="workDir"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected internal string ShellExec(string cmd, string workDir, IEnumerable<string> args)
        {
            Expect.IsNotBlank(cmd, nameof(cmd));
            Expect.IsNotNull(args, nameof(args));

            var psi = new ProcessStartInfo()
            {
                FileName = cmd,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                Arguments = string.Join(" ", args)
            };
            if (workDir.IsNotBlank())
                psi.WorkingDirectory = workDir;
            using (var process = Process.Start(psi))
            {
                process.WaitForExit();
                return process.StandardOutput.ReadToEnd();
            }
        }

        /// <inheritdoc />
        public abstract IQueryBuilder<T> CreateQueryBuilder<T>()
            where T : class, new();
    }
}
