using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    /// <summary>
    /// <para>　</para>
    /// 　常用工具类——CRC校验
    /// <para>　---------------------------------------------------------------</para>
    /// <para>　CRC_16：crc校验，返回校验结果，低字节在前，高字节在后</para>
    /// </summary>
    public class CRCHelper
    {
        #region 循环冗余检验：CRC-16-CCITT查表法.低字节在前，高字节在后  
        /// <summary>  
        /// 循环冗余检验：CRC-16-CCITT查表法  
        /// </summary>  
        public partial class CRC_16
        {
            #region CRC16校验
            /// <summary>
            ///CRC16校验算法,（低字节在前，高字节在后）
            /// </summary>
            /// <param name="data">要校验的数组</param>
            /// <param name="size">长度</param>
            /// <returns>返回校验结果，低字节在前，高字节在后</returns>
            public static byte[] crc16(byte[] data)
            {
                if (data.Length == 0)
                    throw new Exception("调用CRC16校验算法,（低字节在前，高字节在后）时发生异常，异常信息：被校验的数组长度为0。");
                int len = data.Length;
                byte[] temdata = new byte[data.Length + 2];
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
                temdata = new byte[2] { (byte)(xda & 0xFF), (byte)(xda >> 8) };
                byte[] message;
                message = new byte[data.Length + temdata.Length];
                for (int n = 0; n < message.Length; n++)
                {
                    if (n < data.Length)
                    {
                        message[n] = data[n];

                    }
                    else
                    {
                        message[n] = temdata[n - data.Length];
                    }
                }
                return message;
            }

            public static byte[] crc16(byte[] data, int size)
            {
                if (data.Length == 0)
                    throw new Exception("调用CRC16校验算法,（低字节在前，高字节在后）时发生异常，异常信息：被校验的数组长度为0。");
                int len = size;
                byte[] temdata = new byte[size + 2];
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

    }
}
