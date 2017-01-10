using System.Collections.Generic;
using System.IO;
using System.Text;
using NVelocity;
using NVelocity.App;
using Shuhari.Framework.Resources;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Text.Templating
{
    /// <summary>
    /// <see cref="ITemplate"/> implementation using NVelocity engine
    /// </summary>
    public class VelocityTemplate : ITemplate
    {
        /// <summary>
        /// Create template from file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static ITemplate FromFile(string filePath, Encoding encoding = null)
        {
            Expect.FileExist(filePath);
            encoding = encoding ?? EncodingUtil.DefaultEncoding;

            string templateText = File.ReadAllText(filePath, encoding);
            return new VelocityTemplate(templateText);
        }

        /// <summary>
        /// Create from template text
        /// </summary>
        /// <param name="templateText">Template text</param>
        /// <returns></returns>
        public static ITemplate FromString(string templateText)
        {
            Expect.IsNotNull(templateText, nameof(templateText));

            return new VelocityTemplate(templateText);
        }

        /// <summary>
        /// Create from assembly resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static ITemplate FromResource(AssemblyResource resource, Encoding encoding = null)
        {
            Expect.IsNotNull(resource, nameof(resource));

            var templateText = resource.ReadAllText(encoding);
            return FromString(templateText);
        }

        /// <summary>
        /// Initialize from template text
        /// </summary>
        /// <param name="templateText"></param>
        /// <remarks>This ctor is not intented to call directory. Use fromXXX methods instead.</remarks>
        private VelocityTemplate(string templateText)
        {
            Expect.IsNotNull(templateText, nameof(templateText));

            _templateText = templateText;
            _parameters = new Dictionary<string, object>();

            _engine = new VelocityEngine();
            _engine.Init();
        }

        private string _templateText;
        private VelocityEngine _engine;
        private Dictionary<string, object> _parameters;

        /// <inheritdoc />
        public ITemplate Set(string paramName, object value)
        {
            Expect.IsNotBlank(paramName, nameof(paramName));

            _parameters[paramName] = value;
            return this;
        }

        /// <inheritdoc />
        public string Evaluate()
        {
            var context = new VelocityContext();
            foreach (var kv in _parameters)
            {
                context.Put(kv.Key, kv.Value);
            }
            var writer = new StringWriter();
            _engine.Evaluate(context, writer, "log", _templateText);
            return writer.GetStringBuilder().ToString();
        }
    }
}
