using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tools.HTTP
{
    /// <summary>
    /// 请求类型
    /// C# 服务端篇之实现RestFul Service开发(简单实用)
    /// https://www.cnblogs.com/xuliangxing/p/8735552.html
    /// </summary>
    public enum EnumHttpVerb
    {
        /// <summary>
        /// 
        /// </summary>
        GET,
        /// <summary>
        /// 
        /// </summary>
        POST,
        /// <summary>
        /// 
        /// </summary>
        PUT,
        /// <summary>
        /// 
        /// </summary>
        DELETE
    }

    public class RestFulClientHelper
    {
        #region 属性
        /// <summary>
        /// 端点路径
        /// </summary>
        public string EndPoint { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public EnumHttpVerb Method { get; set; }

        /// <summary>
        /// 文本类型（1、application/json 2、txt/html 3.application/x-www-form-urlencoded）
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 请求的数据(一般为JSon格式)
        /// </summary>
        public string PostData { get; set; }
        #endregion

        #region 初始化
        public RestFulClientHelper()
        {
            EndPoint = "";
            Method = EnumHttpVerb.GET;
            ContentType = "application/json";
            PostData = "";
        }

        public RestFulClientHelper(string endpoint)
        {
            EndPoint = endpoint;
            Method = EnumHttpVerb.GET;
            ContentType = "application/json";
            PostData = "";
        }

        public RestFulClientHelper(string endpoint, EnumHttpVerb method)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "application/json";
            PostData = "";
        }

        public RestFulClientHelper(string endpoint, EnumHttpVerb method, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "application/json";
            PostData = postData;
        }
        #endregion

        #region 方法
        /// <summary>
        /// http请求(不带参数请求)
        /// </summary>
        /// <returns></returns>
        public string HttpRequest()
        {
            return HttpRequest("");
        }

        /// <summary>
        /// http请求(带参数)
        /// </summary>
        /// <param name="parameters">parameters例如：?name=LiLei</param>
        /// <returns></returns>
        public string HttpRequest(string parameters)
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;

            if (!string.IsNullOrEmpty(PostData) && Method == EnumHttpVerb.POST)
            {
                var bytes = Encoding.UTF8.GetBytes(PostData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = string.Format("请求数据失败. 返回的 HTTP 状态码：{0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }
                return responseValue;
            }
        }
        #endregion
    }
}