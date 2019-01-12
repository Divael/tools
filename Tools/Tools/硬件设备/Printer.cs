using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools.硬件设备
{
    public class Printer
    {
        private Font printFont;
        private Font titleFont;
        private StringReader streamToPrint;
        private int leftMargin = 0;
        private StringReader lineReader= null;
        System.Drawing.Printing.PrintDocument pd;
        String text;

        public Printer()
        {
            pd = new System.Drawing.Printing.PrintDocument();
            pd.PrintPage += pd_PrintPage1;
        }

        /// <summary>
        /// 设置PrintDocument 的相关属性
        /// </summary>
        /// <param name="PrinterName">打印机名称</param>
        /// <param name="str">要打印的字符串</param>
        public void print1(string str)
        {
            try
            {
                text = str;
                using (streamToPrint = new StringReader(str)) {
                    printFont = new Font("黑体", 10);
                    titleFont = new Font("黑体", 15);
                    pd = new System.Drawing.Printing.PrintDocument();
                    //pd.PrinterSettings.PrinterName = PrinterName;
                    //pd.DocumentName = pd.PrinterSettings.MaximumCopies.ToString();
                    pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd_PrintPage1);

                    pd.PrintController = new System.Drawing.Printing.StandardPrintController();
                    pd.Print();
                    pd.Dispose();
                } ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = this.leftMargin;
            float topMargin = 0;
            String line = null;
            linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);
            while (count < linesPerPage &&
            ((line = streamToPrint.ReadLine()) != null))
            {
                if (count == 0)
                {
                    yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                    ev.Graphics.DrawString(line, titleFont, Brushes.Black, leftMargin + 10, yPos, new StringFormat());
                }
                else
                {
                    yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                    ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                }
                count++;
            }
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;

        }


        private void pd_PrintPage1(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
            Graphics g = e.Graphics; //获得绘图对象
            float linesPerPage = 0; //页面的行号
            float yPosition = 0;   //绘制字符串的纵向位置
            int count = 0; //行计数器
            float leftMargin = this.leftMargin; //左边距        e.MarginBounds.Left= 100
            float topMargin = 10; //上边距        e.MarginBounds.Top=100
            string line = ""; //行字符串
            Font printFont = new Font("黑体", 10); //当前的打印字体
            SolidBrush myBrush = new SolidBrush(Color.Black);//刷子

            linesPerPage = ((e.PageBounds.Height - topMargin) / printFont.GetHeight(g)); ;//每页可打印的行数-
            //逐行的循环打印一页
            while (count < linesPerPage && ((line = lineReader.ReadLine()) != null))
            {
                yPosition = topMargin + (count * printFont.GetHeight(g));
                g.DrawString(line, printFont, myBrush, leftMargin, yPosition, new StringFormat());
                count++;
            }
            // 注意：使用本段代码前，要在该窗体的类中定义lineReader对象：
            //       StringReader lineReader = null;
            //如果本页打印完成而line不为空,说明还有没完成的页面,这将触发下一次的打印事件。在下一次的打印中lineReader会
            //自动读取上次没有打印完的内容，因为lineReader是这个打印方法外的类的成员，它可以记录当前读取的位置
            if (line != null)
                e.HasMorePages = true;
            else
            {
                e.HasMorePages = false;
                // 重新初始化lineReader对象，不然使用打印预览中的打印按钮打印出来是空白页
                lineReader = new StringReader(text); // textBox是你要打印的文本框的内容
            }
        }

        public void 打印预览(string text) {
            this.text = text;
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = pd;
            lineReader = new StringReader(text);
            try
            {
                printPreviewDialog.ShowDialog();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void 页面设置() {
            PageSetupDialog pageSetupDialog = new PageSetupDialog();
            pageSetupDialog.Document = pd;
            pageSetupDialog.ShowDialog();
        }

        public void 打印设置() {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = pd;
            printDialog.ShowDialog();
        }

        public void setPage() {
            pd.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Custom",500,300);
        }

        public void 开始打印(string text) {
            lineReader = new StringReader(text);   // 获取要打印的字符串
            pd.Print();
        }

        public void 设置纸张大小(Int32 width,Int32 height) {
            pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", width,height);
        }

        public void Close()
        {
            pd.Dispose();
            try
            {
                lineReader.Dispose();
                lineReader.Close();
            }
            catch (Exception ex)
            {
                Loger.err("打印机释放",ex);
            }
            
        }
    }
}
