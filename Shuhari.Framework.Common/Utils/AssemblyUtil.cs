using System.Collections.Generic;
using System.Reflection;
using Shuhari.Framework.Resources;

namespace Shuhari.Framework.Utils
{
    /// <summary>
    /// Helper functions to process assembly
    /// </summary>
    public static class AssemblyUtil
    {
        /// <summary>
        /// Given file is compiled into assembly as resource (NOTE: not EmbedResource),
        /// get a reference of <see cref="AssemblyResource"/> to it.
        /// </summary>
        /// <param name="assembly">Assembly which contains the resource</param>
        /// <param name="resourcePath">ResourcePath of resource relative to assembly</param>
        /// <returns>Resource locator</returns>
        public static AssemblyResource GetResource(this Assembly assembly, string resourcePath)
        {
            return new AssemblyResource(assembly, resourcePath);
        }

        /// <summary>
        /// Get all compiled resources in assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IEnumerable<AssemblyResource> GetAllResources(this Assembly assembly)
        {
            Expect.IsNotNull(assembly, nameof(assembly));

            return AssemblyResource.FindAll(assembly);
        }
    }
}