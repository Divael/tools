using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace WPF
{
    /// <summary>
    /// Loading.xaml 的交互逻辑
    /// </summary>
    public partial class Loading : Window
    {
        public Loading()
        {
            InitializeComponent();
        }
        BackgroundWorker bgMeet;
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            bgMeet = new BackgroundWorker();
            bgMeet.WorkerReportsProgress = true;
            bgMeet.DoWork += new DoWorkEventHandler(bgMeet_DoWork);
            bgMeet.ProgressChanged += new ProgressChangedEventHandler(bgMeet_ProgressChanged);
            bgMeet.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgMeet_RunWorkerCompleted);
            bgMeet.RunWorkerAsync();
        }
        void bgMeet_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            loading.Visibility = System.Windows.Visibility.Collapsed;
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.lab_pro.Content = "完成";
            }));
        }
        void bgMeet_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.lab_pro.Content = e.ProgressPercentage;
            }));
        }

        void bgMeet_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                loading.Visibility = System.Windows.Visibility.Visible;
            }));
            GetData();
        }
        public void GetData()
        {
            for (int i = 0; i < 11; i++)
            {
                bgMeet.ReportProgress(i);
                System.Threading.Thread.Sleep(500);
            }
        }
    }
}