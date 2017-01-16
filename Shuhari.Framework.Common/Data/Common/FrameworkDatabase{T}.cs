using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.Resources;
using Shuhari.Framework.Text;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Common
{
    /// <summary>
    /// Define applilcation level database
    /// </summary>
    public abstract class FrameworkDatabase<TContext>
        where TContext : BaseDbContext
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connectionName"></param>
        /// <param name="entityAssembly">Assembly that contains entity definitions</param>
        /// <param name="repositoryAssembly">Assembly that contains repository implementations</param>
        public FrameworkDatabase(DatabaseType dbType, string connectionName, Assembly entityAssembly,
            Assembly repositoryAssembly)
        {
            Expect.IsNotBlank(connectionName, nameof(connectionName));
            Expect.IsNotNull(entityAssembly, nameof(entityAssembly));
            Expect.IsNotNull(repositoryAssembly, nameof(repositoryAssembly));

            var config = ConfigurationManager.ConnectionStrings[connectionName];
            if (config == null || config.ConnectionString.IsBlank())
                throw new ConfigurationErrorsException("Connection string not configurated: " + connectionName);
            ConnectionString = config.ConnectionString;
            Engine = DbRegistry.GetEngine(dbType);

            SessionFactory = Engine.CreateSessionFactory(ConnectionString);
            MappingFactory.MapEntitiesWithAnnonations(SessionFactory, entityAssembly);

            _repositoryImpls = new Dictionary<PropertyInfo, Type>();
            RegisterRepositoryTypes(repositoryAssembly);
        }

        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Db engine
        /// </summary>
        public IDbEngine Engine { get; private set; }

        /// <summary>
        /// Session factory
        /// </summary>
        public ISessionFactory SessionFactory { get; private set; }

        private Dictionary<PropertyInfo, Type> _repositoryImpls;

        /// <summary>
        /// Lookup <typeparamref name="TContext"/> for repository properties,
        /// and find implemention class in <paramref name="assembly"/>
        /// </summary>
        /// <param name="assembly"></param>
        private void RegisterRepositoryTypes(Assembly assembly)
        {
            Expect.IsNotNull(assembly, nameof(assembly));

            var reposIntfs = typeof(TContext).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(t => typeof(IRepository).IsAssignableFrom(t.PropertyType))
                .ToList();
            foreach (var type in assembly.GetExportedTypes())
            foreach (var reposIntf in reposIntfs.ToArray())
            {
                if (type.IsClass && !type.IsAbstract && reposIntf.PropertyType.IsAssignableFrom(type))
                {
                    _repositoryImpls[reposIntf] = type;
                    reposIntfs.Remove(reposIntf);
                }
            }

            if (reposIntfs.Count > 0)
                throw new ConfigurationErrorsException(string.Format("Following repositories implementation not found: {0}",
                    string.Join(", ", reposIntfs.Select(x => x.PropertyType))));
        }

        /// <summary>
        /// Create database context
        /// </summary>
        /// <returns></returns>
        public TContext CreateDbContext()
        {
            var context = (TContext)Activator.CreateInstance(typeof(TContext), new object[] { SessionFactory });
            foreach (var kv in _repositoryImpls)
            {
                var repos = Activator.CreateInstance(kv.Value);
                kv.Key.SetValue(context, repos);
            }
            return context;
        }

        /// <summary>
        /// 脚本嵌入程序集资源
        /// </summary>
        /// <param name="scriptPath"></param>
        /// <returns></returns>
        public AssemblyResource GetScriptResource(string scriptPath)
        {
            Expect.IsNotBlank(scriptPath, nameof(scriptPath));
            return GetType().Assembly.GetResource(scriptPath);
        }

        /// <summary>
        /// 测试用，将资源脚本拷贝到应用程序根目录
        /// </summary>
        /// <param name="scriptPath"></param>
        /// <returns></returns>
        public string CopyScriptResourceToBaseDir(string scriptPath)
        {
            Expect.IsNotBlank(scriptPath, nameof(scriptPath));

            var resource = GetScriptResource(scriptPath);
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileName(scriptPath));
            resource.CopyToFile(filePath);
            return filePath;
        }

        /// <summary>
        /// Execute resource script
        /// </summary>
        /// <param name="scriptPath"></param>
        /// <param name="options"></param>
        /// <param name="replacer">Optional. Replace string in script before execute, for example changed database name</param>
        /// <returns></returns>
        public string ExecuteScriptResource(string scriptPath, DbManagementCommandOptions options,
            StringReplacer replacer = null)
        {
            Expect.IsNotBlank(scriptPath, nameof(scriptPath));

            var filePath = CopyScriptResourceToBaseDir(scriptPath);
            if (replacer != null)
                replacer.ApplyToFile(filePath);

            options = options ?? DbManagementCommandOptions.GetDefault();
            options.FileName = filePath;
            return Engine.ExecuteCommand(options);
        }
    }
}