using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Tools
{
    /// <summary>
    /// <para>　</para>
    /// 　常用工具类——GUID相关类
    /// <para>　--------------------------------------------------</para>
    /// <para>　CreateGUID：生成新的GUID值[+3方法重载]</para>
    /// <para>　IsGuid：验证给定字符串是否是合法的Guid</para>
    /// </summary>
    public class GUIDHelper
    {
        #region 生成新的GUID值小写
        /// <summary>
        /// 生成GUID值小写
        /// </summary>
        /// <returns>新生成的GUID的字符串</returns>
        public static string CreateGUID()
        {
            return System.Guid.NewGuid().ToString().ToLower();
        }
        /// <summary>
        /// 生成GUID值小写，并指定是否显示中间的横线分隔符
        /// </summary>
        /// <param name="ShowLine">是否显示中间的横线分隔符</param>
        /// <returns></returns>
        public static string CreateGUID(bool ShowLine)
        {
            string GUIDStr = CreateGUID();
            if (!ShowLine)
            {
                GUIDStr = GUIDStr.Replace("-", "");
            }
            return GUIDStr;
        }
        /// <summary>
        /// 生成GUID值，是否显示大写，并指定是否显示中间的横线分隔符
        /// </summary>
        /// <param name="ShowLine">是否显示中间的横线分隔符</param>
        /// <param name="Upper">是否转换为大写</param>
        /// <returns></returns>
        public static string CreateGUID(bool ShowLine, bool Upper)
        {
            string GUIDStr = CreateGUID(ShowLine);
            if (Upper) GUIDStr = GUIDStr.ToUpper();
            else GUIDStr = GUIDStr.ToLower();
            return GUIDStr;
        }
        #endregion

        #region 验证给定字符串是否是合法的Guid
        /// <summary>
        /// 验证给定字符串是否是合法的Guid
        /// </summary>
        /// <param name="strToValidate">要验证的字符串</param>
        /// <returns>true/false</returns>
        public static bool IsGuid(string strToValidate)
        {
            bool isGuid = false;
            string strRegexPatten = @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\"
                    + @"-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$";
            try
            {
                if (strToValidate != null && !strToValidate.Equals(""))
                {
                    isGuid = Regex.IsMatch(strToValidate, strRegexPatten);
                }
            }
            catch (Exception)
            {

            }
            return isGuid;
        }
        #endregion
    }
}