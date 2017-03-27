using System.IO;
using System.Text;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.IO
{
    /// <summary>
    /// Extensions for file directory/file
    /// </summary>
    public static class FileSystemExtensions
    {
        /// <summary>
        /// Get changed attributes
        /// </summary>
        /// <param name="original"></param>
        /// <param name="toAdd"></param>
        /// <param name="toRemove"></param>
        /// <returns></returns>
        public static FileAttributes GetNewAttributes(FileAttributes original,
            FileAttributes toAdd = 0,
            FileAttributes toRemove = 0)
        {
            var result = original;
            result |= toAdd;
            result &= ~toRemove;
            return result;
        }

        /// <summary>
        /// Clear hidden/system/readonly attributes for directory/file
        /// </summary>
        /// <param name="fsi"></param>
        /// <param name="recursive">Clear attributes recursively in subfolders if set</param>
        public static void ClearAttributes(this FileSystemInfo fsi, bool recursive = false)
        {
            Expect.IsNotNull(fsi, nameof(fsi));

            var newAttr = GetNewAttributes(fsi.Attributes, 0,
                FileAttributes.Hidden | FileAttributes.System | FileAttributes.ReadOnly);
            if (newAttr != fsi.Attributes)
                fsi.Attributes = newAttr;

            if (recursive)
            {
                var di = fsi as DirectoryInfo;
                if (di != null)
                {
                    foreach (var subDi in di.GetDirectories())
                        ClearAttributes(subDi, true);
                    foreach (var fi in di.GetFiles())
                        ClearAttributes(fi);
                }
            }
        }

        /// <summary>
        /// Create directory if not exist
        /// </summary>
        /// <param name="di"></param>
        public static void Ensure(this DirectoryInfo di)
        {
            Expect.IsNotNull(di, nameof(di));

            if (!di.Exists)
                Directory.CreateDirectory(di.FullName);
        }

        /// <summary>
        /// Create parent directory for specified file, if not exist
        /// </summary>
        /// <param name="fi"></param>
        /// <returns></returns>
        public static FileInfo EnsureDirectory(this FileInfo fi)
        {
            Expect.IsNotNull(fi, nameof(fi));

            var dirName = Path.GetDirectoryName(fi.FullName);
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            return fi;
        }

        /// <summary>
        /// Write text
        /// </summary>
        /// <param name="fi"></param>
        /// <param name="text"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static FileInfo WriteText(this FileInfo fi, string text, Encoding encoding = null)
        {
            Expect.IsNotNull(fi, nameof(fi));
            Expect.IsNotNull(text, nameof(text));
            encoding = encoding ?? Encoding.UTF8;

            File.WriteAllText(fi.FullName, text, encoding);
            return fi;
        }

        /// <summary>
        /// Write file bytes
        /// </summary>
        /// <param name="fi"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static FileInfo WriteBytes(this FileInfo fi, byte[] bytes)
        {
            Expect.IsNotNull(fi, nameof(fi));
            Expect.IsNotNull(bytes, nameof(bytes));

            File.WriteAllBytes(fi.FullName, bytes);
            return fi;
        }

        /// <summary>
        /// Clear attributes before delete of directory
        /// to avoid failure
        /// </summary>
        /// <param name="di"></param>
        public static void ForceDelete(this DirectoryInfo di)
        {
            Expect.IsNotNull(di, nameof(di));

            di.Refresh();
            di.ClearAttributes(true);
            di.Delete(true);
        }
    }
}