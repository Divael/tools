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
    public partial class Loading : Window
    {
        int Seconds { get; set; }
        public string Text { get; set; }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            this.Hide();
        }

        public Loading(string str = "",int seconds = 0)
        {
            InitializeComponent();
            Width = Tools.WindowHelper.WIDTH * 0.5;
            Height = Width * 0.33;
            Text = str;
            Seconds = seconds;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
            //Common.RunInBackThread(() => {
            //    Thread.Sleep(TimeSpan.FromSeconds(Seconds));
            //    this.Dispatcher.Invoke(() => {
            //        this.Close();
            //    });
            //});
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
