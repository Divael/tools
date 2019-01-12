using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Tools.TCP
{
    /// <summary>
    /// Action<Client> NewClient;
    /// Listen
    /// Dispose
    /// </summary>
    public class Server : IDisposable
  {
    private Socket handle = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    public event Action<Client> NewClient;

    public void Listen(int Port)
    {
      Server server = this;
      bool lockTaken = false;
      try
      {
        Monitor.Enter((object) server, ref lockTaken);
        this.handle.Bind((EndPoint) new IPEndPoint(IPAddress.Any, Port));
        this.handle.Listen(int.MaxValue);
        this.BeginListen();
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit((object) server);
      }
    }

    private void BeginListen()
    {
      this.handle.BeginAccept(new AsyncCallback(this.Acceptor), (object) null);
    }

    private void Acceptor(IAsyncResult ir)
    {
      Client client = new Client(this.handle.EndAccept(ir));
      // ISSUE: reference to a compiler-generated field
      if (this.NewClient != null)
      {
        // ISSUE: reference to a compiler-generated field
        this.NewClient(client);
      }
      this.BeginListen();
    }

    public void Dispose()
    {
      try
      {
        if (!this.handle.Connected)
          return;
        this.handle.Shutdown(SocketShutdown.Both);
        this.handle.Close();
        this.handle.Dispose();
      }
      catch
      {
      }
    }
  }
}
