using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Tools
{
  
    /// <summary>
    /// 实现通过HTTP发送数据
    /// </summary>
    public class HttpServices
    {
        public static string JsonType = "application/json";
        public static string TextType = "application/x-www-form-urlencoded";
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";//浏览器  
        private static Encoding requestEncoding = System.Text.Encoding.UTF8;//字符集  
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }
        private HttpWebRequest getHttpWebRequest(string url)
        {
            HttpWebRequest request = null;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            return request;
        }
     
        /// <summary>
        /// 发送数据，接收返回
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="data">数据</param>
        /// <param name="contentType">发送类型</param>
        /// <returns></returns>
        public string PostService(string url, string data, string contentType)
        {
            HttpWebRequest request = getHttpWebRequest(url);
            HttpWebResponse resonse = Post(request, data, contentType);
            return DealResponse(resonse);
        }
        public string PostService(string url, string data, string contentType,string [] HeaderName,string[] HeaderValue)
        {
            HttpWebRequest request = getHttpWebRequest(url);

            request.PreAuthenticate = false;
            for (int i = 0; i < HeaderName.Length; i++)
            {
                request.Headers.Add(HeaderName[i], HeaderValue[i]);
            }
            HttpWebResponse resonse = Post(request, data, contentType);
            return DealResponse(resonse);
        }

        private HttpWebResponse Post(HttpWebRequest request, string _data, string _contentType)
        {
            Stream stream = null;//用于传参数的流  

            request.Method = "POST";//传输方式  
            request.ContentType = _contentType;//协议   
            request.UserAgent = DefaultUserAgent;//请求的客户端浏览器信息,默认IE            

            //    request.p                 
            request.Timeout = 6000;//超时时间，写死6秒  
                                   //随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空  

            System.Globalization.DateTimeFormatInfo dtfi;

            dtfi = System.Globalization.DateTimeFormatInfo.InvariantInfo;
            //   Console.WriteLine(dtfi.IsReadOnly);

            dtfi = new System.Globalization.DateTimeFormatInfo();
            //  Console.WriteLine(dtfi.IsReadOnly);

            dtfi = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat;
            //  Console.WriteLine(dtfi.IsReadOnly);


            //如果需求POST传数据，转换成utf-8编码  
            if (!_data.Equals(""))
            {
                byte[] data = requestEncoding.GetBytes(_data);
                request.ContentLength = data.Length;

                stream = request.GetRequestStream();

                stream.Write(data, 0, data.Length);

                stream.Close();
            }

            HttpWebResponse temp = request.GetResponse() as HttpWebResponse;

            return temp;

        }

        private string DealResponse(HttpWebResponse hwr)
        {
            Stream s = hwr.GetResponseStream();
            StreamReader sRead = new StreamReader(s);
            string res = sRead.ReadToEnd();
            s.Close();
            sRead.Close();
            return res;
        }
    }
}
