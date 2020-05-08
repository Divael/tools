using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class SerialPortBase
    {
        private List<byte> data = new List<byte>();

        public SerialPort SerialPort { get; set; }
        public event Action<SerialPort, byte[]> DataReceive;//用于接收数据


        /// <summary>
        ///  "COM5",9600,"N",8,1
        /// </summary>
        /// <param name="portName">COM1</param>
        /// <param name="baudRate">9600 38400  </param>
        /// <param name="parity">None</param>
        /// <param name="dataBits">8</param>
        /// <param name="stopBits">one</param>
        public SerialPortBase(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            SerialPort = new SerialPort();
            //串口名 = COM1
            SerialPort.PortName = portName;
            //波特率 = 9600 38400  
            SerialPort.BaudRate = baudRate;
            //奇偶校验 = N
            SerialPort.Parity = parity;
            //每个字节标准数据位 = 8
            SerialPort.DataBits = dataBits;
            //停止位 = 1
            SerialPort.StopBits = stopBits;
            //字节编码
            SerialPort.Encoding = Encoding.UTF8;
            //在添加到序列缓冲区前，是否丢弃接口上接收的空字节。
            SerialPort.DiscardNull = false;
            //在通信过程中，是否启用数据终端就绪行。
            SerialPort.DtrEnable = false;
            //获取或设置串行端口数据传输的握手协议。
            SerialPort.Handshake = Handshake.None;
            //获取或设置串行端口输出缓冲区的大小。常用的是2048或4096，2048足够用
            //serialPort.WriteBufferSize = 2048;
            //决定了当串口读缓存中数据多少个时才触发DataReceived事件
            SerialPort.ReceivedBytesThreshold = 1;

            SerialPort.DataReceived += SerialPort_DataReceived;

        }

        /// <summary>
        /// 默认COM1,9600,N,8,1
        /// </summary>
        public SerialPortBase()
        {
            SerialPort = new SerialPort();
            //串口名 = COM1
            SerialPort.PortName = SerialPort.GetPortNames()[0];
            //波特率 = 9600 38400  
            SerialPort.BaudRate = 9600;
            //奇偶校验 = N
            SerialPort.Parity = Parity.None;
            //每个字节标准数据位 = 8
            SerialPort.DataBits = 8;
            //停止位 = 1
            SerialPort.StopBits = StopBits.One;
            //字节编码
            SerialPort.Encoding = Encoding.UTF8;
            //在添加到序列缓冲区前，是否丢弃接口上接收的空字节(byte = 0x00)。
            SerialPort.DiscardNull = false;
            //在通信过程中，是否启用数据终端就绪行。
            SerialPort.DtrEnable = false;
            //获取或设置串行端口数据传输的握手协议。
            SerialPort.Handshake = Handshake.None;
            //获取或设置串行端口输出缓冲区的大小。常用的是2048或4096，2048足够用
            //serialPort.WriteBufferSize = 2048;
            //决定了当串口读缓存中数据多少个时才触发DataReceived事件
            SerialPort.ReceivedBytesThreshold = 1;

            SerialPort.DataReceived += SerialPort_DataReceived;

        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            lock (data)
            {
                try
                {
                    int bytesToRead;
                    while (SerialPort.IsOpen && (bytesToRead = SerialPort.BytesToRead) > 0)
                    {
                        byte[] array = new byte[bytesToRead];
                        SerialPort.Read(array, 0, bytesToRead);
                        data.AddRange(array);
                    }
                    this.DataReceive?.Invoke(SerialPort, data.ToArray());
                }
                catch (Exception e2)
                {
                    Tools.Loger.err("tools_loghelper", e2);
                }
                finally
                {
                    data.Clear();
                }
            }
        }

        #region 发送并返回数据
        byte[] bytesForSendReceived = null;//用于发送接收一体化的接收数据
        private Stopwatch sw = new Stopwatch();//用于发送接收一体化的定时器
        private bool isLoadDataReceive = false;//用于控制DataReceive += SerialPortBase_DataReceive;

        private void SerialPortBase_DataReceive(SerialPort arg1, byte[] arg2)
        {
            bytesForSendReceived = arg2;
        }
        /// <summary>
        /// 发送并返回数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="timeout">3s带返回 TimeSpan.FromSeconds(3)</param>
        /// <returns></returns>
        public byte[] SendAndReceived(byte[] data, TimeSpan timeout)
        {
            if (!SerialPort.IsOpen)
            {
                throw new Exception("串口未打开！");
            }

            bytesForSendReceived = null;

            SerialPort.Write(data, 0, data.Length);

            sw.Reset();
            sw.Start();

            if (!isLoadDataReceive)
            {
                DataReceive += SerialPortBase_DataReceive;
                isLoadDataReceive = true;
            }

            while (bytesForSendReceived == null && sw.Elapsed < timeout)
            {
                System.Threading.Thread.Sleep(10);
            }

            sw.Stop();
            DataReceive -= SerialPortBase_DataReceive;
            isLoadDataReceive = false;

            return bytesForSendReceived;
        }
        /// <summary>
        /// 发送带返回
        /// </summary>
        /// <param name="data"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public byte[] SendResponse(byte[] data, int delay = 100)
        {
            if (!SerialPort.IsOpen)
            {
                throw new Exception("串口未打开！");
            }
            bytesForSendReceived = null;
            lock (this.data)
            {
                this.data.Clear();
                if (this.Send(data))
                {
                    System.Common.Sleep(delay);
                    int bytesToRead;
                    while ((bytesToRead = SerialPort.BytesToRead) > 0)
                    {
                        byte[] array = new byte[bytesToRead];
                        SerialPort.Read(array, 0, bytesToRead);
                        this.data.AddRange(array);
                        System.Common.Sleep(Math.Max(50, delay / 2));
                    }
                }
                bytesForSendReceived = this.data.ToArray();
                this.data.Clear();
            }
            return bytesForSendReceived;
        }
        #endregion


        /// <summary>
        /// 发送16进制字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Send(byte[] data)
        {
            if (!SerialPort.IsOpen)
            {
                return false;
            }
            SerialPort.Write(data, 0, data.Length);
            return true;
        }

        /// <summary>
        /// 发送字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Send(string data)
        {
            if (!SerialPort.IsOpen)
            {
                return false;
            }
            SerialPort.Write(data);
            return true;
        }

        public void Close()
        {
            if (SerialPort.IsOpen)
            {
                SerialPort.Close();
            }
        }

        public bool IsOpen
        {
            get
            {
                return SerialPort.IsOpen;
            }
        }

        public bool Open()
        {
            bool result;
            try
            {
                SerialPort.Open();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="portName">COM1</param>
        /// <param name="baudRate">9600 38400  </param>
        /// <param name="parity">None</param>
        /// <param name="dataBits">8</param>
        /// <param name="stopBits">one</param>
        /// <returns></returns>
        public bool Open(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            bool result;
            try
            {
                if (SerialPort.IsOpen)
                {
                    Close();
                }
                SerialPort.PortName = portName;
                SerialPort.BaudRate = baudRate;
                SerialPort.Parity = parity;
                SerialPort.DataBits = dataBits;
                SerialPort.StopBits = stopBits;
                SerialPort.Open();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }

    }
}
