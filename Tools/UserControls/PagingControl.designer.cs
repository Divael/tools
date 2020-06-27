namespace UserControls
{
    partial class PagingControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txb_pageindex = new System.Windows.Forms.TextBox();
            this.lb_page = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lb_pagecount = new System.Windows.Forms.Label();
            this.ButtonEnd = new System.Windows.Forms.Button();
            this.ButtonNext = new System.Windows.Forms.Button();
            this.ButtonToPage = new System.Windows.Forms.Button();
            this.ButtonPre = new System.Windows.Forms.Button();
            this.ButtonFirst = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F);
            this.label1.Location = new System.Drawing.Point(75, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "当前页:";
            // 
            // txb_pageindex
            // 
            this.txb_pageindex.Font = new System.Drawing.Font("宋体", 9F);
            this.txb_pageindex.Location = new System.Drawing.Point(136, 3);
            this.txb_pageindex.Name = "txb_pageindex";
            this.txb_pageindex.Size = new System.Drawing.Size(49, 21);
            this.txb_pageindex.TabIndex = 3;
            this.txb_pageindex.Text = "1";
            this.txb_pageindex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txb_pageindex_KeyDown);
            // 
            // lb_page
            // 
            this.lb_page.AutoSize = true;
            this.lb_page.Font = new System.Drawing.Font("宋体", 9F);
            this.lb_page.Location = new System.Drawing.Point(236, 8);
            this.lb_page.Name = "lb_page";
            this.lb_page.Size = new System.Drawing.Size(53, 12);
            this.lb_page.TabIndex = 5;
            this.lb_page.Text = "总页数：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F);
            this.label3.Location = new System.Drawing.Point(403, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "每页:";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("宋体", 9F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "10",
            "20",
            "50",
            "100"});
            this.comboBox1.Location = new System.Drawing.Point(444, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(47, 20);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.Text = "10";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lb_pagecount
            // 
            this.lb_pagecount.AutoSize = true;
            this.lb_pagecount.Font = new System.Drawing.Font("宋体", 9F);
            this.lb_pagecount.Location = new System.Drawing.Point(497, 7);
            this.lb_pagecount.Name = "lb_pagecount";
            this.lb_pagecount.Size = new System.Drawing.Size(71, 12);
            this.lb_pagecount.TabIndex = 10;
            this.lb_pagecount.Text = "(共0条记录)";
            // 
            // ButtonEnd
            // 
            this.ButtonEnd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButtonEnd.Location = new System.Drawing.Point(367, 2);
            this.ButtonEnd.Name = "ButtonEnd";
            this.ButtonEnd.Size = new System.Drawing.Size(30, 22);
            this.ButtonEnd.TabIndex = 7;
            this.ButtonEnd.Text = ">>";
            this.ButtonEnd.Click += new System.EventHandler(this.ButtonEnd_Click);
            // 
            // ButtonNext
            // 
            this.ButtonNext.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButtonNext.Location = new System.Drawing.Point(331, 2);
            this.ButtonNext.Name = "ButtonNext";
            this.ButtonNext.Size = new System.Drawing.Size(30, 22);
            this.ButtonNext.TabIndex = 6;
            this.ButtonNext.Text = ">";
            this.ButtonNext.Click += new System.EventHandler(this.ButtonNext_Click);
            // 
            // ButtonToPage
            // 
            this.ButtonToPage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButtonToPage.Location = new System.Drawing.Point(191, 2);
            this.ButtonToPage.Name = "ButtonToPage";
            this.ButtonToPage.Size = new System.Drawing.Size(39, 23);
            this.ButtonToPage.TabIndex = 4;
            this.ButtonToPage.Text = "跳转";
            this.ButtonToPage.Click += new System.EventHandler(this.ButtonToPage_Click);
            // 
            // ButtonPre
            // 
            this.ButtonPre.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButtonPre.Location = new System.Drawing.Point(39, 2);
            this.ButtonPre.Name = "ButtonPre";
            this.ButtonPre.Size = new System.Drawing.Size(30, 22);
            this.ButtonPre.TabIndex = 1;
            this.ButtonPre.Text = "<";
            this.ButtonPre.Click += new System.EventHandler(this.ButtonPre_Click);
            // 
            // ButtonFirst
            // 
            this.ButtonFirst.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButtonFirst.Location = new System.Drawing.Point(3, 2);
            this.ButtonFirst.Name = "ButtonFirst";
            this.ButtonFirst.Size = new System.Drawing.Size(30, 22);
            this.ButtonFirst.TabIndex = 0;
            this.ButtonFirst.Text = "<<";
            this.ButtonFirst.Click += new System.EventHandler(this.ButtonFirst_Click);
            // 
            // PagingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lb_pagecount);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ButtonEnd);
            this.Controls.Add(this.ButtonNext);
            this.Controls.Add(this.lb_page);
            this.Controls.Add(this.ButtonToPage);
            this.Controls.Add(this.txb_pageindex);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ButtonPre);
            this.Controls.Add(this.ButtonFirst);
            this.Name = "PagingControl";
            this.Size = new System.Drawing.Size(652, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonFirst;
        private System.Windows.Forms.Button ButtonPre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_pageindex;
        private System.Windows.Forms.Button ButtonToPage;
        private System.Windows.Forms.Label lb_page;
        private System.Windows.Forms.Button ButtonNext;
        private System.Windows.Forms.Button ButtonEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lb_pagecount;
    }
}
