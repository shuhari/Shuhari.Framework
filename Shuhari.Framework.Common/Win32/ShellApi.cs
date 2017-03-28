using System;
using System.Runtime.InteropServices;
using static Shuhari.Framework.Win32.UserApi;

namespace Shuhari.Framework.Win32
{
#pragma warning disable 1591
    /// <summary>
    /// Wrap shell api
    /// </summary>
    public static class ShellApi
    {
        public const int BFFM_INITIALIZED = 1;

        public const int BFFM_SELCHANGED = 2;

        public const int BFFM_VALIDATEFAILEDA = 3;

        public const int BFFM_VALIDATEFAILEDW = 4;

        public const int BFFM_IUNKNOWN = 5;

        public const int BFFM_SETSTATUSTEXTA = WM_USER + 100;

        public const int BFFM_ENABLEOK = WM_USER + 101;

        public const int BFFM_SETSELECTIONA = WM_USER + 102;

        public const int BFFM_SETSELECTIONW = WM_USER + 103;

        public const int BFFM_SETSTATUSTEXTW = WM_USER + 104;

        public const int BFFM_SETOKTEXT = WM_USER + 105;

        public const int BFFM_SETEXPANDED = WM_USER + 106;

        public const uint BIF_RETURNONLYFSDIRS = 0x0001;
        
        public const uint BIF_DONTGOBELOWDOMAIN = 0x0002;
        
        public const uint BIF_STATUSTEXT = 0x0004;
        
        public const uint BIF_RETURNFSANCESTORS = 0x0008;
        
        public const uint BIF_EDITBOX = 0x0010;
        
        public const uint BIF_VALIDATE = 0x0020;
        
        public const uint BIF_NEWDIALOGSTYLE = 0x0040;
        
        public const uint BIF_USENEWUI = 0x0040 + 0x0010;
        
        public const uint BIF_BROWSEINCLUDEURLS = 0x0080;
        
        public const uint BIF_UAHINT = 0x0100;
        
        public const uint BIF_NONEWFOLDERBUTTON = 0x0200;
        
        public const uint BIF_NOTRANSLATETARGETS = 0x0400;

        public const uint BIF_BROWSEFORCOMPUTER = 0x1000;

        public const uint BIF_BROWSEFORPRINTER = 0x2000;

        public const uint BIF_BROWSEINCLUDEFILES = 0x4000;

        public const uint BIF_SHAREABLE = 0x8000;

        [DllImport("shell32.dll")]
        public static extern IntPtr SHBrowseForFolder(ref BROWSEINFO lpbi);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern bool SHGetPathFromIDList(IntPtr pidl, IntPtr pszPath);

        public delegate int BrowseCallBackProc(IntPtr hwnd, int msg, IntPtr lp, IntPtr wp);

        public struct BROWSEINFO
        {
            public IntPtr hwndOwner;

            public IntPtr pidlRoot;

            public string pszDisplayName;

            public string lpszTitle;

            public uint ulFlags;

            public BrowseCallBackProc lpfn;

            public IntPtr lParam;

            public int iImage;
        }
    }
#pragma warning restore 1591
}
