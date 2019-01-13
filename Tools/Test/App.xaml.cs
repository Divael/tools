using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Test
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        App() {
            //单例程序，让程序只有一个存在！
            this.Startup += new StartupEventHandler(App_Startup);

        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            bool ret;
            //System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString()  获取程序名称
            var mutex = new System.Threading.Mutex(true, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString(), out ret);
            if (!ret)
            {
                MessageBox.Show("已有一个程序实例运行,或者请至任务管理器中，将其结束!");
                Environment.Exit(0);
            }

            if (!Tools.注册机.AuthorizeCheckAction.getInstance().init("1","1").AuthorizeCheck())
            {
                MessageBox.Show("机器未获得使用权限!");
                Environment.Exit(0);
            }


        }
    }
}
