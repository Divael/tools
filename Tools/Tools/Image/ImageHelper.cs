using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        public static ImageHelper getInstance() {
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

        /// <summary>
        /// Bitmap转换为BitmapSource
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public System.Windows.Media.Imaging.BitmapSource BitmapToBitmapSource(System.Drawing.Bitmap source)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                source.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        /// <summary>
        /// BitmapSource转换为Bitmap
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public System.Drawing.Bitmap BitmapFromSource(BitmapSource source)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new PngBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(source));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                // return bitmap; <-- leads to problems, stream is closed/closing ...
                return new Bitmap(bitmap);
            }
        }

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
        public void Compress(Bitmap srcBitmap, Stream destStream, long level)
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
        public void Compress(Bitmap srcBitMap, string destFile, long level)
        {
            Stream s = new FileStream(destFile, FileMode.Create);
            Compress(srcBitMap, s, level);
            s.Close();
        }

        /// <summary>
        /// 将bmp图片转成jpg图片
        /// </summary>
        /// <param name="BmpFilePath"></param>
        /// <param name="JpgFilePath"></param>
        public void BmpToJpg(string BmpFilePath, string JpgFilePath)
        {
            string BMPFiles = BmpFilePath;
            //BMP文件所在路径
            BitmapImage BitImage = new BitmapImage(new Uri(BMPFiles, UriKind.Absolute));
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(BitImage));
            String JpegImage = JpgFilePath;
            //JPG文件件路径
            using (FileStream fileStream = new FileStream(JpegImage, FileMode.Create, FileAccess.ReadWrite))
            {
                encoder.Save(fileStream);
                fileStream.Close();
            }
            //---------------------
            //作者：flywithmj
            //来源：CSDN
            //原文：https://blog.csdn.net/flywithmj/article/details/6548732 
            //版权声明：本文为博主原创文章，转载请附上博文链接！
        }

        /// <summary>
        /// 图片压缩(降低质量以减小文件的大小)
        /// </summary>
        /// <param name="mg">传入的Bitmap对象</param>
        /// <param name="newSize">大小300*300</param>
        /// <returns></returns>
        public Bitmap Compress(Bitmap mg, System.Drawing.Size newSize)
        {
            double ratio = 0d;
            double myThumbWidth = 0d;
            double myThumbHeight = 0d;
            int x = 0;
            int y = 0;

            Bitmap bp;

            if ((mg.Width / Convert.ToDouble(newSize.Width)) > (mg.Height /
            Convert.ToDouble(newSize.Height)))
                ratio = Convert.ToDouble(mg.Width) / Convert.ToDouble(newSize.Width);
            else
                ratio = Convert.ToDouble(mg.Height) / Convert.ToDouble(newSize.Height);
            myThumbHeight = Math.Ceiling(mg.Height / ratio);
            myThumbWidth = Math.Ceiling(mg.Width / ratio);

            System.Drawing.Size thumbSize = new System.Drawing.Size((int)newSize.Width, (int)newSize.Height);
            bp = new Bitmap(newSize.Width, newSize.Height);
            x = (newSize.Width - thumbSize.Width) / 2;
            y = (newSize.Height - thumbSize.Height);
            System.Drawing.Graphics g = Graphics.FromImage(bp);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            Rectangle rect = new Rectangle(x, y, thumbSize.Width, thumbSize.Height);
            g.DrawImage(mg, rect, 0, 0, mg.Width, mg.Height, GraphicsUnit.Pixel);

            return bp;
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



        public string ImageToBase64(Image image)
        {
            Image img = image;
            BinaryFormatter binFormatter = new BinaryFormatter();
            using (MemoryStream memStream = new MemoryStream())
            {
                binFormatter.Serialize(memStream, img);
                byte[] bytes = memStream.GetBuffer();
                string base64 = Convert.ToBase64String(bytes);
                return base64;
            }
        }

        /// <summary>
        /// 将Base64字符串转换为图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public Bitmap Base64ToBitmap(string base64)
        {
            Bitmap bitmap = null;

            try
            {
                byte[] arr = Convert.FromBase64String(base64);
                using (MemoryStream ms = new MemoryStream(arr))
                {
                    Bitmap bmp = new Bitmap(ms);
                    ms.Close();
                    bitmap = bmp;
                }
            }
            catch
            {
            }
            return bitmap;
            //---------------------
            //作者：qubernet
            //来源：CSDN
            //原文：https://blog.csdn.net/qubernet/article/details/84067203 
            //版权声明：本文为博主原创文章，转载请附上博文链接！
        }

        /// <summary>
        /// 将Base64字符串转换为Image对象
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public Image Base64ToImage(string base64)
        {
            Image bitmap = null;
            try
            {
                byte[] arr = Convert.FromBase64String(base64);
                using (MemoryStream ms = new MemoryStream(arr))
                {
                    BinaryFormatter binFormatter = new BinaryFormatter();
                    Image img = (Image)binFormatter.Deserialize(ms);
                    ms.Close();
                    bitmap = img;
                }
            }
            catch 
            {
            }
            return bitmap;
        }

        /// <summary>
        /// 从文件名传化为base64字符串
        /// </summary>
        /// <param name="Imagefilename"></param>
        /// <returns></returns>
        public string ImgFileToBase64String(string Imagefilename)
        {
            try
            {
                byte[] arr;
                using (MemoryStream ms = new MemoryStream())
                {
                    Bitmap bmp = new Bitmap(Imagefilename);
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    bmp.Dispose();
                    bmp = null;
                    arr = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(arr, 0, (int)ms.Length);

                }
                return Convert.ToBase64String(arr);
            }
            catch 
            {
                return null;
            }
        }


        /// <summary>
        /// threeebase64编码的字符串转为图片
        /// </summary>
        /// <param name="strbase64"></param>
        /// <param name="filepath">jpg路径</param>
        /// <returns></returns>
        public Bitmap Base64StringToBitmap(string strbase64,string filepath)
        {
            try
            {
                byte[] arr = Convert.FromBase64String(strbase64);
                using (MemoryStream ms = new MemoryStream(arr))
                {
                    Bitmap bmp = new Bitmap(ms);
                    bmp.Save(@filepath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //bmp.Save(@"d:\"test.bmp", ImageFormat.Bmp);
                    //bmp.Save(@"d:\"test.gif", ImageFormat.Gif);
                    //bmp.Save(@"d:\"test.png", ImageFormat.Png);
                    ms.Close();
                    return bmp;
                }
            }
            catch 
            {
                return null;
            }
        }

        /// <summary>
        /// 多张base64图片转换为图片另存为，路径有问题，要改！
        /// </summary>
        /// <param name="upimgPath">路径</param>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public string Base64ToImage(string upimgPath, string base64String)
        {
            string goodspath = upimgPath.Substring(0, upimgPath.LastIndexOf('/'));  //用来生成文件夹  
            if (!Directory.Exists(goodspath))
            {
                Directory.CreateDirectory(goodspath);
            }
            var imgPath = string.Empty;
            if (!string.IsNullOrEmpty(base64String))
            {
                var splitBase = base64String.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in splitBase)
                {
                    var path = upimgPath + Guid.NewGuid() + ".jpg";

                    string filePath = path;// Server.MapPath(upimgPath + Guid.NewGuid() + ".jpg");  
                    File.WriteAllBytes(filePath, Convert.FromBase64String(item));
                    imgPath += path + ";";
                }
            }
            else { imgPath = ";"; }
            return imgPath.TrimEnd(';');
        }
    }
}
