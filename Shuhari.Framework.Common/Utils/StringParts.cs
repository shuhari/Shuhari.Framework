using System.Collections.Generic;

namespace Shuhari.Framework.Utils
{
    /// <summary>
    /// Manage string in parts which can be easily configured and joined
    /// </summary>
    public class StringParts
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public StringParts()
        {
            _parts = new List<string>();
        }

        private readonly List<string> _parts;

        /// <summary>
        /// Add one part
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public StringParts Add(string part)
        {
            _parts.Add(part);
            return this;
        }

        /// <summary>
        /// Add part only when it is not blank string
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public StringParts AddIfNotBlank(string part)
        {
            if (part.IsNotBlank())
                _parts.Add(part);
            return this;
        }

        /// <summary>
        /// Add if predicate evaluated to true
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="part"></param>
        /// <returns></returns>
        public StringParts AddIf(bool predicate, string part)
        {
            if (predicate)
                _parts.Add(part);
            return this;
        }

        /// <summary>
        /// Join parts together
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public string Join(string separator)
        {
            return string.Join(separator, _parts);
        }
    }
}
