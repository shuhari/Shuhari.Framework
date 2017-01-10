namespace Shuhari.Framework.Resources
{
    /// <summary>
    /// Define framework used strings
    /// </summary>
    public static class FrameworkStrings
    {
        /// <summary>
        /// Expect parameter to be not null
        /// </summary>
        public const string EXPECT_NOTNULL = "Parameter {0} should not be null.";

        /// <summary>
        /// Expect parameter to be not blank.
        /// </summary>
        public const string EXPECT_NOTBLANK = "Parameter {0} should not be a blank string.";

        /// <summary>
        /// Expect result is true
        /// </summary>
        public const string EXPECT_TRUE = "The result should evaluate to true.";

        /// <summary>
        /// Expect file exist
        /// </summary>
        public const string EXPECT_FILEEXIST = "File not exist: {0}";

        /// <summary>
        /// Expect directory exist
        /// </summary>
        public const string EXPECT_DIREXIST = "Directory not exist: {0}";

        /// <summary>
        /// Resource not found
        /// </summary>
        public const string ERROR_RES_NOT_FOUND = "Could not found resource {1} in assembly {0}";

        /// <summary>
        /// Byte format error
        /// </summary>
        public const string ERROR_BYTES_FORMAT = "Bytes string should have even length";

        /// <summary>
        /// Exception log format
        /// </summary>
        public const string EXCEPTION_TRACE_FORMAT
            = "Exception raised at {1:yyyy-MM-dd HH:mm:ss}{0}  Messsage: {2}{0}  Type: {3}{0}  Stacktrace: {4}{0}{0}";

        /// <summary>
        /// Type is not nullable
        /// </summary>
        public const string ERROR_NOT_NULLABLE = "Type {0} is not nullable";

        /// <summary>
        /// Expect value type
        /// </summary>
        public const string ERROR_EXPECT_VALUETYPE = "Type {0} is not a value type";
    }
}
