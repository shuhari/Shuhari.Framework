using System;
using System.CodeDom.Compiler;
using System.Linq;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Runtime.CompilerServices
{
    /// <summary>
    /// Compiler exception
    /// </summary>
    public sealed class CompilerException : ApplicationException
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="errors"></param>
        public CompilerException(CompilerErrorCollection errors)
            : base(GetErrorMessages(errors))
        {
            Expect.IsNotNull(errors, nameof(errors));

            Errors = errors.OfType<CompilerError>().ToArray();
        }

        /// <summary>
        /// Collection of errors
        /// </summary>
        public CompilerError[] Errors { get; }

        private static string GetErrorMessages(CompilerErrorCollection errors)
        {
            return string.Join(Environment.NewLine, errors.OfType<CompilerError>()
                .Select(GetErrorMessage));
        }

        private static string GetErrorMessage(CompilerError error)
        {
            return error.ErrorText;
        }
    }
}
