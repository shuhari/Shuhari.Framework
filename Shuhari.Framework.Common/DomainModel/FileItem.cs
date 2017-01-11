using System.Linq;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Save file name with content
    /// </summary>
    public class FileItem
    {
        /// <summary>
        /// Default initialize
        /// </summary>
        public FileItem()
        {
        }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mimeType"></param>
        /// <param name="content"></param>
        public FileItem(string name, string mimeType, byte[] content)
        {
            Expect.IsNotBlank(name, nameof(name));
            Expect.IsNotNull(content, nameof(content));

            this.Name = name;
            this.MimeType = mimeType;
            this.Content = content;
        }

        /// <summary>
        /// File name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mime type
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public byte[] Content { get; set; }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var other = obj as FileItem;
            if (other == null)
                return false;
            return Name == other.Name &&
                MimeType == other.MimeType &&
                Content.SequenceEqual(other.Content);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        /// <summary>
        /// Check equality
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator==(FileItem lhs, FileItem rhs)
        {
            return object.Equals(lhs, rhs);
        }

        /// <summary>
        /// Check unequality
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator!=(FileItem lhs, FileItem rhs)
        {
            return !object.Equals(lhs, rhs);
        }
    }
}
