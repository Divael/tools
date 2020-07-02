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

        public static string[] typeSet = { "error", "info" };

        #endregion

        #region 变量
        private static string m_LogName = "Log_";
        /// <summary>
        /// 日志文件夹路径名称
        /// </summary>
        private static string m_LogDire = string.Empty;

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

                if (!typeSet.Contains(type))
                    type = "UNKNOWN";

                m_LogDire = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Log\\";
                if (!Directory.Exists(m_LogDire))
                {
                    // 不存在，创建
                    Directory.CreateDirectory(m_LogDire);
                }

                File.AppendAllText(m_LogDire + m_LogName + type + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log",
                        "【" + System.DateTime.Now.ToString("HH:mm:ss:fff") + "】\r\n" + ">>>>>" + logInfo + "\r\n" + "----------------------------" + "\r\n",
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
        public static void err(string logInfo, Exception err, string type = "error")
        {
            try
            {
                if (!IsLogToFile)
                {
                    // 不记录日志
                    return;
                }

                if (!typeSet.Contains(type))
                    type = "UNKNOWN";

                m_LogDire = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Log\\";
                if (!Directory.Exists(m_LogDire))
                {
                    // 不存在，创建
                    Directory.CreateDirectory(m_LogDire);
                }

                File.AppendAllText(m_LogDire + m_LogName + type + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log",
                        "时间：" + System.DateTime.Now.ToString("HH:mm:ss:fff") + "\r\n" + "类型：" + type + "\r\n" + "信息：" + logInfo + ">>>" + err.ToString() + "\r\n" + "\r\n",
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
        public static void err(string logInfo, string err, string type = "error")
        {
            try
            {
                if (!IsLogToFile)
                {
                    // 不记录日志
                    return;
                }

                if (!typeSet.Contains(type))
                    type = "UNKNOWN";

                m_LogDire = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Log\\";
                if (!Directory.Exists(m_LogDire))
                {
                    // 不存在，创建
                    Directory.CreateDirectory(m_LogDire);
                }

                File.AppendAllText(m_LogDire + m_LogName + type + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log",
                        "时间：" + System.DateTime.Now.ToString("HH:mm:ss:fff") + "\r\n" + "类型：" + type + "\r\n" + "信息：" + logInfo + ">>>" + err.ToString() + "\r\n" + "\r\n",
                        Encoding.Default);
            }
            catch
            {
            }
        }
        #endregion
    }
}
