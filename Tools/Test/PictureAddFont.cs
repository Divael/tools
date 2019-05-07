using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class PictureAddFont
    {

        private static PictureAddFont sClass;
        private static object locks = new object();
        private PictureAddFont()
        {

        }

        public static PictureAddFont getInstance()
        {
            if (sClass == null)
            {
                lock (locks)
                {
                    if (sClass == null)
                    {
                        sClass = new PictureAddFont();
                        return sClass;
                    }
                }
            }
            return sClass;
        }

        public int _base_left { get; set; } = 10;
        public int _left_space { get; set; } = 10;
        public int _base_top { get; set; } = 10;
        public int _top_space { get; set; } = 10;


        /// <summary>
        /// PicAddFonts
        /// 将文字写在图片上(需要提前设置文字的位置属性)
        /// </summary>
        /// <param name="tempPath">模板图片路径 @"D:\Users\Pictures\壁纸\wallhaven-541924.png"</param>
        /// <param name="content">内容</param>
        /// <param name="savePath">生成文字图片保存路径</param>
        public void PicAddFonts(string tempPath,string  content,string savePath) {
            string path = @tempPath;

            Bitmap bmp = new Bitmap(path);
            Graphics g = Graphics.FromImage(bmp);
            String str = content;
            Font font = new Font("KaiTi", 32, FontStyle.Bold);//设置字体，大小，粗细
            SolidBrush sbrush = new SolidBrush(Color.Red);//设置颜色
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
                    g.DrawString(str[i] + "", font, sbrush, new PointF(base_left+(left_space*i), base_top));
                    g.DrawString(str[i] + "", font, sbrush, new PointF(base_left, base_top+(i*top_space)));
                }

            }

            //MemoryStream ms = new MemoryStream();
            //bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            bmp.Save(@savePath);
        }

       
    }
}
