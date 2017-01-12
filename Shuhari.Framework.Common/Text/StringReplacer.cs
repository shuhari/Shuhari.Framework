using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Text
{
    /// <summary>
    /// Describe options to replace one string to other
    /// </summary>
    public class StringReplacer
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="oldString"></param>
        /// <param name="newString"></param>
        public StringReplacer(string oldString, string newString)
        {
            Expect.IsNotBlank(oldString, nameof(oldString));
            Expect.IsNotNull(newString, nameof(newString));

            this.OldString = oldString;
            this.NewString = newString;
        }

        /// <summary>
        /// Old string
        /// </summary>
        public string OldString { get; private set; }

        /// <summary>
        /// New string
        /// </summary>
        public string NewString { get; private set; }

        /// <summary>
        /// apply
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Apply(string content)
        {
            Expect.IsNotNull(content, nameof(content));

            return content.Replace(OldString, NewString);
        }

        /// <summary>
        /// Apply string replace in file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        public void ApplyToFile(string filePath, Encoding encoding = null)
        {
            Expect.FileExist(filePath);
            encoding = encoding ?? EncodingUtil.DefaultEncoding;

            var oldContent = File.ReadAllText(filePath, encoding);
            var newContent = Apply(oldContent);
            if (newContent != oldContent)
            {
                File.Delete(filePath);
                File.WriteAllText(filePath, newContent, encoding);
            }
        }
    }
}
