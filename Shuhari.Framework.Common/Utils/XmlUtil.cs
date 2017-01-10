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
        public static string SafeAttr(this XmlElement elem, string attrName, string defaultValue = null)
        {
            Expect.IsNotNull(elem, nameof(elem));
            Expect.IsNotBlank(attrName, nameof(attrName));

            return elem.HasAttribute(attrName) ? elem.Attributes[attrName].Value
                : defaultValue;
        }
    }
}