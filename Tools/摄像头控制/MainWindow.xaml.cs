using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 摄像头控制
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }
        public webcam wcam = null;


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            start();
        }

        public void start()
        {
            //以panel1为容器显示视频内容
            wcam = new webcam(panel1.Handle, 0, 0, this.panel1.Width, this.panel1.Height);
            wcam.Start();
        }
    }

}
