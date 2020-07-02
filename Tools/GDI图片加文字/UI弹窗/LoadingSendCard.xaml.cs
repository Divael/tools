using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Test.UI弹窗
{
    /// <summary>
    /// NoticeView.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingSendCard : Window
    {
        int Seconds { get; set; }
        public string Text { get; set; }

        bool _isok = false;


        public LoadingSendCard(bool isok = false, int seconds = 0)
        {
            InitializeComponent();
            Width = Tools.WindowHelper.WIDTH * 0.3;
            Height = Width * 0.75;
            _isok = isok;
            DisplayLoading(isok);
        }

        void DisplayLoading(bool flag)
        {
            if (!flag)
            {
                sp_loading.Visibility = Visibility.Visible;
                sp_right.Visibility = Visibility.Hidden;
            }
            else
            {
                sp_loading.Visibility = Visibility.Hidden;
                sp_right.Visibility = Visibility.Visible;
            }

        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            this.Hide();
        }
        /*
        public void Show(bool isok, int seconds = 0)
        {
            _isok = isok;
            if (isok)
            {
                this.Dispatcher.Invoke(() => {
                    DisplayLoading(isok);
                });
                this.Show();
                Task.Run(() => {
                    Thread.Sleep(1000*5);
                    InitApp.getInstance().loadingSendCard.Dispatcher.Invoke(() => {
                        InitApp.getInstance().loadingSendCard.Close();
                    });
                    InitApp.getInstance().Booking.Dispatcher.BeginInvoke((Action)delegate {
                        Tools.UIHelper.ToUI(null, InitApp.getInstance().mainView);
                    });
                });

            }
            else
            {
                this.Dispatcher.Invoke(() => {
                    DisplayLoading(isok);
                });
                this.Show();
            }
            
            
        }
        */
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
            if (Seconds > 0)
            {
                Thread.Sleep(Seconds * 1000);
                this.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    this.Close();
                });
                /*
                InitApp.getInstance().Booking.Dispatcher.Invoke(()=> {
                    Tools.UIHelper.ToUI(null,InitApp.getInstance().mainView);
                });
                */
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

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            //((MediaElement)sender).Position = ((MediaElement)sender).Position.Add(TimeSpan.FromMilliseconds(1));
            (sender as MediaElement).Stop();
            (sender as MediaElement).Play();
        }
    }
}
