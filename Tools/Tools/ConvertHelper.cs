using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Interop;
using System.WinSystem;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using System.Drawing;
using System.Drawing.Imaging;

namespace Tools
{
    /// <summary>
    /// <para>　</para>
    /// 　常用工具类——转换操作类
    /// <para>　---------------------------------------------------------------</para>
    /// <para>　ConvertToDataTable：从IQueryable 转换成DataTable</para>
    /// <para>　UniteTable：合并两个相同结构的 DataTable到一个新表中</para>
    /// <para>　UnitesTable：合并两个相同结构的表到第一个表中</para>
    /// <para>　GetStrArray：将字符串数组转为泛型LIST</para>
    /// <para>　GetArrayStr：将泛型ILIST转换为分隔字符串</para>
    /// <para>　GetStrArray：将分隔字符串","转换为字符串数组</para>
    /// <para>　===============XML和DataSet、DataTable之间的互相转换===============</para>
    /// <para>　DataTableoXml：将Table转换成XML字符串</para>
    /// <para>　DataSetToXml：将DataSet对象中或DataSet中指定的Table转换成XML字符串[+2 重载]</para>
    /// <para>　DataViewToXml：将DataView对象转换成XML字符串</para>
    /// <para>　DataTableToXmlFile：将DataTable对象或DataSet对象中指定的表保存为XML文件[+2 重载]</para>
    /// <para>　DataSetToXmlFile：将DataSet对象转换成XML文件</para>
    /// <para>　DataViewToXmlFile：将DataView对象转换成XML文件</para>
    /// <para>　XmlToDataSet：将Xml内容字符串转换成DataSet对象</para>
    /// <para>　XmlToDataTable：将Xml字符串转换成DataTable对象[+2 重载]</para>
    /// <para>　XmlFileToDataSet：读取Xml文件信息,并转换成DataSet对象</para>
    /// <para>　XmlFileToDataTable：读取Xml文件信息,并转换成DataTable对象[+2 重载]</para>
    /// <para>　================二进制相关转换类==============</para>
    /// <para>　FileToBinary：将文件转换为二进制数组</para>
    /// <para>　BinaryToFile：二进制数组转为文件</para>
    /// <para>　================图片相关转换类==============</para>
    /// <para>　ChangeStringToImage：将字符串转image</para>
    /// <para>　ChangeImageToString：将image转换成字符串</para>
    /// <para>　CreateBitmapSourceFromBytes：从bytes数组创建BitmapSource</para>相对路径转BitmapImage
    /// <para>　CreateBitmapSourceFromBitmap：将image转BitmapSource</para>
    /// <para>　CreateBitmapSourceFromUri：相对路径转BitmapImage</para>
    /// <para>　SaveToJpg：保存图片为jpg</para>
    /// <para>　SaveToPng：保存图片为png</para>
    /// <para>　CreateImageFromBytes：bytes转Image</para>

    /// </summary>
    public class ConvertHelper
    {

        #region 从IQueryable 转换成DataTable
        /// <summary>
        /// 从IQueryable 转换成DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varlist">IQuerable数据ef</param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();
            // column names 
            PropertyInfo[] oProps = null;
            if (varlist == null)
                return dtReturn;
            foreach (T rec in varlist)
            {
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                             == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        #endregion

        #region 合并两个相同结构的 DataTable到一个新表中
        /// <summary>
        /// 合并两个相同结构的 DataTable到一个新表中
        /// </summary>
        /// <param name="DataTable1">DataTable1</param>
        /// <param name="DataTable2">DataTable2</param>
        /// <returns></returns>
        public static DataTable UniteTable(DataTable DataTable1, DataTable DataTable2)
        {
            DataTable newDataTable = DataTable1.Clone();
            object[] obj = new object[newDataTable.Columns.Count];
            for (int i = 0; i < DataTable1.Rows.Count; i++)
            {
                DataTable1.Rows[i].ItemArray.CopyTo(obj, 0);
                newDataTable.Rows.Add(obj);
            }
            for (int i = 0; i < DataTable2.Rows.Count; i++)
            {
                DataTable2.Rows[i].ItemArray.CopyTo(obj, 0);
                newDataTable.Rows.Add(obj);
            }
            return newDataTable;
        }
        #endregion

        #region 合并两个相同结构的表到第一个表中
        /// <summary>
        /// 合并两个相同结构的表到第一个表中
        /// </summary>
        /// <param name="DataTable1">第一个表</param>
        /// <param name="DataTable2">第二个表</param>
        /// <returns>第一个表</returns>
        public static DataTable UnitesTable(DataTable DataTable1, DataTable DataTable2)
        {
            if (DataTable1 == null)
            {
                DataTable1 = DataTable2.Clone();
            }
            object[] obj = new object[DataTable1.Columns.Count];
            for (int i = 0; i < DataTable2.Rows.Count; i++)
            {
                DataTable2.Rows[i].ItemArray.CopyTo(obj, 0);
                DataTable1.Rows.Add(obj);
            }
            return DataTable1;
        }
        #endregion

        #region 将字符串数组转为泛型LIST
        /// <summary>
        /// 将字符串分隔转为泛型LIST
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="speater">分隔符</param>
        /// <param name="toLower">是否转换为小写</param>
        /// <returns>ILIST</returns>
        public static List<string> GetStrArray(string str, char speater, bool toLower)
        {
            List<string> list = new List<string>();
            string[] ss = str.Split(speater);
            foreach (string s in ss)
            {
                if (!string.IsNullOrEmpty(s) && s != speater.ToString())
                {
                    string strVal = s;
                    if (toLower)
                    {
                        strVal = s.ToLower();
                    }
                    list.Add(strVal);
                }
            }
            return list;
        }
        #endregion

        #region 将泛型ILIST转换为分隔字符串
        /// <summary>
        /// 将泛型ILIST转换为分隔字符串
        /// </summary>
        /// <param name="list">泛型ILIST</param>
        /// <param name="speater">分隔字符</param>
        /// <returns>字符串</returns>
        public static string GetArrayStr(List<string> list, string speater)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(speater);
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 将分隔字符串","转换为字符串数组
        /// <summary>
        /// 将分隔字符串","转换为字符串数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>字符串数组</returns>
        public static string[] GetStrArray(string str)
        {
            return str.Split(new char[',']);
        }
        #endregion

        /*==================以下为XML和DataSet、DataTable之间的互相转换=======================*/
        #region 将Table转换成XML字符串
        /// <summary>
        /// 将DataTable对象转换成XML字符串
        /// </summary>
        /// <param name="Dt">DataTable对象</param>
        /// <returns>XML字符串</returns>
        public static string DataTableToXml(DataTable Dt)
        {
            if (Dt != null)
            {
                MemoryStream ms = null;
                XmlTextWriter XmlWt = null;
                try
                {
                    ms = new MemoryStream();
                    //根据ms实例化XmlWt
                    XmlWt = new XmlTextWriter(ms, Encoding.Unicode);
                    //获取ds中的数据
                    Dt.WriteXml(XmlWt);
                    int count = (int)ms.Length;
                    byte[] temp = new byte[count];
                    ms.Seek(0, SeekOrigin.Begin);
                    ms.Read(temp, 0, count);
                    //返回Unicode编码的文本
                    UnicodeEncoding ucode = new UnicodeEncoding();
                    string returnValue = ucode.GetString(temp).Trim();
                    return returnValue;
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //释放资源
                    if (XmlWt != null)
                    {
                        XmlWt.Close();
                        ms.Close();
                        ms.Dispose();
                    }
                }
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 将DataSet对象中或DataSet中指定的Table转换成XML字符串[+2 重载]
        /// <summary>
        /// 将DataSet对象中指定的Table转换成XML字符串
        /// </summary>
        /// <param name="Ds">DataSet对象</param>
        /// <param name="TableIndex">DataSet对象中的Table索引</param>
        /// <returns>XML字符串</returns>
        public static string DataSetToXml(DataSet Ds, int TableIndex)
        {
            if (TableIndex != -1)
            {
                return DataTableToXml(Ds.Tables[TableIndex]);
            }
            else
            {
                return DataTableToXml(Ds.Tables[0]);
            }
        }
        /// <summary>
        /// 将DataSet对象转换成XML字符串
        /// </summary>
        /// <param name="Ds">DataSet对象</param>
        /// <returns>XML字符串</returns>
        public static string DataSetToXml(DataSet Ds)
        {
            return DataSetToXml(Ds, -1);
        }
        #endregion

        #region 将DataView对象转换成XML字符串
        /// <summary>
        /// 将DataView对象转换成XML字符串
        /// </summary>
        /// <param name="Dv">DataView对象</param>
        /// <returns>XML字符串</returns>
        public static string DataViewToXml(DataView Dv)
        {
            return DataTableToXml(Dv.Table);
        }
        #endregion

        #region 将DataTable对象或DataSet中指定的表保存为XML文件
        /// <summary>
        /// 将DataTable对象数据保存为XML文件
        /// </summary>
        /// <param name="Dt">DataSet</param>
        /// <param name="XmlFilePath">XML文件路径</param>
        /// <returns>bool值</returns>
        public static bool DataTableToXmlFile(DataTable Dt, string XmlFilePath)
        {
            if ((Dt != null) && (!string.IsNullOrEmpty(XmlFilePath)))
            {
                string path = System.Environment.CurrentDirectory+(XmlFilePath);
                MemoryStream ms = null;
                XmlTextWriter XmlWt = null;
                try
                {
                    ms = new MemoryStream();
                    //根据ms实例化XmlWt
                    XmlWt = new XmlTextWriter(ms, Encoding.Unicode);
                    //获取ds中的数据
                    Dt.WriteXml(XmlWt);
                    int count = (int)ms.Length;
                    byte[] temp = new byte[count];
                    ms.Seek(0, SeekOrigin.Begin);
                    ms.Read(temp, 0, count);
                    //返回Unicode编码的文本
                    UnicodeEncoding ucode = new UnicodeEncoding();
                    //写文件
                    StreamWriter sw = new StreamWriter(path);
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    sw.WriteLine(ucode.GetString(temp).Trim());
                    sw.Close();
                    return true;
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //释放资源
                    if (XmlWt != null)
                    {
                        XmlWt.Close();
                        ms.Close();
                        ms.Dispose();
                    }
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 将DataSet对象中指定的Table转换成XML文件
        /// </summary>
        /// <param name="Ds">DataSet对象</param>
        /// <param name="TableIndex">DataSet对象中的Table索引</param>
        /// <param name="XmlFilePath">xml文件路径</param>
        /// <returns>bool值</returns>
        public static bool DataTableToXmlFile(DataSet Ds, int TableIndex, string XmlFilePath)
        {
            if (TableIndex != -1)
            {
                return DataTableToXmlFile(Ds.Tables[TableIndex], XmlFilePath);
            }
            else
            {
                return DataTableToXmlFile(Ds.Tables[0], XmlFilePath);
            }
        }
        #endregion

        #region 将DataSet对象转换成XML文件
        /// <summary>
        /// 将DataSet对象转换成XML文件
        /// </summary>
        /// <param name="Ds">DataSet对象</param>
        /// <param name="XmlFilePath">xml文件路径</param>
        /// <returns>bool值</returns>
        public static bool DataSetToXmlFile(DataSet Ds, string XmlFilePath)
        {
            return DataTableToXmlFile(Ds, -1, XmlFilePath);
        }
        #endregion

        #region 将DataView对象转换成XML文件
        /// <summary>
        /// 将DataView对象转换成XML文件
        /// </summary>
        /// <param name="Dv">DataView对象</param>
        /// <param name="XmlFilePath">xml文件路径</param>
        /// <returns>bool]值</returns>
        public static bool DataViewToXmlFile(DataView Dv, string XmlFilePath)
        {
            return DataTableToXmlFile(Dv.Table, XmlFilePath);
        }
        #endregion

        #region 将Xml内容字符串转换成DataSet对象
        /// <summary>
        /// 将Xml内容字符串转换成DataSet对象
        /// </summary>
        /// <param name="XmlStr">Xml内容字符串</param>
        /// <returns>DataSet对象</returns>
        public static DataSet XmlToDataSet(string XmlStr)
        {
            if (!string.IsNullOrEmpty(XmlStr))
            {
                StringReader StrStream = null;
                XmlTextReader Xmlrdr = null;
                try
                {
                    DataSet ds = new DataSet();
                    //读取字符串中的信息
                    StrStream = new StringReader(XmlStr);
                    //获取StrStream中的数据
                    Xmlrdr = new XmlTextReader(StrStream);
                    //ds获取Xmlrdr中的数据                
                    ds.ReadXml(Xmlrdr);
                    return ds;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    //释放资源
                    if (Xmlrdr != null)
                    {
                        Xmlrdr.Close();
                        StrStream.Close();
                        StrStream.Dispose();
                    }
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 将Xml字符串转换成DataTable对象[+2 重载]
        /// <summary>
        /// 将Xml字符串转换成DataTable对象
        /// </summary>
        /// <param name="XmlStr">Xml字符串</param>
        /// <param name="TableIndex">Table表索引</param>
        /// <returns>DataTable对象</returns>
        public static DataTable XmlToDataTable(string XmlStr, int TableIndex)
        {
            return XmlToDataSet(XmlStr).Tables[TableIndex];
        }
        /// <summary>
        /// 将Xml字符串转换成DataTable对象
        /// </summary>
        /// <param name="XmlStr">Xml字符串</param>
        /// <returns>DataTable对象</returns>
        public static DataTable XmlToDataTable(string XmlStr)
        {
            return XmlToDataSet(XmlStr).Tables[0];
        }
        #endregion

        #region 读取Xml文件信息,并转换成DataSet对象
        /// <summary>
        /// 读取Xml文件信息,并转换成DataSet对象
        /// </summary>
        /// <remarks>
        /// DataSet ds = new DataSet();
        /// ds = CXmlFileToDataSet("/XML/upload.xml");
        /// </remarks>
        /// <param name="XmlFilePath">Xml文件地址</param>
        /// <returns>DataSet对象</returns>
        public static DataSet XmlFileToDataSet(string XmlFilePath)
        {
            if (!string.IsNullOrEmpty(XmlFilePath))
            {
                string path = System.Environment.CurrentDirectory+(XmlFilePath);
                StringReader StrStream = null;
                XmlTextReader Xmlrdr = null;
                try
                {
                    XmlDocument xmldoc = new XmlDocument();
                    //根据地址加载Xml文件
                    xmldoc.Load(path);

                    DataSet ds = new DataSet();
                    //读取文件中的字符流
                    StrStream = new StringReader(xmldoc.InnerXml);
                    //获取StrStream中的数据
                    Xmlrdr = new XmlTextReader(StrStream);
                    //ds获取Xmlrdr中的数据
                    ds.ReadXml(Xmlrdr);
                    return ds;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    //释放资源
                    if (Xmlrdr != null)
                    {
                        Xmlrdr.Close();
                        StrStream.Close();
                        StrStream.Dispose();
                    }
                }
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region 读取Xml文件信息,并转换成DataTable对象[+2 重载]
        /// <summary>
        /// 读取Xml文件信息,并转换成DataTable对象
        /// </summary>
        /// <param name="XmlFilePath">xml文江路径</param>
        /// <param name="TableIndex">Table索引</param>
        /// <returns>DataTable对象</returns>
        public static DataTable XmlFileToDataTable(string XmlFilePath, int TableIndex)
        {
            return XmlFileToDataSet(XmlFilePath).Tables[TableIndex];
        }
        /// <summary>
        /// 读取Xml文件信息,并转换成DataTable对象
        /// </summary>
        /// <param name="XmlFilePath">xml文江路径</param>
        /// <returns>DataTable对象</returns>
        public static DataTable XmlFileToDataTable(string XmlFilePath)
        {
            return XmlFileToDataSet(XmlFilePath).Tables[0];
        }
        #endregion

        //================二进制相关转换类==============

        #region 将文件转换为二进制数组
        /// <summary>
        /// 将文件转换为二进制数组
        /// </summary>
        /// <param name="FilePath">文件完整路径</param>
        /// <returns>二进制数组</returns>
        public static byte[] FileToBinary(string FilePath)
        {
            byte[] Buffer = null;
            if (System.IO.File.Exists(FilePath) && System.IO.Path.HasExtension(FilePath))
            {
                using (FileStream stream = new FileInfo(FilePath).OpenRead())
                {
                    Buffer = new byte[stream.Length];
                    stream.Read(Buffer, 0, Convert.ToInt32(stream.Length));
                }
            }
            else {
                throw new Exception("文件不存在，请检查文件或路径是否正确");
            }
            return Buffer;
        }
        #endregion

        #region 二进制数组转为文件
        /// <summary>
        /// 二进制数组转为文件
        /// </summary>
        /// <param name="FilePath">转到的文件完整路径</param>
        /// <param name="Buffer">二进制数组</param>
        /// <returns>转换是否成功</returns>
        public static bool BinaryToFile(string FilePath, byte[] Buffer)
        {
            bool flag = false;
            FileStream fstream = File.Create(FilePath, Buffer.Length);
            try
            {
                fstream.Write(Buffer, 0, Buffer.Length);
                flag = true;
            }
            catch (Exception)
            {
            }
            finally
            {
                fstream.Close();
            }
            return flag;
        }
        #endregion

        //================图片相关转换类==============

        #region 将图片和字符串的互转
        /// <summary>
        /// 图片转字符串
        /// </summary>
        /// <param name="image">要转换的图片</param>
        /// <returns></returns>
        public static string ChangeImageToString(Image image)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream()) { 
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
        public static BitmapSource CreateBitmapSourceFromBytes(byte[] data)
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
        public static BitmapSource CreateBitmapSourceFromBitmap(Image bp, bool UseOldMode = true)
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
        public static BitmapImage CreateBitmapSourceFromUri(string path, UriKind kind = UriKind.Relative)
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



        #region bitmap bitmapImage icon相互转化


        public static BitmapImage CreateBitmapImageFromBitmap(Bitmap bitmap) {
            BitmapImage bitmapImage = new BitmapImage();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bitmap.Save(ms, bitmap.RawFormat);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }

        /// <summary>
        /// bitmap转Icon
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static Icon CreateIconFromBitmap(Bitmap bmp) {
            System.IntPtr iconHandle = bmp.GetHicon();
            return System.Drawing.Icon.FromHandle(iconHandle);
        }

        /// <summary>
        /// BitmapImage转Bitmap
        /// </summary>
        /// <param name="bitmapSource"></param>
        /// <returns></returns>
        public static Bitmap CreateBitmapFromBitmapImage(BitmapSource bitmapSource) {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(bitmapSource.PixelWidth, bitmapSource.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            System.Drawing.Imaging.BitmapData data = bmp.LockBits(
            new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            bitmapSource.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride); bmp.UnlockBits(data);
            return bmp;
        }

        /// <summary>
        /// BitmapSource转Icon
        /// </summary>
        /// <param name="path"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static Icon CreateIconFromBitmapSource(string path, UriKind kind = UriKind.Relative) {
            BitmapImage bitmapImage = CreateBitmapSourceFromUri(path, UriKind.RelativeOrAbsolute);
            return CreateIconFromBitmap(CreateBitmapFromBitmapImage(bitmapImage));
        }
        #endregion

        #region 保存图片为...png,jpg
        public static void SaveToJpg(BitmapSource source, string filename)
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

        public static void SaveToPng(BitmapSource source, string filename)
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

        public static byte[] GetBitmapData(Image bp, MemoryStream ms)
        {
            ms.SetLength(0L);
            bp.Save((Stream)ms, ImageFormat.Bmp);
            return ms.ToArray();
        }

        /// <summary>
        /// bytes转image图片
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static Image CreateImageFromBytes(byte[] buffer)
        {
            using (MemoryStream ms = new MemoryStream(buffer)) {
                Image image = System.Drawing.Image.FromStream(ms);
                return image;
            }
        }

        /// <summary>
        /// 图片转二进制byte
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] CreateBytesFromImage(Image image)
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

        /// <summary>
        /// 将bytes转图片并保存到本地
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string SaveImageFromBytes(string fileName, byte[] buffer)
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
            File.WriteAllBytes(file, buffer);
            return file;
        }
    }
}
