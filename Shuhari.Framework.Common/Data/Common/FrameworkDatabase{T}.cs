using System.Configuration;
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
        protected FrameworkDatabase(DatabaseType dbType, string connectionName, Assembly entityAssembly,
            Assembly repositoryAssembly)
        {
            Expect.IsNotBlank(connectionName, nameof(connectionName));
            Expect.IsNotNull(entityAssembly, nameof(entityAssembly));
            Expect.IsNotNull(repositoryAssembly, nameof(repositoryAssembly));

            _connectionName = connectionName;
            _entityAssembly = entityAssembly;
            _repositoryAssembly = repositoryAssembly;

            Engine = DbRegistry.GetEngine(dbType);
        }


        private readonly string _connectionName;

        private readonly Assembly _entityAssembly;

        private readonly Assembly _repositoryAssembly;

        private ISessionFactory _sessionFactory;

        private DbContextFactory<TContext> _factory;

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
                    _sessionFactory.MapEntitiesWithAnnonations(_entityAssembly);
                }
                return _sessionFactory;
            }
        }

        private DbContextFactory<TContext> Factory
        {
            get { return _factory ?? (_factory = new DbContextFactory<TContext>(SessionFactory, _repositoryAssembly)); }
        }

        /// <summary>
        /// Create database context
        /// </summary>
        /// <returns></returns>
        public TContext CreateDbContext()
        {
            return Factory.CreateContext();
        }
    }
}