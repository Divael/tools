using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Drawing;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.IO;
using System.Linq;

namespace Tools
{
    /// <summary>
    /// <para>　</para>
    /// 　常用工具类——字符串操作类
    /// <para>　---------------------------------------------------</para>
    /// <para>　IsNum：判断输入的字符是不是全为数字</para>
    /// <para>　t：替换内容中特殊字符为全角</para>
    /// <para>　FormatBytesStr：格式化字节数字符串</para>
    /// <para>　ToSChinese：转换为简体中文</para>
    /// <para>　ToTChinese：转换为繁体中文</para>
    /// <para>　ToColor：将字符串转换为Color</para>
    /// <para>　IsStringDate：检查一个字符串是否可以转化为日期，一般用于验证用户输入日期的合法性。</para>
    /// <para>　CutString：工具方法：NET截取指定长度汉字超出部分以"..."代替</para>
    /// <para>　Verify：生成指定位数的随机数字或字符串</para>
    /// <para>　RTrim：删除字符串尾部的回车/换行/空格</para>
    /// <para>　ResponseFile：以指定的ContentType输出指定文件文件</para>
    /// <para>　StrFormat：替换回车换行符为html换行符</para>
    /// <para>　RemoveUnsafeHtml：过滤HTML中的不安全标签</para>
    /// <para>　FilterStr：删除字符串中不安全字符</para>
    /// <para>　Split：分割字符串</para>
    /// <para>　DelLastComma：删除最后结尾的一个逗号</para>
    /// <para>　DelLastChar：删除最后结尾的指定字符后的字符</para>
    /// <para>　ToSBC：半角转换为全角</para>
    /// <para>　ToDBC：全角转换为半角</para>
    /// <para>　GetArrayID：获取指定字符串在指定字符串数组中的位置</para>
    /// <para>　IsInArray：判断指定字符串是否属于指定字符串数组中的一个元素</para>
    /// <para>　IsInArray：判断指定字符串是否属于内部以分隔符分割单词的字符串的一个元素</para>
    /// <para>  DeleteStrArrTheOne：删除字符串数组中的一个元素</para>
    /// <para>　ClipboardData：将指定字符串复制到剪贴板</para>
    /// <para>　DoubleToRound：将Double类型的数据四舍五入</para>
    /// <para>　GetStrAToZ：将指定字符串中的汉字转换为拼音首字母的缩写，其中非汉字保留为原字符[生成指定字符串的助记码]</para>
    /// <para>　GetStringLength：返回字符串真实长度, 1个汉字长度为2</para>
    /// <para>　UTF8ToGB2312：将UTF-８字符串转为GB2312</para>
    /// <para>　GB2312ToUTF8：将GB2312编码字符串转为UTF8</para>
    /// <para>　BytesToHexString:       bytes转string</para>
    /// <para>　StringToHexString : string 转 HexsStringbytes</para>
    /// <para>　StringToBytes:      string 转 bytes</para>
    /// <para>　Base64ToString:     Base64和String互转</para>
    /// <para>　BytesGetBytes:     从bytes中截取有用的bytes</para>
    /// <para>  StringArraySubStringArray   从string[]中截取有用的string[] StringArraySubStringArray</para>
    /// <para>　StringIsExist:     检查字符串中是否有compare的字符有返回true</para>
    /// </summary>
    public class StringHelper
    {

        #region 判断输入的字符是不是全为数字
        /// <summary>
        /// 判断输入的字符是不是全为数字
        /// </summary>
        /// <param name="Str">要判断的字符串</param>
        /// <returns>是否为字符：True或False</returns>
        public static bool IsNum(string Str)
        {
            bool blResult = true;
            if (Str == "")
                blResult = false;
            else
            {
                foreach (char Char in Str)
                {
                    if (!Char.IsNumber(Char))
                    {
                        blResult = false;
                        break;
                    }
                }
                if (blResult)
                    if (int.Parse(Str) == 0)
                        blResult = false;
            }
            return blResult;
        }

        /// <summary>
        /// 字符串分割  
        /// </summary>
        /// <param name="str">a=1&b=2</param>
        /// <returns></returns>
        public static Dictionary<string,string> StringSplit(string str)
        {
            string[] sArray = Regex.Split(str, "&", RegexOptions.IgnoreCase);
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            foreach (var item in sArray)
            {
                string[] Arrays = Regex.Split(str, "=", RegexOptions.IgnoreCase);
                keyValuePairs.Add(Arrays[0],Arrays[1]);
            }
            return keyValuePairs;
        }
        #endregion

        #region 替换内容中特殊字符为全角
        /// <summary>
        /// 替换内容中特殊字符为全角
        /// </summary>
        /// <param name="str">要替换的字符</param>
        /// <returns>替换后的结果字符串</returns>
        public static string t(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            str = str.Replace("'", "‘");
            str = str.Replace(";", "；");
            str = str.Replace(",", "，");
            str = str.Replace("?", "？");
            str = str.Replace("<", "＜");
            str = str.Replace(">", "＞");
            str = str.Replace("(", "(");
            str = str.Replace(")", ")");
            str = str.Replace("@", "＠");
            str = str.Replace("=", "＝");
            str = str.Replace("+", "＋");
            str = str.Replace("*", "＊");
            str = str.Replace("&", "＆");
            str = str.Replace("#", "＃");
            str = str.Replace("%", "％");
            str = str.Replace("$", "￥");
            return str;
        }
        #endregion

        #region 格式化字节数字符串
        /// <summary>
        /// 格式化字节数字符串
        /// </summary>
        /// <param name="bytes">字节数</param>
        /// <returns>格式化的结果</returns>
        public static string FormatBytesStr(int bytes)
        {
            if (bytes > 1073741824)
            {
                return ((double)(bytes / 1073741824)).ToString("0") + "G";
            }
            if (bytes > 1048576)
            {
                return ((double)(bytes / 1048576)).ToString("0") + "M";
            }
            if (bytes > 1024)
            {
                return ((double)(bytes / 1024)).ToString("0") + "K";
            }
            return bytes.ToString() + "Bytes";
        }
        #endregion

        #region 转换为简体中文
        /// <summary>
        /// 转换为简体中文
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns></returns>
        public static string ToSChinese(string str)
        {
            //System.Globalization.CultureInfo cl = new System.Globalization.CultureInfo("zh-TW", false);
            return Microsoft.VisualBasic.Strings.StrConv(str, VbStrConv.SimplifiedChinese, 0);
        }
        #endregion

        #region 转换为繁体中文
        /// <summary>
        /// 转换为繁体中文
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns></returns>
        public static string ToTChinese(string str)
        {
            System.Globalization.CultureInfo cl = new System.Globalization.CultureInfo("zh-CN", false);
            return Microsoft.VisualBasic.Strings.StrConv(str, Microsoft.VisualBasic.VbStrConv.TraditionalChinese, cl.LCID);
        }
        #endregion

        #region 将字符串转换为Color
        ///// <summary>
        ///// 将字符串转换为Color
        ///// </summary>
        ///// <param name="color">字符串颜色：#000000</param>
        ///// <returns></returns>
        //public static Color ToColor(string color)
        //{
        //    int red, green, blue = 0;
        //    char[] rgb;
        //    color = color.TrimStart('#');
        //    color = Regex.Replace(color.ToLower(), "[g-zG-Z]", "");
        //    switch (color.Length)
        //    {
        //        case 3:
        //            rgb = color.ToCharArray();
        //            red = Convert.ToInt32(rgb[0].ToString() + rgb[0].ToString(), 16);
        //            green = Convert.ToInt32(rgb[1].ToString() + rgb[1].ToString(), 16);
        //            blue = Convert.ToInt32(rgb[2].ToString() + rgb[2].ToString(), 16);
        //            return Color.FromArgb(red, green, blue);
        //        case 6:
        //            rgb = color.ToCharArray();
        //            red = Convert.ToInt32(rgb[0].ToString() + rgb[1].ToString(), 16);
        //            green = Convert.ToInt32(rgb[2].ToString() + rgb[3].ToString(), 16);
        //            blue = Convert.ToInt32(rgb[4].ToString() + rgb[5].ToString(), 16);
        //            return Color.FromArgb(red, green, blue);
        //        default:
        //            return Color.FromName(color);
        //    }
        //}
        #endregion

        #region 检查一个字符串是否可以转化为日期，一般用于验证用户输入日期的合法性。
        /// <summary>
        /// 检查一个字符串是否可以转化为日期，一般用于验证用户输入日期的合法性。
        /// </summary>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否可以转化为日期的bool值。</returns>
        public static bool IsStringDate(string _value)
        {
            DateTime dt;
            try
            {
                dt = DateTime.Parse(_value);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 工具方法：NET截取指定长度汉字超出部分以"..."代替
        /// <summary>
        /// 工具方法：NET截取指定长度汉字超出部分以"..."代替
        /// 使用示例：string str = StringSubstr("abcde",3,"...");
        /// </summary>
        /// <param name="oldStr">要截取的字符串[string]</param>
        /// <param name="maxLength">截取后字符串的最大长度[int]</param>
        /// <param name="endWith">超过长度的后缀，如：...或###等[string]</param>
        /// <returns>如果超过长度，返回截断后的新字符串加上后缀，否则，返回原字符串[string]</returns>
        public static string CutString(string oldStr, int maxLength, string endWith)
        {
            if (string.IsNullOrEmpty(oldStr))
                //   throw   new   NullReferenceException( "原字符串不能为空 ");    
                return oldStr + endWith;
            if (maxLength < 1)
                throw new Exception("返回的字符串长度必须大于[0] ");
            if (oldStr.Length > maxLength)
            {
                string strTmp = oldStr.Substring(0, maxLength);
                if (string.IsNullOrEmpty(endWith))
                    return strTmp;
                else
                    return strTmp + endWith;
            }
            return oldStr;
        }
        #endregion

        #region 生成指定位数的随机数字或字符串
        /// <summary>
        /// 生成指定位数的随机数字或字符串
        /// </summary>
        /// <param name="DataString">自定义随机字符串范围</param>
        /// <param name="RanLength">长度</param>
        /// <param name="Session">是否要将返回结果写入到SESSION</param>
        /// <returns></returns>
        public static string Verify(string DataString, int RanLength, bool Session)
        {
            string ValiDataString;
            if (String.IsNullOrEmpty(DataString))
            {
                ValiDataString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            }
            else
            {
                ValiDataString = DataString.ToUpper(); ;
            }
            string rndstring = "";
            Random rnd = new Random();
            int i = 1;
            while (i <= RanLength)
            {
                rndstring += ValiDataString[rnd.Next(ValiDataString.Length)];
                i++;
            }
            //if (Session) System.Web.HttpContext.Current.Session["verify"] = rndstring;
            return rndstring;
        }
        #endregion

        #region 删除字符串尾部的回车/换行/空格
        /// <summary>
        /// 删除字符串尾部的回车/换行/空格
        /// </summary>
        /// <param name="str">要删除的字符串</param>
        /// <returns></returns>
        public static string RTrim(string str)
        {
            for (int i = str.Length; i >= 0; i--)
            {
                if (str[i].Equals(" ") || str[i].Equals("\r") || str[i].Equals("\n"))
                {
                    str.Remove(i, 1);
                }
            }
            return str;
        }
        #endregion

        #region 以指定的ContentType输出指定文件文件
        /// <summary>
        /// 以指定的ContentType输出指定文件文件
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="filename">输出的文件名</param>
        /// <param name="filetype">将文件输出时设置的ContentType</param>
        //public static void ResponseFile(string filepath, string filename, string filetype)
        //{
        //    Stream iStream = null;

        //    // 缓冲区为10k
        //    byte[] buffer = new Byte[10000];

        //    // 文件长度
        //    int length;

        //    // 需要读的数据长度
        //    long dataToRead;

        //    try
        //    {
        //        // 打开文件
        //        iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);


        //        // 需要读的数据长度
        //        dataToRead = iStream.Length;

        //        HttpContext.Current.Response.ContentType = filetype;
        //        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Utils.EncyptHelper.UrlEncode(filename.Trim()).Replace("+", " "));

        //        while (dataToRead > 0)
        //        {
        //            // 检查客户端是否还处于连接状态
        //            if (HttpContext.Current.Response.IsClientConnected)
        //            {
        //                length = iStream.Read(buffer, 0, 10000);
        //                HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
        //                HttpContext.Current.Response.Flush();
        //                buffer = new Byte[10000];
        //                dataToRead = dataToRead - length;
        //            }
        //            else
        //            {
        //                // 如果不再连接则跳出死循环
        //                dataToRead = -1;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Loger.err("Error : " , ex);
        //    }
        //    finally
        //    {
        //        if (iStream != null)
        //        {
        //            // 关闭文件
        //            iStream.Close();
        //        }
        //    }
        //    HttpContext.Current.Response.End();
        //}
        #endregion

        #region 替换回车换行符为html换行符
        /// <summary>
        /// 替换回车换行符为html换行符
        /// </summary>
        public static string StrFormat(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("\r\n", "<br />");
                str = str.Replace("\n", "<br />");
                str2 = str;
            }
            return str2;
        }
        #endregion

        #region  过滤HTML中的不安全标签
        /// <summary>
        /// 过滤HTML中的不安全标签
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveUnsafeHtml(string content)
        {
            content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return content;
        }
        #endregion

        #region 删除字符串中不安全字符
        /// <summary>
        /// 删除字符串中不安全字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterStr(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Trim();
                str = str.Replace("&", "＠");
                str = str.Replace("<", "＜");
                str = str.Replace(">", "＞");
                str = str.Replace("'", "＇");
                str = str.Replace("《", "＜＜");
                str = str.Replace("》", "＞＞");
                str = str.Replace("select", "ＳＥＬＥＣＴ");
                str = str.Replace("join", "ＪＯＩＮ");
                str = str.Replace("union", "ＵＮＩＯＮ");
                str = str.Replace("where", "ＷＨＥＲＥ");
                str = str.Replace("insert", "ＩＮＳＥＲＴ");
                str = str.Replace("delete", "ＤＥＬＥＴＥ");
                str = str.Replace("update", "ＵＰＤＡＴＥ");
                str = str.Replace("like", "ＬＩＫＥ");
                str = str.Replace("drop", "ＤＲＯＰ");
                str = str.Replace("create", "ＣＲＥＡＴＥ");
                str = str.Replace("modify", "ＭＯＤＩＦＹ");
                str = str.Replace("rename", "ＲＥＮＡＭＥ");
                str = str.Replace("alert", "ＡＬＥＲＴ");
                str = str.Replace("cast", "ＣＡＳＴ");
                str = str.Replace("frame", "ＦＲＡＭＥ");
            }
            return str;
        }
        #endregion

        #region 分割字符串
        /// <summary>
        /// 分割字符串
        /// <param name="strContent">要分割的字符串</param>
        /// <param name="strSplit">分隔符</param>
        /// </summary>
        public static string[] Split(string strContent, string strSplit)
        {
            if (strContent.IndexOf(strSplit) < 0)
            {
                string[] tmp = { strContent };
                return tmp;
            }
            return Regex.Split(strContent, @strSplit.Replace(".", @"\."), RegexOptions.IgnoreCase);
        }
        #endregion

        #region 删除最后结尾的一个逗号
        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        /// <param name="str">要删除逗号的字符串</param>
        /// <returns>删除逗号后的字符串</returns>
        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(","));
        }
        #endregion

        #region 删除最后结尾的指定字符后的字符
        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        /// <param name="str">未删除的字符串</param>
        /// <param name="strchar">要删除之后的字符串</param>
        /// <returns>删除后的字符串</returns>
        public static string DelLastChar(string str, string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar));
        }
        #endregion

        #region 半角转换为全角
        /// <summary>
        /// 半角转换为全角
        /// </summary>
        /// <param name="input">要转换的半角字符串</param>
        /// <returns>转换后的全角字符串</returns>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }
        #endregion

        #region 全角转换为半角
        /// <summary>
        ///  转半角的函数(SBC case)
        /// </summary>
        /// <param name="input">要转换的全角</param>
        /// <returns>转换后的半角</returns>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion

        #region 获取指定字符串在指定字符串数组中的位置
        /// <summary>
        /// 获取指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="Str">字符串</param>
        /// <param name="StrArray">字符串数组</param>
        /// <param name="CaseAa">是否区分大小写：True为区分；False为不区分</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetArrayID(string Str, string[] StrArray, bool CaseAa)
        {
            int Id = -1;
            for (int i = 0; i < StrArray.Length; i++)
            {
                if (!CaseAa)
                {
                    if (Str.ToLower() == StrArray[i].ToLower()) Id = i;
                }
                else
                {
                    if (Str == StrArray[i]) Id = i;
                }
            }
            return Id;
        }
        #endregion

        #region 判断指定字符串是否属于指定字符串数组中的一个元素
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="Str">字符串</param>
        /// <param name="StrArray">字符串数组</param>
        /// <param name="CaseAa">是否区分大小写：True为区分，False为不区分</param>
        /// <returns>True或False</returns>
        public static bool IsInArray(string Str, string[] StrArray, bool CaseAa)
        {
            return GetArrayID(Str, StrArray, CaseAa) >= 0;
        }
        #endregion

        #region 判断指定字符串是否属于内部以分隔符分割单词的字符串的一个元素
        /// <summary>
        ///  判断指定字符串是否属于内部以逗号分割单词的字符串的一个元素
        /// </summary>
        /// <param name="Str">字符串</param>
        /// <param name="ArrayStr">内部以分隔符分割单词的字符串</param>
        /// <param name="StrSplit">字符串分隔符</param>
        /// <param name="CaseAa">是否区分大小写：True为区分，False为不区分</param>
        /// <returns>True或False</returns>
        public static bool IsInArray(string Str, string ArrayStr, string StrSplit, bool CaseAa)
        {
            return GetArrayID(Str, Split(ArrayStr, StrSplit), CaseAa) >= 0;
        }
        #endregion

        #region 删除字符串数组中的一个元素
        /// <summary>
        /// 删除字符串数组中的一个元素
        /// </summary>
        /// <param name="StrArr"></param>
        /// <param name="DeleteStr"></param>
        /// <returns></returns>
        public static string[] DeleteStrArrTheOne(string[] StrArr, string DeleteStr)
        {
            List<string> StrList = new List<string>();
            for (int i = 0; i < StrArr.Length; i++)
            {
                if (StrArr[i] != DeleteStr) StrList.Add(StrArr[i]);
            }
            string ArrStr = string.Empty;
            for (int j = 0; j < StrList.Count; j++)
            {
                if (j > 0) ArrStr += ",";
                ArrStr += StrList[j].ToString();
            }
            return Tools.StringHelper.Split(ArrStr, ",");
        }
        #endregion

        #region 将指定字符串复制到剪贴板
        /// <summary>
        /// 将指定字符串复制到剪贴板'
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="ClipblardStr">要复制到剪贴板的字符串内容</param>
        //public static void ClipboardData(System.Web.UI.Page page, string ClipblardStr)
        //{
        //    if (!string.IsNullOrEmpty(ClipblardStr))
        //    {
        //        string JsStr = "<script language='javascript'>";
        //        JsStr += "window.clipboardData.setData('text', '" + ClipblardStr + "')";
        //        JsStr += "</script>";
        //        page.ClientScript.RegisterStartupScript(page.GetType(), "message", JsStr);
        //    }
        //}
        /// <summary>
        /// 将指定字符串复制到剪贴板并指定返回的信息
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="ClipblardStr">要复制到剪贴板的字符串内容</param>
        /// <param name="BackMessage">要返回弹出消息，null为不弹出</param>
        //public static void ClipboardData(System.Web.UI.Page page, string ClipblardStr, string BackMessage)
        //{
        //    if (!string.IsNullOrEmpty(ClipblardStr))
        //    {
        //        string JsStr = "<script language='javascript'>";
        //        JsStr += "window.clipboardData.setData('text', '" + ClipblardStr + "');";
        //        if (!string.IsNullOrEmpty(BackMessage)) JsStr += "alert('" + BackMessage + "');";
        //        JsStr += "</script>";
        //        page.ClientScript.RegisterStartupScript(page.GetType(), "message", JsStr);
        //    }
        //}
        #endregion

        #region 将Double类型的数据四舍五入
        /// <summary>
        /// 将Double类型的数据四舍五入
        /// </summary>
        /// <param name="Doubles">要四舍五入的Double类型数据</param>
        /// <param name="Point">保留小数点位数</param>
        /// <returns></returns>
        public static string DoubleToRound(double Doubles, int Point)
        {
            return Doubles.ToString("F" + Point);
        }
        #endregion

        #region 将指定字符串中的汉字转换为拼音首字母的缩写，其中非汉字保留为原字符[生成指定字符串的助记码]
        /// <summary>
        /// 将指定字符串中的汉字转换为拼音首字母的缩写，其中非汉字保留为原字符[生成指定字符串的助记码]
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetStrAToZ(string text)
        {
            char pinyin;
            byte[] array;
            StringBuilder sb = new StringBuilder(text.Length);
            foreach (char c in text)
            {
                pinyin = c;
                array = Encoding.Default.GetBytes(new char[] { c });

                if (array.Length == 2)
                {
                    int i = array[0] * 0x100 + array[1];
                    if (i < 0xB0A1) pinyin = c;
                    else
                        if (i < 0xB0C5) pinyin = 'a';
                    else
                            if (i < 0xB2C1) pinyin = 'b';
                    else
                                if (i < 0xB4EE) pinyin = 'c';
                    else
                                    if (i < 0xB6EA) pinyin = 'd';
                    else
                                        if (i < 0xB7A2) pinyin = 'e';
                    else
                                            if (i < 0xB8C1) pinyin = 'f';
                    else
                                                if (i < 0xB9FE) pinyin = 'g';
                    else
                                                    if (i < 0xBBF7) pinyin = 'h';
                    else
                                                        if (i < 0xBFA6) pinyin = 'g';
                    else
                                                            if (i < 0xC0AC) pinyin = 'k';
                    else
                                                                if (i < 0xC2E8) pinyin = 'l';
                    else
                                                                    if (i < 0xC4C3) pinyin = 'm';
                    else
                                                                        if (i < 0xC5B6) pinyin = 'n';
                    else
                                                                            if (i < 0xC5BE) pinyin = 'o';
                    else
                                                                                if (i < 0xC6DA) pinyin = 'p';
                    else
                                                                                    if (i < 0xC8BB) pinyin = 'q';
                    else
                                                                                        if (i < 0xC8F6) pinyin = 'r';
                    else
                                                                                            if (i < 0xCBFA) pinyin = 's';
                    else
                                                                                                if (i < 0xCDDA) pinyin = 't';
                    else
                                                                                                    if (i < 0xCEF4) pinyin = 'w';
                    else
                                                                                                        if (i < 0xD1B9) pinyin = 'x';
                    else
                                                                                                            if (i < 0xD4D1) pinyin = 'y';
                    else
                                                                                                                if (i < 0xD7FA) pinyin = 'z';
                }
                sb.Append(pinyin);
            }
            return sb.ToString();
        }
        #endregion

        #region 返回字符串真实长度, 1个汉字长度为2个byte，也不固定，如果是UTF-8的话
        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2个byte，也不固定，如果是UTF-8的话
        /// </summary>
        /// <param name="Str">字符串</param>
        /// <returns>字符长度</returns>
        public static int GetStringLength(string Str)
        {
            return Encoding.Default.GetBytes(Str).Length;
        }
        #endregion

        #region 将UTF-８字符串转为GB2312
        /// <summary>
        /// 将UTF-８字符串转为GB2312
        /// </summary>
        /// <param name="Utf8Str">Utf8编码字符串</param>
        /// <returns></returns>
        public static string UTF8ToGB2312(string Utf8Str)
        {
            string Gb2312Str = string.Empty;
            Encoding Utf8 = Encoding.UTF8;
            Encoding Gb2312 = Encoding.GetEncoding("gb2312");
            byte[] unicodeBytes = Utf8.GetBytes(Utf8Str);
            byte[] asciiBytes = Encoding.Convert(Utf8, Gb2312, unicodeBytes);
            char[] asciiChars = new char[Gb2312.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            Gb2312.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            Gb2312Str = new string(asciiChars);
            return Gb2312Str;
        }
        #endregion

        #region 将GB2312编码字符串转为UTF8
        /// <summary>
        /// 将GB2312编码字符串转为UTF8
        /// </summary>
        /// <param name="Gb2312Str">GB2312编码字符串</param>
        /// <returns></returns>
        public static string GB2312ToUTF8(string Gb2312Str)
        {
            try
            {
                Encoding uft8 = Encoding.UTF8;
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] temp = gb2312.GetBytes(Gb2312Str);
                byte[] temp1 = Encoding.Convert(gb2312, uft8, temp);
                string result = uft8.GetString(temp1);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region 字节数组转16进制字符串
        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToHexString(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2")+" ";
                }
            }
            return returnStr;
        }
        #endregion

        #region string 转 HexString
        /// <summary>
        /// string 转 HexString
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符，以 空格 隔开
            {
                result += " " + Convert.ToString(b[i], 16);
            }
            return result;
        }
        #endregion

        #region Base64和String互转
        /// <summary>
        /// base64转字符串类型
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string Base64ToString(string source, Encoding e)
        {
            return Common.GetString(Convert.FromBase64String(source), e);
        }

        /// <summary>
        /// 字符串类型转base64
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string StringToBase64(string source, Encoding e)
        {
            return Convert.ToBase64String(Common.GetBytes(source, e));
        }

        #endregion

        #region 16进制字符串转bytes数组 A1 E2 =》 bytes以一个空格隔开
        /// <summary>
        /// 16进制字符串转bytes数组
        /// A1 E2 =》 bytes以一个空格隔开
        /// </summary>
        /// <param name="s">16进制的字符串</param>
        /// <returns>byte[]</returns>
        public static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "").Trim().ToUpper();
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }
        #endregion

        #region 从bytes中截取有用的bytes
        /// <summary>
        /// 从bytes中截取有用的bytes
        /// </summary>
        /// <param name="bytes">要截取的byte数组</param>
        /// <param name="StartIndex">开始截取的位置index</param>
        /// <param name="Length">要截取的长度</param>
        /// <returns></returns>
        public static byte[] BytesGetBytes(byte[] bytes,int StartIndex,int Length) {
            return bytes.Skip(StartIndex).Take(Length).ToArray();
        }
        #endregion

        #region 从string[]中截取有用的string[] StringArraySubStringArray
        /// <summary>
        /// 从string[]中截取有用的string[]
        /// </summary>
        /// <param name="strArray">要截取的String[]数组</param>
        /// <param name="StartIndex">开始截取的位置index</param>
        /// <param name="Length">要截取的长度</param>
        /// <returns></returns>
        public static string[] StringArraySubStringArray(string[] strArray, int StartIndex, int Length)
        {
            return strArray.Skip(StartIndex).Take(Length).ToArray();
        }
        #endregion

        /// <summary>
        /// 有疑问  需要能直接转成数字的字符串
        /// str转为16进制字符串 5位 例如
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringConvert16(string str)
        {
            StringBuilder d1 = new StringBuilder();
            d1.Append(int.Parse(str).ToString("X2"));
            for (int i = 0; i <= 4; i++)
            {
                i = d1.Length - 1;
                if (d1.Length < 4)
                {
                    d1.Insert(0, 0);
                }
                if (d1.Length == 4)
                {
                    d1.Insert(2, " ");
                }
            }
            return d1.ToString();
        }


        #region Hex string and Byte[] transform

        /// <summary>
        /// 字节数据转化成16进制表示的字符串 -> byte[] 中单个byte不分割
        /// Byte data into a string of 16 binary representations
        /// </summary>
        /// <param name="InBytes">字节数组</param>
        /// <returns>返回的字符串</returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <example>
        /// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ByteToHexStringExample1" title="ByteToHexString示例" />
        /// </example>
        public static string ByteToHexString(byte[] InBytes)
        {
            return ByteToHexString(InBytes, (char)0);
        }

        /// <summary>
        /// 疑问
        /// 字节数据转化成16进制表示的字符串 ->
        /// Byte data into a string of 16 binary representations
        /// </summary>
        /// <param name="InBytes">字节数组</param>
        /// <param name="segment">分割符</param>
        /// <returns>返回的字符串</returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <example>
        /// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ByteToHexStringExample2" title="ByteToHexString示例" />
        /// </example>
        public static string ByteToHexString(byte[] InBytes, char segment)
        {
            /*
            if (bytes != null)
            {
                return Encoding.Default.GetString(bytes);
            }
            return "";
             */
            if (InBytes==null)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            foreach (byte InByte in InBytes)
            {
                if (segment == 0) sb.Append(string.Format("{0:X2}", InByte));
                else sb.Append(string.Format("{0:X2}{1}", InByte, segment));
            }

            if (segment != 0 && sb.Length > 1 && sb[sb.Length - 1] == segment)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }



        /// <summary>
        /// 疑问
        /// 字符串数据转化成16进制表示的字符串 ->
        /// String data into a string of 16 binary representations
        /// </summary>
        /// <param name="InString">输入的字符串数据Encoding.Unicode.GetBytes</param>
        /// <returns>返回的字符串</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static string ByteToHexString(string InString)
        {
            return ByteToHexString(Encoding.Unicode.GetBytes(InString));
        }


        private static List<char> hexCharList = new List<char>()
            {
                '0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F'
            };

        /// <summary>
        /// 将16进制的字符串转化成Byte数据，将检测每2个字符转化，也就是说，中间可以是任意字符 ->
        /// Converts a 16-character string into byte data, which will detect every 2 characters converted, that is, the middle can be any character
        /// </summary>
        /// <param name="hex">十六进制的字符串，中间可以是任意的分隔符</param>
        /// <returns>转换后的字节数组</returns>
        /// <remarks>参数举例：AA 01 34 A8</remarks>
        /// <example>
        /// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="HexStringToBytesExample" title="HexStringToBytes示例" />
        /// </example>
        public static byte[] HexStringToBytes(string hex)
        {
            hex = hex.ToUpper();

            MemoryStream ms = new MemoryStream();

            for (int i = 0; i < hex.Length; i++)
            {
                if ((i + 1) < hex.Length)
                {
                    if (hexCharList.Contains(hex[i]) && hexCharList.Contains(hex[i + 1]))
                    {
                        // 这是一个合格的字节数据
                        ms.WriteByte((byte)(hexCharList.IndexOf(hex[i]) * 16 + hexCharList.IndexOf(hex[i + 1])));
                        i++;
                    }
                }
            }

            byte[] result = ms.ToArray();
            ms.Dispose();
            return result;
        }

        #endregion

        /// <summary>
        /// 将十六进制字节数组转化为int类型卡号
        /// </summary>
        /// <param name="vs"></param>
        /// <returns></returns>
        public static int bytesToInt32(byte [] vs) {
            try
            {
               return BitConverter.ToInt32(vs, 0);

            }
            catch (Exception)
            {
                throw new Exception("bytesToInt32转换失败！");
            }
        }
    }
}
