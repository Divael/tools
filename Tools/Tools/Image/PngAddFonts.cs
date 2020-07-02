using System;
using System.Drawing;

namespace Tools
{
    /// <summary>
    /// 在png图片上添加文字,默认是竖排排列
    /// </summary>
    public class PngAddFonts
    {
        #region 位置坐标
        public int _base_left { get; set; } = 10;
        public int _left_space { get; set; } = 10;
        public int _base_top { get; set; } = 10;
        public int _top_space { get; set; } = 10;
        #endregion

        /// <summary>
        /// 在png图片上添加文字
        /// </summary>
        /// <param name="base_left"></param>
        /// <param name="left_space"></param>
        /// <param name="base_top"></param>
        /// <param name="top_space"></param>
        public PngAddFonts(int base_left, int base_top)
        {
            _base_left = base_left;
            _base_top = base_top;
        }

        public PngAddFonts()
        {
        }

        /// <summary>
        /// PicAddFonts
        /// 将文字写在图片上(需要提前设置文字的位置属性)
        /// </summary>
        /// <param name="tempPath">模板图片路径 @"D:\Users\Pictures\壁纸\wallhaven-541924.png"</param>
        /// <param name="content">内容</param>
        /// <param name="savePath">生成文字图片保存路径</param>
        public void PicAddFonts(string tempPath, string content, string savePath)
        {
            string path = @tempPath;

            Bitmap bmp = new Bitmap(path);
            Graphics g = Graphics.FromImage(bmp);
            String str = content;
            Font font = new Font("KaiTi", 52, FontStyle.Bold);//设置字体，大小，粗细
            SolidBrush sbrush = new SolidBrush(Color.Red);//设置颜色

            //设置位置
            float base_left = _base_left;
            float left_space = (float)font.GetHeight(g);
            float base_top = _base_top;
            float top_space = (float)font.GetHeight(g);

            for (int i = 0; i < str.Length; i++)
            {
                if (i > 13)
                {
                    g.DrawString(str[i] + "", font, sbrush, new PointF(base_left + (left_space * 2), base_top + (top_space * (i - 14))));
                    continue;
                }
                else if (i > 6)
                {
                    g.DrawString(str[i] + "", font, sbrush, new PointF(base_left + (left_space * 1), base_top + (top_space * (i - 7))));
                }
                else
                {
                    g.DrawString(str[i] + "", font, sbrush, new PointF(base_left, base_top + (top_space * i)));
                }

            }

            //MemoryStream ms = new MemoryStream();
            //bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            bmp.Save(@savePath);
        }
    }
}
