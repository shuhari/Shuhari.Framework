using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Web.Mvc
{
    /// <summary>
    /// Custom Model metadata provider, provider additional strings based on convension
    /// </summary>
    public class FrameworkModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        /// <inheritdoc />
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType,
            Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var result = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            if (result.DisplayName == null && modelType != null && propertyName.IsNotBlank())
            {
                var lookupKeys = new[]
                {
                    string.Format("{0}.{1}", containerType.FullName, propertyName),
                    string.Format("{0}.{1}", containerType.Name, propertyName),
                    string.Format("{0}", propertyName),
                };
                foreach (var key in lookupKeys)
                {
                    var name = ResourceRegistry.GetString(key);
                    if (name != null)
                    {
                        result.DisplayName = name;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
