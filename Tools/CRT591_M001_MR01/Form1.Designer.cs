namespace CRT591_M001_MR01
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
            this.lb_msg = new System.Windows.Forms.ListBox();
            this.gb1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbb_TxAddr = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button13 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.gb1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_com);
            this.groupBox1.Controls.Add(this.bt_CloseCom);
            this.groupBox1.Controls.Add(this.bt_openCom);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(463, 74);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "串口操作";
            // 
            // cb_com
            // 
            this.cb_com.FormattingEnabled = true;
            this.cb_com.Location = new System.Drawing.Point(25, 29);
            this.cb_com.Margin = new System.Windows.Forms.Padding(4);
            this.cb_com.Name = "cb_com";
            this.cb_com.Size = new System.Drawing.Size(84, 23);
            this.cb_com.TabIndex = 6;
            // 
            // bt_CloseCom
            // 
            this.bt_CloseCom.Location = new System.Drawing.Point(283, 29);
            this.bt_CloseCom.Margin = new System.Windows.Forms.Padding(4);
            this.bt_CloseCom.Name = "bt_CloseCom";
            this.bt_CloseCom.Size = new System.Drawing.Size(100, 29);
            this.bt_CloseCom.TabIndex = 2;
            this.bt_CloseCom.Text = "关闭串口";
            this.bt_CloseCom.UseVisualStyleBackColor = true;
            this.bt_CloseCom.Click += new System.EventHandler(this.bt_CloseCom_Click);
            // 
            // bt_openCom
            // 
            this.bt_openCom.Location = new System.Drawing.Point(151, 29);
            this.bt_openCom.Margin = new System.Windows.Forms.Padding(4);
            this.bt_openCom.Name = "bt_openCom";
            this.bt_openCom.Size = new System.Drawing.Size(100, 29);
            this.bt_openCom.TabIndex = 1;
            this.bt_openCom.Text = "打开串口";
            this.bt_openCom.UseVisualStyleBackColor = true;
            this.bt_openCom.Click += new System.EventHandler(this.bt_openCom_Click);
            // 
            // lb_msg
            // 
            this.lb_msg.FormattingEnabled = true;
            this.lb_msg.ItemHeight = 15;
            this.lb_msg.Location = new System.Drawing.Point(501, 13);
            this.lb_msg.Margin = new System.Windows.Forms.Padding(4);
            this.lb_msg.Name = "lb_msg";
            this.lb_msg.Size = new System.Drawing.Size(528, 499);
            this.lb_msg.TabIndex = 17;
            // 
            // gb1
            // 
            this.gb1.Controls.Add(this.button10);
            this.gb1.Controls.Add(this.groupBox3);
            this.gb1.Controls.Add(this.groupBox2);
            this.gb1.Controls.Add(this.button3);
            this.gb1.Controls.Add(this.button2);
            this.gb1.Controls.Add(this.button1);
            this.gb1.Controls.Add(this.label1);
            this.gb1.Controls.Add(this.cbb_TxAddr);
            this.gb1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gb1.Location = new System.Drawing.Point(13, 95);
            this.gb1.Margin = new System.Windows.Forms.Padding(4);
            this.gb1.Name = "gb1";
            this.gb1.Padding = new System.Windows.Forms.Padding(4);
            this.gb1.Size = new System.Drawing.Size(463, 291);
            this.gb1.TabIndex = 18;
            this.gb1.TabStop = false;
            this.gb1.Text = "卡机操作";
            this.gb1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "从机地址：";
            // 
            // cbb_TxAddr
            // 
            this.cbb_TxAddr.FormattingEnabled = true;
            this.cbb_TxAddr.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.cbb_TxAddr.Location = new System.Drawing.Point(117, 25);
            this.cbb_TxAddr.Margin = new System.Windows.Forms.Padding(4);
            this.cbb_TxAddr.Name = "cbb_TxAddr";
            this.cbb_TxAddr.Size = new System.Drawing.Size(84, 23);
            this.cbb_TxAddr.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 56);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 29);
            this.button1.TabIndex = 7;
            this.button1.Text = "读卡器初始化";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(164, 56);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 29);
            this.button2.TabIndex = 10;
            this.button2.Text = "卡片状态";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(273, 56);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 29);
            this.button3.TabIndex = 11;
            this.button3.Text = "读卡器状态";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button8);
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Location = new System.Drawing.Point(25, 102);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(259, 100);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "移动卡";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(7, 25);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(77, 29);
            this.button4.TabIndex = 13;
            this.button4.Text = "持卡位";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(92, 25);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(77, 29);
            this.button5.TabIndex = 14;
            this.button5.Text = "RF卡位";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(92, 64);
            this.button6.Margin = new System.Windows.Forms.Padding(4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(77, 29);
            this.button6.TabIndex = 15;
            this.button6.Text = "IC卡位";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(177, 25);
            this.button7.Margin = new System.Windows.Forms.Padding(4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(77, 29);
            this.button7.TabIndex = 16;
            this.button7.Text = "回收";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(7, 64);
            this.button8.Margin = new System.Windows.Forms.Padding(4);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(77, 29);
            this.button8.TabIndex = 17;
            this.button8.Text = "不持卡位";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button13);
            this.groupBox3.Location = new System.Drawing.Point(26, 208);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(259, 67);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "读卡";
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(7, 25);
            this.button13.Margin = new System.Windows.Forms.Padding(4);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(77, 29);
            this.button13.TabIndex = 13;
            this.button13.Text = "RF卡";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(929, 526);
            this.button9.Margin = new System.Windows.Forms.Padding(4);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(100, 41);
            this.button9.TabIndex = 20;
            this.button9.Text = "Clear";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.ForeColor = System.Drawing.Color.Red;
            this.button10.Location = new System.Drawing.Point(369, 19);
            this.button10.Margin = new System.Windows.Forms.Padding(4);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(77, 29);
            this.button10.TabIndex = 14;
            this.button10.Text = "发卡";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 580);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.gb1);
            this.Controls.Add(this.lb_msg);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.gb1.ResumeLayout(false);
            this.gb1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cb_com;
        private System.Windows.Forms.Button bt_CloseCom;
        private System.Windows.Forms.Button bt_openCom;
        private System.Windows.Forms.ListBox lb_msg;
        private System.Windows.Forms.GroupBox gb1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbb_TxAddr;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
    }
}

