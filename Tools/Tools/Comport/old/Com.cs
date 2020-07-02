using System.IO.Ports;

namespace Comport
{
    public class Com
    {
        public virtual event SerialDataReceivedEventHandler DataReceive;

        public ComPortParm Cpp { get; private set; }

        protected SerialPort handle;

        public Com(ComPortParm cpp)
        {
            this.Cpp = cpp;
            this.handle = new SerialPort();
            this.handle.PortName = cpp.串口名称;
            this.handle.BaudRate = cpp.波特率;
            this.handle.DataBits = cpp.数据位;
            this.handle.Parity = cpp.校验位;
            this.handle.StopBits = cpp.停止位;
            this.handle.DiscardNull = cpp.DiscardNull;
            this.handle.DtrEnable = cpp.DtrEnable;
            this.handle.Handshake = cpp.Handshake;
            this.handle.RtsEnable = cpp.RtsEnable;
            this.handle.DataReceived += delegate (object a, SerialDataReceivedEventArgs b)
            {
                SerialDataReceivedEventHandler dataReceive = this.DataReceive;
                if (dataReceive == null)
                {
                    return;
                }
                dataReceive(a, b);
            };
        }

        /// <summary>
        /// 发送16进制字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
		public bool Send(byte[] data)
        {
            if (!this.IsOpen)
            {
                return false;
            }
            this.handle.Write(data, 0, data.Length);
            return true;
        }



        /// <summary>
        /// 发送字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Send(string data)
        {
            if (!this.IsOpen)
            {
                return false;
            }
            this.handle.Write(data);
            return true;
        }

        public void Close()
        {
            if (this.handle.IsOpen)
            {
                this.handle.Close();
            }
        }

        public bool IsOpen
        {
            get
            {
                return this.handle.IsOpen;
            }
        }

        public bool Open()
        {
            bool result;
            try
            {
                this.SetParm(this.Cpp);
                this.handle.Open();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public bool SetParm(ComPortParm cpp)
        {
            bool result;
            try
            {
                if (this.IsOpen)
                {
                    this.Close();
                }
                this.handle.BaudRate = cpp.波特率;
                this.handle.PortName = cpp.串口名称;
                this.handle.DataBits = cpp.数据位;
                this.handle.StopBits = cpp.停止位;
                this.handle.Parity = cpp.校验位;
                this.handle.DiscardNull = cpp.DiscardNull;
                this.handle.DtrEnable = cpp.DtrEnable;
                this.handle.Handshake = cpp.Handshake;
                this.handle.RtsEnable = cpp.RtsEnable;
                this.Cpp = cpp;
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public string[] GetAllItems()
        {
            return SerialPort.GetPortNames();
        }


    }
}
