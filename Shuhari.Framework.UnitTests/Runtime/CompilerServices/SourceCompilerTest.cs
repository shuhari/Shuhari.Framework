using NUnit.Framework;
using Shuhari.Framework.Runtime.CompilerServices;

namespace Shuhari.Framework.UnitTests.Runtime.CompilerServices
{
    [TestFixture]
    public class SourceCompilerTest
    {
        private SourceCompiler _compiler;

        [SetUp]
        public void SetUp()
        {
            _compiler = new SourceCompiler();
        }

        [Test]
        public void Compile_SourceInvalid_Throw()
        {
            Assert.Throws<CompilerException>(() => _compiler.Compile("invalid code"));
        }

        [Test]
        public void Compiler_SourceValid()
        {
            string source1 = @"namespace TestNS { public interface I1 {} }";
            string source2 = @"namespace TestNS { public class C1 : I1 {} }";
            var assembly = _compiler.AddReference("System.dll")
                .Compile(source1, source2);

            Assert.IsNotNull(assembly);
            Assert.IsNotNull(assembly.GetType("TestNS.I1"));
            Assert.IsNotNull(assembly.GetType("TestNS.C1"));
        }
    }
}
