using Shuhari.Framework.Utils;

namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Validation result item
    /// </summary>
    public class ValidationErrorDTO
    {
        /// <summary>
        /// Default initialize
        /// </summary>
        public ValidationErrorDTO()
        {
        }

        /// <summary>
        /// Initialize proeprties
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="message"></param>
        public ValidationErrorDTO(string prop, string message)
        {
            Expect.IsNotBlank(message, nameof(message));

            this.Property = prop;
            this.Message = message;
        }

        /// <summary>
        /// Property, or blank for overall error
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }
    }
}
