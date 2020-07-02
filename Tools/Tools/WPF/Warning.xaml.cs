using System.Windows;

namespace Tools
{
    /// <summary>
    /// Warning.xaml 的交互逻辑
    /// </summary>
    public partial class Warning : Window
    {
        private static Warning warning;

        private static object lockobj = new object();

        public static Warning getInstance()
        {
            if (warning == null)
            {
                lock (lockobj)
                {
                    return warning ?? (warning = new Warning());
                }
            }
            return warning;
        }

        private Warning()
        {
            InitializeComponent();
            Width = SystemParameters.FullPrimaryScreenWidth * 0.5;
            Height = Width * 0.33;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public Warning setText(string v)
        {
            Error_message.Text = "";
            Error_message.Text = v;
            return warning;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
