namespace Shuhari.Framework.Resources
{
    /// <summary>
    /// Define framework used strings
    /// </summary>
    internal static class FrameworkStrings
    {
        /// <summary>
        /// Expect parameter to be not null
        /// </summary>
        internal const string EXPECT_NOTNULL = "Parameter {0} should not be null.";

        /// <summary>
        /// Expect parameter to be not blank.
        /// </summary>
        internal const string EXPECT_NOTBLANK = "Parameter {0} should not be a blank string.";

        /// <summary>
        /// Expect result is true
        /// </summary>
        internal const string EXPECT_TRUE = "The result should evaluate to true.";

        /// <summary>
        /// Expect file exist
        /// </summary>
        internal const string EXPECT_FILEEXIST = "File not exist: {0}";

        /// <summary>
        /// Expect directory exist
        /// </summary>
        internal const string EXPECT_DIREXIST = "Directory not exist: {0}";

        /// <summary>
        /// Resource not found
        /// </summary>
        internal const string ERROR_RES_NOT_FOUND = "Could not found resource {1} in assembly {0}";

        /// <summary>
        /// Byte format error
        /// </summary>
        internal const string ERROR_BYTES_FORMAT = "Bytes string should have even length";

        /// <summary>
        /// Exception log format
        /// </summary>
        internal const string EXCEPTION_TRACE_FORMAT
            = "Exception raised at {1:yyyy-MM-dd HH:mm:ss}{0}  Messsage: {2}{0}  Type: {3}{0}  Stacktrace: {4}{0}{0}";

        /// <summary>
        /// Type is not nullable
        /// </summary>
        internal const string ERROR_NOT_NULLABLE = "Type {0} is not nullable";

        /// <summary>
        /// Expect value type
        /// </summary>
        internal const string ERROR_EXPECT_VALUETYPE = "Type {0} is not a value type";

        /// <summary>
        /// Not a enum type
        /// </summary>
        internal const string ERROR_NOT_ENUM = "Type {0} is not a enum";

        /// <summary>
        /// Expected target have parse method
        /// </summary>
        internal const string ERROR_PARSE_METHOD_EXPECTED = "Expected static {0} Parse(string) method in type {0}, but could not found one";

        /// <summary>
        /// Page parameter invalid
        /// </summary>
        internal const string ERROR_PAGE_INVALID = "page should be >=0";

        /// <summary>
        /// PerPage parameter invalid
        /// </summary>
        internal const string ERROR_PERPAGE_INVALID = "perPage should be >= 0";

        /// <summary>
        /// Number should be great or equal than zero
        /// </summary>
        internal const string ERROR_NUMBER_POSITIVE = "Number should be >= 0";
    }
}
