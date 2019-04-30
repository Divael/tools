using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.WinSystem;

namespace Tools
{
    /// <summary>
    /// 图片常用操作类
    /// </summary>
    public class ImageHelper
    {
        #region init

        private static ImageHelper mImageHelper;

        private static object obj = new object();

        private ImageHelper() {

        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public ImageHelper getInstance() {
            if (mImageHelper == null)
            {
                lock (obj)
                {
                    if (mImageHelper == null)
                    {
                        mImageHelper = new ImageHelper();
                    }
                }
            }
            return mImageHelper;
        }

        #endregion

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        /// <summary>
        /// 图片压缩(降低质量以减小文件的大小)
        /// </summary>
        /// <param name="srcBitmap">传入的Bitmap对象</param>
        /// <param name="destStream">压缩后的Stream对象</param>
        /// <param name="level">压缩等级，0到100，0 最差质量，100 最佳</param>
        public static void Compress(Bitmap srcBitmap, Stream destStream, long level)
        {
            ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;

            // Get an ImageCodecInfo object that represents the JPEG codec.
            myImageCodecInfo = GetEncoderInfo("image/jpeg");

            // Create an Encoder object based on the GUID

            // for the Quality parameter category.
            myEncoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.
            // An EncoderParameters object has an array of EncoderParameter
            // objects. In this case, there is only one

            // EncoderParameter object in the array.
            myEncoderParameters = new EncoderParameters(1);

            // Save the bitmap as a JPEG file with 给定的 quality level
            myEncoderParameter = new EncoderParameter(myEncoder, level);
            myEncoderParameters.Param[0] = myEncoderParameter;
            srcBitmap.Save(destStream, myImageCodecInfo, myEncoderParameters);
        }

        /// <summary>
        /// 图片压缩(降低质量以减小文件的大小)
        /// </summary>
        /// <param name="srcBitMap">传入的Bitmap对象</param>
        /// <param name="destFile">压缩后的图片保存路径</param>
        /// <param name="level">压缩等级，0到100，0 最差质量，100 最佳</param>
        public static void Compress(Bitmap srcBitMap, string destFile, long level)
        {
            Stream s = new FileStream(destFile, FileMode.Create);
            Compress(srcBitMap, s, level);
            s.Close();
        }

        /// <summary>
        /// 将bytes转图片并保存到本地
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public string SaveImageFromBytes(string fileName, byte[] buffer)
        {
            string file = fileName;
            Image image = CreateImageFromBytes(buffer);
            ImageFormat format = image.RawFormat;
            if (format.Equals(ImageFormat.Jpeg))
            {
                file += ".jpeg";
            }
            else if (format.Equals(ImageFormat.Png))
            {
                file += ".png";
            }
            else if (format.Equals(ImageFormat.Bmp))
            {
                file += ".bmp";
            }
            else if (format.Equals(ImageFormat.Gif))
            {
                file += ".gif";
            }
            else if (format.Equals(ImageFormat.Icon))
            {
                file += ".icon";
            }
            System.IO.FileInfo info = new System.IO.FileInfo(file);
            System.IO.Directory.CreateDirectory(info.Directory.FullName);
            try
            {
                File.WriteAllBytes(file, buffer);
            }
            catch (Exception ex)
            {
                string err = "图片保存出错：" + fileName + ":" + ex;
                err.logThis();
            }

            return file;
        }

        /// <summary>
        /// bytes转image图片
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private Image CreateImageFromBytes(byte[] buffer)
        {
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                Image image = System.Drawing.Image.FromStream(ms);
                return image;
            }
        }

        #region 将图片和字符串的互转
        /// <summary>
        /// 图片转字符串
        /// </summary>
        /// <param name="image">要转换的图片</param>
        /// <returns></returns>
        public string ChangeImageToString(Image image)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    byte[] arr = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(arr, 0, (int)ms.Length);
                    ms.Close();
                    string pic = Convert.ToBase64String(arr);

                    return pic;
                }
            }
            catch (Exception)
            {
                return "Fail to change bitmap to string!";
            }
        }

        /// <summary>
        /// 字符串转图片
        /// </summary>
        /// <param name="pic">图片的字符串</param>
        /// <returns></returns>
        public static Image ChangeStringToImage(string pic)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(pic);
                //读入MemoryStream对象  
                MemoryStream memoryStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
                memoryStream.Write(imageBytes, 0, imageBytes.Length);
                //转成图片  
                Image image = Image.FromStream(memoryStream);

                return image;
            }
            catch (Exception)
            {
                Image image = null;
                return image;
            }
        }

        #endregion

        #region 从bytes数组创建BitmapSource
        /// <summary>
        /// 从bytes数组创建BitmapSource
        /// </summary>
        /// <param name="data">bytes数组</param>
        /// <returns></returns>
        public BitmapSource CreateBitmapSourceFromBytes(byte[] data)
        {
            using (MemoryStream memoryStream1 = new MemoryStream(data))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                int num = 1;
                bitmapImage.CacheOption = (BitmapCacheOption)num;
                MemoryStream memoryStream2 = memoryStream1;
                bitmapImage.StreamSource = (Stream)memoryStream2;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                memoryStream1.Dispose();
                return (BitmapSource)bitmapImage;
            }
        }
        #endregion

        #region 从image创建BitmapSource
        /// <summary>
        /// 将image转BitmapSource
        /// </summary>
        /// <param name="bp">image图片</param>
        /// <param name="UseOldMode">不填</param>
        /// <returns></returns>
        public BitmapSource CreateBitmapSourceFromBitmap(Image bp, bool UseOldMode = true)
        {
            if (UseOldMode)
            {
                IntPtr hbitmap = (bp as Bitmap).GetHbitmap();
                IntPtr palette = IntPtr.Zero;
                Int32Rect empty = Int32Rect.Empty;
                BitmapSizeOptions sizeOptions = BitmapSizeOptions.FromEmptyOptions();
                BitmapSource sourceFromHbitmap = Imaging.CreateBitmapSourceFromHBitmap(hbitmap, palette, empty, sizeOptions);
                if (!WinApi.DeleteObject(hbitmap))
                    throw new Win32Exception("图片资源释放失败。。");
                return sourceFromHbitmap;
            }
            using (MemoryStream memoryStream1 = new MemoryStream())
            {
                BitmapImage bitmapImage = new BitmapImage();
                bp.Save((Stream)memoryStream1, ImageFormat.Bmp);
                bitmapImage.BeginInit();
                int num = 1;
                bitmapImage.CacheOption = (BitmapCacheOption)num;
                MemoryStream memoryStream2 = memoryStream1;
                bitmapImage.StreamSource = (Stream)memoryStream2;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                memoryStream1.Dispose();
                return (BitmapSource)bitmapImage;
            }
        }
        #endregion

        #region 相对路径转BitmapImage
        /// <summary>
        /// 相对路径转BitmapImage
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="kind">相对</param>
        /// <returns></returns>
        public BitmapImage CreateBitmapSourceFromUri(string path, UriKind kind = UriKind.Relative)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            Uri uri = new Uri(path, kind);
            bitmapImage.UriSource = uri;
            int num = 1;
            bitmapImage.CacheOption = (BitmapCacheOption)num;
            bitmapImage.EndInit();
            bitmapImage.Freeze();
            return bitmapImage;
        }
        #endregion

        #region 保存图片为...png,jpg
        public void SaveToJpg(BitmapSource source, string filename)
        {
            using (FileStream fileStream1 = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
            {
                JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
                jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(source));
                FileStream fileStream2 = fileStream1;
                jpegBitmapEncoder.Save((Stream)fileStream2);
                fileStream1.Flush();
                fileStream1.Close();
            }
        }

        public void SaveToPng(BitmapSource source, string filename)
        {
            using (FileStream fileStream1 = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
            {
                PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
                pngBitmapEncoder.Frames.Add(BitmapFrame.Create(source));
                FileStream fileStream2 = fileStream1;
                pngBitmapEncoder.Save((Stream)fileStream2);
                fileStream1.Flush();
                fileStream1.Close();
            }
        }
        #endregion


        public byte[] GetBitmapData(Image bp, MemoryStream ms)
        {
            ms.SetLength(0L);
            bp.Save((Stream)ms, ImageFormat.Bmp);
            return ms.ToArray();
        }

        /// <summary>
        /// 图片转二进制byte[]
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public byte[] CreateBytesFromImage(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    image.Save(ms, ImageFormat.Icon);
                }
                byte[] buffer = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

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
            public void PicAddFonts(string tempPath, string content, string savePath)
            {
                string path = @tempPath;

                Bitmap bmp = new Bitmap(path);
                Graphics g = Graphics.FromImage(bmp);
                String str = content;
                Font font = new Font("KaiTi", 52, FontStyle.Bold);//设置字体，大小，粗细
                SolidBrush sbrush = new SolidBrush(Color.Red);//设置颜色
                int base_left = _base_left;
                int left_space = _left_space;

                int base_top = _base_top;
                int top_space = _top_space;
                for (int i = 0; i < str.Length; i++)
                {
                    if (i > 13)
                    {
                        continue;
                        g.DrawString(str[i] + "", font, sbrush, new PointF(base_left + (left_space * 2), base_top + (top_space * (i - 14))));
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
}
