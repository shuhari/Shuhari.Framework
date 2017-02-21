using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Common
{
    /// <summary>
    /// Define applilcation level database
    /// </summary>
    public abstract class FrameworkDatabase<TContext>
        where TContext : FrameworkDbContext
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

            _connectionName = connectionName;
            _entityAssembly = entityAssembly;
            _repositoryAssembly = repositoryAssembly;
            _repositoryImpls = new Dictionary<PropertyInfo, Type>();

            Engine = DbRegistry.GetEngine(dbType);
        }


        private readonly string _connectionName;

        private readonly Assembly _entityAssembly;

        private readonly Assembly _repositoryAssembly;

        private ISessionFactory _sessionFactory;

        private Dictionary<PropertyInfo, Type> _repositoryImpls;

        /// <summary>
        /// Db engine
        /// </summary>
        public IDbEngine Engine { get; private set; }

        /// <summary>
        /// Session factory
        /// </summary>
        public ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var config = ConfigurationManager.ConnectionStrings[_connectionName];
                    if (config == null || config.ConnectionString.IsBlank())
                        throw new ConfigurationErrorsException("Connection string not configurated: " + _connectionName);
                    var connectionString = config.ConnectionString;

                    _sessionFactory = Engine.CreateSessionFactory(connectionString);
                    MappingFactory.MapEntitiesWithAnnonations(_sessionFactory, _entityAssembly);
                    RegisterRepositoryTypes();
                }
                return _sessionFactory;
            }
        }

        /// <summary>
        /// Lookup <typeparamref name="TContext"/> for repository properties,
        /// and find implemention class in repositoryAssembly
        /// </summary>
        private void RegisterRepositoryTypes()
        {
            var reposIntfs = typeof(TContext).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(t => typeof(IRepository).IsAssignableFrom(t.PropertyType))
                .ToList();
            foreach (var type in _repositoryAssembly.GetExportedTypes())
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
    }
}