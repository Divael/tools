using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRT591_M001_MR01
{
    /// <summary>
    /// 本程序适用于CRT591_M001_MR01发卡器
    /// </summary>
    public partial class Form1 : Form
    {
        private CRT591MR01_Controller controller;

        public delegate void Response(string str);//委托
        /// <summary>
        /// 必须
        /// </summary>
        public Response response;

        public Form1()
        {
            InitializeComponent();
            cb_com.DataSource =  SerialPort.GetPortNames();
            Control.CheckForIllegalCrossThreadCalls = false;
            controller = new CRT591MR01_Controller();
            this.FormClosed += Form1_FormClosed; ;
            cbb_TxAddr.SelectedIndexChanged += Cbb_TxAddr_SelectedIndexChanged;
        }

        private void Cbb_TxAddr_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.SetTxAddr(0x00);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            controller.CloseCom();
            controller.ReleaseContext();
        }

        private void bt_openCom_Click(object sender, EventArgs e)
        {
            if (controller.OpenCom(cb_com.SelectedItem.ToString(),"9600"))
            {
                lb_msg.Items.Add("串口已经打开");
            }
            else
            {
                lb_msg.Items.Add("串口未打开");
            }
            visable();
            controller.InitializeContext().ToString().logThis() ;
            "初始化只能读卡器".logThis();
            var tp = controller.GetSCardReaderStatus();
            foreach (var item in tp)
            {
                lb_msg.Items.Add(item.ToString());
            }
        }

        private void visable()
        {
            gb1.Visible = true;
            groupBox2.Visible = true;
            groupBox3.Visible = true;
        }
        private void unvisable()
        {
            gb1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
        }

        private void bt_CloseCom_Click(object sender, EventArgs e)
        {
            if (controller.CloseCom())
            {
                controller.ReleaseContext();
                lb_msg.Items.Add("串口已经关闭");
                unvisable();
            }
            else
            {
                lb_msg.Items.Add("串口未关闭");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var tp = controller.Reset(enum_InitPm.移动卡片到卡口);
                if (tp)
                {
                    lb_msg.Items.Add("success!");
                }
                else
                {
                    lb_msg.Items.Add("fail!");
                }
            }
            catch (Exception ex)
            {
                "初始化失败！".logErr(ex);
                lb_msg.Items.Add("初始化失败!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var tp = controller.Poll();
                if (tp)
                {
                    lb_msg.Items.Add("success!");
                }
                else
                {
                    lb_msg.Items.Add("fail!");
                }
            }
            catch (Exception ex)
            {
                "poll失败！".logErr(ex);
                lb_msg.Items.Add("poll失败!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var tp = controller.PollWithReader();
                if (tp)
                {
                    lb_msg.Items.Add("success!");
                }
                else
                {
                    lb_msg.Items.Add("fail!");
                }
            }
            catch (Exception ex)
            {
                "poll失败！".logErr(ex);
                lb_msg.Items.Add("poll失败!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (controller.MoveCard(enum_MoveCardPm.移动卡片到持卡位))
            {
                lb_msg.Items.Add("移动卡片到持卡位success!");
            }
            else
            {
                lb_msg.Items.Add("移动卡片到持卡位fail!");
            }
            
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (controller.MoveCard(enum_MoveCardPm.移动卡片到RF卡位))
            {
                lb_msg.Items.Add("移动卡片到RF卡位success!");
            }
            else
            {
                lb_msg.Items.Add("移动卡片到RF卡位fail!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (controller.MoveCard(enum_MoveCardPm.移动卡片到回收盒))
            {
                lb_msg.Items.Add("移动卡片到回收盒success!");
            }
            else
            {
                lb_msg.Items.Add("移动卡片到回收盒fail!");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (controller.MoveCard(enum_MoveCardPm.移动卡片到出卡口))
            {
                lb_msg.Items.Add("移动卡片到出卡口success!");
            }
            else
            {
                lb_msg.Items.Add("移动卡片到出卡口fail!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (controller.MoveCard(enum_MoveCardPm.移动卡片到IC卡位))
            {
                lb_msg.Items.Add("移动卡片到IC卡位success!");
            }
            else
            {
                lb_msg.Items.Add("移动卡片到IC卡位fail!");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            byte[] bt = controller.ReadRFCard();
            if (bt==null)
            {
                lb_msg.Items.Add("读卡RF fail!");
                return;
            }
            string a = Tools.StringHelper.byteToHexStr(bt);
            lb_msg.Items.Add("读卡="+a);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            lb_msg.Items.Clear();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //发卡
            if (controller.Reset(enum_InitPm.同Pm31H启动回收卡计数))
            {
                if (controller.MoveCard(enum_MoveCardPm.移动卡片到RF卡位))
                {
                    byte[] bt = controller.ReadRFCard();
                    if (bt == null)
                    {
                        if (controller.MoveCard(enum_MoveCardPm.移动卡片到回收盒))
                        {
                            lb_msg.Items.Add("读卡RF fail!");
                            return;
                        }
                        lb_msg.Items.Add("读卡错误!");
                        return;
                    }
                    string a = Tools.StringHelper.byteToHexStr(bt);
                    lb_msg.Items.Add("读卡=" + a);
                    if (controller.MoveCard(enum_MoveCardPm.移动卡片到持卡位))
                    {
                        lb_msg.Items.Add("已完成出卡!");
                    }
                    else
                    {
                        lb_msg.Items.Add("读卡成功，出卡错误!");
                    }
                }
            }

        }
    }
}
