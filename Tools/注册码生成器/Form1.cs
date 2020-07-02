using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using 注册码生成器.Entities;

namespace 注册码生成器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbMechCode.Text))
            {
                MessageBox.Show("填入客户的机器码");
            }

            try
            {
                tbRegisterCode.Text = AuthorizeEncrypted(tbMechCode.Text);
                //button2_Click(null, null);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }


        /// <summary>
        /// 一个自定义的加密方法，传入一个原始数据，返回一个加密结果
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        private string AuthorizeEncrypted(string origin)
        {
            // 此处使用了组件支持的DES对称加密技术
            // 此处使用了组件支持的DES对称加密技术
            string code = origin + tbMarchName.Text + tbMechID.Text;
            return Tools.EncyptHelper.MD5Encrypt(code, "yangsea1");
        }


        private void button2_Click(object sender, EventArgs e)
        {
            using (var db = new yhxrEntities())
            {
                var list = db.DEV_REG.Where((x) => x.R_RCODE.Equals(tbRegisterCode.Text)).ToList();
                if (list.Count > 0)
                {
                    return;
                }

                DEV_REG devReg = new DEV_REG();
                devReg.DEV_NO = tbMechID.Text;
                devReg.R_DEVCODE = tbMechCode.Text;
                devReg.R_DT = DateTime.Now;
                devReg.R_RCODE = tbRegisterCode.Text;
                devReg.R_User = tbMarchName.Text;

                db.DEV_REG.Add(devReg);
                db.SaveChanges();
            }
        }
    }
}
