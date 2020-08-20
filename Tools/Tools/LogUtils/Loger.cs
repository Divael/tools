using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Tools
{
    /// <summary>
    /// 日志处理类
    /// <para>　------------------------------------------------</para>
    /// <para>　info:  常用信息输出</para>
    /// <para>　err:   错误信息输出</para>
    /// <para>　</para>
    /// </summary>
    public class Loger
    {
        //string path = System.Environment.CurrentDirectory;//获取当前目录
        #region 日志处理
        #region 属性



        public static bool IsLogToFile { get; set; } = true;

        //public static string[] typeSet = { "error", "info" };

        #endregion

        #region 变量
        private static string m_LogName = "Log_";
        /// <summary>
        /// 日志文件夹路径名称
        /// </summary>
        private static string m_LogDire = "";

        #endregion
        /// <summary>
        /// 记录组件日志
        /// </summary>
        /// <param name="logInfo">日志内容</param>
        public static void info(string logInfo, string type = "info")
        {
            try
            {
                if (!IsLogToFile)
                {
                    // 不记录日志
                    return;
                }

                //if (!typeSet.Contains(type))
                //    type = "UNKNOWN";

                m_LogDire = AppDomain.CurrentDomain.BaseDirectory + "Log\\";
                if (!Directory.Exists(m_LogDire))
                {
                    // 不存在，创建
                    Directory.CreateDirectory(m_LogDire);
                }

                File.AppendAllText(m_LogDire + m_LogName + "Info_" + DateTime.Now.ToString("yyyyMMdd") + ".log",
                        "【" + System.DateTime.Now.ToString("HH:mm:ss:fff") + "】\r\n" + 
                        "【类型】" + type + "\r\n" + 
                        ">>>>>" + logInfo + "\r\n" +
                        "------------------------------" + "\r\n",
                        Encoding.Default);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 记录错误组件日志
        /// </summary>
        /// <param name="logInfo">日志内容</param>
        public static void err(string logInfo, string ex, string type = "Error")
        {
            try
            {
                if (!IsLogToFile)
                {
                    // 不记录日志
                    return;
                }

                m_LogDire = AppDomain.CurrentDomain.BaseDirectory + "Log\\";
                if (!Directory.Exists(m_LogDire))
                {
                    // 不存在，创建
                    Directory.CreateDirectory(m_LogDire);
                }

                File.AppendAllText(m_LogDire + m_LogName + "Error_" + DateTime.Now.ToString("yyyyMMdd") + ".log",
                "【时间】：" + System.DateTime.Now.ToString("HH:mm:ss:fff") + "\r\n" +
                "【描述】：" + logInfo + "\r\n" +
                "【Error】：" + ex + "\r\n" +
                "------------------------------" + "\r\n",
                Encoding.Default);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 记录错误组件日志
        /// </summary>
        /// <param name="logInfo">日志内容</param>
        public static void err(string logInfo, Exception ex, string type = "Error")
        {
            try
            {
                if (!IsLogToFile)
                {
                    // 不记录日志
                    return;
                }

                m_LogDire = AppDomain.CurrentDomain.BaseDirectory + "Log\\";
                if (!Directory.Exists(m_LogDire))
                {
                    // 不存在，创建
                    Directory.CreateDirectory(m_LogDire);
                }

                File.AppendAllText(m_LogDire + m_LogName + "Error_" + DateTime.Now.ToString("yyyyMMdd") + ".log",
                "【时间】：" + System.DateTime.Now.ToString("HH:mm:ss:fff") + "\r\n" +
                "【描述】：" + logInfo + "\r\n" +
                "【Sorce】：" + ex.Source + "\r\n" +
                "【StackTrace】：" + ex.StackTrace + "\r\n" +
                "【Message】：" + ex.Message + "\r\n" +
                "------------------------------" + "\r\n",
                Encoding.Default);
            }
            catch
            {
            }
        }
        /// <summary>
        /// 记录错误组件日志
        /// </summary>
        /// <param name="ex">Exception</param>
        public static void err(Exception ex)
        {
            try
            {
                //if (!IsLogToFile)
                //{
                //    // 不记录日志
                //    return;
                //}
                m_LogDire = AppDomain.CurrentDomain.BaseDirectory + "Log\\";
                if (!Directory.Exists(m_LogDire))
                {
                    // 不存在，创建目录
                    Directory.CreateDirectory(m_LogDire);
                }
                File.AppendAllText(m_LogDire + m_LogName + "Error_" + DateTime.Now.ToString("yyyyMMdd") + ".log",
                        "【时间】：" + System.DateTime.Now.ToString("HH:mm:ss:fff") + "\r\n" +
                        "【Sorce】：" + ex.Source + "\r\n" +
                        "【StackTrace】：" + ex.StackTrace + "\r\n" +
                        "【Message】：" + ex.Message + "\r\n" +
                        "------------------------------" + "\r\n",
                        Encoding.Default);
            }
            catch
            {
            }
        }
        #endregion
    }
}
