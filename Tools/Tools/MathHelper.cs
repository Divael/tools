using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
namespace Tools
{
    /// <summary>
    /// <para>　</para>
    /// 　常用工具类——验证类
    /// <para>　----------------------------------------------</para>
    /// <para>　IsEmail：检测是否符合email格式</para>
    /// <para>　IsURL：检测是否是正确的Url</para>
    /// <para>　CheckSql：检测是否有Sql危险字符</para>
    /// <para>　IsIP：是否为ip</para>
    /// <para>　IsNumber：判断是否是数字</para>
    /// <para>　IsNumberSign：是否数字字符串 可带正负号</para>
    /// <para>　IsDecimal：是否是浮点数</para>
    /// <para>　IsDecimalSign：是否是浮点数 可带正负号</para>
    /// <para>　IsPhone：检查字符串是否为手机号</para>
    /// <para>　IsHasCHZN：检测是否有中文字符</para>
    /// <para>　IsBoolen字符串能否转为Boolen类型</para>
    /// <para>　IsColor 字符串能否转为16进制字符串</para>
    /// </summary>
    public class MathHelper
    {
        #region 成员
        private static Regex RegPhone = new Regex("^[0-9]+[-]?[0-9]+[-]?[0-9]$");
        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //等价于^[+-]?\d+[.]?\d+$
        private static Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 
        private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");
        #endregion


        #region 检测是否符合email格式
        /// <summary>
        /// 检测是否符合email格式,需引用：using System.Text.RegularExpressions;
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        #endregion

        #region 检测是否是正确的Url
        /// <summary>
        /// 检测是否是正确的Url,需引用：using System.Text.RegularExpressions;
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }
        #endregion

        #region 检测是否有Sql危险字符
        /// <summary>
        /// 错误需要验证
        /// 检测是否有Sql危险字符,,需引用：using System.Text.RegularExpressions;
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool CheckSql(string str)
        {
            if (Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']") || Regex.IsMatch(str, @"select|insert|delete|from|count(|drop table|update|truncate|asc(|mid(|char(|xp_cmdshell|exec master|netlocalgroup administrators|:|net user|""|or|and"))
            {
                return true;
            }
            else
            {
                return false;
            }
            //return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }
        #endregion

        #region 是否为ip
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        #endregion

        #region 判断是否是数字
        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="inputData">字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {            
            Match m = RegNumber.Match(inputData);
            return m.Success;
        }
        #endregion

        #region  是否数字字符串 可带正负号
        /// <summary>
        /// 是否数字字符串 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumberSign(string inputData)
        {
            Match m = RegNumberSign.Match(inputData);
            return m.Success;
        }
        #endregion

        #region 是否是浮点数
        /// <summary>
        /// 是否是浮点数
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimal(string inputData)
        {
            Match m = RegDecimal.Match(inputData);
            return m.Success;
        }
        #endregion

        #region 是否是浮点数 可带正负号
        /// <summary>
        /// 是否是浮点数 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimalSign(string inputData)
        {
            Match m = RegDecimalSign.Match(inputData);
            return m.Success;
        }
        #endregion

        #region 检查字符串是否为手机号
        /// <summary>
        /// 检查字符串是否为手机号
        /// </summary>
        /// <param name="inputData">字符串</param>
        /// <returns>是否为手机号</returns>
        public static bool IsPhone(string inputData)
        {
            Match m = RegPhone.Match(inputData);
            return m.Success;
        }
        #endregion

        #region 检测是否有中文字符
        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsHasCHZN(string inputData)
        {
            Match m = RegCHZN.Match(inputData);
            return m.Success;
        }
        #endregion

        #region 字符串能否转为Boolen类型
        /// <summary>
        /// 字符串能否转为Boolen类型
        /// </summary>
        /// <param name="Str">字符串,一般为True或False</param>
        /// <returns></returns>
        public static bool IsBoolen(string Str)
        {
            bool Flag = false;
            try
            {
                Convert.ToBoolean(Str);
                Flag = true;
            }
            catch (Exception)
            {
            }
            return Flag;
        }
        #endregion

        #region IsColor 字符串能否转为16进制字符串
        /// <summary>
        /// IsColor 字符串能否转为16进制字符串
        /// </summary>
        /// <param name="ColorNumber">#000000</param>
        /// <returns></returns>
        public static bool IsColor(string ColorNumber) {
            return Regex.IsMatch(ColorNumber, "^#([0-9a-fA-F]{6}|[0-9a-fA-F]{3})$");
        }
        #endregion
    }
}
