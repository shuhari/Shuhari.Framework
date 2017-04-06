using System;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Wpf.Models
{
    /// <summary>
    /// Log item
    /// </summary>
    public class LogItem
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="type"></param>
        /// <param name="time"></param>
        /// <param name="content"></param>
        public LogItem(LogType type, DateTime time, string content)
        {
            Expect.IsNotNull(content, nameof(content));

            this.Type = type;
            this.Time = time;
            this.Content = content;
        }

        /// <summary>
        /// Type
        /// </summary>
        public LogType Type { get; private set; }

        /// <summary>
        /// Time
        /// </summary>
        public DateTime Time { get; private set; }

        /// <summary>
        /// content
        /// </summary>
        public string Content { get; private set; }
    }
}
