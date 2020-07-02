using System.Runtime.InteropServices;

namespace System.WinSystem
{
    public class WinApi
    {
        [DllImport("winmm")]
        public static extern void timeEndPeriod(int t);

        [DllImport("winmm")]
        public static extern void timeBeginPeriod(int t);

        [DllImport("gdi32")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32")]
        public static extern bool DeleteObject(int hObject);

        [DllImport("User32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIdex);

        [DllImport("User32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("User32.dll")]
        public static extern void keybd_event(byte bVK, byte bScan, Int32 dwFlags, int dwExtraInfo);

        [DllImport("User32.dll")]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll", EntryPoint = "GetKeyboardState")]
        public static extern int GetKeyboardState(byte[] pbKeyState);
    }
}
