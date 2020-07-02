using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Test.UI弹窗
{
    /// <summary>
    /// NoticeView.xaml 的交互逻辑
    /// </summary>
    public partial class toast : Window
    {
        int Seconds { get; set; }
        public string Text { get; set; }

        public event Action Response;
        public toast(string str, int seconds = 0)
        {
            InitializeComponent();
            Width = Tools.WindowHelper.WIDTH * 0.3;
            Height = Width * 0.75;
            Text = str;
            Seconds = seconds;
            tb_notice.Content = Text;
            pwd.TextDecorations = new TextDecorationCollection(new TextDecoration[] {
                new TextDecoration() {
                     Location= TextDecorationLocation.Strikethrough,
                      Pen= new Pen(Brushes.Black, 15f) {
                          DashCap =  PenLineCap.Round,
                           StartLineCap= PenLineCap.Round,
                            EndLineCap= PenLineCap.Round,
                            DashStyle= new DashStyle(new double[] {0.0,1.2 }, 0.6f)
                      }
                }

            });
        }

        public void Show(string str, int seconds = 0)
        {
            Text = str;
            Seconds = seconds;
            tb_notice.Content = Text;
            this.Show();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
            textBlock.Visibility = Visibility.Hidden;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {

        }



        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (pwd.Text.Trim() == Config.backPwd)
            {
                Response?.Invoke();
            }
            else
            {
                pwd.Text = "";
                textBlock.Visibility = Visibility.Visible;

            }*/
        }

        private void Pwd_GotFocus(object sender, RoutedEventArgs e)
        {
            Tools.WindowHelper.openProcesses(System.Environment.CurrentDirectory + "/keyboard.exe");
            textBlock.Visibility = Visibility.Hidden;
        }

        private void Pwd_LostFocus(object sender, RoutedEventArgs e)
        {
            Tools.WindowHelper.exitProcess(System.Environment.CurrentDirectory + "/keyboard.exe");
        }
    }
}
