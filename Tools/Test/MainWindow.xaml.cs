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
            //http://123.206.110.239:8383/pay.php
            WebServiceInfo webServiceInfo = new WebServiceInfo() {
                WebServiceName = "pay",
                WebServiceUrl = "http://123.206.110.239:8383"
            };
            int Amt = (int)Math.Round((decimal)(100.00 * 0.01), 0, MidpointRounding.AwayFromZero);
            string dataString = $"mid={898323473720126}&tid={86743852}&amt={Amt}&ord=23136&paycode=135074208938566876";
            Hashtable pars = new Hashtable();
            pars["mid"] = 898323473720126;
            pars["tid"] = 86743852;
            pars["amt"] = Amt;
            pars["ord"] = 23136;
            pars["paycode"] = 135074208938566876;
            var str = Tools.WebServiceHelper.QueryPostWebService(webServiceInfo,"", pars);
            Console.ReadKey();
        }



        public void msg(string msg) {
            text_msg.Dispatcher.BeginInvoke((Action)delegate() {
                text_msg.Text += "msg"+"\r\n";
            });
        }
    }
}
