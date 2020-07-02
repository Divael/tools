using System.Windows;
using System.Windows.Input;

namespace System
{
    /// <summary>
    /// Waiting.xaml 的交互逻辑
    /// </summary>
    public partial class Waiting : Window
    {
        public Waiting()
        {
            InitializeComponent();
            MouseLeftButtonDown += Waiting_MouseLeftButtonDown;
        }

        private void Waiting_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }
    }
}
