using System;
using System.Net;
using System.Net.Sockets;

namespace Tools.TCP
{
    /// <summary>
    /// Client
    /// DataReceived
    /// DisConnected
    /// Send
    /// Dispose
    /// BeginRead
    /// 
    /// </summary>
    public class Client : IDisposable
    {
        public Socket handle = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        protected byte[] BUFFER = new byte[(int)ushort.MaxValue];

        public event Action<byte[], int, Client> DataReceived;

        public event Action<Client, Exception> DisConnected;

        public Client(string ip, int port)
          : this(ip, port, 0)
        {
        }

        public Client(string ip, int port, int local)
        {
            this.handle.Bind((EndPoint)new IPEndPoint(IPAddress.Any, local));
            this.handle.Connect(IPAddress.Parse(ip), port);
        }

        public Client(Socket tcp)
        {
            this.handle = tcp;
        }

        public void Dispose()
        {
            try
            {
                Socket socket1 = this.handle;
                if ((socket1 != null ? (socket1.Connected ? 1 : 0) : 0) != 0)
                    this.handle.Disconnect(true);
                Socket socket2 = this.handle;
                if (socket2 != null)
                {
                    socket2.Dispose();
                }
                this.handle = (Socket)null;
            }
            catch
            {
            }
        }

        public void BeginRead()
        {
            this.handle.BeginReceive(this.BUFFER, 0, this.BUFFER.Length, SocketFlags.None, new AsyncCallback(this.ReadCallBack), (object)null);
        }

        protected virtual void ReadCallBack(IAsyncResult ir)
        {
            try
            {
                int num1 = this.handle.EndReceive(ir);
                if (num1 > 0)
                {
                    // ISSUE: reference to a compiler-generated field
                    Action<byte[], int, Client> action = this.DataReceived;
                    if (action != null)
                    {
                        byte[] numArray = this.BUFFER;
                        int num2 = num1;
                        action(numArray, num2, this);
                    }
                    this.BeginRead();
                }
                else
                {
                    this.DisConnected?.Invoke(this, null);
                }
            }
            catch (Exception ex)
            {
                // ISSUE: reference to a compiler-generated field
                Action<Client, Exception> action = this.DisConnected;
                if (action == null)
                    return;
                Exception exception = ex;
                action(this, exception);
            }
        }

        public void Send(byte[] data)
        {
            this.Send(data, data.Length);
        }

        public virtual void Send(byte[] data, int len)
        {
            if (this.handle == null || !this.handle.Connected)
                return;
            this.handle.Send(data, len, SocketFlags.None);
        }

        protected void OnDatareceived(byte[] data)
        {
            // ISSUE: reference to a compiler-generated field
            Action<byte[], int, Client> action = this.DataReceived;
            if (action == null)
                return;
            byte[] numArray = data;
            int length = data.Length;
            action(numArray, length, this);
        }

        protected void OnDisConnected(Exception ex)
        {
            Action<Client, Exception> action = this.DisConnected;
            if (action == null)
                return;
            Exception exception = ex;
            action(this, exception);
        }
    }
}
