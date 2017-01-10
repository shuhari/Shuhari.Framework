using System.IO;
using System.Text;
using System.Windows.Markup;
using System.Xml;

namespace Shuhari.Framework.Utils
{
    /// <summary>
    /// Helper to process xaml
    /// </summary>
    public static class XamlUtil
    {
        /// <summary>
        /// Serialize object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="formatted"></param>
        public static string Serialize(object obj, bool formatted = false)
        {
            Expect.IsNotNull(obj, nameof(obj));

            var xmlSettings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),
                ConformanceLevel = ConformanceLevel.Document,
            };
            if (formatted)
            {
                xmlSettings.Indent = true;
                xmlSettings.IndentChars = "    ";
            }
            using (var stream = new MemoryStream())
            using (var writer = XmlWriter.Create(stream, xmlSettings))
            {
                XamlWriter.Save(obj, new XamlDesignerSerializationManager(writer));
                return xmlSettings.Encoding.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xaml"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xaml)
            where T: class
        {
            Expect.IsNotNull(xaml, nameof(xaml));

            using (var reader = new StringReader(xaml))
            {
                var result = (T)XamlReader.Load(XmlReader.Create(reader));
                return result;
            }
        }
    }

}