using System;

namespace Shuhari.Framework.Xml.Serialization
{
    /// <summary>
    /// Control serialization order for attributes/elements
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class XmlOrderAttribute : Attribute
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="order"></param>
        public XmlOrderAttribute(int order)
        {
            this.Order = order;
        }

        /// <summary>
        /// Order
        /// </summary>
        public int Order { get; private set; }
    }
}
