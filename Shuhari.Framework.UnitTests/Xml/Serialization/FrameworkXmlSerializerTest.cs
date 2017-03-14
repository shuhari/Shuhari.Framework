using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Castle.Components.DictionaryAdapter;
using NUnit.Framework;
using Shuhari.Framework.Xml.Serialization;

namespace Shuhari.Framework.UnitTests.Xml.Serialization
{
    class ChildElem
    {
        [XmlText]
        public string Text { get; set; }
    }

    class DirectCollection
    {
        public DirectCollection()
        {
            Items = new List<CollectionElem>();
        }

        [XmlArray]
        public List<CollectionElem> Items { get; set; }
    }

    class CollectionElem
    {
        [XmlAttribute]
        public bool BoolProp { get; set; }

        [XmlAttribute]
        public DateTime TimeProp { get; set; }
    }

    abstract class BaseSerializeModel
    {
        protected BaseSerializeModel()
        {
            Child = new ChildElem();
            DirectCollection = new DirectCollection();
            NestedCollection = new EditableList<CollectionElem>();
        }

        [XmlAttribute, XmlOrder(0)]
        public string StrProp { get; set; }

        [XmlAttribute("str-prop-with-name"), XmlOrder(1)]
        public string StrPropWithName { get; set; }

        [XmlElement]
        public ChildElem Child { get; set; }

        [XmlElement]
        public DirectCollection DirectCollection { get; set; }

        [XmlArray("NestedCollection")]
        public List<CollectionElem> NestedCollection { get; set; }
    }

    [XmlRoot("Root")]
    [XmlAdditionalAttribute("xml:space", "preserve")]
    class ModelWithNoNamespace : BaseSerializeModel
    {
    }

    [XmlRoot("Root", Namespace = "http://test.org")]
    class ModelWithNamespace : BaseSerializeModel
    {
    }

    [TestFixture]
    public class FrameworkXmlSerializerTest
    {
        private T CreateModel<T>() 
            where T : BaseSerializeModel, new()
        {
            var model = new T()
            {
                StrProp = "sp1",
                StrPropWithName = "sp2",
            };
            model.Child.Text = "txt";
            var colElem = new CollectionElem
            {
                BoolProp = true,
                TimeProp = new DateTime(2016, 1, 1)
            };
            model.DirectCollection.Items.Add(colElem);
            model.NestedCollection.Add(colElem);
            return model;
        }

        [Test]
        public void Serialize_NoNamespace()
        {
            var model = CreateModel<ModelWithNoNamespace>();
            var serializer = GetSerializer();
            var xml = serializer.Serialize(model);

            // Console.WriteLine(xml);
            string expected = @"
<Root StrProp=""sp1"" str-prop-with-name=""sp2"" xml:space=""preserve"">
    <Child>txt</Child>
    <DirectCollection>
        <CollectionElem BoolProp=""true"" TimeProp=""2016-01-01 00:00:00"" />
    </DirectCollection>
    <NestedCollection>
        <CollectionElem BoolProp=""true"" TimeProp=""2016-01-01 00:00:00"" />
    </NestedCollection>
</Root>
".Trim();
            var expectedLines = expected.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            var xmlLines = xml.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            CollectionAssert.AreEqual(expectedLines, xmlLines);

            var deser = serializer.Deserialize<ModelWithNoNamespace>(xml);
            Assert.IsNotNull(deser);
        }

        [Test]
        public void Serialize_WithNamespace()
        {
            var model = CreateModel<ModelWithNamespace>();
            var serializer = GetSerializer();
            var xml = serializer.Serialize(model);

            Console.WriteLine(xml);
            var deser = serializer.Deserialize<ModelWithNamespace>(xml);
            Assert.IsNotNull(deser);
        }

        private FrameworkXmlSerializer GetSerializer()
        {
            return new FrameworkXmlSerializer();
        }
    }
}
