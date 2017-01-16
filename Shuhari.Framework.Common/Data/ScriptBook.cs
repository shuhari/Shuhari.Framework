using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Shuhari.Framework.Resources;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Store sql scripts in file aparted from code.
    /// Multiple script are splitted by section in following format:
    /// <code>
    /// --[script]--
    /// sql line1
    /// sql line2
    /// 
    /// --[script2]--
    /// sql line1
    /// sql line2
    /// ...
    /// </code>
    /// </summary>
    public class ScriptBook
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public ScriptBook()
        {
            _dict = new Dictionary<string, string>();
        }

        private Dictionary<string, string> _dict;

        /// <summary>
        /// Load from resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="encoding"></param>
        public void LoadResource(AssemblyResource resource, Encoding encoding = null)
        {
            Expect.IsNotNull(resource, nameof(resource));
            encoding = encoding ?? EncodingUtil.DefaultEncoding;

            string content = resource.ReadAllText(encoding);
            LoadString(content);
        }

        /// <summary>
        /// 按照键值访问sql语句
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                Expect.IsNotBlank(key, nameof(key));

                if (!_dict.ContainsKey(key))
                    throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture,
                        "Key not found: {0}", key));
                return _dict[key];
            }
        }

        /// <summary>
        /// Load from string
        /// </summary>
        /// <param name="content"></param>
        public void LoadString(string content)
        {
            Expect.IsNotNull(content, nameof(content));

            var lines = content.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string name = null;
            var nameLines = new List<string>();
            foreach (var line in lines)
            {
                string lineStr = line.Trim();

                if (lineStr.IsBlank())
                    continue;
                if (lineStr.StartsWith("/*", StringComparison.Ordinal) && lineStr.EndsWith("*/", StringComparison.Ordinal))
                    continue;

                var nextName = ParseName(lineStr);
                if (nextName == null && name != null)
                {
                    nameLines.Add(lineStr);
                }
                else if (nextName != null && name == null)
                {
                    name = nextName;
                }
                else if (nextName != null && name != null)
                {
                    string sql = string.Join(Environment.NewLine, nameLines);
                    _dict[name] = sql;
                    name = nextName;
                    nameLines.Clear();
                }
            }

            if (name != null)
            {
                string sql = string.Join(Environment.NewLine, nameLines);
                _dict[name] = sql;
            }
        }

        private static string ParseName(string line)
        {
            var re = new Regex(@"^--\[(.+)\]--$");
            var match = re.Match(line);
            if (match.Success)
                return match.Groups[1].Value;
            else
                return null;
        }
    }
}
