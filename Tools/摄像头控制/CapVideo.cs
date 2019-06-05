using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace 摄像头控制
{
    public class CapVideo
    {
        #region DLL Import Method
        [DllImport("avicap32.dll")]
        public static extern IntPtr capCreateCaptureWindowA(byte[] lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int nID);
        [DllImport("avicap32.dll")]
        public static extern bool capGetDriverDescriptionA(short wDriver, byte[] lpszName, int cbName, byte[] lpszVer, int cbVer);
        [DllImport("avicap32.dll")]
        public static extern int capGetVideoFormat(IntPtr hWnd, IntPtr psVideoFormat, int wSize);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, short wParam, int lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, short wParam, FrameEventHandler lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, ref BITMAPINFO lParam);
        [DllImport("User32.dll")]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        #endregion

        #region public const Fields
        public const int WM_USER = 0x400;
        public const int WS_CHILD = 0x40000000;
        public const int WS_VISIBLE = 0x10000000;
        public const int SWP_NOMOVE = 0x2;
        public const int SWP_NOZORDER = 0x4;
        public const int WM_CAP_DRIVER_CONNECT = WM_USER + 10;
        public const int WM_CAP_DRIVER_DISCONNECT = WM_USER + 11;
        public const int WM_CAP_SET_CALLBACK_FRAME = WM_USER + 5;
        public const int WM_CAP_SET_PREVIEW = WM_USER + 50;
        public const int WM_CAP_SET_PREVIEWRATE = WM_USER + 52;
        public const int WM_CAP_SET_VIDEOFORMAT = WM_USER + 45;
        public const int WM_CAP_GRAB_FRAME_NOSTOP = WM_USER + 61;
        public const int WM_CAP_GRAB_FRAME = WM_USER + 60;
        public const int WM_CAP_FILE_SAVEDIBA = WM_USER + 25;
        #endregion

        #region Structures
        [StructLayout(LayoutKind.Sequential)]
        public struct VIDEOHDR
        {
            [MarshalAs(UnmanagedType.I4)]
            public int lpData;
            [MarshalAs(UnmanagedType.I4)]
            public int dwBufferLength;
            [MarshalAs(UnmanagedType.I4)]
            public int dwBytesUsed;
            [MarshalAs(UnmanagedType.I4)]
            public int dwTimeCaptured;
            [MarshalAs(UnmanagedType.I4)]
            public int dwUser;
            [MarshalAs(UnmanagedType.I4)]
            public int dwFlags;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public int[] dwReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFOHEADER
        {
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biSize;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biWidth;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biHeight;
            [MarshalAs(UnmanagedType.I2)]
            public short biPlanes;
            [MarshalAs(UnmanagedType.I2)]
            public short biBitCount;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biCompression;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biSizeImage;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biXPelsPerMeter;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biYPelsPerMeter;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biClrUsed;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biClrImportant;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFO
        {
            [MarshalAs(UnmanagedType.Struct, SizeConst = 40)]
            public BITMAPINFOHEADER bmiHeader;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public Int32[] bmiColors;
        }
        #endregion

        #region Public Method
        public delegate void FrameEventHandler(IntPtr lwnd, IntPtr lpVHdr);

        public static object GetStructure(IntPtr ptr, ValueType structure)
        {
            return Marshal.PtrToStructure(ptr, structure.GetType());
        }

        public static object GetStructure(int ptr, ValueType structure)
        {
            return GetStructure(new IntPtr(ptr), structure);
        }

        public static void Copy(IntPtr ptr, byte[] data)
        {
            Marshal.Copy(ptr, data, 0, data.Length);
        }

        public static void Copy(int ptr, byte[] data)
        {
            Copy(new IntPtr(ptr), data);
        }

        public static int SizeOf(object structure)
        {
            return Marshal.SizeOf(structure);
        }
        #endregion
    }
//--------------------- 
//作者：saloon_yuan
//来源：CSDN
//原文：https://blog.csdn.net/saloon_yuan/article/details/9259505 
//版权声明：本文为博主原创文章，转载请附上博文链接！
}
