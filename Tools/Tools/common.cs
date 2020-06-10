using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.WinSystem;
using Tools;

namespace System
{
    /// <summary>
    /// 我的扩展
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// 打开一个窗口，并返回而不等待新打开的窗口关闭。
        /// </summary>
        /// <param name="win"></param>
        /// <param name="owner"></param>
        public static void Show(this Window win, Window owner) {
            win.Owner = owner;
            win.Show();
        }
        /// <summary>
        /// 打开一个窗口，并返回而不等待新打开的窗口关闭。
        /// </summary>
        /// <param name="win"></param>
        public static void ShowEx(this Window win) {
            win.Show(Application.Current.MainWindow);
        }

        /// <summary>
        /// 显示对话框,打开一个窗口，并关闭新打开的窗口时，才返回。
        /// </summary>
        /// <param name="win"></param>
        /// <param name="owner"></param>
        public static void ShowDialog(this Window win, Window owner)
        {
            win.Owner = owner;
            win.ShowDialog();
        }

        /// <summary>
        /// 显示对话框,打开一个窗口，并关闭新打开的窗口时，才返回。
        /// </summary>
        /// <param name="win"></param>
        public static void ShowDialogEx(this Window win)
        {
            win.ShowDialog(Application.Current.MainWindow);
        }

        public static void ForEach<T>(this IEnumerable<T> shit, Action<T> action)
        {
            foreach (T obj in shit)
                action(obj);
        }

        /// <summary>
        /// 根据默认编码Default获取字节的字符串
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string GetString(byte[] b)
        {
            if (b != null)
                return Encoding.Default.GetString(b);
            return (string)null;
        }

        /// <summary>
        /// 根据Encoding编码获取字节的字符串
        /// </summary>
        /// <param name="b">byte</param>
        /// <param name="e">Encoding</param>
        /// <returns></returns>
        public static string GetString(byte[] b, Encoding e)
        {
            if (b != null)
                return e.GetString(b);
            return (string)null;
        }

        /// <summary>
        /// 当前线程睡眠
        /// </summary>
        /// <param name="ms">毫秒</param>
        public static void Sleep(int ms)
        {
            //Lazy.Sleep(ms);
            Thread.Sleep(ms);
        }

        /// <summary>
        /// 当前线程睡眠
        /// </summary>
        /// <param name="ts"></param>
        public static void Sleep(TimeSpan ts)
        {
            //Lazy.Sleep(ts);
            Thread.Sleep(ts);
        }

        /// <summary>
        /// 用到了winapi  timeBeginPeriod ,当前不在更新，可以直接用Thread
        /// </summary>
        private static void SetSleepTick()
        {
            WinSystem.WinApi.timeBeginPeriod(1);
        }

        /// <summary>
        ///用到了winapi  timeEndPeriod ,当前不在更新，可以直接用Thread
        /// </summary>
        private static void AbortSleepTick()
        {
            WinSystem.WinApi.timeEndPeriod(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="act"></param>
        /// <param name="exception"></param>
        public static void invoke(Action act, Action<Exception> exception = null)
        {
            try
            {
                if (act == null)
                    return;
                act();
            }
            catch (Exception ex)
            {
                try
                {
                    if (exception == null)
                        return;
                    exception(ex);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 将一个不确定的方法执行，可以避免报错
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="exception"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static T invoke<T>(Func<T> method, Action<Exception> exception = null, T error = default(T))
        {
            T result;
            try
            {
                result = method();
            }
            catch (Exception obj)
            {
                if (exception != null)
                {
                    exception(obj);
                }
                result = error;
            }
            return result;
        }

        /// <summary>
        /// 合并多个bytes到一个bytes
        /// </summary>
        /// <param name="data">多个bytes</param>
        /// <returns></returns>
        public static byte[] Combine(params byte[][] data)
        {
            long num = 0L;
            foreach (byte[] array in data)
            {
                num += (long)array.Length;
            }
            byte[] array2 = new byte[num];
            num = 0L;
            foreach (byte[] array3 in data)
            {
                array3.CopyTo(array2, num);
                num += (long)array3.Length;
            }
            return array2;
        }

        /// <summary>
        /// 获取Encoding字符串的bytes
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string s, Encoding e)
        {
            return e.GetBytes(s);
        }

        /// <summary>
        /// 获取默认字符串的bytes
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string s)
        {
            return Encoding.Default.GetBytes((s == null) ? string.Empty : s);
        }

        /// <summary>
        /// MD5Code加密转ToBase64String
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string MD5Str(string data)
        {
            return Convert.ToBase64String(Common.MD5Code(data.GetBytess(Encoding.UTF8)));
        }
        /// <summary>
        /// gbk
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] GBKToUTF8(byte[] buffer)
        {
            string txt = Encoding.GetEncoding("GBK").GetString(buffer);
            return Encoding.UTF8.GetBytes(txt);
        }

        public static byte[] GetBytess(this string data, Encoding e)
        {
            return Common.GetBytes(data, e);
        }

        public static byte[] MD5Code(byte[] input)
        {
            return MD5.Create().ComputeHash(input);
        }

        public static T FromJson<T>(byte[] data)
        {
            DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
            T result;
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                result = (T)((object)dataContractJsonSerializer.ReadObject(memoryStream));
            }
            return result;
        }

        /// <summary>
        /// 交给线程池进行托管处理一些小任务
        /// </summary>
        /// <param name="act"></param>
        /// <param name="LogException"></param>
        public static void RunInBackThread(Action act, bool LogException = true)
        {
            Common.RunInBackThread(act, delegate (Exception p)
            {
                if (LogException)
                {
                    logErr("Tools内部错误RunInBackThread ", p);
                }
            });
        }

        /// <summary>
        /// 交给线程池进行托管处理一些小任务
        /// </summary>
        /// <param name="act"></param>
        /// <param name="WhenFail"></param>
        public static void RunInBackThread(Action act, Action<Exception> WhenFail)
        {
            //交给线程池进行托管处理一些小任务
            ThreadPool.QueueUserWorkItem(delegate (object p)
            {
                try
                {
                    act();
                }
                catch (Exception obj)
                {
                    Action<Exception> whenFail = WhenFail;
                    if (whenFail != null)
                    {
                        whenFail(obj);
                    }
                }
            });
        }

        /// <summary>
        /// 正常日志
        /// </summary>
        /// <param name="mess"></param>
        public static void logThis(this string mess)
        {
            Tools.Loger.info(mess);
        }

        /// <summary>
        /// 正常日志
        /// </summary>
        /// <param name="mess"></param>
        public static void logThis(this Exception mess)
        {
            Tools.Loger.info(mess.Message.ToString());
        }

        /// <summary>
        /// 正常日志
        /// </summary>
        /// <param name="mess"></param>
        public static void logThis(this string mess,string title)
        {
            Tools.Loger.info(title +"  " +mess.ToString());
        }


        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="mess">信息</param>
        /// <param name="ex">异常</param>
        public static void logErr(this string mess,Exception ex)
        {
            Tools.Loger.err(mess,ex);
        }



        public static string ToHex(this byte[] data, string split = " ")
        {
            return data.ToJoinString1((byte m) => m.ToString("X2"), split);
        }

        public static string ToJoinString1<T>(this IEnumerable<T> source, Func<T, string> how, string split = ",")
        {
            return string.Join(split, from m in source
                                      select how(m));
        }

        /// <summary>
        /// 字符串转16进制
        /// </summary>
        /// <param name="data"></param>
        /// <param name="split">分隔符</param>
        /// <returns></returns>
        public static byte[] ToHex(this string data, string split = " ")
        {
            data = data.Replace(split, string.Empty);
            return (from i in Enumerable.Range(0, data.Length)
                    where i % 2 == 0
                    select Convert.ToByte(data.Substring(i, 2), 16)).ToArray<byte>();
        }

        /// <summary>
        /// 从IQueryable转换DataTable
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable(this IEnumerable enumerable)
        {
            var dataTable = new DataTable();
            foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(enumerable.GetType()))
            {
                dataTable.Columns.Add(pd.Name, pd.PropertyType);
            }
            foreach (var item in enumerable)
            {
                var Row = dataTable.NewRow();

                foreach (PropertyDescriptor dp in TypeDescriptor.GetProperties(enumerable.GetType()))
                {
                    Row[dp.Name] = dp.GetValue(item);
                }
                dataTable.Rows.Add(Row);
            }
            return dataTable;
        }

        /// <summary>
        /// 从IQueryable转换DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static DataTable CopyToDataTable1<T>(this IEnumerable<T> array)
        {
            var ret = new DataTable();
            foreach (PropertyDescriptor dp in TypeDescriptor.GetProperties(typeof(T)))
                ret.Columns.Add(dp.Name, dp.PropertyType);
            foreach (T item in array)
            {
                var Row = ret.NewRow();
                foreach (PropertyDescriptor dp in TypeDescriptor.GetProperties(typeof(T)))
                    Row[dp.Name] = dp.GetValue(item);
                ret.Rows.Add(Row);
            }
            return ret;
        }

    }
}
