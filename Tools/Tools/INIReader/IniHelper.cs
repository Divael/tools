using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        private static string filePath = System.Environment.CurrentDirectory + "/";
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="INIPath"></param>
        public IniHelper(string INIPath)
        {
            filePath = INIPath;
        }

        #region 导入DLL
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, Byte[] retVal, int size, string filePath);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern long GetPrivateProfileStringB(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern uint GetPrivateProfileStringA(string section, string key,
            string def, Byte[] retVal, int size, string filePath);
        #endregion

        public static void SetFilePath(String filepath)
        {
            filePath = filepath;
        }

        #region 读取section段落 如果没有，则返回""或者list.count = 0;
        public static List<string> ReadSections()
        {
            return ReadSections(filePath);
        }

        public static List<string> ReadSections(string iniFilename)
        {
            List<string> result = new List<string>();
            Byte[] buf = new Byte[65536];
            uint len = GetPrivateProfileStringA(null, null, null, buf, buf.Length, iniFilename);
            int j = 0;
            for (int i = 0; i < len; i++)
                if (buf[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buf, j, i - j));
                    j = i + 1;
                }
            return result;
        }
        #endregion

        #region 读取Sections里的keys
        public static List<string> ReadKeys(String SectionName)
        {
            return ReadKeys(SectionName, filePath);
        }

        public static List<string> ReadKeys(string SectionName, string iniFilename)
        {
            List<string> result = new List<string>();
            Byte[] buf = new Byte[65536];
            uint len = GetPrivateProfileStringA(SectionName, null, null, buf, buf.Length, iniFilename);
            int j = 0;
            for (int i = 0; i < len; i++)
                if (buf[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buf, j, i - j));
                    j = i + 1;
                }
            return result;
        }
        #endregion

        #region 读取指定section下的所有键值对HashTable
        public static Hashtable ReadIniDataByHashTable(string SectionName)
        {
            Hashtable ht = new Hashtable();
            var tp = ReadKeys(SectionName);
            foreach (var item in tp)
            {
                ht.Add(item, ReadIniData(SectionName, item, ""));
            }
            return ht;
        }
        #endregion

        #region 写INI文件
        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="Section">要写入的段落</param>
        /// <param name="Key">要写入的键</param>
        /// <param name="Value">要写的的键值</param>
        /// <param name="Value">路径</param>
        public static void IniWriteValue(string Section, string Key, string Value, string Path = "")
        {
            if (string.IsNullOrWhiteSpace(Path))
            {
                Path = IniHelper.filePath;
            }
            long OpStation = WritePrivateProfileString(Section, Key, Value, Path);
            if (OpStation == 0)
            {
                throw new Exception("写文件错误！");
            }
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
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, filePath);
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
            int i = GetPrivateProfileString(section, key, "", temp, 255, filePath);
            return temp;
        }
        #endregion

        #region 删除ini文件下所有段落
        /// <summary>
        /// 删除ini文件下所有段落
        /// </summary>
        public static bool ClearAllSection()
        {
            return WriteIniData(null, null, null);
        }
        #endregion

        #region 删除指定段落下的所有键
        /// <summary>
        /// 删除指定段落下的所有键
        /// </summary>
        /// <param name="Section">要删除的段落</param>
        public static bool ClearSection(string Section)
        {
            return WriteIniData(Section, null, null);
        }
        #endregion

        #region 读Ini文件,如果没有，则返回""或者list.count = 0;

        public static string ReadIniData(string Section, string Key, string NoText)
        {
            return ReadIniData(Section, Key, NoText, filePath);
        }

        public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileStringB(Section, Key, NoText, temp, 1024, iniFilePath);
                return temp.ToString();
            }
            else
                return String.Empty;
        }

        #endregion

        #region 写Ini文件

        public static bool WriteIniData(string Section, string Key, string Value)
        {
            return WriteIniData(Section, Key, Value, filePath);
        }
        public static bool WriteIniData(string Section, string Key, string Value, string iniFilePath)
        {
            long OpStation = WritePrivateProfileString(Section, Key, Value, iniFilePath);
            if (OpStation == 0)
                return false;
            else
                return true;
        }
        #endregion
    }
}
