using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools;

namespace SerialPortTools
{
    public partial class Form1 : Form
    {
        SerialPortBase serialPort;

        public Form1()
        {
            InitializeComponent();
            serialPort = new SerialPortBase(true);
            serialPort.DataReceive += SerialPort_DataReceive;
            string[] a = serialPort.GetPortNames();
            cb_com.DataSource = a;
            cb_com.SelectedIndex = cb_com.Items.Count - 1;
        }

        private void SerialPort_DataReceive(System.IO.Ports.SerialPort arg1, byte[] arg2)
        {
            byte[] vs = arg2;

            this.BeginInvoke(new EventHandler(delegate (Object sd, EventArgs ag)
            {
                lb_msg.Items.Add(Tools.StringHelper.ByteToHexString(vs));
            }));
        }

        private void Bt_openCom_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                serialPort.Open(cb_com.SelectedItem.ToString(), 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
            }
            if (serialPort.IsOpen)
            {
                MessageBox.Show("打开成功");
            }
            else
            {
                MessageBox.Show("打开失败");
            }
        }

        private void Bt_CloseCom_Click(object sender, EventArgs e)
        {
            serialPort.Close();
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.DataReceive -= SerialPort_DataReceive;
                byte[] bytes = serialPort.SendAndReceived(tbSendTxt.Text.ToHex(), new TimeSpan(0, 0, 0, 0, 1000));
                serialPort.DataReceive += SerialPort_DataReceive;
                if (bytes == null)
                {
                    lb_msg.Items.Add("null");
                }
                else
                {
                    lb_msg.Items.Add(Tools.StringHelper.ByteToHexString(bytes));
                }

            }
            else
            {
                MessageBox.Show("请先打开串口");
            }
        }

        private async void userButton1_Click(object sender, EventArgs e)
        {
            //主线程
            await Task.Run(()=> {
                //子线程
                var rs = this.BeginInvoke((Action)delegate () {
                    //主线程..
                    
                });
                this.EndInvoke(rs);
            });

            await Task.Delay(100);//不影响主线程的delay
            //主线程
        }
    }
}
