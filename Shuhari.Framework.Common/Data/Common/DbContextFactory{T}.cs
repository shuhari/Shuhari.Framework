using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Common
{
    /// <summary>
    /// Create db context instance
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DbContextFactory<T>
        where T: FrameworkDbContext
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="sessionFactory">Session factory</param>
        /// <param name="implAssembly">Repository implementation defined assembly</param>
        public DbContextFactory(ISessionFactory sessionFactory, Assembly implAssembly)
        {
            Expect.IsNotNull(sessionFactory, nameof(sessionFactory));
            Expect.IsNotNull(implAssembly, nameof(implAssembly));

            _sessionFactory = sessionFactory;
            _mappings = new List<RepositoryMapping>();
            RegisterMappings(implAssembly);
        }

        private readonly ISessionFactory _sessionFactory;

        private readonly List<RepositoryMapping> _mappings;

        private void RegisterMappings(Assembly implAssembly)
        {
            Expect.IsNotNull(implAssembly, nameof(implAssembly));

            var intfProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(t => typeof(IRepository).IsAssignableFrom(t.PropertyType))
                .ToArray();
            var implTypes = implAssembly.GetExportedTypes()
                .Where(x => typeof(IRepository).IsAssignableFrom(x))
                .ToArray();

            foreach (var prop in intfProps)
            {
                var implType = implTypes.FirstOrDefault(x => IsImplementationOf(prop, x));
                if (implType == null)
                    throw new MappingException(string.Format("Could not found implementation for repository {0}", prop.Name));
                _mappings.Add(new RepositoryMapping(prop, implType));
            }
        }

        private bool IsImplementationOf(PropertyInfo prop, Type implType)
        {
            Expect.IsNotNull(prop, nameof(prop));
            Expect.IsNotNull(implType, nameof(implType));

            return implType.IsClass &&
                !implType.IsAbstract &&
                prop.PropertyType.IsAssignableFrom(implType);
        }

        /// <summary>
        /// Create context
        /// </summary>
        /// <returns></returns>
        public T CreateContext()
        {
            var context = (T)Activator.CreateInstance(typeof(T), new object[] { _sessionFactory });
            foreach (var mapping in _mappings)
            {
                var repos = Activator.CreateInstance(mapping.ImplementationType);
                mapping.Property.SetValue(context, repos);
            }
            return context;
        }
    }

    /// <summary>
    /// Repository type mapping
    /// </summary>
    internal class RepositoryMapping
    {
        public RepositoryMapping(PropertyInfo prop, Type implType)
        {
            Expect.IsNotNull(prop, nameof(prop));
            Expect.IsNotNull(implType, nameof(implType));

            Property = prop;
            ImplementationType = implType;
        }

        /// <summary>
        /// Repository interface
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// Repository implementation
        /// </summary>
        public Type ImplementationType { get; }
    }
}
