using NUnit.Framework;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Utils
{
    [TestFixture]
    public class StringPartsTest
    {
        [Test]
        public void Join_PredicateMatched()
        {
            Assert.AreEqual("part1,part2,part3", new StringParts()
                .Add("part1")
                .AddIfNotBlank("part2")
                .AddIf(true, "part3")
                .Join(","));
        }

        [Test]
        public void Join_PredicateNotMatched()
        {
            Assert.AreEqual("part1", new StringParts()
                .Add("part1")
                .AddIfNotBlank("")
                .AddIf(false, "part3")
                .Join(","));
        }
    }
}
