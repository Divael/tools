using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SGJLToolLib.Tool.Encrpy;

namespace 数据库配置工具
{
    public partial class DBForm : Form
    {
        private DbConfig dbConfig = null;

        public DBForm()
        {
            InitializeComponent();

            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dbConfig = new DbConfig();
            tbservername.Text = dbConfig.Sevice;
            tbdbname.Text = dbConfig.database;
            tbusername.Text = dbConfig.username;
            tbpassword.Text = dbConfig.password;
            tbselectNumber.Text = dbConfig.selectNumber.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            if (ck.Checked)
            {
                tbpassword.PasswordChar = '\0';
            }
            else
            {
                tbpassword.PasswordChar = '*';
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dbConfig.Sevice = tbservername.Text;
            dbConfig.database = tbdbname.Text;
            dbConfig.username = tbusername.Text;
            dbConfig.password = tbpassword.Text;
            dbConfig.selectNumber = int.Parse(tbselectNumber.Text);
            dbConfig.SaveConfig();
            MessageBox.Show("保存完成");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBClass dbClass = new DBClass(tbservername.Text,tbdbname.Text,tbusername.Text,tbpassword.Text);
            if (dbClass.OpenTest())
            {
                MessageBox.Show("连接成功");
            }
            else
            {
                MessageBox.Show("连接失败");
            }
        }
    }
}
