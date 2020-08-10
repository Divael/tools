using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;

namespace Tools
{
    public class SerialPortBase
    {
        private List<byte> m_data = new List<byte>();//接收的数据

        public SerialPort SerialPort { get;}
        public event Action<SerialPort, byte[]> DataReceive;//用于接收数据

        private bool m_isReceived = false;
        private static object locker = new object();//锁这个类
        /// <summary>
        ///  "COM5",9600,"N",8,1
        /// </summary>
        /// <param name="portName">COM1</param>
        /// <param name="baudRate">9600 38400  </param>
        /// <param name="parity">None</param>
        /// <param name="dataBits">8</param>
        /// <param name="stopBits">one</param>
        /// <param name="IsNeedDataReceived">是否需要事件中断模式接收数据，false则相关功能不可用</param>
        public SerialPortBase(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits,bool IsNeedDataReceived)
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

            if (IsNeedDataReceived)
            {
                SerialPort.DataReceived += SerialPort_DataReceived;//事件
            }
        }

        /// <summary>
        /// 默认COM1,9600,N,8,1
        /// <param name="IsNeedDataReceived">是否需要事件中断模式接收数据，false则相关功能不可用</param>
        /// </summary>
        public SerialPortBase(bool IsNeedDataReceived)
        {
            SerialPort = new SerialPort();
            //串口名 = COM1
            //SerialPort.PortName = SerialPort.GetPortNames()[0];
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

            if (IsNeedDataReceived)
            {
                SerialPort.DataReceived += SerialPort_DataReceived;//事件
            }
        }



        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            lock (locker)
            {
                try
                {
                    int bytesToRead;
                    while (SerialPort.IsOpen && (bytesToRead = SerialPort.BytesToRead) > 0)
                    {
                        byte[] array = new byte[bytesToRead];
                        SerialPort.Read(array, 0, bytesToRead);
                        m_data.AddRange(array);
                    }
                    m_isReceived = true;
                    this.DataReceive?.Invoke(SerialPort, m_data.ToArray());
                }
                catch (Exception e2)
                {
                    Tools.Loger.err("tools_loghelper", e2);
                }
                finally
                {
                    m_data.Clear();
                }
            }
        }

        #region 发送并返回数据
        private Stopwatch sw = new Stopwatch();//用于发送接收一体化的定时器

        /// <summary>
        /// 发送并返回数据,使用事件的方式
        /// </summary>
        /// <param name="_sendData"></param>
        /// <param name="timeout">3s带返回 TimeSpan.FromSeconds(3)</param>
        /// <returns></returns>
        public byte[] SendAndReceived(byte[] _sendData, TimeSpan timeout)
        {
            lock (locker)
            {
                if (!SerialPort.IsOpen)
                {
                    throw new Exception("串口未打开！");
                }
                this.m_data.Clear();
                m_isReceived = false;

                Send(_sendData);

                sw.Reset();
                sw.Start();

                while (sw.Elapsed < timeout)
                {
                    if (m_isReceived)
                    {
                        m_isReceived = false;
                        sw.Stop();
                        return this.m_data.ToArray();
                    }
                    System.Threading.Thread.Sleep(10);
                }
                sw.Stop();
                return null;
            }
        }


        /// <summary>
        /// 发送带返回,使用轮询的方式
        /// </summary>
        /// <param name="data"></param>
        /// <param name="receivedNum">接收数据的大小</param>
        /// <param name="delay"></param>
        /// <returns></returns>Received
        public byte[] SendResponse(byte[] data, int receivedNum, int delay = 100)
        {
            lock (locker)
            {
                if (!SerialPort.IsOpen)
                {
                    throw new Exception("串口未打开！");
                }
                this.m_data.Clear();
                Send(data);
                sw.Reset();
                sw.Start();
                int bytesToRead = 0;
                do
                {
                    try
                    {
                        if ((bytesToRead = SerialPort.BytesToRead) > 0)
                        {
                            byte[] array = new byte[bytesToRead];
                            SerialPort.Read(array, 0, bytesToRead);
                            this.m_data.AddRange(array);
                        }
                    }
                    catch {
                        sw.Reset();
                        return this.m_data.ToArray();
                    }
                } while (sw.ElapsedMilliseconds > delay && this.m_data.Count >= receivedNum);
                sw.Reset();
                return this.m_data.ToArray();
            }
        }
        

        private void ClearBuffer()
        {
            //清空缓冲区
            SerialPort.DiscardOutBuffer();
            SerialPort.DiscardInBuffer();
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
