using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Tools
{
    /// <summary>
    /// <para>　</para>
    /// 　常用工具类——INI文件读写类
    /// <para>　-----------------------------------------------------</para>
    /// <para>　IniWriteValue：写INI文件</para>
    /// <para>　IniReadValue：读取INI文件中键值为字符串的键</para>
    /// <para>　IniReadValues：读取INI文件中键值为字节的键</para>
    /// <para>　ClearAllSection：删除ini文件下所有段落</para>
    /// <para>　ClearSection：删除指定段落下的所有键</para>
    /// </summary>
    public class IniHelper
    {
        private static string path = System.Environment.CurrentDirectory+"/";
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="INIPath"></param>
        public IniHelper(string INIPath)
        {
            path = INIPath;
        }

        #region 导入DLL
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, Byte[] retVal, int size, string filePath);
        #endregion

        #region 写INI文件
        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="Section">要写入的段落</param>
        /// <param name="Key">要写入的键</param>
        /// <param name="Value">要写的的键值</param>
        public static void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value,path);
        }
        #endregion

        #region 读取INI文件中键值为字符串的键
        /// <summary>
        /// 读取INI文件中键值为字符串的键
        /// </summary>
        /// <param name="Section">要读取的段落</param>
        /// <param name="Key">要读取的键</param>
        /// <returns>对应的键值</returns>
        public static string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, path);
            return temp.ToString();
        }
        #endregion

        #region 读取INI文件中键值为字节的键
        /// <summary>
        /// 读取INI文件中键值为字节的键
        /// </summary>
        /// <param name="section">要读取的段落</param>
        /// <param name="key">要读取的键</param>
        /// <returns>对应的键值</returns>
        public byte[] IniReadValues(string section, string key)
        {
            byte[] temp = new byte[255];
            int i = GetPrivateProfileString(section, key, "", temp, 255, path);
            return temp;

        }
        #endregion

        #region 删除ini文件下所有段落
        /// <summary>
        /// 删除ini文件下所有段落
        /// </summary>
        public static void ClearAllSection()
        {
            IniWriteValue(null, null, null);
        }
        #endregion

        #region 删除指定段落下的所有键
        /// <summary>
        /// 删除指定段落下的所有键
        /// </summary>
        /// <param name="Section">要删除的段落</param>
        public static void ClearSection(string Section)
        {
            IniWriteValue(Section, null, null);
        }
        #endregion
    }
}
