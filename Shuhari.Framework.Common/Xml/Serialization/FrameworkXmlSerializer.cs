using System;
using System.ComponentModel;
using System.Xml;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Xml.Serialization
{
    /// <summary>
    /// Custom xml serializer with following rules:
    /// <ul>
    ///   <li>XmlArray can ignore ElementName to define collection that serialized as direct children of parent</li>
    ///   <li>Use <see cref="XmlOrderAttribute"/> to control serialize order</li>
    ///   <li>use <see cref="XmlAdditionalAttributeAttribute"/> to add additional attributes</li>
    /// </ul>
    /// For simplify, this serialier only support xml with no namespace or just one default namespace
    /// (define in XmlRoot)
    /// </summary>
    public class FrameworkXmlSerializer
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public FrameworkXmlSerializer()
        {
            Format = true;
        }

        /// <summary>
        /// In case the serialize could not tell which type to create (such as collection property
        /// which define base class as element type, but derived class are used
        /// </summary>
        public Func<string, Type> TypeFatory { get; set; }

        /// <summary>
        /// DateTime serialize format. If null then default format 
        /// <see cref="DateTimeValueSerializer.DEFAULT_FORMAT"/> is used instead
        /// </summary>
        public string DateTimeFormat { get; set; }

        /// <summary>
        /// include xml declaration
        /// </summary>
        [DefaultValue(false)]
        public bool IncludeXmlDeclaration { get; set; }

        /// <summary>
        /// Format xml
        /// </summary>
        [DefaultValue(true)]
        public bool Format { get; set; }

        /// <summary>
        /// Serialize object
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string Serialize(object target)
        {
            Expect.IsNotNull(target, nameof(target));

            var doc = new XmlDocument();
            var ctx = new XmlSerializationContext(this, doc, target);
            ctx.Serialize();

            return doc.ToXmlString(IncludeXmlDeclaration, Format);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public T Deserialize<T>(string xml)
            where T : class, new()
        {
            Expect.IsNotBlank(xml, nameof(xml));

            var target = new T();
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var ctx = new XmlSerializationContext(this, doc, target);
            ctx.Deserialize();
            return target;
        }
    }
}
