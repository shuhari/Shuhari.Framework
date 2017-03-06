using System.Text;
using System.Xml;

namespace Shuhari.Framework.Utils
{
    /// <summary>
    /// Helper methods to process xml
    /// </summary>
    public static class XmlUtil
    {
        /// <summary>
        /// Return attribute value if attribute <paramref name="attrName"/>
        /// exist, on <paramref name="defaultValue"/> if not exist.
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="attrName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetAttr(this XmlElement elem, string attrName, string defaultValue = null)
        {
            Expect.IsNotNull(elem, nameof(elem));
            Expect.IsNotBlank(attrName, nameof(attrName));

            return elem.HasAttribute(attrName) ? elem.Attributes[attrName].Value
                : defaultValue;
        }

        /// <summary>
        /// Set attribute in fluent mode
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="attrName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static XmlElement SetAttr(this XmlElement elem, string attrName, string value)
        {
            Expect.IsNotNull(elem, nameof(elem));
            Expect.IsNotBlank(attrName, nameof(attrName));

            elem.SetAttribute(attrName, value);
            return elem;
        }

        /// <summary>
        /// Create child element
        /// </summary>
        /// <param name="parent">Parent element</param>
        /// <param name="name">Name of child element</param>
        /// <param name="xmlns">Child namespace</param>
        /// <returns></returns>
        public static XmlElement AppendElement(this XmlElement parent, string name, string xmlns = null)
        {
            Expect.IsNotNull(parent, nameof(parent));
            Expect.IsNotBlank(name, nameof(name));

            var elem = (xmlns != null) ? parent.OwnerDocument.CreateElement(name, xmlns)
                : parent.OwnerDocument.CreateElement(name);
            parent.AppendChild(elem);
            return elem;
        }

        /// <summary>
        /// Convert to xml string
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="xmlHeader"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToXmlString(this XmlDocument doc, bool xmlHeader = true, bool format = true)
        {
            Expect.IsNotNull(doc, nameof(doc));


            var sb = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),
                ConformanceLevel = ConformanceLevel.Document,
            };
            if (xmlHeader)
            {
                var instruction = doc.CreateProcessingInstruction("xml", "version='1.0' encoding='utf-8'");
                doc.PrependChild(instruction);
            }
            else
            {
                settings.OmitXmlDeclaration = true;
            }
            if (format)
            {
                settings.Indent = true;
                settings.IndentChars = "    ";
            }
            using (var writer = XmlWriter.Create(sb, settings))
            {
                doc.WriteTo(writer);
            }
            return sb.ToString();
        }
    }
}