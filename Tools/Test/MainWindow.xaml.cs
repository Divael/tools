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
            Tools.IniHelper.SetFilePath(@"D:\config.ini");
            /*
            var p = Tools.IniHelper.WriteIniData("Config","调试模式",true.ToString());
            var p1  = Tools.IniHelper.WriteIniData("Config","调试模式1",true.ToString());
            var p2 = Tools.IniHelper.WriteIniData("Config","调试模式2",true.ToString());
            var p3 = Tools.IniHelper.WriteIniData("Config1","调试模式",true.ToString());

            var s = Tools.IniHelper.ReadIniDataByHashTable("Config");
            string a = (string)s["调试模式"];
            var tp = Tools.IniHelper.ReadSections();
            var tp1 = Tools.IniHelper.ReadKeys("Config");
            var tp2 = Tools.IniHelper.ReadIniData("Config", "调试模式1","");
            var e1 = Tools.IniHelper.ClearSection("Config1");
            var e2 = Tools.IniHelper.ClearAllSection();
            var _1tp = Tools.IniHelper.ReadSections();
            var _1tp1 = Tools.IniHelper.ReadKeys("Config");
            var _1tp2 = Tools.IniHelper.ReadIniData("Config1", "调试模式1", "");
            */
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

            PictureAddFont.getInstance().PicAddFonts(text_msg.Text, "123456", @"D:\cover.png");
        }
    }
}
