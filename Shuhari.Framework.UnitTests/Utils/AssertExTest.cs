using System;
using NUnit.Framework;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Utils
{
    [TestFixture]
    public class AssertExTest
    {
        [Test]
        public void IsNotEmpty_Guid_ParamEmpty_Throw()
        {
            Assert.Throws<AssertionException>(() => AssertEx.IsNotEmpty(Guid.Empty));
        }

        [Test]
        public void IsNotEmpty_Guid_ParamNotEmpty_Pass()
        {
            AssertEx.IsNotEmpty(Guid.NewGuid());
            Assert.Pass();
        }
    }
}
