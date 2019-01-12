using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Tools
{
    /// <summary>
    /// 鼠标移动，检测是否移动
    /// <para>GetMousePoint() 获取鼠标坐标</para>
    /// <para>HaveUsedTo() 判断鼠标是否移动</para>
    /// </summary>
    public class MouseMoveEventsHelper
    {
        private event Action DoEvent;

        public MouseMoveEventsHelper(Action action) {
            DoEvent += action;
        }

        private DispatcherTimer mousePositionTimer;    //长时间不操作该程序退回到登录界面的计时器
        public Point mousePosition;    //鼠标的位置

        public bool IsEnable { get => mousePositionTimer.IsEnabled;}

        /// <summary>
        /// 启动鼠标移动timer
        /// </summary>
        /// <param name="seconds">每隔seconds秒检测一次鼠标位置是否变动</param>
        public void Start(Int32 seconds)
        {
            mousePosition = MouseHelper.GetMousePoint();  //获取鼠标坐标
            mousePositionTimer = new DispatcherTimer();
            mousePositionTimer.Tick += new EventHandler(MousePositionTimedEvent);
            mousePositionTimer.Interval = new TimeSpan(0, 0, seconds);     //每隔10秒检测一次鼠标位置是否变动
            mousePositionTimer.Start();
        }

        /// <summary>
        /// 停止鼠标移动线程timer
        /// </summary>
        public void Stop()
        {
            if (mousePositionTimer == null)
            {
                return;
            }
            if (mousePositionTimer.IsEnabled)
            {
                mousePositionTimer.Stop();
            }
        }

        private void MousePositionTimedEvent(object sender, EventArgs e)
        {
            if (!HaveUsedTo())
            {
                mousePositionTimer.Stop();
                //做些事情
                DoEvent?.Invoke();
            }
        }

        //判断鼠标是否移动
        private bool HaveUsedTo()
        {
            Point point = MouseHelper.GetMousePoint();
            if (point == mousePosition)
            {
                return false;
            }
            mousePosition = point;
            return true;
        }
    }
}
