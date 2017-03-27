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
        internal static string ExpectNotNull => ResourceRegistry.GetString("Expect.NotNull");

        /// <summary>
        /// Expect parameter to be not blank.
        /// </summary>
        [Display]
        internal static string ExpectNotBlank => ResourceRegistry.GetString("Expect.NotBlank");

        /// <summary>
        /// Expect result is true
        /// </summary>
        [Display]
        internal static string ExpectTrue => ResourceRegistry.GetString("Expect.True");

        /// <summary>
        /// Expect file exist
        /// </summary>
        [Display]
        internal static string ExpectFileExist => ResourceRegistry.GetString("Expect.FileExist");

        /// <summary>
        /// Expect directory exist
        /// </summary>
        [Display]
        internal static string ExpectDirectoryExist => ResourceRegistry.GetString("Expect.DirectoryExist");

        /// <summary>
        /// Resource not found
        /// </summary>
        [Display]
        internal static string ErrorResourceNotFound => ResourceRegistry.GetString("Error.ResourceNotFound");

        /// <summary>
        /// Byte format error
        /// </summary>
        [Display]
        internal static string ErrorBytesFormat => ResourceRegistry.GetString("Error.BytesFormat");

        /// <summary>
        /// Exception log format
        /// </summary>
        [Display]
        internal static string ExceptionTraceFormat => ResourceRegistry.GetString("Exception.TraceFormat");

        /// <summary>
        /// Type is not nullable
        /// </summary>
        [Display]
        internal static string ErrorTypeNotNullable => ResourceRegistry.GetString("Error.TypeNotNullable");

        /// <summary>
        /// Expect value type
        /// </summary>
        [Display]
        internal static string ErrorTypeNotValueType => ResourceRegistry.GetString("Error.TypeNotValueType");

        /// <summary>
        /// Not a enum type
        /// </summary>
        [Display]
        internal static string ErrorTypeNotEnum => ResourceRegistry.GetString("Error.TypeNotEnum");

        /// <summary>
        /// Expected target have parse method
        /// </summary>
        [Display]
        internal static string ErrorParseMethodNotExist => ResourceRegistry.GetString("Error.ParseMethodNotExist");

        /// <summary>
        /// Page parameter invalid
        /// </summary>
        [Display]
        internal static string ErrorNumberShouldBePositive => ResourceRegistry.GetString("Error.NumberShouldBePositive");

        /// <summary>
        /// Key not found
        /// </summary>
        [Display]
        internal static string ErrorKeyNotFound => ResourceRegistry.GetString("Error.KeyNotFound");

        /// <summary>
        /// Unknown database type
        /// </summary>
        [Display]
        internal static string ErrorUnknownDbType => ResourceRegistry.GetString("Error.UnknownDbType");

        /// <summary>
        /// Unsupported type
        /// </summary>
        [Display]
        internal static string ErrorUnsupportedType => ResourceRegistry.GetString("Error.UnsupportedType");

        /// <summary>
        /// Unsupported type with value displayed
        /// </summary>
        [Display]
        internal static string ErrorUnsupportedTypeWithValue => ResourceRegistry.GetString("Error.UnsupportedTypeWithValue");

        /// <summary>
        /// Primary key not defined
        /// </summary>
        [Display]
        internal static string ErrorPrimaryKeyNotFound => ResourceRegistry.GetString("Error.PrimaryKeyNotFound");

        /// <summary>
        /// Mapping information not found
        /// </summary>
        [Display]
        public static string ErrorMappingNotFound => ResourceRegistry.GetString("Error.MapperNotFound");

        /// <summary>
        /// Field count different than reader count
        /// </summary>
        [Display]
        public static string ErrorFieldCountNotMatch => ResourceRegistry.GetString("Error.FieldCountNotMatch");

        /// <summary>
        /// Field count different than reader count
        /// </summary>
        [Display]
        public static string ErrorPrimaryKeyTypeUnmatch => ResourceRegistry.GetString("Error.PrimaryKeyTypeUnmatch");

        /// <summary>
        /// No fields specified
        /// </summary>
        [Display]
        public static string ErrorNoFieldsSepecified => ResourceRegistry.GetString("Error.NoFieldsSepecified");

        /// <summary>
        /// Property value is null
        /// </summary>
        [Display]
        public static string ErrorPropertyValueNull => ResourceRegistry.GetString("Error.PropertyValueNull");

        /// <summary>
        /// DbContext has no repository
        /// </summary>
        [Display]
        public static string ErrorNoRepository => ResourceRegistry.GetString("Error.NoRepository");

        /// <summary>
        /// Repository has no session
        /// </summary>
        [Display]
        public static string ErrorNoSession => ResourceRegistry.GetString("Error.NoSession");

        /// <summary>
        /// A db transaction already inplace
        /// </summary>
        [Display]
        public static string ErrorTransactionAlreadyExist => ResourceRegistry.GetString("Error.TransactionAlreadyExist");

        /// <summary>
        /// Transaction is managed by other session
        /// </summary>
        [Display]
        public static string ErrorTransactionBelongToOther => ResourceRegistry.GetString("Error.TransactionBelongToOther");
    }
}
