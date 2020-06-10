using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Test.UI弹窗
{
    /// <summary>
    /// NoticeView.xaml 的交互逻辑
    /// </summary>
    public partial class ToastNone : Window
    {
        int Seconds { get; set; }
        public string Text { get; set; }


        public ToastNone(string title, string str, int seconds = 0)
        {
            InitializeComponent();
            Width = Tools.WindowHelper.WIDTH * 0.3;
            Height = Width * 0.75;
            if (str != "")
            {
                Text = str;
                tb_notice.Content = title;
                tb_msg.Text = str;
            }
            Seconds = seconds;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            this.Hide();
        }
        public void Show(string title,string str, int seconds = 0)
        {
            Seconds = seconds;
            tb_notice.Content = title;
            tb_msg.Text = str;
            this.Show();
            if (Seconds > 0)
            {
                Task.Run(()=> {
                    Thread.Sleep(Seconds * 1000);
                    this.Dispatcher.BeginInvoke((Action)delegate () {
                        this.Close();
                    });/*
                    InitApp.getInstance().Booking.Dispatcher.Invoke(() => {
                        Tools.UIHelper.ToUI(null, InitApp.getInstance().mainView);
                    });*/
                });
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
            if (Seconds > 0)
            {
                Task.Run(() => {
                    Thread.Sleep(Seconds * 1000);
                    this.Dispatcher.BeginInvoke((Action)delegate () {
                        this.Close();
                    });/*
                    InitApp.getInstance().Booking.Dispatcher.Invoke(() => {
                        Tools.UIHelper.ToUI(null, InitApp.getInstance().mainView);
                    });*/
                });
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
