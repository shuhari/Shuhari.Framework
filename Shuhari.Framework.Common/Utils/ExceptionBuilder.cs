using System;
using System.Collections.Generic;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.Resources;

namespace Shuhari.Framework.Utils
{
    /// <summary>
    /// Helper to build exception
    /// </summary>
    public static class ExceptionBuilder
    {
        /// <summary>
        /// Create <see cref="ExpectionException"/>.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ExpectionException Expection(string format, params object[] args)
        {
            return new ExpectionException(string.Format(format, args));
        }

        /// <summary>
        /// Create <see cref="ResourceException"/>
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ResourceException Resource(string format, params object[] args)
        {
            return new ResourceException(string.Format(format, args));
        }

        /// <summary>
        /// Create NotSupportedException
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static NotSupportedException NotSupported(string format, params object[] args)
        {
            return new NotSupportedException(string.Format(format, args));
        }

        /// <summary>
        /// Create Key not found exception
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static KeyNotFoundException KeyNotFound(string format, params object[] args)
        {
            return new KeyNotFoundException(string.Format(format, args));
        }

        /// <summary>
        /// Create mapping exception
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static MappingException Mapping(string format, params object[] args)
        {
            return new MappingException(string.Format(format, args));
        }

        /// <summary>
        /// Create invalid operation exception
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static InvalidOperationException InvalidOperation(string format, params object[] args)
        {
            return new InvalidOperationException(string.Format(format, args));
        }

        /// <summary>
        /// Create type access exception
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static TypeAccessException TypeAccess(string format, params object[] args)
        {
            return new TypeAccessException(string.Format(format, args));
        }
    }
}