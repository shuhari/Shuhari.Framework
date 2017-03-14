using System;
using System.IO;
using Shuhari.Framework.Globalization;

namespace Shuhari.Framework.Utils
{
    /// <summary>
    /// Check parameter/condition/environment/... should match developer's expection,
    /// and throw <see cref="ExpectionException"/> if check failed.
    /// </summary>
    public static class Expect
    {
        /// <summary>
        /// Check <paramref name="param"/> should be not null value.
        /// </summary>
        /// <param name="param">Parameter to check</param>
        /// <param name="paramName">Parameter name display to user</param>
        /// <exception cref="ExpectionException">Throw if check failed.</exception>
        public static void IsNotNull(object param, string paramName)
        {
            if (param == null)
                throw ExceptionBuilder.Expection(FrameworkStrings.ExpectNotNull, paramName);
        }

        /// <summary>
        ///Check <paramref name="param"/> should be non-blank string.
        /// </summary>
        /// <param name="param">Parameter to check</param>
        /// <param name="paramName">Parameter name display to user</param>
        /// <exception cref="ExpectionException">Throw if check failed.</exception>
        /// <remarks>blank string means null, 0-length or contains blank chars only.
        /// Same as <see cref="string.IsNullOrWhiteSpace(string)"/></remarks>
        public static void IsNotBlank(string param, string paramName)
        {
            if (string.IsNullOrWhiteSpace(param))
                throw ExceptionBuilder.Expection(FrameworkStrings.ExpectNotBlank, paramName);
        }

        /// <summary>
        /// Expect <paramref name="predicate"/> should evalute to true result.
        /// </summary>
        /// <param name="predicate">Predicate to check.</param>
        /// <param name="message">Error message display to user.</param>
        /// <exception cref="ExpectionException">Throw if check failed.</exception>
        public static void That(bool predicate, string message = null)
        {
            if (!predicate)
            {
                message = message ?? FrameworkStrings.ExpectTrue;
                throw ExceptionBuilder.Expection(message);
            }
        }

        /// <summary>
        /// Expect <paramref name="predicate"/> should evalute to true, or raise custom exception
        /// </summary>
        /// <param name="predicate">Predicate to check</param>
        /// <param name="builder">Raise custom exception</param>
        public static void That(bool predicate, Func<Exception> builder)
        {
            if (!predicate)
            {
                throw builder();
            }
        }

        /// <summary>
        /// Expect dirPath specified by <paramref name="filePath"/> should exist on disk.
        /// </summary>
        /// <param name="filePath">ResourcePath of file</param>
        /// <exception cref="ExpectionException">Throw if check failed.</exception>
        public static void FileExist(string filePath)
        {
            IsNotBlank(filePath, nameof(filePath));

            if (!File.Exists(filePath))
                throw ExceptionBuilder.Expection(FrameworkStrings.ExpectFileExist, filePath);
        }

        /// <summary>
        /// Expect dirPath specified by <paramref name="dirPath"/> should exist on disk.
        /// </summary>
        /// <param name="dirPath">ResourcePath of directory</param>
        /// <exception cref="ExpectionException">Throw if check failed.</exception>
        public static void DirectoryExist(string dirPath)
        {
            IsNotBlank(dirPath, nameof(dirPath));

            if (!Directory.Exists(dirPath))
                throw ExceptionBuilder.Expection(FrameworkStrings.ExpectDirectoryExist, dirPath);
        }
    }
}