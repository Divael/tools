using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 摄像头控制
{
    public class Camera
    {

        public Camera(IntPtr handle, int width, int height)
        {
            mControlPtr = handle;
            mWidth = width;
            mHeight = height;
        }

        //定义委托
        public delegate void RecievedFrameEventHandler(byte[] data);
        public event RecievedFrameEventHandler RecievedFrame;

        private IntPtr lwndC;
        private IntPtr mControlPtr;
        private int mWidth;
        private int mHeight;

        private CapVideo.FrameEventHandler mFrameEventHandler;

        #region Camera Operation

        //开始采集
        public bool StartCamera()
        {
            byte[] lpszName = new byte[100];
            byte[] lpszVer = new byte[100];

            //获取设备信息
            CapVideo.capGetDriverDescriptionA(0, lpszName, 100, lpszVer, 100);
            //创建捕获窗口
            this.lwndC = CapVideo.capCreateCaptureWindowA(lpszName, CapVideo.WS_VISIBLE + CapVideo.WS_CHILD, 0, 0, mWidth, mHeight, mControlPtr, 0);

            //连接设备
            if (this.capDriverConnect(this.lwndC, 0))
            {
                //设置为预览模式，初始帧率为60
                this.capPreview(this.lwndC, true);
                this.capPreviewRate(this.lwndC, 60);

                //BitMap及其头信息
                CapVideo.BITMAPINFO bitmapinfo = new CapVideo.BITMAPINFO();
                bitmapinfo.bmiHeader.biSize = CapVideo.SizeOf(bitmapinfo.bmiHeader);
                bitmapinfo.bmiHeader.biWidth = 400;
                bitmapinfo.bmiHeader.biHeight = 200;
                bitmapinfo.bmiHeader.biPlanes = 1;
                bitmapinfo.bmiHeader.biBitCount = 24;

                //设置视频参数
                this.capSetVideoFormat(this.lwndC, ref bitmapinfo, CapVideo.SizeOf(bitmapinfo));

                //注册回调
                this.mFrameEventHandler = new CapVideo.FrameEventHandler(FrameCallBack);
                this.capSetCallbackOnFrame(this.lwndC, this.mFrameEventHandler);

                //设置窗口
                CapVideo.SetWindowPos(this.lwndC, 0, 0, 0, mWidth, mHeight, 6);
                return true;
            }
            else
            {
                return false;
            }
        }


        //停止采集
        public void CloseCamera()
        {
            this.capDriverDisconnect(this.lwndC);
        }

        //快照
        public bool SnapShot(string str)
        {
            bool blRes = CapVideo.SendMessage(this.lwndC, CapVideo.WM_CAP_GRAB_FRAME_NOSTOP, 0, 0);
            if (!blRes)
            {
                return blRes;
            }
            IntPtr ptr = Marshal.StringToHGlobalAnsi(str);
            //Marshal.FreeHGlobal(ptr);
            return CapVideo.SendMessage(this.lwndC, CapVideo.WM_CAP_FILE_SAVEDIBA, 0, ptr.ToInt32());

        }

        //收到帧数据时会执行此回调函数
        private void FrameCallBack(IntPtr lwnd, IntPtr lpVHdr)
        {
            CapVideo.VIDEOHDR videoHeader = new CapVideo.VIDEOHDR();
            byte[] VideoData;
            //获取帧头信息图像数据地址
            videoHeader = (CapVideo.VIDEOHDR)CapVideo.GetStructure(lpVHdr, videoHeader);
            VideoData = new byte[videoHeader.dwBytesUsed];
            //复制图像数据
            CapVideo.Copy(videoHeader.lpData, VideoData);
            if (this.RecievedFrame != null)
                this.RecievedFrame(VideoData);
        }

        //连接设备
        private bool capDriverConnect(IntPtr lwnd, short i)
        {
            return CapVideo.SendMessage(lwnd, CapVideo.WM_CAP_DRIVER_CONNECT, i, 0);
        }

        //断开连接
        private bool capDriverDisconnect(IntPtr lwnd)
        {
            return CapVideo.SendMessage(lwnd, CapVideo.WM_CAP_DRIVER_DISCONNECT, 0, 0);
        }

        //设置为预览模式
        private bool capPreview(IntPtr lwnd, bool f)
        {
            return CapVideo.SendMessage(lwnd, CapVideo.WM_CAP_SET_PREVIEW, f, 0);
        }

        //设置预览帧率
        private bool capPreviewRate(IntPtr lwnd, short wMS)
        {
            return CapVideo.SendMessage(lwnd, CapVideo.WM_CAP_SET_PREVIEWRATE, wMS, 0);
        }

        //设置回调函数
        private bool capSetCallbackOnFrame(IntPtr lwnd, CapVideo.FrameEventHandler lpProc)
        {
            return CapVideo.SendMessage(lwnd, CapVideo.WM_CAP_SET_CALLBACK_FRAME, 0, lpProc);
        }

        //设置视频格式
        private bool capSetVideoFormat(IntPtr hCapWnd, ref CapVideo.BITMAPINFO BmpFormat, int CapFormatSize)
        {
            return CapVideo.SendMessage(hCapWnd, CapVideo.WM_CAP_SET_VIDEOFORMAT, CapFormatSize, ref BmpFormat);
        }
        #endregion

    }
//--------------------- 
//作者：saloon_yuan
//来源：CSDN
//原文：https://blog.csdn.net/saloon_yuan/article/details/9259505 
//版权声明：本文为博主原创文章，转载请附上博文链接！
}
