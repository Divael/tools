using System;
using System.Collections.Generic;

namespace Tools
{
    /// <summary>
    /// <para>　</para>
    /// 　常用工具类——CRC校验
    /// <para>　---------------------------------------------------------------</para>
    /// <para>　ModbusCRC16(两个字节) ，返回校验结果</para>
    /// <para>　8位校验和（一个字节） ADD8，返回校验结果</para>
    /// </summary>
    public class CRCHelper
    {
        #region 循环冗余检验：ModbusCRC16 
        /// <summary>  
        /// 循环冗余检验：ModbusCRC16
        /// </summary>  
        public partial class CRC_16
        {
            #region ModbusCRC16
            /// <summary>
            ///CRC16校验算法,（低字节在前，高字节在后）
            /// </summary>
            /// <param name="data">要校验的数组</param>
            /// <param name="size">长度</param>
            /// <returns>返回校验结果，低字节在前，高字节在后</returns>
            public static byte[] CRC16(byte[] data)
            {
                if (data.Length == 0)
                    throw new Exception("调用CRC16校验算法,（低字节在前，高字节在后）时发生异常，异常信息：被校验的数组长度为0。");
                int len = data.Length;
                int xda, xdapoly;
                byte i, j, xdabit;
                xda = 0xFFFF;
                xdapoly = 0xA001;
                for (i = 0; i < data.Length; i++)
                {
                    xda ^= data[i];
                    for (j = 0; j < 8; j++)
                    {
                        xdabit = (byte)(xda & 0x01);
                        xda >>= 1;
                        if (xdabit == 1)
                            xda ^= xdapoly;
                    }
                }
                List<byte> list = new List<byte>();
                list.AddRange(data);
                list.Add((byte)(xda & 0xFF));//校验的高字节
                list.Add((byte)(xda >> 8));//校验的低字节
                return list.ToArray();
            }

            public static byte[] CRC16(byte[] data, int size)
            {
                if (data.Length == 0)
                    throw new Exception("调用CRC16校验算法,（低字节在前，高字节在后）时发生异常，异常信息：被校验的数组长度为0。");
                int len = size;
                byte[] temdata;
                int xda, xdapoly;
                byte i, j, xdabit;
                xda = 0xFFFF;
                xdapoly = 0xA001;
                for (i = 0; i < size; i++)
                {
                    xda ^= data[i];
                    for (j = 0; j < 8; j++)
                    {
                        xdabit = (byte)(xda & 0x01);
                        xda >>= 1;
                        if (xdabit == 1)
                            xda ^= xdapoly;
                    }
                }
                temdata = new byte[2] { (byte)(xda & 0xFF), (byte)(xda >> 8) };

                return temdata;
            }
            #endregion
        }
        #endregion

        #region 8位校验和（一个字节）
        public partial class ADD8
        {
            /// <summary>
            /// 累加校验和
            /// </summary>
            /// <param name="memorySpage">需要校验的数据</param>
            /// <returns>返回校验和结果</returns>
            public static byte[] ADD8_Add(byte[] memorySpage)
            {
                if (memorySpage == null)
                {
                    throw new Exception("memorySpage is null!");
                }
                if (memorySpage.Length <= 2)
                {
                    throw new Exception("memorySpage.Length > 2 !"); ;
                }
                List<byte> list = new List<byte>();
                list.AddRange(memorySpage);
                int sum = 0;
                for (int i = 0; i < memorySpage.Length; i++)
                {
                    sum += memorySpage[i];
                }
                sum = sum & 0xff;
                var str = sum.ToString("X");
                list.AddRange(str.ToHex());
                //返回累加校验和
                return list.ToArray();
            }

            /// <summary>
            /// 累加校验和
            /// </summary>
            /// <param name="memorySpage">需要校验的数据</param>
            /// <returns>返回校验和结果</returns>
            public static bool ADD8_Check(byte[] memorySpage)
            {
                List<byte> list = new List<byte>();
                list.AddRange(memorySpage);
                byte a = list[list.Count - 1];
                list.RemoveAt(list.Count - 1);
                int sum = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    sum += list[i];
                }
                sum = sum & 0xff;
                var str = sum.ToString("X");
                if (str.ToHex()[0] == a)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

    }
}
