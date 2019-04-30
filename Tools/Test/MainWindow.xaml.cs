using System;
using System.Collections;
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
using Tools;

namespace Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //http://123.206.110.239:8383/pay.php;

        }



        public void msg(string msg) {
            text_msg.Dispatcher.BeginInvoke((Action)delegate() {
                text_msg.Text += "msg"+"\r\n";
            });
        }

        private void Sdf_Click(object sender, RoutedEventArgs e)
        {
            PictureAddFont.getInstance()._base_top = int.Parse(base_top.Text);
            PictureAddFont.getInstance()._base_left = int.Parse(base_left.Text);
            PictureAddFont.getInstance()._left_space = int.Parse(space_left.Text);
            PictureAddFont.getInstance()._top_space = int.Parse(space_top.Text);

            PictureAddFont.getInstance().PicAddFonts(text_msg.Text, "魔鬼", @"D:\cover.png");
        }
    }
}
