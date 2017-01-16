using System.Globalization;
using Moq;
using NUnit.Framework;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Web.Mvc;

namespace Shuhari.Framework.UnitTests.Web.Mvc
{
    public class MetadataEntity
    {
        public string Prop { get; set; }
    }

    [TestFixture]
    public class FrameworkModelMetadataProviderTest
    {

        [SetUp]
        public void SetUp()
        {
            ResourceRegistry.Reset();
            _mockProvider = new Mock<IResourceProvider>();
            ResourceRegistry.Register(_mockProvider.Object);

            _provider = new FrameworkModelMetadataProvider();
        }

        private Mock<IResourceProvider> _mockProvider;
        private FrameworkModelMetadataProvider _provider;

        [Test]
        public void GetMetadata_DisplayNameNotRegistered_ShouldBeNull()
        {
            var metadata = _provider.GetMetadataForProperty(null, typeof(MetadataEntity), "Prop");
            Assert.IsNull(metadata.DisplayName);
        }

        [Test]
        public void GetMeatadata_FullNameRegistered_ShouldReturnRegistered()
        {
            string propKey = typeof(MetadataEntity).FullName + ".Prop";
            _mockProvider.Setup(m => m.GetString(propKey, It.IsAny<CultureInfo>())).Returns("FullName");

            var metadata = _provider.GetMetadataForProperty(null, typeof(MetadataEntity), "Prop");
            Assert.AreEqual("FullName", metadata.DisplayName);
        }

        [Test]
        public void GetMeatadata_PropertyRegistered_ShouldReturnPropertyName()
        {
            _mockProvider.Setup(m => m.GetString("Shuhari.Library.UnitTests.Web.MetadataEntity.Prop",
                It.IsAny<CultureInfo>())).Returns<string>(null);
            _mockProvider.Setup(m => m.GetString("MetadataEntity.Prop",
                It.IsAny<CultureInfo>())).Returns<string>(null);
            _mockProvider.Setup(m => m.GetString("Prop", It.IsAny<CultureInfo>())).Returns("PropName");

            var metadata = _provider.GetMetadataForProperty(null, typeof(MetadataEntity), "Prop");
            Assert.AreEqual("PropName", metadata.DisplayName);
        }
    }
}
