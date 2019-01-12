using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tools
{
    public class WindowHelper
    {
        //全屏幕大小
        public static double WIDTH = SystemParameters.FullPrimaryScreenWidth;

        public static double HEIGHT = SystemParameters.FullPrimaryScreenHeight;

        /// <summary>
        /// 是窗口居中
        /// </summary>
        /// <param name="win"></param>
        public static void center(Window win) {
            double workHeight = SystemParameters.WorkArea.Height;

            double workWidth = SystemParameters.WorkArea.Width;

            win.Top = (workHeight - win.Height) / 2;

            win.Left = (workWidth - win.Width) / 2;
        }

        /// <summary>
        /// 打开指定程序（目录+.exe）
        /// </summary>
        /// <param name="fileExe"></param>
        public static void openProcesses(string fileExe) {
            try
            {
                Process.Start(fileExe);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 关闭指定程序
        /// 例子:
        /// keyboard.exe
        /// </summary>
        /// <param name="ExeName">keyboard</param>
        public static void exitProcess(string ExeName)
        {
            try
            {
                Process[] processes = Process.GetProcesses();
                foreach (Process p in processes)
                {
                    if (p.ProcessName == ExeName)
                    {
                        p.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


    }
}
