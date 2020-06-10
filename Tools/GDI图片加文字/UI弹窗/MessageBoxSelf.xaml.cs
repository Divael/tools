using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
