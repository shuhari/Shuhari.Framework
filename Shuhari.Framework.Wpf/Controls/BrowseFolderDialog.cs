using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using Shuhari.Framework.Utils;
using Shuhari.Framework.Win32;
using static Shuhari.Framework.Win32.UserApi;
using static Shuhari.Framework.Win32.ShellApi;

namespace Shuhari.Framework.Wpf.Controls
{
    /// <summary>
    /// Wrapper for folder browse dialog
    /// </summary>
    internal class BrowseFolderDialog
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="title"></param>
        /// <param name="initPath"></param>
        /// <param name="flags"></param>
        public BrowseFolderDialog(string title, string initPath,
            uint flags)
        {
            _initPath = initPath;
            _title = title;
            _flags = flags;
        }

        private readonly string _initPath;

        private readonly string _title;

        private readonly uint _flags;

        /// <summary>
        /// Selected path
        /// </summary>
        public string SelectedPath { get; private set; }

        /// <summary>
        /// Show dialog
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public bool ShowDialog(Window owner)
        {
            Expect.IsNotNull(owner, nameof(owner));

            StringBuilder sb = new StringBuilder(256);
            IntPtr bufferAddress = Marshal.AllocHGlobal(256); ;
            IntPtr pidl = IntPtr.Zero;
            BROWSEINFO bi = new BROWSEINFO();
            bi.hwndOwner = new WindowInteropHelper(owner).Handle;
            bi.pidlRoot = IntPtr.Zero;

            if (_title != null)
                bi.lpszTitle = _title;
            bi.ulFlags = _flags;
            bi.lpfn = OnBrowse;
            bi.lParam = IntPtr.Zero;
            bi.iImage = 0;
            SelectedPath = null;

            try
            {
                pidl = SHBrowseForFolder(ref bi);
                if (!SHGetPathFromIDList(pidl, bufferAddress))
                {
                }
                sb.Append(Marshal.PtrToStringAuto(bufferAddress));
            }
            finally
            {
                // Caller is responsible for freeing this memory.
                Marshal.FreeCoTaskMem(pidl);
            }

            SelectedPath = sb.ToString();
            return SelectedPath.IsNotBlank();
        }

        private int OnBrowse(IntPtr hWnd, int msg, IntPtr lp, IntPtr lpData)
        {
            switch (msg)
            {
                case BFFM_INITIALIZED: // Required to set initialPath
                    {
                        //Win32.SendMessage(new HandleRef(null, hWnd), BFFM_SETSELECTIONA, 1, lpData);
                        // Use BFFM_SETSELECTIONW if passing a Unicode string, i.e. native CLR Strings.
                        SendMessage(new HandleRef(null, hWnd), BFFM_SETSELECTIONW, 1, _initPath);
                        break;
                    }
                case BFFM_SELCHANGED:
                    {
                        IntPtr pathPtr = Marshal.AllocHGlobal((int)(260 * Marshal.SystemDefaultCharSize));
                        if (SHGetPathFromIDList(lp, pathPtr))
                            SendMessage(new HandleRef(null, hWnd), BFFM_SETSTATUSTEXTW, 0, pathPtr);
                        Marshal.FreeHGlobal(pathPtr);
                        break;
                    }
            }

            return 0;
        }
    }
}
