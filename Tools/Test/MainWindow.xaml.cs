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
            var t = Tools.CRCHelper.ADD8.ADD8_Add("E2 00 09 00 00 00 00 00 18 00 00 00 F1".ToHex());//E2 00 09 00 00 00 00 00 18 00 00 00 F1 F4
            string g = Tools.StringHelper.byteToHexStr(t);
            bool a = Tools.CRCHelper.ADD8.ADD8_Check("E2 00 09 00 00 00 00 00 18 00 00 00 F1 F4".ToHex());
             t = Tools.CRCHelper.CRC_16.crc16("02 05 00 01 FF 00".ToHex());//02 05 00 01 FF 00 DD C9
             g = Tools.StringHelper.byteToHexStr(t);
            Console.ReadKey();
        }
    }
}
