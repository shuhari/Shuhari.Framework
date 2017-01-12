using System;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Describe mapping error
    /// </summary>
    public sealed class MappingException : Exception
    {
        /// <summary>
        /// Initialize with message
        /// </summary>
        /// <param name="message"></param>
        public MappingException(string message)
            : base(message)
        {
        }
    }
}
