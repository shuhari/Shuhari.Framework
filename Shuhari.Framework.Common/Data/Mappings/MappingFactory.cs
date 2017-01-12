using System;
using System.Linq;
using System.Reflection;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Factory to create mapping
    /// </summary>
    internal static class MappingFactory
    {
        /// <summary>
        /// Create entity mapping by attribute annonations
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static EntityMapper<T> CreateEntityMappingFromAnnonations<T>()
            where T: class
        {
            var mapper = new EntityMapper<T>();
            mapper.Load();
            return mapper;
        }

        /// <summary>
        /// Register entity mapping from annonated types
        /// </summary>
        /// <param name="sessionFactory"></param>
        /// <param name="assembly"></param>
        internal static void MapEntitiesWithAnnonations(this ISessionFactory sessionFactory, Assembly assembly)
        {
            Expect.IsNotNull(sessionFactory, nameof(sessionFactory));
            Expect.IsNotNull(assembly, nameof(assembly));

            var genericMethod = typeof(MappingFactory).GetMethod("CreateEntityMappingFromAnnonations", BindingFlags.Public | BindingFlags.Static);
            Expect.IsNotNull(genericMethod, nameof(genericMethod));

            var annonatedTypes = assembly.GetExportedTypes()
                .Where(IsAnnonatedEntityType)
                .ToArray();
            foreach (var type in annonatedTypes)
            {
                var createMethod = genericMethod.MakeGenericMethod(type);
                var mapper = createMethod.Invoke(null, new object[0]);
                sessionFactory.RegisterMapper(type, mapper);
            }
        }

        /// <summary>
        /// check the entity is mapping target
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsAnnonatedEntityType(Type type)
        {
            Expect.IsNotNull(type, nameof(type));

            return type.IsClass &&
                !type.IsAbstract &&
                type.GetCustomAttribute<TableAttribute>() != null;
        }
    }
}
