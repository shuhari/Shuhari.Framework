using System.Diagnostics;
using System.IO;
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