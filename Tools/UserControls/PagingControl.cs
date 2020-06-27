using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;


namespace UserControls
{
    public partial class PagingControl : System.Windows.Forms.UserControl
    {

        /// <summary>
        /// 总记录数
        /// </summary>
        public int AllCount
        {
            get { return _allCount; }
            set { _allCount = value; }
        }

        /// <summary>
        /// 一页显示记录数
        /// </summary>
        public int PageSize
        {
            get
            {
                try
                {
                    return int.Parse(comboBox1.Text);
                }
                catch (Exception e)
                {
                    return 10;
                }

            }
            set { comboBox1.Text = value.ToString(); }
        }

        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex
        {
            get
            {
                try
                {
                    int temp = Convert.ToInt32(txb_pageindex.Text);
                    int pageCount = GetPageCount();
                    if (temp > pageCount && pageCount > 0)
                    {
                        txb_pageindex.Text = pageCount.ToString();
                        return pageCount;
                    }
                    else if (temp < 1)
                    {
                        txb_pageindex.Text = "1";
                        return 1;
                    }

                    return temp;
                }
                catch
                {
                    txb_pageindex.Text = "1";
                    return 1;
                }
            }
            set { txb_pageindex.Text = value.ToString(); }
        }

        private int _allCount = 0;

        public int GetPageCount()
        {
            int count = 0;
            if (_allCount % PageSize == 0)
            {
                count = _allCount / PageSize;
            }
            else
                count = _allCount / PageSize + 1;
            return count;
        }

        public delegate void PagingEvents(int pageIndex, int pageSize);
        /// <summary>
        /// 分页事件
        /// </summary>
        public event PagingEvents PageChangedEvents;

        public PagingControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 刷新分页控件状态
        /// </summary>
        public void RefreshPager()
        {
            this.lb_page.Text = string.Format("总页数:{0}", GetPageCount().ToString());
            lb_pagecount.Text = string.Format("(共{0}条记录)", AllCount);
            //textEditCurPage.Text = curPage.ToString() ;
            txb_pageindex.Text = PageIndex.ToString();
        }


        private void ButtonFirst_Click(object sender, EventArgs e)
        {
            if (PageChangedEvents != null)
            {
                PageIndex = 1;
                PageChangedEvents(PageIndex, PageSize);
            }
        }

        private void ButtonPre_Click(object sender, EventArgs e)
        {
            if (PageChangedEvents != null)
            {
                if (PageIndex > 1)
                    PageIndex -= 1;
                PageChangedEvents(PageIndex, PageSize);
            }
        }

        private void ButtonToPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (PageChangedEvents != null)
                {
                    PageChangedEvents(PageIndex, PageSize);
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            if (PageChangedEvents != null)
            {
                if (PageIndex < GetPageCount())
                    PageIndex += 1;
                PageChangedEvents(PageIndex, PageSize);
            }
        }

        private void ButtonEnd_Click(object sender, EventArgs e)
        {
            if (PageChangedEvents != null)
            {
                PageIndex = GetPageCount();
                PageChangedEvents(PageIndex, PageSize);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if ( PageSize> 0)
                //{
                PageChangedEvents(PageIndex, PageSize);
                //}

            }
            catch (Exception)
            {

            }
        }

        private void txb_pageindex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键  
            {
                PageChangedEvents(PageIndex, PageSize);
            }
        }
    }
}
/* 调用例子

       private void pagingControl1_PageChangedEvents(int pageIndex, int pageSize)
        {
            bind() ;
        }


    private void bind()
        {

            GroupListResultDTO orderDto = JsonConvert.DeserializeObject<GroupListResultDTO>(json);

            pagingControl1.AllCount = orderDto.RecordCount;
            pagingControl1.RefreshPager();

            List<GroupListResultDataDTO> dataDto = orderDto.Data;

            dataGridView1.DataSource = dataDto;
        }
 
 */
