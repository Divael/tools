using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Tools
{
    /// <summary>
    /// 实现通过Http发送数据
    /// </summary>
    public class HttpService
    {
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
        /// 获取accesstoken
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="jsondata">json字符串</param>
        /// <returns></returns>
        public string GetAccessTokenJSON(string url, string jsondata)
        {
            HttpWebRequest request = getHttpWebRequest(url);
            HttpWebResponse response = Post(request, jsondata, "application/json");

            return DealResponse(response);
        }
        /// <summary>
        /// Post方法
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="jsondata">json字符串</param>
        /// <returns></returns>
        public string PostService(string url,string data,string contentType)
        {
            HttpWebRequest request = getHttpWebRequest(url);
            HttpWebResponse resonse = Post(request,data, contentType);
            return DealResponse(resonse);
        }
        /// <summary>
        /// pos通服务交互
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="jsondata">json字符串</param>
        /// <param name="accessToken">accessToken</param>
        /// <returns></returns>
        public string PostServiceJSON(string url, string jsondata, string accessToken)
        {
            HttpWebRequest request = getHttpWebRequest(url);

            request.PreAuthenticate = true;
            request.Headers.Add("Authorization", accessToken);//accessToken;
            HttpWebResponse response = Post(request, jsondata,"application/json");
            return DealResponse(response);
        }

        private HttpWebResponse Post(HttpWebRequest request, string _data , string _contentType)
        {
            Stream stream = null;//用于传参数的流  

            request.Method = "POST";//传输方式  
            request.ContentType = _contentType;//协议   
            request.UserAgent = DefaultUserAgent;//请求的客户端浏览器信息,默认IE            

            //    request.p                 
            request.Timeout = 9000;//超时时间，写死6秒  
                                   //随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空  

            System.Globalization.DateTimeFormatInfo dtfi;

            dtfi = System.Globalization.DateTimeFormatInfo.InvariantInfo;
            //   Console.WriteLine(dtfi.IsReadOnly);

            dtfi = new System.Globalization.DateTimeFormatInfo();
            //  Console.WriteLine(dtfi.IsReadOnly);

            dtfi = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat;
            //  Console.WriteLine(dtfi.IsReadOnly);


            //如果需求POST传数据，转换成utf-8编码  
            byte[] data = requestEncoding.GetBytes(_data);
            request.ContentLength = data.Length;

            stream = request.GetRequestStream();

            stream.Write(data, 0, data.Length);

            stream.Close();

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
