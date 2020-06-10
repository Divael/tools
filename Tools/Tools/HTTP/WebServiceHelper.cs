using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Xml;
using System.Collections;
using System.Net;
using System.Xml.Serialization;
using System.Drawing;

namespace Tools
{
    /// <summary>
    /// WebService代理服务的服务器名及方法名[WebServiceHelper类用]
    /// 比HttpService封装的更加完善一点
    /// </summary>
    public class WebServiceInfo
    {
        private string _WebServiceUrl = "http://localhost";
        /// <summary>
        /// WebService的服务器地址：默认：http://localhost
        /// </summary>
        public string WebServiceUrl { get { return this._WebServiceUrl; } set { this._WebServiceUrl = value; } }
        /// <summary>
        /// WebService服务名，如：UserService.asmx UserService.php...
        /// </summary>
        public string WebServiceName { get; set; }
    }
    /// <summary>
    /// <para>　</para>
    /// <para>　常用工具类——WebService代理类</para>
    /// <para>　----------------------------------------------</para>
    /// <para>　QueryPostWebService：采用Post方式调用WebService</para>
    /// <para>　QueryGetWebService：采用Get方式调用WebService</para>
    /// <para>　QueryGetWebService：通过SOAP协议调用WebService</para>
    /// </summary>
    public class WebServiceHelper
    {
        /// <summary>   
        /// 缓存xmlNameSpace，避免重复调用GetNameSpace   
        /// </summary>   
        private static Hashtable _xmlNamespaces = new Hashtable();

        /// <summary>   
        /// 采用Post方式调用WebService   
        /// 
        /// </summary>    
        /// <param name="WebServiceInfos"></param>
        /// <param name="MethodName"></param>   
        /// <param name="Para">string型的参数名和参数值组成的哈希表</param>
        /// <param name="contentType">application/x-www-form-urlencoded</param>   
        /// <returns></returns>   
        public static XmlDocument QueryPostWebService(WebServiceInfo WebServiceInfos, string MethodName, Hashtable Para ,string contentType = "application/x-www-form-urlencoded")
        {
            HttpWebRequest request;
            if (string.IsNullOrWhiteSpace(MethodName))
            {
                request = (HttpWebRequest)HttpWebRequest.Create(WebServiceInfos.WebServiceUrl + "/" + WebServiceInfos.WebServiceName);
            }
            else
            {
                request = (HttpWebRequest)HttpWebRequest.Create(WebServiceInfos.WebServiceUrl + "/" + WebServiceInfos.WebServiceName + "/" + MethodName);
            }
            request.Method = "POST";
            request.ContentType = contentType;
            SetWebRequest(request);
            byte[] data = EncodePars(Para);
            WriteRequestData(request, data);

            return ReadXmlResponse(request.GetResponse());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="WebServiceInfos">web地址信息</param>
        /// <param name="MethodName"></param>
        /// <param name="Para">post 参数hashtable.add("","")</param>
        /// <param name="contentType">默认application/x-www-form-urlencoded</param>
        /// <returns>string 可以是json</returns>
        public static string QueryPostWebServiceReString(WebServiceInfo WebServiceInfos, string MethodName, Hashtable Para,string contentType = "application/x-www-form-urlencoded")
        {
            HttpWebRequest request;
            if (string.IsNullOrWhiteSpace(MethodName))
            {
                request = (HttpWebRequest)HttpWebRequest.Create(WebServiceInfos.WebServiceUrl + "/" + WebServiceInfos.WebServiceName);
            }
            else
            {
                request = (HttpWebRequest)HttpWebRequest.Create(WebServiceInfos.WebServiceUrl + "/" + WebServiceInfos.WebServiceName + "/" + MethodName);
            }
            request.Method = "POST";
            request.ContentType = contentType;
            SetWebRequest(request);
            byte[] data = EncodePars(Para);
            WriteRequestData(request, data);
            string res = string.Empty;
            using (var s = request.GetResponse().GetResponseStream())
            {
                if (s!= null)
                {
                    using (StreamReader sRead = new StreamReader(s))
                    {
                        res = sRead.ReadToEnd();
                    }
                }
            }
            return res;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WebServiceInfos">web地址信息</param>
        /// <param name="MethodName"></param>
        /// <param name="JsonPara">post Json方式提交("","")</param>
        /// <param name="contentType">默认application/x-www-form-urlencoded</param>
        /// <returns>string 可以是json</returns>
        public static string QueryPostWebServiceReString(WebServiceInfo WebServiceInfos, string MethodName, string JsonPara, string contentType = "application/json")
        {
            HttpWebRequest request;
            if (string.IsNullOrWhiteSpace(MethodName))
            {
                request = (HttpWebRequest)HttpWebRequest.Create(WebServiceInfos.WebServiceUrl + "/" + WebServiceInfos.WebServiceName);
            }
            else
            {
                request = (HttpWebRequest)HttpWebRequest.Create(WebServiceInfos.WebServiceUrl + "/" + WebServiceInfos.WebServiceName + "/" + MethodName);
            }
            request.Method = "POST";
            request.ContentType = contentType;
            SetWebRequest(request);
            byte[] data = Encoding.UTF8.GetBytes(JsonPara);
            WriteRequestData(request, data);
            string res = string.Empty;
            using (var s = request.GetResponse().GetResponseStream())
            {
                if (s != null)
                {
                    using (StreamReader sRead = new StreamReader(s))
                    {
                        res = sRead.ReadToEnd();
                    }
                }
            }
            return res;
        }


        /// <summary>   
        /// 采用Get方式调用WebService   
        /// </summary>   
        /// <param name="WebServiceInfos"></param>
        /// <param name="MethodName"></param>   
        /// <param name="Para"></param>   
        /// <returns></returns>   
        public static XmlDocument QueryGetWebService(WebServiceInfo WebServiceInfos, string MethodName, Hashtable Para)
        {
            HttpWebRequest request;
            if (string.IsNullOrWhiteSpace(MethodName))
            {
                request = (HttpWebRequest)HttpWebRequest.Create(WebServiceInfos.WebServiceUrl + "/" + WebServiceInfos.WebServiceName);
            }
            else
            {
                request = (HttpWebRequest)HttpWebRequest.Create(WebServiceInfos.WebServiceUrl + "/" + WebServiceInfos.WebServiceName + "/" + MethodName);
            }
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            SetWebRequest(request);

            return ReadXmlResponse(request.GetResponse());
        }
        /// <summary>   
        /// 通过SOAP协议调用WebService   
        /// </summary>   
        /// <param name="WebServiceInfos"></param>
        /// <param name="MethodName"></param>   
        /// <param name="Para"></param>   
        /// <returns></returns>   
        public static XmlDocument QuerySoapWebService(WebServiceInfo WebServiceInfos, string MethodName, Hashtable Para)
        {
            if (_xmlNamespaces.ContainsKey(WebServiceInfos.WebServiceUrl+"/"+WebServiceInfos.WebServiceName))
            {
                return QuerySoapWebService(WebServiceInfos, MethodName, Para, _xmlNamespaces[WebServiceInfos.WebServiceUrl+"/"+WebServiceInfos.WebServiceName].ToString());
            }
            else
            {
                return QuerySoapWebService(WebServiceInfos, MethodName, Para, GetNamespace(WebServiceInfos.WebServiceUrl + "/" + WebServiceInfos.WebServiceName));
            }
        }

        #region private method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="WebServiceInfos"></param>
        /// <param name="MethodName"></param>
        /// <param name="Para"></param>
        /// <param name="XmlNs"></param>
        /// <returns></returns>
        private static XmlDocument QuerySoapWebService(WebServiceInfo WebServiceInfos, String MethodName, Hashtable Para, string XmlNs)
        {
            _xmlNamespaces[WebServiceInfos.WebServiceUrl+"/"+WebServiceInfos.WebServiceName] = XmlNs;//加入缓存，提高效率   
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(WebServiceInfos.WebServiceUrl+"/"+WebServiceInfos.WebServiceName+".asmx");
            request.Method = "POST";
            request.ContentType = "text/xml; charset=utf-8";
            request.Headers.Add("SOAPAction", "\"" + XmlNs + (XmlNs.EndsWith("/") ? "" : "/") + MethodName + "\"");
            SetWebRequest(request);
            byte[] data = EncodeParsToSoap(Para, XmlNs, MethodName);
            WriteRequestData(request, data);
            XmlDocument doc = new XmlDocument(), doc2 = new XmlDocument();
            doc = ReadXmlResponse(request.GetResponse());
            XmlNamespaceManager mgr = new XmlNamespaceManager(doc.NameTable);
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            String RetXml = doc.SelectSingleNode("//soap:Body/*/*", mgr).InnerXml;
            doc2.LoadXml("<root>" + RetXml + "</root>");
            AddDelaration(doc2);
            return doc2;
        }

        /// <summary>   
        /// 根据WebService的Url地址(以.asmx结尾的地址)获取其命名空间   
        /// </summary>   
        /// <param name="URL"></param>   
        /// <returns></returns>   
        private static string GetNamespace(String URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL + "?WSDL");
            SetWebRequest(request);
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sr.ReadToEnd());
            sr.Close();

            return doc.SelectSingleNode("//@targetNamespace").Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Pars"></param>
        /// <param name="XmlNs"></param>
        /// <param name="MethodName"></param>
        /// <returns></returns>
        private static byte[] EncodeParsToSoap(Hashtable Pars, String XmlNs, String MethodName)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"></soap:Envelope>");
            AddDelaration(doc);
            XmlElement soapBody = doc.CreateElement("soap", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
            XmlElement soapMethod = doc.CreateElement(MethodName);
            soapMethod.SetAttribute("xmlns", XmlNs);
            foreach (string k in Pars.Keys)
            {
                XmlElement soapPar = doc.CreateElement(k);
                soapPar.InnerXml = ObjectToSoapXml(Pars[k]);
                soapMethod.AppendChild(soapPar);
            }
            soapBody.AppendChild(soapMethod);
            doc.DocumentElement.AppendChild(soapBody);
            return Encoding.UTF8.GetBytes(doc.OuterXml);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static string ObjectToSoapXml(object o)
        {
            XmlSerializer mySerializer = new XmlSerializer(o.GetType());
            MemoryStream ms = new MemoryStream();
            mySerializer.Serialize(ms, o);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Encoding.UTF8.GetString(ms.ToArray()));
            if (doc.DocumentElement != null)
            {
                return doc.DocumentElement.InnerXml;
            }
            else
            {
                return o.ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        private static void SetWebRequest(HttpWebRequest request)
        {
            request.Credentials = CredentialCache.DefaultCredentials;
            //request.Timeout = 30000;
            request.Timeout = 120000;
            //request.PreAuthenticate = false;
        }

        private static void WriteRequestData(HttpWebRequest request, byte[] data)
        {
            request.ContentLength = data.Length;
            using (Stream writer = request.GetRequestStream())
            {
                writer.Write(data, 0, data.Length);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Pars"></param>
        /// <returns></returns>
        private static byte[] EncodePars(Hashtable Pars)
        {
            return Encoding.UTF8.GetBytes(ParsToString(Pars));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Pars"></param>
        /// <returns></returns>
        private static String ParsToString(Hashtable Pars)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string k in Pars.Keys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                sb.Append(HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(Pars[k].ToString()));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static XmlDocument ReadXmlResponse(WebResponse response)
        {
            XmlDocument doc;
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                String retXml = sr.ReadToEnd();
                sr.Close();
                doc = new XmlDocument();
                doc.LoadXml(retXml);
            }
            return doc;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        private static void AddDelaration(XmlDocument doc)
        {
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.InsertBefore(decl, doc.DocumentElement);
        }

        #endregion

    }
}
/* 通过代理类动态调用webservice，只需传入webservice的url，要调用的方法名，方法需要的参数名和参数值，  
             * 注意参数名要与webservice中定义的行参名相同。  
             
            Hashtable pars = new Hashtable();   
            string url = "http://localhost/WebService1/Service.asmx";   
            pars["input"] = this.textBox1.Text;   
            XmlDocument doc = WebServCaller.QuerySoapWebService(url, "ReserveString", pars);   
            this.textBox3.Text = "xml文件输出:\r\n" + doc.OuterXml + "\r\n----------------\r\n"    
                                + "输出返回值：\r\n" + doc.InnerText;  
*/