namespace Tools
{
    /// <summary>
    /// <para>　</para>
    /// 　常用工具类——网络相关类
    /// <para>　==============以下为服务器信息==================</para>
    /// <para>　GetServerName：获取站点服务器名称</para>
    /// <para>　GetServerRootPath：获取站点根目录</para>
    /// <para>　GetServerIP：获取站点服务器ＩＰ地址</para>
    /// <para>　GetServerDoMain：获取站点服务器域名</para>
    /// <para>　GetServerNetVersion：获取站点服务器.NET框架版本</para>
    /// <para>　GetServerOsVersion：获取站点服务器操作系统</para>
    /// <para>　GetServerIIS：获取站点服务器ＩＩＳ环境</para>
    /// <para>　GetServerSitePort：获取站点服务器站点协议</para>
    /// <para>　GetServerSiteProtocol：获取站点服务器站点端口</para>
    /// <para>　GetServerScriptTimeOut：获取站点服务器脚本超时时间</para>
    /// <para>　GetServerDateTime：获取站点服务器当前时间</para>
    /// <para>　GetServerJDPath：获取站点服务器虚拟目录绝对路径</para>
    /// <para>　GetServerTranslatedPath：获取站点服务器执行文件绝对路径</para>
    /// <para>　CheckServeIsSupportHTTPS：获取站点服务器是否支持HTTPS</para>
    /// <para>　GetServerSessionCount：获取站点服务器SESSION总数</para>
    /// <para>　GetServerApplicationCount：获取站点服务器Application总数</para>
    /// <para>　CheckServerIsSupportJMail：检测服务器是否支持JMail</para>
    /// <para>　CheckServerIsSupportPersitesMail：检测服务器是否支持Persites邮件</para>
    /// <para>　CheckServerIsSupportLyfUpload：检测服务器是否支持LyfUpload上传</para>
    /// <para>　CheckServerIsSupportADOConnection：检测服务器是否支持ADO数据连接</para>
    /// <para>　CheckServerIsSupportFSO：检测服务器是否支持FSO</para>
    /// <para>　CheckServerIsSupportCDONTS：检测服务器是否支持CDONTSMail</para>
    /// <para>　ShowAllServerVariablesInfo：显示所有的服务器ServerVariables信息</para>
    /// <para>　===============以下为客户端信息=================</para>
    /// <para>　GetCliectIP：获取客户端IP地址</para>
    /// <para>　GetClientServerVerion：客户端操作系统</para>
    /// <para>　GetClientBrowserType：客户端浏览器类别</para>
    /// <para>　GetClientBrowserVersion：客户端浏览器版本号</para>
    /// <para>　CheckClientBrowserIsSupportVBScript：客户端浏览器是否支持VBScript</para>
    ///  <para>　CheckClientBrowserIsSupportMobileDevice：客户端浏览器是否能识别移动设备</para>
    ///   <para>　CheckClientBrowserIsSupportActiveX：客户端浏览器是否能支持ActiveX</para>
    ///   <para>　CheckClientBrowserIsSupportBackgroundSounds：客户端浏览器是否能支持背景音乐播放</para>
    ///   <para>　GetClientBrowserNetVersion：客户端浏览器NET框架版本</para>
    ///   <para>　CheckClientBrowserSupportCookies：客户端浏览器是否支持Cookies</para>
    ///   <para>　CheckClientBrowserSupportFrames：客户端浏览器是否支持HTML框架</para>
    ///   <para>　CheckClientBrowserSupportJavaApplets：客户端浏览器是否支持Java</para>
    ///   <para>　GetClientLanguage：获取客户端默认语言</para>
    ///   <para>　GetClientIP：获得当前页面客户端的IP[ +2 重载 ]</para>
    /// </summary>
    //public class NetWorkHelper
    //{

    //    #region 初始化组件支持
    //    private static bool CheckObject(string Object)
    //    {
    //        bool flag = false;
    //        try
    //        {
    //            object Obj = HttpContext.Current.Server.CreateObject(Object);
    //            flag = true;
    //        }
    //        catch (Exception)
    //        {
    //        }
    //        return flag;
    //    }

    //    #endregion

    //    #region 获取站点服务器名称
    //    /// <summary>
    //    /// 获取站点服务器名称
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetServerName()
    //    {
    //        return HttpContext.Current.Server.MachineName;
    //    }
    //    #endregion

    //    #region 获取站点根目录
    //    /// <summary>
    //    /// 获取站点根目录
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetServerRootPath(string StrPath)
    //    {
    //        if (HttpContext.Current != null)
    //        {
    //            return HttpContext.Current.Server.MapPath(StrPath);
    //        }
    //        else //非web程序引用
    //        {
    //            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StrPath);
    //        }

    //    }
    //    #endregion

    //    #region 获取站点服务器ＩＰ地址
    //    /// <summary>
    //    /// 获取站点服务器ＩＰ地址
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetServerIP()
    //    {
    //        return HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
    //    }
    //    #endregion

    //    #region 获取站点服务器域名
    //    /// <summary>
    //    /// 获取站点服务器域名
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetServerDoMain()
    //    {
    //        return HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
    //    }
    //    #endregion

    //    #region 获取站点服务器.NET框架版本
    //    /// <summary>
    //    /// 获取站点服务器.NET框架版本
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetServerNetVersion()
    //    {
    //        return Environment.Version.ToString();
    //    }
    //    #endregion

    //    #region 获取站点服务器操作系统
    //    /// <summary>
    //    /// 获取站点服务器操作系统
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetServerOsVersion()
    //    {
    //        return Environment.OSVersion.ToString();
    //    }
    //    #endregion

    //    #region 获取站点服务器ＩＩＳ环境
    //    /// <summary>
    //    /// 获取站点服务器ＩＩＳ环境
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetServerIISVersion()
    //    {
    //        return HttpContext.Current.Request.ServerVariables["SERVER_SOFTWARE"];
    //    }
    //    #endregion

    //    #region 获取站点服务器站点端口
    //    /// <summary>
    //    /// 获取站点服务器站点端口
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetServerSitePort()
    //    {
    //        return HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
    //    }
    //    #endregion

    //    #region 获取站点服务器站点协议
    //    /// <summary>
    //    /// 获取站点服务器站点协议
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetServerSiteProtocol()
    //    {
    //        return HttpContext.Current.Request.ServerVariables["Server_Protocol"];
    //    }
    //    #endregion

    //    #region 获取站点服务器脚本超时时间
    //    /// <summary>
    //    /// 获取站点服务器脚本超时时间
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetServerScriptTimeOut()
    //    {
    //        return HttpContext.Current.Server.ScriptTimeout.ToString();
    //    }
    //    #endregion

    //    #region 获取站点服务器当前时间
    //    /// <summary>
    //    /// 获取站点服务器当前时间
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetServerDateTime()
    //    {
    //        return DateTime.Now.ToString("F");
    //    }
    //    #endregion

    //    #region 获取站点服务器虚拟目录绝对路径
    //    /// <summary>
    //    /// 获取站点服务器虚拟目录绝对路径
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetServerPhysicalPath()
    //    {
    //        return HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"];
    //    }
    //    #endregion

    //    #region 获取站点服务器执行文件绝对路径
    //    /// <summary>
    //    /// 获取站点服务器执行文件绝对路径
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetServerTranslatedPath()
    //    {
    //        return HttpContext.Current.Request.ServerVariables["PATH_TRANSLATED"];
    //    }
    //    #endregion

    //    #region 获取站点服务器HTTPS支持
    //    /// <summary>
    //    /// 获取站点服务器HTTPS支持
    //    /// </summary>
    //    /// <returns></returns>
    //    public static bool CheckServeIsSupportHTTPS()
    //    {
    //        bool flag = false;
    //        if (HttpContext.Current.Request.ServerVariables["HTTPS"] == "on") flag = true;
    //        return flag;
    //    }
    //    #endregion

    //    #region 获取站点服务器SESSION总数
    //    /// <summary>
    //    /// 获取站点服务器SESSION总数
    //    /// </summary>
    //    /// <returns></returns>
    //    public static int GetServerSessionCount()
    //    {
    //        return HttpContext.Current.Session.Keys.Count;
    //    }
    //    #endregion

    //    #region 获取站点服务器Application总数
    //    /// <summary>
    //    /// 获取站点服务器Application总数
    //    /// </summary>
    //    /// <returns></returns>
    //    public static int GetServerApplicationCount()
    //    {
    //        return HttpContext.Current.Application.Keys.Count;
    //    }
    //    #endregion

    //    #region  检测服务器是否支持JMail
    //    /// <summary>
    //    /// 检测服务器是否支持JMail
    //    /// </summary>
    //    /// <returns></returns>
    //    public static bool CheckServerIsSupportJMail()
    //    {
    //        return CheckObject("JMail.SmtpMail");
    //    }
    //    #endregion

    //    #region  检测服务器是否支持Persites邮件
    //    /// <summary>
    //    /// 检测服务器是否支持Persites邮件
    //    /// </summary>
    //    /// <returns></returns>
    //    public static bool CheckServerIsSupportPersitesMail()
    //    {
    //        return CheckObject("Persits.MailSender");
    //    }
    //    #endregion

    //    #region  检测服务器是否支持LyfUpload上传
    //    /// <summary>
    //    /// 检测服务器是否支持LyfUpload上传
    //    /// </summary>
    //    /// <returns></returns>
    //    public static bool CheckServerIsSupportLyfUpload()
    //    {
    //        return CheckObject("LyfUpload.UploadFile");
    //    }
    //    #endregion

    //    #region  检测服务器是否支持ADO数据连接
    //    /// <summary>
    //    /// 检测服务器是否支持ADO数据连接
    //    /// </summary>
    //    /// <returns></returns>
    //    public static bool CheckServerIsSupportADOConnection()
    //    {
    //        return CheckObject("ADODB.Connection");
    //    }
    //    #endregion

    //    #region  检测服务器是否支持FSO
    //    /// <summary>
    //    /// 检测服务器是否支持FSO
    //    /// </summary>
    //    /// <returns></returns>
    //    public static bool CheckServerIsSupportFSO()
    //    {
    //        return CheckObject("Scripting.FileSystemObject");
    //    }
    //    #endregion

    //    #region  检测服务器是否支持CDONTSMail
    //    /// <summary>
    //    /// 检测服务器是否支持CDONTSMail
    //    /// </summary>
    //    /// <returns></returns>
    //    public static bool CheckServerIsSupportCDONTS()
    //    {
    //        return CheckObject("CDONTS.NewMail");
    //    }
    //    #endregion

    //    #region 显示所有的服务器ServerVariables信息
    //    /// <summary>
    //    /// 显示所有的服务器ServerVariables信息
    //    /// </summary>
    //    public static void ShowAllServerVariablesInfo()
    //    {
    //        foreach (string i in HttpContext.Current.Request.ServerVariables)
    //        {
    //            HttpContext.Current.Response.Write("<font color='blue'>" + i + "</font>：<font color='red'>" + HttpContext.Current.Request.ServerVariables[i] + "</font><br>");
    //        }
    //    }
    //    #endregion

    //    //以下为客户端部分    

    //    #region 客户端操作系统
    //    /// <summary>
    //    /// 客户端操作系统
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetClientServerVerion()
    //    {
    //        return HttpContext.Current.Request.Browser.Platform.ToString();
    //    }
    //    #endregion

    //    #region 客户端浏览器类别
    //    /// <summary>
    //    /// 客户端浏览器类别
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetClientBrowserType()
    //    {
    //        return HttpContext.Current.Request.Browser.Browser.ToString();
    //    }
    //    #endregion

    //    #region 客户端浏览器版本号
    //    /// <summary>
    //    /// 客户端浏览器版本号
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetClientBrowserVersion()
    //    {
    //        return HttpContext.Current.Request.Browser.Version.ToString() + HttpContext.Current.Request.Browser.MinorVersionString.ToString();
    //    }
    //    #endregion

    //    #region 客户端浏览器是否支持VBScript
    //    /// <summary>
    //    /// 客户端浏览器是否支持VBScript
    //    /// </summary>
    //    /// <returns></returns>
    //    public static bool CheckClientBrowserIsSupportVBScript()
    //    {
    //        return HttpContext.Current.Request.Browser.VBScript;
    //    }
    //    #endregion

    //    #region 客户端浏览器是否能识别移动设备
    //    /// <summary>
    //    /// 客户端浏览器是否能识别移动设备
    //    /// </summary>
    //    /// <returns></returns>
    //    public static bool CheckClientBrowserIsSupportMobileDevice()
    //    {
    //        return HttpContext.Current.Request.Browser.IsMobileDevice;
    //    }
    //    #endregion

    //    #region 客户端浏览器是否能支持ActiveX
    //    /// <summary>
    //    /// 客户端浏览器是否能支持ActiveX
    //    /// </summary>
    //    /// <returns></returns>
    //    public static bool CheckClientBrowserIsSupportActiveX()
    //    {
    //        return HttpContext.Current.Request.Browser.ActiveXControls;
    //    }
    //    #endregion

    //    #region 客户端浏览器是否能支持背景音乐播放
    //    /// <summary>
    //    /// 客户端浏览器是否能支持背景音乐播放
    //    /// </summary>
    //    /// <returns></returns>
    //    public static bool CheckClientBrowserIsSupportBackgroundSounds()
    //    {
    //        return HttpContext.Current.Request.Browser.BackgroundSounds;
    //    }
    //    #endregion

    //    #region 客户端浏览器NET框架版本
    //    /// <summary>
    //    /// 客户端浏览器NET框架版本
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetClientBrowserNetVersion()
    //    {
    //        return HttpContext.Current.Request.Browser.ClrVersion.ToString();
    //    }
    //    #endregion

    //    #region 客户端浏览器是否支持Cookies
    //    /// <summary>
    //    /// 客户端浏览器是否支持Cookies
    //    /// </summary>
    //    /// <returns></returns>
    //    public static bool CheckClientBrowserSupportCookies()
    //    {
    //        return HttpContext.Current.Request.Browser.Cookies;
    //    }
    //    #endregion

    //    #region 客户端浏览器是否支持HTML框架
    //    /// <summary>
    //    /// 客户端浏览器是否支持HTML框架
    //    /// </summary>
    //    /// <returns></returns>
    //    public static bool CheckClientBrowserSupportFrames()
    //    {
    //        return HttpContext.Current.Request.Browser.Frames;
    //    }
    //    #endregion

    //    #region 客户端浏览器是否支持Java
    //    /// <summary>
    //    /// 客户端浏览器是否支持Java
    //    /// </summary>
    //    /// <returns></returns>
    //    public static bool CheckClientBrowserSupportJavaApplets()
    //    {
    //        return HttpContext.Current.Request.Browser.JavaApplets;
    //    }
    //    #endregion

    //    #region 获取客户端默认语言
    //    /// <summary>
    //    /// 获取客户端默认语言
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetClientLanguage()
    //    {
    //        return HttpContext.Current.Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"];
    //    }
    //    #endregion

    //    #region 获得当前页面客户端的IP
    //    /// <summary>
    //    /// 获得当前页面客户端的IP
    //    /// </summary>
    //    /// <returns>当前页面客户端的IP</returns>
    //    public static string GetClientIP()
    //    {
    //        string result = String.Empty;
    //        result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
    //        if (null == result || result == String.Empty)
    //        {
    //            result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
    //        }
    //        if (null == result || result == String.Empty)
    //        {
    //            result = HttpContext.Current.Request.UserHostAddress;
    //        }
    //        if (null == result || result == String.Empty || !Utils.MathHelper.IsIP(result))
    //        {
    //            return "0.0.0.0";
    //        }
    //        return result;
    //    }
    //    /// <summary>
    //    /// 获得当前页面客户端的IP，并以指定显示位数，其余显示为*号
    //    /// </summary>
    //    /// <param name="ShowLength">显示IP的位数，其它以*号代替</param>
    //    /// <returns></returns>
    //    public static string GetClientIP(int ShowLength)
    //    {
    //        string Result = string.Empty;
    //        string ClientIP = GetClientIP();
    //        if (!string.IsNullOrEmpty(ClientIP))
    //        {
    //            Result = GetClientIP(ClientIP, ShowLength);
    //        }
    //        return Result;
    //    }
    //    /// <summary>
    //    /// 将指定IP格式化：显示位数，其他显示为*号代替
    //    /// </summary>
    //    /// <param name="ClientIP">客户端IP</param>
    //    /// <param name="ShowLength">显示IP的位数，其它以*号代替</param>
    //    /// <returns></returns>
    //    public static string GetClientIP(string ClientIP, int ShowLength)
    //    {
    //        string Result = string.Empty;
    //        if (!string.IsNullOrEmpty(ClientIP))
    //        {
    //            string[] IPArr = Utils.StringHelper.Split(ClientIP, ".");
    //            if (ShowLength >= 4) ShowLength = 4;
    //            if (ShowLength <= 0) ShowLength = 0;
    //            if (IPArr != null && IPArr.Length > 0)
    //            {
    //                for (int i = 0; i < IPArr.Length; i++)
    //                {
    //                    if (i < ShowLength)
    //                    {
    //                        Result += IPArr[i]+".";
    //                    }
    //                    else
    //                    {
    //                        Result += "*.";                            
    //                    }
    //                }
    //            }
    //            Result = Result.Substring(0, Result.Length - 1);
    //        }
    //        return Result;
    //    }
    //    #endregion
    //}
}
