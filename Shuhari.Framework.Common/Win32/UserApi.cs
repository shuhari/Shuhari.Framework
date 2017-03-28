using System;
using System.Runtime.InteropServices;

namespace Shuhari.Framework.Win32
{
#pragma warning disable 1591
    /// <summary>
    /// User api
    /// </summary>
    public static class UserApi
    {
        public const int WM_USER = 0x400;

        [DllImport("user32.dll", PreserveSig = true)]
        public static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, string lParam);
    }
#pragma warning restore 1591
}
