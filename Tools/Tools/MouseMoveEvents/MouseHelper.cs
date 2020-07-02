using System.Drawing;
using System.Runtime.InteropServices;

namespace Tools
{
    /// <summary>
    /// 关于鼠标移动的操作
    /// <para>GetMousePoint  获取当前屏幕鼠标位置</para>
    /// </summary>
    public class MouseHelper
    {
        /// <summary>
        /// 获取当前屏幕鼠标位置
        /// </summary>
        /// <returns>Point System.Drawing;</returns>
        public static Point GetMousePoint()
        {
            MPoint mpt = new MPoint();
            GetCursorPos(out mpt);
            Point p = new Point(mpt.X, mpt.Y);
            return p;
        }

        /// <summary>
        /// 判断鼠标是否移动
        /// </summary>
        /// <param name="currectPosition">Point</param>
        /// <returns></returns>
        public bool HaveUsedTo(Point currectPosition)
        {
            Point point = GetMousePoint();
            if (point == currectPosition)
            {
                return false;
            }
            currectPosition = point;
            return true;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool GetCursorPos(out MPoint mpt);

        [StructLayout(LayoutKind.Sequential)]
        private struct MPoint
        {
            public int X;
            public int Y;

            public MPoint(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }
    }
}
