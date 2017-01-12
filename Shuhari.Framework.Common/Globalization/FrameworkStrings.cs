using System.ComponentModel.DataAnnotations;

namespace Shuhari.Framework.Globalization
{
    /// <summary>
    /// Define framework used strings
    /// </summary>
    internal static class FrameworkStrings
    {
        /// <summary>
        /// Expect parameter to be not null
        /// </summary>
        [Display]
        internal static readonly string ExpectNotNull = ResourceRegistry.GetString("Expect.NotNull");

        /// <summary>
        /// Expect parameter to be not blank.
        /// </summary>
        [Display]
        internal static readonly string ExpectNotBlank = ResourceRegistry.GetString("Expect.NotBlank");

        /// <summary>
        /// Expect result is true
        /// </summary>
        [Display]
        internal static readonly string ExpectTrue = ResourceRegistry.GetString("Expect.True");

        /// <summary>
        /// Expect file exist
        /// </summary>
        [Display]
        internal static readonly string ExpectFileExist = ResourceRegistry.GetString("Expect.FileExist");

        /// <summary>
        /// Expect directory exist
        /// </summary>
        [Display]
        internal static readonly string ExpectDirectoryExist = ResourceRegistry.GetString("Expect.DirectoryExist");

        /// <summary>
        /// Resource not found
        /// </summary>
        [Display]
        internal static readonly string ErrorResourceNotFound = ResourceRegistry.GetString("Error.ResourceNotFound");

        /// <summary>
        /// Byte format error
        /// </summary>
        [Display]
        internal static readonly string ErrorBytesFormat = ResourceRegistry.GetString("Error.BytesFormat");

        /// <summary>
        /// Exception log format
        /// </summary>
        [Display]
        internal static readonly string ExceptionTraceFormat = ResourceRegistry.GetString("Exception.TraceFormat");

        /// <summary>
        /// Type is not nullable
        /// </summary>
        [Display]
        internal static readonly string ErrorTypeNotNullable = ResourceRegistry.GetString("Error.TypeNotNullable");

        /// <summary>
        /// Expect value type
        /// </summary>
        [Display]
        internal static readonly string ErrorTypeNotValueType = ResourceRegistry.GetString("Error.TypeNotValueType");

        /// <summary>
        /// Not a enum type
        /// </summary>
        [Display]
        internal static readonly string ErrorTypeNotEnum = ResourceRegistry.GetString("Error.TypeNotEnum");

        /// <summary>
        /// Expected target have parse method
        /// </summary>
        [Display]
        internal static readonly string ErrorParseMethodNotExist = ResourceRegistry.GetString("Error.ParseMethodNotExist");

        /// <summary>
        /// Page parameter invalid
        /// </summary>
        [Display]
        internal static readonly string ErrorNumberShouldBePositive = ResourceRegistry.GetString("Error.NumberShouldBePositive");

        /// <summary>
        /// Key not found
        /// </summary>
        [Display]
        internal static readonly string ErrorKeyNotFound = ResourceRegistry.GetString("Error.KeyNotFound");

        /// <summary>
        /// Unknown database type
        /// </summary>
        [Display]
        internal static readonly string ErrorUnknownDbType = ResourceRegistry.GetString("Error.UnknownDbType");

        /// <summary>
        /// Unsupported type
        /// </summary>
        [Display]
        internal static readonly string ErrorUnsupportedType = ResourceRegistry.GetString("Error.UnsupportedType");

        /// <summary>
        /// Unsupported type with value displayed
        /// </summary>
        [Display]
        internal static readonly string ErrorUnsupportedTypeWithValue = ResourceRegistry.GetString("Error.UnsupportedTypeWithValue");

        /// <summary>
        /// Primary key not defined
        /// </summary>
        [Display]
        internal static readonly string ErrorPrimaryKeyNotFound = ResourceRegistry.GetString("Error.PrimaryKeyNotFound");
    }
}
