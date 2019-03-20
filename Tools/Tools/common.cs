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
using System.WinSystem;
using Tools;

namespace System
{
    /// <summary>
    /// 我的扩展
    /// </summary>
    public static class Common
    {

        public static void ForEach<T>(this IEnumerable<T> shit, Action<T> action)
        {
            foreach (T obj in shit)
                action(obj);
        }

        public static string GetString(byte[] b)
        {
            if (b != null)
                return Encoding.Default.GetString(b);
            return (string)null;
        }

        public static string GetString(byte[] b, Encoding e)
        {
            if (b != null)
                return e.GetString(b);
            return (string)null;
        }

        public static void Sleep(int ms)
        {
            Lazy.Sleep(ms);
        }

        public static void Sleep(TimeSpan ts)
        {
            Lazy.Sleep(ts);
        }

        public static void SetSleepTick()
        {
            WinSystem.WinApi.timeBeginPeriod(1);
        }

        public static void AbortSleepTick()
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

        public static byte[] GetBytes(string s, Encoding e)
        {
            return e.GetBytes(s);
        }

        public static byte[] GetBytes(string s)
        {
            return Encoding.Default.GetBytes((s == null) ? string.Empty : s);
        }

        public static string MD5Str(string data)
        {
            return Convert.ToBase64String(Common.MD5Code(data.GetBytes1(Encoding.UTF8)));
        }

        public static byte[] GBKToUTF8(byte[] buffer)
        {
            string txt = Encoding.GetEncoding("GBK").GetString(buffer);
            return Encoding.UTF8.GetBytes(txt);
        }

        public static byte[] GetBytes1(this string data, Encoding e)
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

        public static void RunInBackThread(Action act, bool LogException = true)
        {
            Common.RunInBackThread(act, delegate (Exception p)
            {
                if (LogException)
                {
                    logErr("Tools内部错误",p);
                }
            });
        }

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

        public static void logThis(this Exception mess)
        {
            Tools.Loger.info(mess.ToString());
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
