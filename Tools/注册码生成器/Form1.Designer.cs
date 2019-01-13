namespace 注册码生成器
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
            this.tbMechID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMarchName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbRegisterCode = new System.Windows.Forms.TextBox();
            this.btCreateCode = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbMechCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbMechID);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbMarchName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbRegisterCode);
            this.groupBox1.Controls.Add(this.btCreateCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbMechCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(609, 161);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "注册码生成";
            // 
            // tbMechID
            // 
            this.tbMechID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMechID.Location = new System.Drawing.Point(71, 47);
            this.tbMechID.Name = "tbMechID";
            this.tbMechID.Size = new System.Drawing.Size(538, 21);
            this.tbMechID.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "客户机器ID";
            // 
            // tbMarchName
            // 
            this.tbMarchName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMarchName.Location = new System.Drawing.Point(71, 20);
            this.tbMarchName.Name = "tbMarchName";
            this.tbMarchName.Size = new System.Drawing.Size(538, 21);
            this.tbMarchName.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "客户名";
            // 
            // tbRegisterCode
            // 
            this.tbRegisterCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRegisterCode.Location = new System.Drawing.Point(71, 103);
            this.tbRegisterCode.Name = "tbRegisterCode";
            this.tbRegisterCode.Size = new System.Drawing.Size(538, 21);
            this.tbRegisterCode.TabIndex = 10;
            // 
            // btCreateCode
            // 
            this.btCreateCode.Location = new System.Drawing.Point(71, 130);
            this.btCreateCode.Name = "btCreateCode";
            this.btCreateCode.Size = new System.Drawing.Size(75, 23);
            this.btCreateCode.TabIndex = 9;
            this.btCreateCode.Text = "生成注册码";
            this.btCreateCode.UseVisualStyleBackColor = true;
            this.btCreateCode.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "客户注册码";
            // 
            // tbMechCode
            // 
            this.tbMechCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMechCode.Location = new System.Drawing.Point(71, 74);
            this.tbMechCode.Name = "tbMechCode";
            this.tbMechCode.Size = new System.Drawing.Size(538, 21);
            this.tbMechCode.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "客户机器码";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(628, 184);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "智能仓储物料柜注册码生成程序";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbMechID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMarchName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbRegisterCode;
        private System.Windows.Forms.Button btCreateCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbMechCode;
        private System.Windows.Forms.Label label1;
    }
}

