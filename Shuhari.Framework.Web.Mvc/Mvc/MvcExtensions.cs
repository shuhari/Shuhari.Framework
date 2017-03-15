using System.Linq;
using System.Web.Mvc;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Web.Mvc
{
    /// <summary>
    /// Extensions for MVC class
    /// </summary>
    public static class MvcExtensions
    {
        /// <summary>
        /// Key of temp message
        /// </summary>
        private const string KEY_TEMP_MSG = "__message";

        /// <summary>
        /// Get temp message
        /// </summary>
        /// <param name="tempData"></param>
        /// <returns></returns>
        public static string GetTempMessage(this TempDataDictionary tempData)
        {
            Expect.IsNotNull(tempData, nameof(tempData));

            return tempData.ContainsKey(KEY_TEMP_MSG) ? (string)tempData[KEY_TEMP_MSG] : null;
        }

        /// <summary>
        /// Set temp message
        /// </summary>
        /// <param name="tempData"></param>
        /// <param name="value"></param>
        public static void SetTempMessage(this TempDataDictionary tempData, string value)
        {
            Expect.IsNotNull(tempData, nameof(tempData));

            tempData[KEY_TEMP_MSG] = value;
        }

        /// <summary>
        /// Get all errors in ModelState
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static ModelError[] GetErrors(this ModelStateDictionary modelState)
        {
            Expect.IsNotNull(modelState, nameof(modelState));

            return modelState.Values.SelectMany(x => x.Errors)
                .ToArray();
        }

        /// <summary>
        /// Get first error in ModelState, or null if no error
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static ModelError GetFirstError(this ModelStateDictionary modelState)
        {
            Expect.IsNotNull(modelState, nameof(modelState));

            return GetErrors(modelState).FirstOrDefault();
        }
    }
}