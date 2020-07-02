using System.ComponentModel;
using System.Windows;

namespace Test.UI弹窗
{
    /// <summary>
    /// MessageBoxSelf.xaml 的交互逻辑
    /// </summary>
    public sealed partial class MessageBoxSelf : Window
    {
        private static MessageBoxSelf sClass;
        private static object locks = new object();

        public MessageBoxSelf()
        {
            InitializeComponent();
            Width = Application.Current.MainWindow.Width;
            Height = Application.Current.MainWindow.Height;
        }

        public static MessageBoxSelf getInstance()
        {
            if (sClass == null)
            {
                lock (locks)
                {
                    if (sClass == null)
                    {
                        sClass = new MessageBoxSelf();
                        return sClass;
                    }
                }
            }
            return sClass;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //this.Hide();
            //e.Cancel = true;
            base.OnClosing(e);
        }
    }
}
