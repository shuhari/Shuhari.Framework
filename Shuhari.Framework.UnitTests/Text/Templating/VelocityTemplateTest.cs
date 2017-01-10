using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shuhari.Framework.Resources;
using Shuhari.Framework.Text.Templating;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Text.Templating
{
    [TestFixture]
    public class VelocityTemplateTest
    {
        private readonly AssemblyResource _res = typeof(VelocityTemplateTest).Assembly.GetResource("ResourceFiles/res.vm");

        [Test]
        public void FromString()
        {
            var tplText = _res.ReadAllText(Encoding.UTF8);
            AssertTemplate(VelocityTemplate.FromString(tplText));
        }

        [Test]
        public void FromResource()
        {
            AssertTemplate(VelocityTemplate.FromResource(_res));
        }

        [Test]
        public void FromFile()
        {
            var destPath = _res.CopyToBaseDirectory();
            AssertTemplate(VelocityTemplate.FromFile(destPath));
        }

        private void AssertTemplate(ITemplate template)
        {
            var result = template.Set("user", "guest")
                .Evaluate();
            Assert.AreEqual("this is guest", result);
        }
    }
}
