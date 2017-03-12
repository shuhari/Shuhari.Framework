using System;
using System.Linq;
using System.Xml.Serialization;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Xml.Serialization
{
    /// <summary>
    /// Save type serialization info
    /// </summary>
    internal class TypeSerializationInfo
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="targetType"></param>
        public TypeSerializationInfo(Type targetType)
        {
            Expect.IsNotNull(targetType, nameof(targetType));

            this.TargetType = targetType;
            Load();
        }

        /// <summary>
        /// Target type
        /// </summary>
        public Type TargetType { get; private set; }

        /// <summary>
        /// Attribute infos
        /// </summary>
        public XmlAttributeInfo<XmlAttributeAttribute>[] AttributeInfos { get; private set; }

        /// <summary>
        /// Element infos
        /// </summary>
        public XmlAttributeInfo<XmlElementAttribute>[] ElementInfos { get; private set; }

        /// <summary>
        /// Array infos
        /// </summary>
        public XmlAttributeInfo<XmlArrayAttribute>[] ArrayInfos { get; private set; }

        /// <summary>
        /// Text info
        /// </summary>
        public XmlAttributeInfo<XmlTextAttribute> TextInfo { get; private set; }

        /// <summary>
        /// Additional attributes
        /// </summary>
        public XmlAdditionalAttributeAttribute[] AdditionalAttributes { get; private set; }

        private void Load()
        {
            AttributeInfos = XmlAttributeInfo<XmlAttributeAttribute>.LoadAll(TargetType);
            AdditionalAttributes = XmlAdditionalAttributeAttribute.LoadAll(TargetType);
            ElementInfos = XmlAttributeInfo<XmlElementAttribute>.LoadAll(TargetType);
            ArrayInfos = XmlAttributeInfo<XmlArrayAttribute>.LoadAll(TargetType);
            TextInfo = XmlAttributeInfo<XmlTextAttribute>.LoadAll(TargetType).FirstOrDefault();
        }
    }
}
