using NUnit.Framework;
using Shuhari.Framework.IO;

namespace Shuhari.Framework.UnitTests.IO
{
    [TestFixture]
    public class CommandLineTest
    {
        [Test]
        public void ShellExec()
        {
            var output = new CommandLine("ipconfig").Exec();
            Assert.IsNotEmpty(output);
        }
    }
}
