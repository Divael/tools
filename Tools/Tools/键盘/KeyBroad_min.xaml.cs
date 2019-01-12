using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.WinSystem.Hook;

namespace Tools
{
    /// <summary>
    /// KeyBroad_min.xaml 的交互逻辑
    /// </summary>
    public partial class KeyBroad_min : System.Windows.Controls.UserControl
    {
        public KeyBroad_min()
        {
            InitializeComponent();
        }

        public event Action<object, RoutedEventArgs> KeyFinished;
        void BtClick(object sender, RoutedEventArgs e)
        {
            var bt = (sender as System.Windows.Controls.RadioButton);
            //bt.Focus();
            switch (bt.Content as string)
            {
                case "0":
                    KeyboardSimulator.KeyPress(Keys.D0);
                    break;
                case "1":
                    KeyboardSimulator.KeyPress(Keys.D1);
                    break;
                case "2":
                    KeyboardSimulator.KeyPress(Keys.D2);
                    break;
                case "3":
                    KeyboardSimulator.KeyPress(Keys.D3);
                    break;
                case "4":
                    KeyboardSimulator.KeyPress(Keys.D4);
                    break;
                case "5":
                    KeyboardSimulator.KeyPress(Keys.D5);
                    break;
                case "6":
                    KeyboardSimulator.KeyPress(Keys.D6);
                    break;
                case "7":
                    KeyboardSimulator.KeyPress(Keys.D7);
                    break;
                case "8":
                    KeyboardSimulator.KeyPress(Keys.D8);
                    break;
                case "9":
                    KeyboardSimulator.KeyPress(Keys.D9);
                    break;
                case "删除":
                    KeyboardSimulator.KeyPress(Keys.Back);
                    break;
                case "确认":
                    KeyFinished?.Invoke(sender, e);
                    break;
            }
        }
    }
}
