using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    /// <summary>
    /// GetLocalIPAddress: 获取本地ip地址,首选第一个
    /// 
    /// </summary>
    public static class TcpHelper
    {
        /// <summary>
        /// 获取本地ip地址,首选第一个
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLocalIPAddress() {

            IPAddress localIp = null;

            try
            {
                IPAddress[] ipArray;
                ipArray = Dns.GetHostAddresses(Dns.GetHostName());
                localIp = ipArray.First(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            }
            catch (Exception ex)
            {
                $"GetLocalIPAddress 捕获 = {ex}".logThis();
            }
            if (localIp == null)
            {
                localIp = IPAddress.Parse("127.0.0.1");
            }
            return localIp;
        }


    }
}
