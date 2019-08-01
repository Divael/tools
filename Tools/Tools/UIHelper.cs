using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Tools
{
    public class UIHelper
    {
        /// <summary>
        /// 当前页面
        /// </summary>
        private static UserControl cur_page = null;
        /// <summary>
        /// 上一页面
        /// </summary>
        private static UserControl last_page = null;

        /// <summary>
        /// 主页
        /// </summary>
        private static UserControl main_page = null;

        public static ContentControl DisplayUserControl
        {
            get; set;
        }

        public static UserControl Last_page
        {
            get
            {
                return last_page;
            }

            set
            {
                last_page = value;
            }
        }

        public static UserControl Cur_page
        {
            get
            {
                return cur_page;
            }

            set
            {
                cur_page = value;
            }
        }

        public static UserControl Main_page
        {
            get
            {
                return main_page;
            }

            set
            {
                main_page = value;
            }
        }



        /// <summary>
        /// 初始化，绑定ContentControl
        /// </summary>
        /// <param name="mainContentControl">ContentControl</param>
        /// <param name="mainUserControl">mainUserControl</param>
        public static void Init(ContentControl mainContentControl, UserControl mainUserControl = null)
        {
            DisplayUserControl = mainContentControl;
            if (mainUserControl != null)
            {
                Main_page = mainUserControl;
            }
        }

        /// <summary>
        /// 指定页面
        /// </summary>
        /// <param name="curControl">当前页面</param>
        /// <param name="nextControlControl">下一页面</param>
        public static void ToUI(UserControl curControl, UserControl nextControlControl)
        {
            if (DisplayUserControl != null) {
                if (Main_page == null)
                {
                    Main_page = curControl;
                }
                Last_page = curControl;
                Cur_page = nextControlControl;
                DisplayUserControl.Content = nextControlControl;
            }
            else
            {
                throw new Exception("没有设置ContentControl!");
            }
        }

        /// <summary>
        /// 主页
        /// </summary>
        /// <param name="curControl"></param>
        public static void ToMainUI(UserControl curControl = null)
        {
            Last_page = curControl;
            Cur_page = Main_page;
            DisplayUserControl.Content = Main_page;
        }

        public static void ToUpUI(UserControl curControl = null)
        {
            Cur_page = Last_page;
            DisplayUserControl.Content = Last_page;
            Last_page = curControl;
        }
    }
}
