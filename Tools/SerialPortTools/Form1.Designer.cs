namespace SerialPortTools
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_com = new System.Windows.Forms.ComboBox();
            this.bt_CloseCom = new System.Windows.Forms.Button();
            this.bt_openCom = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.lb_msg = new System.Windows.Forms.ListBox();
            this.tbSendTxt = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.userButton1 = new Tools.注册机.UserButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_com);
            this.groupBox1.Controls.Add(this.bt_CloseCom);
            this.groupBox1.Controls.Add(this.bt_openCom);
            this.groupBox1.Location = new System.Drawing.Point(22, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 59);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "串口操作";
            // 
            // cb_com
            // 
            this.cb_com.FormattingEnabled = true;
            this.cb_com.Location = new System.Drawing.Point(19, 23);
            this.cb_com.Name = "cb_com";
            this.cb_com.Size = new System.Drawing.Size(64, 20);
            this.cb_com.TabIndex = 6;
            // 
            // bt_CloseCom
            // 
            this.bt_CloseCom.Location = new System.Drawing.Point(212, 23);
            this.bt_CloseCom.Name = "bt_CloseCom";
            this.bt_CloseCom.Size = new System.Drawing.Size(75, 23);
            this.bt_CloseCom.TabIndex = 2;
            this.bt_CloseCom.Text = "关闭串口";
            this.bt_CloseCom.UseVisualStyleBackColor = true;
            this.bt_CloseCom.Click += new System.EventHandler(this.Bt_CloseCom_Click);
            // 
            // bt_openCom
            // 
            this.bt_openCom.Location = new System.Drawing.Point(113, 23);
            this.bt_openCom.Name = "bt_openCom";
            this.bt_openCom.Size = new System.Drawing.Size(75, 23);
            this.bt_openCom.TabIndex = 1;
            this.bt_openCom.Text = "打开串口";
            this.bt_openCom.UseVisualStyleBackColor = true;
            this.bt_openCom.Click += new System.EventHandler(this.Bt_openCom_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(713, 418);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 33);
            this.button8.TabIndex = 21;
            this.button8.Text = "Clear";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // lb_msg
            // 
            this.lb_msg.FormattingEnabled = true;
            this.lb_msg.ItemHeight = 12;
            this.lb_msg.Location = new System.Drawing.Point(391, 12);
            this.lb_msg.Name = "lb_msg";
            this.lb_msg.Size = new System.Drawing.Size(397, 400);
            this.lb_msg.TabIndex = 20;
            // 
            // tbSendTxt
            // 
            this.tbSendTxt.Location = new System.Drawing.Point(22, 128);
            this.tbSendTxt.Name = "tbSendTxt";
            this.tbSendTxt.Size = new System.Drawing.Size(347, 21);
            this.tbSendTxt.TabIndex = 22;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(294, 165);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 24);
            this.btnSend.TabIndex = 23;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // userButton1
            // 
            this.userButton1.BackColor = System.Drawing.Color.Transparent;
            this.userButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.userButton1.CustomerInformation = "";
            this.userButton1.EnableColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.userButton1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.userButton1.Location = new System.Drawing.Point(304, 238);
            this.userButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.userButton1.Name = "userButton1";
            this.userButton1.Size = new System.Drawing.Size(65, 31);
            this.userButton1.TabIndex = 24;
            this.userButton1.UIText = "异步写法";
            this.userButton1.Click += new System.EventHandler(this.userButton1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 468);
            this.Controls.Add(this.userButton1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbSendTxt);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.lb_msg);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cb_com;
        private System.Windows.Forms.Button bt_CloseCom;
        private System.Windows.Forms.Button bt_openCom;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.ListBox lb_msg;
        private System.Windows.Forms.TextBox tbSendTxt;
        private System.Windows.Forms.Button btnSend;
        private Tools.注册机.UserButton userButton1;
    }
}

