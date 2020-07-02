using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;

namespace Comport
{
    /// <summary>
    /// 
    /// 
    ///        static ComWaitRead com;
    ///         var cpp = ComPortParm.Parse(p);
    ///         com = new ComWaitRead(cpp, 20);
    ///         com.DataReceive += Com_DataReceive;
    /// 
    /// 
    /// </summary>
    public class ComWaitRead : Com
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpp">参数</param>
        /// <param name="ReceiveDelay">等待接收时间</param>
		public ComWaitRead(ComPortParm cpp, int ReceiveDelay = 50) : base(cpp)
        {
            base.DataReceive += this.ComWaitRead_DataReceive;
            this.ReceiveDelay = ReceiveDelay;
        }

        /// <summary>
        /// 第2个参数是串口返回的bytes
        /// 子类如果用new的意思是 子类有一个 和父类一样的同命方法 但是这个方法 和父类不一样
        /// </summary>
		public new event Action<ComWaitRead, byte[]> DataReceive;

        private void ComWaitRead_DataReceive(object sender, SerialDataReceivedEventArgs e)
        {
            List<byte> data = this.Data;
            lock (data)
            {
                try
                {
                    int bytesToRead;
                    while (base.IsOpen && (bytesToRead = this.handle.BytesToRead) > 0)
                    {
                        byte[] array = new byte[bytesToRead];
                        this.handle.Read(array, 0, bytesToRead);
                        this.Data.AddRange(array);
                        System.Common.Sleep(this.ReceiveDelay);
                    }
                    Action<ComWaitRead, byte[]> dataReceive = this.DataReceive;
                    if (dataReceive != null)
                    {
                        dataReceive(this, this.Data.ToArray());
                    }
                }
                catch (Exception e2)
                {
                    Tools.Loger.err("tools_loghelper", e2);
                }
                finally
                {
                    this.Data.Clear();
                }
            }
        }
        /// <summary>
        /// 接收延时
        /// </summary>
		public int ReceiveDelay = 50;

        private List<byte> Data = new List<byte>();

        public static Stopwatch sw = new Stopwatch();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="timeout">3s带返回 TimeSpan.FromSeconds(3)</param>
        /// <returns></returns>
        public byte[] SendAndGetReply(byte[] data, TimeSpan timeout)
        {
            byte[] bytes = null;
            if (!this.IsOpen)
            {
                throw new Exception("串口未打开！");
            }

            this.DataReceive += (s, e) =>
            {
                bytes = e;
            };

            this.handle.Write(data, 0, data.Length);

            sw.Reset();
            sw.Start();

            while (bytes == null && sw.Elapsed < timeout)
            {
                System.Threading.Thread.Sleep(10);
            }
            sw.Stop();
            return bytes;
        }
    }
}
