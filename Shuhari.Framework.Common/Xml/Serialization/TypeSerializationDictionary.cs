using System;
using System.Collections.Generic;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Xml.Serialization
{
    /// <summary>
    /// Load type serialization in lazy mode
    /// </summary>
    internal class TypeSerializationDictionary
    {
        public TypeSerializationDictionary()
        {
            _dict = new Dictionary<Type, TypeSerializationInfo>();
        }

        private readonly Dictionary<Type, TypeSerializationInfo> _dict;

        /// <summary>
        /// Load info if not fetched yet
        /// </summary>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public TypeSerializationInfo GetInfo(Type targetType)
        {
            Expect.IsNotNull(targetType, nameof(targetType));

            if (!_dict.ContainsKey(targetType))
            {
                var info = new TypeSerializationInfo(targetType);
                _dict[targetType] = info;
            }
            return _dict[targetType];
        }
    }
}
