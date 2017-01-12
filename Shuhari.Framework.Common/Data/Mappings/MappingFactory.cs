using System;
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
    }
}
