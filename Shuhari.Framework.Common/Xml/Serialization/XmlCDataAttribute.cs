using System;

namespace Shuhari.Framework.Xml.Serialization
{
    /// <summary>
    /// String property serialized as CDATA
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class XmlCDataAttribute : Attribute
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public XmlCDataAttribute(string elementName = null)
        {
            this.ElementName = elementName;
        }

        /// <summary>
        /// Element name. If null, then CDATA is add to parent directly.
        /// </summary>
        public string ElementName { get; private set; }
    }
}
