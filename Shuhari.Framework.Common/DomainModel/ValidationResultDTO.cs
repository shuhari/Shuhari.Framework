using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Validation result
    /// </summary>
    public class ValidationResultDTO
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public ValidationResultDTO()
        {
            Success = true;
            Errors = new ValidationErrorDTO[0];
        }

        /// <summary>
        /// If success
        /// </summary>
        [DefaultValue(true)]
        public bool Success { get; set; }

        /// <summary>
        /// Errors
        /// </summary>
        public ValidationErrorDTO[] Errors { get; set; }

        /// <summary>
        /// Optional redirect to url
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Set result to error
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="message"></param>
        public void SetError(string propName, string message)
        {
            Expect.IsNotBlank(message, nameof(message));

            SetErrors(new[] { new ValidationErrorDTO(propName, message) });
        }

        /// <summary>
        /// Set errors
        /// </summary>
        /// <param name="errors"></param>
        public void SetErrors(IEnumerable<ValidationErrorDTO> errors)
        {
            Expect.IsNotNull(errors, nameof(errors));

            this.Success = errors.Count() == 0;
            this.Errors = errors.ToArray();
        }
    }
}
