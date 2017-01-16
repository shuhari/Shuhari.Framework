using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Web.Mvc
{
    /// <summary>
    /// Custom json result
    /// </summary>
    public class CustomJsonResult : JsonResult
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="data"></param>
        /// <param name="behavior"></param>
        public CustomJsonResult(object data, JsonRequestBehavior behavior)
        {
            this.Data = data;
            this.JsonRequestBehavior = behavior;
        }

        /// <summary>
        /// Execute result
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            Expect.IsNotNull(context, nameof(context));

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("JSON GET is not allowed");

            var response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            if (Data == null)
                return;

            var settings = new JsonSerializerSettings();
            var json = JsonConvert.SerializeObject(Data, settings);
            response.Write(json);
        }
    }
}
