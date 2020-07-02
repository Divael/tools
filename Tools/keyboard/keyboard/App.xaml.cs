using System.Diagnostics;
using System.Windows;

namespace keyboard
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Process thisProc = Process.GetCurrentProcess();

            if (Process.GetProcessesByName(thisProc.ProcessName).Length > 1)
            {

                // MessageBox.Show("Application is already running.");
                Application.Current.Shutdown();
                return;
            }

            base.OnStartup(e);

        }
    }
}
