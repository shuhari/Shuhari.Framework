using System;
using System.Windows;
using NUnit.Framework;
using Shuhari.Framework.Wpf.Controls;

namespace Shuhari.Framework.UnitTests.Wpf.Controls
{
    [TestFixture]
    public class GridUtilTest
    {
        [TestCase("abc")]
        [TestCase("abc*")]
        public void Parse_InvalidValue_ShouldThrow(string str)
        {
            Assert.Throws<FormatException>(() => GridUtil.Parse(str));
        }

        [TestCase("Auto", 1D, GridUnitType.Auto)]
        [TestCase("auto", 1D, GridUnitType.Auto)]
        [TestCase("@", 1D, GridUnitType.Auto)]
        [TestCase("*", 1D, GridUnitType.Star)]
        [TestCase("2*", 2D, GridUnitType.Star)]
        [TestCase("123", 123D, GridUnitType.Pixel)]
        public void Parse_ValidValue(string inputValue, double value, GridUnitType unit)
        {
            var length = GridUtil.Parse(inputValue);
            Assert.AreEqual(value, length.Value);
            Assert.AreEqual(unit, length.GridUnitType);
        }

        [Test]
        public void ParseAll()
        {
            var lengths = GridUtil.ParseAll("@,2*,123");

            Assert.AreEqual(3, lengths.Length);
            CollectionAssert.AreEqual(new[]
            {
                GridLength.Auto,
                new GridLength(2, GridUnitType.Star),
                new GridLength(123, GridUnitType.Pixel),
            }, lengths);
        }
    }
}
