using System.Windows;
using Shuhari.Framework.Utils;
using static Shuhari.Framework.Win32.ShellApi;

namespace Shuhari.Framework.Wpf.Controls
{
    /// <summary>
    /// Extensions for window
    /// </summary>
    public static class WindowExtensions
    {
        /// <summary>
        /// Show browse folder dialog, and return selected path.
        /// </summary>
        /// <param name="window"></param>
        /// <param name="title">window caption</param>
        /// <param name="initPath"></param>
        /// <param name="flags">dialog styles</param>
        /// <returns></returns>
        public static string BrowseForFolder(this Window window, 
            string title = null, string initPath = null, 
            uint flags = BIF_NEWDIALOGSTYLE)
        {
            Expect.IsNotNull(window, nameof(window));

            var dlg = new BrowseFolderDialog(title, initPath, flags);
            if (dlg.ShowDialog(window))
                return dlg.SelectedPath;
            return null;
        }
    }
}
