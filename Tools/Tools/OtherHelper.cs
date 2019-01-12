using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace Tools
{
    /// <summary>
    /// <para>　</para>
    /// 常用工具类——其它杂项
    /// <para>　-------------------------------------------------</para>
    /// <para>　CreateBar39：生成39码条形码,请生成的条码文件置于静态控件中：Literal[ +2重载 ]</para>
    /// <para>　CreateBarEAN13：生成EAN-13码条形码,请生成的条码文件置于静态控件中：Literal</para>
    /// <para>　AsynWebUrl：通过WebRequest异步访问地址并返回值</para>
    /// </summary>
    //public class OtherHelper
    //{
    //    #region 生成39码条形码,请生成的条码文件置于静态控件中：Literal
    //    /// <summary>
    //    /// 生成39码条形码,请生成的条码文件置于静态控件中：Literal
    //    /// </summary>
    //    /// <param name="CodeText">和码文字</param>
    //    /// <param name="CodeLineWidth">线条宽度：０为默认值：2</param>
    //    /// <param name="CodeLineHeight">线条高度：0为默认值：60</param>
    //    /// <param name="CodeTextColor">条码底部文字颜色[字符串颜色：#000000]，为空或null默认为黑色</param>
    //    /// <param name="CodeLineColor">条码线条颜色[字符串颜色：#000000],为空或null为默认黑色</param>
    //    /// <returns></returns>
    //    public static string CreateBar39(string CodeText, int CodeLineWidth, int CodeLineHeight, string CodeTextColor, string CodeLineColor)
    //    {
    //        Hashtable ht = new Hashtable();
    //        #region 39码 12位
    //        ht.Add('A', "110101001011");
    //        ht.Add('B', "101101001011");
    //        ht.Add('C', "110110100101");
    //        ht.Add('D', "101011001011");
    //        ht.Add('E', "110101100101");
    //        ht.Add('F', "101101100101");
    //        ht.Add('G', "101010011011");
    //        ht.Add('H', "110101001101");
    //        ht.Add('I', "101101001101");
    //        ht.Add('J', "101011001101");
    //        ht.Add('K', "110101010011");
    //        ht.Add('L', "101101010011");
    //        ht.Add('M', "110110101001");
    //        ht.Add('N', "101011010011");
    //        ht.Add('O', "110101101001");
    //        ht.Add('P', "101101101001");
    //        ht.Add('Q', "101010110011");
    //        ht.Add('R', "110101011001");
    //        ht.Add('S', "101101011001");
    //        ht.Add('T', "101011011001");
    //        ht.Add('U', "110010101011");
    //        ht.Add('V', "100110101011");
    //        ht.Add('W', "110011010101");
    //        ht.Add('X', "100101101011");
    //        ht.Add('Y', "110010110101");
    //        ht.Add('Z', "100110110101");
    //        ht.Add('0', "101001101101");
    //        ht.Add('1', "110100101011");
    //        ht.Add('2', "101100101011");
    //        ht.Add('3', "110110010101");
    //        ht.Add('4', "101001101011");
    //        ht.Add('5', "110100110101");
    //        ht.Add('6', "101100110101");
    //        ht.Add('7', "101001011011");
    //        ht.Add('8', "110100101101");
    //        ht.Add('9', "101100101101");
    //        ht.Add('+', "100101001001");
    //        ht.Add('-', "100101011011");
    //        ht.Add('*', "100101101101");
    //        ht.Add('/', "100100101001");
    //        ht.Add('%', "101001001001");
    //        ht.Add('$', "100100100101");
    //        ht.Add('.', "110010101101");
    //        ht.Add(' ', "100110101101");
    //        #endregion
    //        CodeText = "*" + CodeText.ToUpper() + "*";
    //        CodeLineWidth = CodeLineWidth == '0' ? 2 : CodeLineWidth;
    //        CodeLineHeight = CodeLineHeight == '0' ? 60 : CodeLineHeight;
    //        string CodeTextFont = "OCR-B-10 BT";
    //        bool FontIsExits = false;
    //        foreach (FontFamily FF in FontFamily.Families)
    //        {
    //            if (FF.Name == CodeTextFont) FontIsExits = true;
    //        }
    //        if (!FontIsExits) CodeTextFont = "楷体";
    //        string result_bin = "";//二进制串
    //        try
    //        {
    //            foreach (char ch in CodeText)
    //            {
    //                result_bin += ht[ch].ToString();
    //                result_bin += "0";//间隔，与一个单位的线条宽度相等
    //            }
    //        }
    //        catch { return "存在不允许的字符！"; }
    //        string result_html = "";//HTML代码
    //        string color = "";//颜色
    //        foreach (char c in result_bin)
    //        {
    //            color = c == '0' ? "#FFFFFF" : (!string.IsNullOrEmpty(CodeLineColor) ? CodeLineColor : "#000000");
    //            result_html += "<div style=\"width:" + CodeLineWidth + "px;height:" + CodeLineHeight + "px;float:left;background:" + color + ";\"></div>";
    //        }
    //        result_html += "<div style=\"clear:both\"></div>";

    //        int len = ht['*'].ToString().Length;
    //        foreach (char c in CodeText)
    //        {
    //            result_html += "<div style=\"width:" + (CodeLineWidth * (len + 1)) + "px;float:left;color:" + CodeTextColor + ";text-align:center;\">" + c + "</div>";
    //        }
    //        result_html += "<div style=\"clear:both\"></div>";

    //        return "<div style=\"background:#FFFFFF;padding:5px;font-size:" + (CodeLineWidth * 10) + "px;font-family:'" + CodeTextFont + "';\">" + result_html + "</div>";
    //    }

    //    /// <summary>
    //    /// 生成39码条形码,请生成的条码文件置于静态控件中：Literal
    //    /// </summary>
    //    /// <param name="CodeText">条码文字</param>
    //    /// <param name="CodeLineWidth">线条宽度：０为默认值：2</param>
    //    /// <param name="CodeLineHeight">线条高度：0为默认值：60</param>
    //    /// <param name="CodeTextColor">条码底部文字颜色[Color颜色：Color.red]，为空或null默认为黑色</param>
    //    /// <param name="CodeLineColor">条码线条颜色[Color颜色：Color.red],为空或null为默认黑色</param>
    //    /// <returns></returns>
    //    public static string CreateBar39(string CodeText, int CodeLineWidth, int CodeLineHeight, Color CodeTextColor, Color CodeLineColor)
    //    {
    //        return CreateBar39(CodeText, CodeLineWidth, CodeLineHeight, CodeTextColor.Name, CodeLineColor.Name);
    //    }
    //    #endregion

    //    #region 生成EAN-13码条形码,请生成的条码文件置于静态控件中：Literal
    //    /// <summary>
    //    /// 生成EAN-13码条形码,请生成的条码文件置于静态控件中：Literal
    //    /// </summary>
    //    /// <param name="CodeText">条码文字</param>
    //    /// <param name="CodeLineWidth">条码线条宽度：0为默认值：２</param>
    //    /// <param name="CodeLineHeight">条码线条高度：０为默认值：100</param>
    //    /// <param name="CodeTextColor">条码文字颜色[字符串颜色：#000000]：为空或null为默认黑色</param>
    //    /// <param name="CodeLineColor">条码线条颜色[字符串颜色：#000000]：为空或null为默认黑色</param>
    //    /// <returns></returns>
    //    public static string CreateBarEAN13(string CodeText, int CodeLineWidth, int CodeLineHeight, string CodeTextColor, string CodeLineColor)
    //    {
    //        int checkcode_input = -1;//输入的校验码
    //        CodeLineHeight = CodeLineHeight == 0 ? 2 : CodeLineHeight;
    //        CodeLineWidth = CodeLineWidth == 0 ? 100 : CodeLineWidth;
    //        if (!Regex.IsMatch(CodeText, @"^\d{12}$"))
    //        {
    //            if (!Regex.IsMatch(CodeText, @"^\d{13}$"))
    //            {
    //                return "存在不允许的字符！";
    //            }
    //            else
    //            {
    //                checkcode_input = int.Parse(CodeText[12].ToString());
    //                CodeText = CodeText.Substring(0, 12);
    //            }
    //        }

    //        int sum_even = 0;//偶数位之和
    //        int sum_odd = 0;//奇数位之和

    //        for (int i = 0; i < 12; i++)
    //        {
    //            if (i % 2 == 0)
    //            {
    //                sum_odd += int.Parse(CodeText[i].ToString());
    //            }
    //            else
    //            {
    //                sum_even += int.Parse(CodeText[i].ToString());
    //            }
    //        }

    //        int checkcode = (10 - (sum_even * 3 + sum_odd) % 10) % 10;//校验码

    //        if (checkcode_input > 0 && checkcode_input != checkcode)
    //        {
    //            return "输入的校验码错误！";
    //        }

    //        CodeText += checkcode;//变成13位

    //        // 000000000101左侧42个01010右侧35个校验7个101000000000
    //        // 6        101左侧6位 01010右侧5位 校验1位101000000000

    //        string result_bin = "";//二进制串
    //        result_bin += "000000000101";

    //        string type = ean13type(CodeText[0]);
    //        for (int i = 1; i < 7; i++)
    //        {
    //            result_bin += ean13(CodeText[i], type[i - 1]);
    //        }
    //        result_bin += "01010";
    //        for (int i = 7; i < 13; i++)
    //        {
    //            result_bin += ean13(CodeText[i], 'C');
    //        }
    //        result_bin += "101000000000";

    //        string result_html = "";//HTML代码
    //        string color = "";//颜色
    //        string LColor = !string.IsNullOrEmpty(CodeLineColor) ? CodeLineColor : "#000000";
    //        string TColor = !string.IsNullOrEmpty(CodeTextColor) ? CodeTextColor : "#000000";
    //        int height_bottom = CodeLineWidth * 5;
    //        string CodeTextFont = "OCR-B-10 BT";
    //        bool FontIsExits = false;
    //        foreach (FontFamily FF in FontFamily.Families)
    //        {
    //            if (FF.Name == CodeTextFont) FontIsExits = true;
    //        }
    //        if (!FontIsExits) CodeTextFont = "楷体";
    //        foreach (char c in result_bin)
    //        {
    //            color = c == '0' ? "#FFFFFF" : LColor;
    //            result_html += "<div style=\"width:" + CodeLineWidth + "px;height:" + CodeLineHeight + "px;float:left;background:" + color + ";\"></div>";
    //        }
    //        result_html += "<div style=\"clear:both\"></div>";
    //        result_html += "<div style=\"float:left;color:" + TColor + ";width:" + (CodeLineWidth * 9) + "px;text-align:center;\">" + CodeText[0] + "</div>";
    //        result_html += "<div style=\"float:left;width:" + CodeLineWidth + "px;height:" + height_bottom + "px;background:" + LColor + ";\"></div>";
    //        result_html += "<div style=\"float:left;width:" + CodeLineWidth + "px;height:" + height_bottom + "px;background:#FFFFFF;\"></div>";
    //        result_html += "<div style=\"float:left;width:" + CodeLineWidth + "px;height:" + height_bottom + "px;background:" + LColor + ";\"></div>";
    //        for (int i = 1; i < 7; i++)
    //        {
    //            result_html += "<div style=\"float:left;width:" + (CodeLineWidth * 7) + "px;color:" + TColor + ";text-align:center;\">" + CodeText[i] + "</div>";
    //        }
    //        result_html += "<div style=\"float:left;width:" + CodeLineWidth + "px;height:" + height_bottom + "px;background:#FFFFFF;\"></div>";
    //        result_html += "<div style=\"float:left;width:" + CodeLineWidth + "px;height:" + height_bottom + "px;background:" + LColor + ";\"></div>";
    //        result_html += "<div style=\"float:left;width:" + CodeLineWidth + "px;height:" + height_bottom + "px;background:#FFFFFF;\"></div>";
    //        result_html += "<div style=\"float:left;width:" + CodeLineWidth + "px;height:" + height_bottom + "px;background:" + LColor + ";\"></div>";
    //        result_html += "<div style=\"float:left;width:" + CodeLineWidth + "px;height:" + height_bottom + "px;background:#FFFFFF;\"></div>";
    //        for (int i = 7; i < 13; i++)
    //        {
    //            result_html += "<div style=\"float:left;width:" + (CodeLineWidth * 7) + "px;color:" + TColor + ";text-align:center;\">" + CodeText[i] + "</div>";
    //        }
    //        result_html += "<div style=\"float:left;width:" + CodeLineWidth + "px;height:" + height_bottom + "px;background:" + LColor + ";\"></div>";
    //        result_html += "<div style=\"float:left;width:" + CodeLineWidth + "px;height:" + height_bottom + "px;background:#FFFFFF;\"></div>";
    //        result_html += "<div style=\"float:left;width:" + CodeLineWidth + "px;height:" + height_bottom + "px;background:" + LColor + ";\"></div>";
    //        result_html += "<div style=\"float:left;color:#000000;width:" + (CodeLineWidth * 9) + "px;\"></div>";
    //        result_html += "<div style=\"clear:both\"></div>";
    //        return "<div style=\"background:#FFFFFF;padding:0px;font-size:" + (CodeLineWidth * 10) + "px;font-family:'" + CodeTextFont + "';\">" + result_html + "</div>";
    //    }

    //    /// <summary>
    //    /// 生成EAN-13码条形码,请生成的条码文件置于静态控件中：Literal
    //    /// </summary>
    //    /// <param name="CodeText">条码文字</param>
    //    /// <param name="CodeLineWidth">条码线条宽度：0为默认值：２</param>
    //    /// <param name="CodeLineHeight">条码线条高度：０为默认值：100</param>
    //    /// <param name="CodeTextColor">条码文字颜色[字符串颜色：#000000]：为空或null为默认黑色</param>
    //    /// <param name="CodeLineColor">条码线条颜色[字符串颜色：#000000]：为空或null为默认黑色</param>
    //    /// <returns></returns>
    //    public static string CreateBarEAN13(string CodeText, int CodeLineWidth, int CodeLineHeight, Color CodeTextColor, Color CodeLineColor)
    //    {
    //        return CreateBarEAN13(CodeText, CodeLineWidth, CodeLineHeight, CodeTextColor.Name, CodeLineColor.Name);
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="c"></param>
    //    /// <param name="type"></param>
    //    /// <returns></returns>
    //    private static string ean13(char c, char type)
    //    {
    //        switch (type)
    //        {
    //            case 'A':
    //                {
    //                    switch (c)
    //                    {
    //                        case '0': return "0001101";
    //                        case '1': return "0011001";
    //                        case '2': return "0010011";
    //                        case '3': return "0111101";//011101
    //                        case '4': return "0100011";
    //                        case '5': return "0110001";
    //                        case '6': return "0101111";
    //                        case '7': return "0111011";
    //                        case '8': return "0110111";
    //                        case '9': return "0001011";
    //                        default: return "Error!";
    //                    }
    //                }
    //            case 'B':
    //                {
    //                    switch (c)
    //                    {
    //                        case '0': return "0100111";
    //                        case '1': return "0110011";
    //                        case '2': return "0011011";
    //                        case '3': return "0100001";
    //                        case '4': return "0011101";
    //                        case '5': return "0111001";
    //                        case '6': return "0000101";//000101
    //                        case '7': return "0010001";
    //                        case '8': return "0001001";
    //                        case '9': return "0010111";
    //                        default: return "Error!";
    //                    }
    //                }
    //            case 'C':
    //                {
    //                    switch (c)
    //                    {
    //                        case '0': return "1110010";
    //                        case '1': return "1100110";
    //                        case '2': return "1101100";
    //                        case '3': return "1000010";
    //                        case '4': return "1011100";
    //                        case '5': return "1001110";
    //                        case '6': return "1010000";
    //                        case '7': return "1000100";
    //                        case '8': return "1001000";
    //                        case '9': return "1110100";
    //                        default: return "Error!";
    //                    }
    //                }
    //            default: return "Error!";
    //        }
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="c"></param>
    //    /// <returns></returns>
    //    private static string ean13type(char c)
    //    {
    //        switch (c)
    //        {
    //            case '0': return "AAAAAA";
    //            case '1': return "AABABB";
    //            case '2': return "AABBAB";
    //            case '3': return "AABBBA";
    //            case '4': return "ABAABB";
    //            case '5': return "ABBAAB";
    //            case '6': return "ABBBAA";//中国
    //            case '7': return "ABABAB";
    //            case '8': return "ABABBA";
    //            case '9': return "ABBABA";
    //            default: return "Error!";
    //        }
    //    }
    //    #endregion

    //    #region 通过WebRequest异步访问地址并返回值
    //    /// <summary>
    //    /// 通过WebRequest异步访问地址并返回值
    //    /// <para>　注意：如果WebUrl中带有中文字符，并须先进行编码再传入：</para>
    //    /// <para>　HttpUtility.UrlEncode(CONTENT, Encoding.GetEncoding("gb2312"));</para>
    //    /// </summary>
    //    /// <param name="WebUrl">访问地址</param>
    //    /// <returns></returns>
    //    public static string AsynWebUrl(string WebUrl)
    //    {
    //        string Back = string.Empty;
    //        try
    //        {
    //            WebRequest MyRequest = WebRequest.Create(WebUrl);
    //            WebResponse MyResponse = MyRequest.GetResponse();
    //            Stream ResStream = MyResponse.GetResponseStream();
    //            StreamReader MySr = new StreamReader(ResStream, Encoding.Default);
    //            Back = MySr.ReadToEnd();
    //            MyResponse.Close();
    //            MySr.Close();
    //        }
    //        catch (Exception)
    //        {
    //        }
    //        return Back;
    //    }
    //    #endregion

    //}
}
