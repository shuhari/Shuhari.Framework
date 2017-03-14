using System.CodeDom.Compiler;
using System.Reflection;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Runtime.CompilerServices
{
    /// <summary>
    /// Compile source code to assembly at runtime
    /// </summary>
    public class SourceCompiler
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public SourceCompiler()
        {
            _provider = CodeDomProvider.CreateProvider("csharp");
            _params = new CompilerParameters()
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
                IncludeDebugInformation = true,
            };
        }

        private readonly CodeDomProvider _provider;
        private readonly CompilerParameters _params;

        /// <summary>
        /// Add assembly reference
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        public SourceCompiler AddReference(string reference)
        {
            Expect.IsNotBlank(reference, "reference");

            _params.ReferencedAssemblies.Add(reference);
            return this;
        }

        /// <summary>
        /// Compile to assembly
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        public Assembly Compile(params string[] sources)
        {
            var result = _provider.CompileAssemblyFromSource(_params, sources);
            if (result.Errors.Count > 0)
                throw new CompilerException(result.Errors);
            return result.CompiledAssembly;
        }
    }
}
