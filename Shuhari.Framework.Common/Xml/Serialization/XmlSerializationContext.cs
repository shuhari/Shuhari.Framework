using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
using System.Xml.Serialization;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Xml.Serialization
{
    /// <summary>
    /// Serialize context
    /// </summary>
    internal class XmlSerializationContext
    {
        internal XmlSerializationContext(FrameworkXmlSerializer serializer, XmlDocument doc, object root)
        {
            Expect.IsNotNull(serializer, nameof(serializer));
            Expect.IsNotNull(doc, nameof(doc));
            Expect.IsNotNull(root, nameof(root));

            Serializer = serializer;
            Document = doc;
            Root = root;
            NamespaceManager = new XmlNamespaceManager(doc.NameTable);

            _typeDict = new TypeSerializationDictionary();
        }

        private readonly TypeSerializationDictionary _typeDict;

        /// <summary>
        /// Serializer
        /// </summary>
        public FrameworkXmlSerializer Serializer { get; }

        /// <summary>
        /// Xml
        /// </summary>
        public XmlDocument Document { get; }

        /// <summary>
        /// Root object
        /// </summary>
        public object Root { get; }

        /// <summary>
        /// Namespace manager
        /// </summary>
        public XmlNamespaceManager NamespaceManager { get; }

        /// <summary>
        /// Serialize
        /// </summary>
        public void Serialize()
        {
            var rootElem = CreateRootElement();
            SerializeRecursive(rootElem, Root);
        }

        private XmlElement CreateRootElement()
        {
            string elemName = Root.GetType().Name;
            var attr = Root.GetType().GetCustomAttribute<XmlRootAttribute>();
            if (attr != null && attr.ElementName.IsNotBlank())
                elemName = attr.ElementName;
            if (attr.Namespace.IsNotBlank())
                NamespaceManager.AddNamespace(string.Empty, attr.Namespace);
            var rootElem = AppendElement(null, elemName);
            return rootElem;
        }

        private XmlElement AppendElement(XmlElement parent, string name)
        {
            Expect.IsNotBlank(name, nameof(name));

            var elem = NamespaceManager.DefaultNamespace.IsNotBlank()
                ? Document.CreateElement(name, NamespaceManager.DefaultNamespace)
                : Document.CreateElement(name);
            if (parent != null)
                parent.AppendChild(elem);
            else
                Document.AppendChild(elem);

            return elem;
        }

        private void SerializeRecursive(XmlElement elem, object target)
        {
            Expect.IsNotNull(elem, nameof(elem));
            Expect.IsNotNull(target, nameof(target));

            SerializeAttributes(elem, target);
            SerializeChildren(elem, target);
        }

        private void SerializeAttributes(XmlElement elem, object target)
        {
            Expect.IsNotNull(elem, nameof(elem));
            Expect.IsNotNull(target, nameof(target));
            var typeInfo = _typeDict.GetInfo(target.GetType());

            foreach (var attrInfo in typeInfo.AttributeInfos)
            {
                var value = attrInfo.Property.GetValue(target);
                if (value == null)
                    continue;
                // DefaultValue not serialized
                var defaultAttr = attrInfo.Property.GetCustomAttribute<DefaultValueAttribute>();
                if (defaultAttr != null && Equals(defaultAttr.Value, value))
                    continue;

                var valueStr = GetValueSerializer(attrInfo.Property.PropertyType).Serialize(value);
                string attrName = attrInfo.Attribute.AttributeName;
                if (attrName.IsBlank())
                    attrName = attrInfo.Property.Name;
                elem.SetAttribute(attrName, valueStr);
            }

            foreach (var attr in typeInfo.AdditionalAttributes)
            {
                elem.SetAttribute(attr.Name, attr.Value);
            }

            if (typeInfo.TextInfo != null)
            {
                var text = Convert.ToString(typeInfo.TextInfo.Property.GetValue(target));
                elem.InnerText = text;
            }
        }

        private IValueSerializer GetValueSerializer(Type valueType)
        {
            Expect.IsNotNull(valueType, nameof(valueType));

            if (valueType == typeof(bool))
                return new BoolValueSerializer();
            else if (valueType == typeof(DateTime))
                return new DateTimeValueSerializer(Serializer.DateTimeFormat);
            return new DefaultValueSerializer();
        }

        private void SerializeChildren(XmlElement elem, object target)
        {
            Expect.IsNotNull(elem, nameof(elem));
            Expect.IsNotNull(target, nameof(target));
            var typeInfo = _typeDict.GetInfo(target.GetType());

            foreach (var elemInfo in typeInfo.ElementInfos)
            {
                var child = elemInfo.Property.GetValue(target);
                if (child == null)
                    continue;
                var childElemName = elemInfo.Attribute.ElementName;
                if (childElemName.IsBlank())
                    childElemName = elemInfo.Property.Name;
                var childElem = AppendElement(elem, childElemName);
                SerializeRecursive(childElem, child);
            }

            foreach (var arrayInfo in typeInfo.ArrayInfos)
            {
                // if XmlArray.ElementName=null then add direct to elem, 
                // else add to child container elem
                var parentElem = elem;
                if (arrayInfo.Attribute.ElementName.IsNotBlank())
                    parentElem = AppendElement(parentElem, arrayInfo.Attribute.ElementName);
                var children = arrayInfo.Property.GetValue(target, null) as IEnumerable;
                if (children != null)
                {
                    var itemAttr = arrayInfo.Property.GetCustomAttribute<XmlArrayItemAttribute>();
                    foreach (object child in children)
                    {
                        string childElemName = itemAttr != null ? itemAttr.ElementName : child.GetType().Name;
                        var childElem = AppendElement(parentElem, childElemName);
                        SerializeRecursive(childElem, child);
                    }
                }
            }
        }

        private const string NAMESPACE_PREFIX = "_";

        /// <summary>
        /// Deserialize
        /// </summary>
        public void Deserialize()
        {
            var rootAttr = Root.GetType().GetCustomAttribute<XmlRootAttribute>();
            if (rootAttr != null && rootAttr.Namespace.IsNotBlank())
                NamespaceManager.AddNamespace(NAMESPACE_PREFIX, rootAttr.Namespace);

            DeserializeRecursive(Document.DocumentElement, Root);
        }

        private void DeserializeRecursive(XmlElement elem, object target)
        {
            Expect.IsNotNull(elem, nameof(elem));
            Expect.IsNotNull(target, nameof(target));

            DeserializeAttributes(elem, target);
            DeserializeChildren(elem, target);
        }

        private void DeserializeAttributes(XmlElement elem, object target)
        {
            Expect.IsNotNull(elem, nameof(elem));
            Expect.IsNotNull(target, nameof(target));
            var typeInfo = _typeDict.GetInfo(target.GetType());

            foreach (var info in typeInfo.AttributeInfos)
            {
                string attrName = info.Attribute.AttributeName;
                if (attrName.IsBlank())
                    attrName = info.Property.Name;
                if (elem.HasAttribute(attrName))
                {
                    var valueStr = elem.GetAttribute(attrName);
                    var value = TypeDescriptor.GetConverter(info.Property.PropertyType).ConvertFrom(valueStr);
                    info.Property.SetValue(target, value);
                }
            }

            if (typeInfo.TextInfo != null)
            {
                typeInfo.TextInfo.Property.SetValue(target, elem.InnerText);
            }
        }

        private void DeserializeChildren(XmlElement elem, object target)
        {
            Expect.IsNotNull(elem, nameof(elem));
            Expect.IsNotNull(target, nameof(target));
            var typeInfo = _typeDict.GetInfo(target.GetType());

            foreach (var info in typeInfo.ElementInfos)
            {
                string childElemName = info.Attribute.ElementName;
                if (childElemName.IsBlank())
                    childElemName = info.Property.Name;
                var childElem = SelectChildElement(elem, childElemName);
                var child = info.Property.GetValue(target);
                if (childElem != null && child != null)
                    DeserializeRecursive(childElem, child);
            }

            foreach (var info in typeInfo.ArrayInfos)
            {
                var parentElem = elem;
                if (info.Attribute.ElementName.IsNotBlank())
                    parentElem = SelectChildElement(elem, info.Attribute.ElementName);
                if (parentElem != null)
                {
                    var collection = info.Property.GetValue(target) as IList;
                    if (collection != null)
                    {
                        foreach (var childElem in parentElem.ChildNodes.OfType<XmlElement>())
                        {
                            var child = CreateInstance(childElem, info);
                            collection.Add(child);
                        }
                    }
                }
            }
        }

        private XmlElement SelectChildElement(XmlElement parentElem, string elemName)
        {
            Expect.IsNotNull(parentElem, nameof(parentElem));

            Expect.IsNotBlank(elemName, nameof(elemName));
            if (NamespaceManager.HasNamespace(NAMESPACE_PREFIX))
                return parentElem.SelectSingleNode(NAMESPACE_PREFIX + ":" + elemName, NamespaceManager) as XmlElement;
            else
                return parentElem.SelectSingleNode(elemName) as XmlElement;
        }

        private object CreateInstance(XmlElement elem, XmlAttributeInfo<XmlArrayAttribute> info)
        {
            Type targetType;
            var genericArgs = info.Property.PropertyType.GenericTypeArguments;
            if (genericArgs != null && genericArgs.Length == 1 &&
                !genericArgs[0].IsAbstract)
                targetType = genericArgs[0];
            else
            {
                if (Serializer.TypeFactory == null)
                    throw new InvalidOperationException("Serializer have no type factory");
                targetType = Serializer.TypeFactory(elem.Name);
            }
            var instance = Activator.CreateInstance(targetType);
            DeserializeRecursive(elem, instance);
            return instance;
        }
    }
}
