using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Tools
{
    /// <summary>
    /// <para>　</para>
    /// 　常用工具类——日期时间类
    /// <para>　--------------------------------------------------------------</para>
    /// <para>　ConvertDate：返回当前日期的标准日期格式：2009-09-26</para>
    /// <para>　ConvertTime：返回标准时间格式：21:29:30</para>
    /// <para>　IsTime：是否为时间格式</para>
    /// <para>　IsDate：是否为日期格式：2009-09-03</para>
    /// <para>　IsDateTime：是否为日期加时间格式：2009-09-03 12:12:12</para>
    /// <para>　FormatDate：2008-9-6形式的字符串转成2008-09-06形式的方法</para>
    /// <para>　FileNameStr：生成年月日时分秒字符串</para>
    /// <para>　SecondToMinute：把秒转换成分钟</para>
    /// <para>　GetMonthLastDate：返回某年某月最后一天</para>
    /// <para>　DateDiff：计算两个日期的时间差</para>
    /// <para>　GetYearDateList：生成两个日期中的月份数组</para>
    /// <para>　GetFullWeek：获取指定日期的星期数(+2重载)</para>
    /// </summary>
    public class DateTimeHelper
    {

        #region 返回当前日期的标准日期格式：2009-09-26
        /// <summary>
        /// 返回当前日期的标准日期格式：2009-09-26
        /// </summary>
        public static string CurrentDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 返回指定日期string的标准日期格式：2009-09-26
        /// </summary>
        /// <param name="Dates">String格式日期</param>
        /// <returns></returns>
        public static string CurrentDate(string Dates)
        {
            string Conv = null;
            try
            {
                if (!string.IsNullOrEmpty(Dates))
                {
                    Conv = Convert.ToDateTime(Dates).ToString("yyyy-MM-dd");
                }
            }
            catch (Exception)
            {

            }
            return Conv;
        }
        /// <summary>
        /// 返回指定日期DateTime的标准日期格式string
        /// </summary>
        /// <param name="Dates">日期</param>
        /// <returns></returns>
        public static string CurrentDate(DateTime Dates)
        {
            return Dates.ToString("yyyy-MM-dd");
        }
        #endregion

        #region 返回标准时间格式：21:29:30
        /// <summary>
        /// 返回标准时间格式：21:29:30
        /// </summary>
        public static string CurrentTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
        #endregion

        #region 是否为时间格式
        /// <summary>
        /// 是否为时间格式
        /// </summary>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }
        #endregion

        #region  是否为日期格式：2009-09-03
        /// <summary>
        ///  是否为日期格式：2009-09-03
        /// </summary>
        /// <param name="DateStr">日期字符串</param>
        /// <returns></returns>
        public static bool IsDate(string DateStr)
        {
            bool flag = false;
            try
            {
                DateTime DaTi = Convert.ToDateTime(DateStr);
                if (DaTi.ToString("yyyy-MM-dd") == DateStr) flag = true;
            }
            catch (Exception)
            {
            }
            return flag;
        }
        #endregion

        #region 是否为日期加时间格式：2009-09-03 12:12:12
        /// <summary>
        /// 是否为日期加时间格式：2009-09-03 12:12:12
        /// </summary>
        /// <param name="DateTimeStr">日期加时间字符串</param>
        /// <returns></returns>
        public static bool IsDateTime(string DateTimeStr)
        {
            bool flag = false;
            try
            {
                DateTime DaTi = Convert.ToDateTime(DateTimeStr);
                if (DaTi.ToString("yyyy-MM-dd HH:mm:ss") == DateTimeStr) flag = true;
            }
            catch (Exception)
            {
            }
            return flag;
        }
        #endregion

        #region 2008-9-6形式的字符串转成2008-09-06形式的方法
        /// <summary>
        /// 2008-9-6形式的字符串转成2008-09-06形式的方法
        /// </summary>
        /// <param name="str">要转换的日期</param>
        /// <returns></returns>
        public static string FormatDate(string str)
        {
            DateTime dt = DateTime.Parse(str);
            string res = String.Format("{0:yyyy-MM-dd}", dt);
            return res;
        }
        #endregion

        #region 生成 （当前年月日时分秒+随机数子 的字符串）
        /// <summary>
        /// 工具方法:生成 （当前年月日时分秒+随机数子 的字符串）
        /// </summary>
        /// <param name="link">年月日时分秒之间的连接字符</param>
        /// <param name="RanLength">最后生成随机数的位数</param>
        /// <returns>返回年月日时分秒毫秒四位随机数字的字符串</returns>
        public static string FileNameStr(string link, int RanLength)
        {
            if (string.IsNullOrEmpty(link))
            {
                link = "";
            }
            int Year = DateTime.Now.Year;            //年
            int Month = DateTime.Now.Month;          //月份
            int Day = DateTime.Now.Day;              //日期
            int Hour = DateTime.Now.Hour;            //小时
            int Minute = DateTime.Now.Minute;        //分钟
            int Second = DateTime.Now.Second;        //秒
            int Milli = DateTime.Now.Millisecond;    //毫秒
            //生成随机数字
            string DataString = "0123456789";
            Random rnd = new Random();
            string rndstring = "";
            int i = 1;
            while (i <= RanLength)
            {
                rndstring += DataString[rnd.Next(DataString.Length)];
                i++;
            }
            string FileNameStr = (Year + link + Month + link + Day + link + Hour + link + Minute + link + Second + link + Milli + link + rndstring).ToString();
            return FileNameStr;
        }
        #endregion

        #region 把秒转换成分钟，往小取整
        /// <summary>
        /// 把秒转换成分钟，往小取整
        /// </summary>
        /// <param name="Second">秒</param>
        /// <returns>分钟</returns>
        public static int SecondToMinute(int Second)
        {
            decimal mm = (decimal)((decimal)Second / (decimal)60);
            return Convert.ToInt32(Math.Ceiling(mm));
        }
        #endregion

        #region 返回某年某月最后一天
        /// <summary>
        /// 返回某年某月最后一天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>日</returns>
        public static int GetMonthLastDate(int year, int month)
        {
            DateTime lastDay = new DateTime(year, month, new System.Globalization.GregorianCalendar().GetDaysInMonth(year, month));
            int Day = lastDay.Day;
            return Day;
        }
        #endregion

        #region 计算两个日期的时间差
        /// <summary>
        /// 返回类型
        /// </summary>
        public enum BackType
        {
            /// <summary>
            /// 返回间隔天数
            /// </summary>
            GetDays,
            /// <summary>
            /// 返回间隔小时数
            /// </summary>
            GetHours,
            /// <summary>
            /// 返回间隔分钟数
            /// </summary>            
            GetMinutes,
            /// <summary>
            /// 返回间隔秒数
            /// </summary>
            GetSeconds,
            /// <summary>
            /// 返回间隔整毫秒数
            /// </summary>
            GetMilliseconds,
            /// <summary>
            /// 返回X月X日或X小时前或X分钟前
            /// </summary>
            GetString,
        }
        /// <summary>
        /// 计算两个日期的时间差 
        /// </summary>
        /// <param name="DateTime1">原日期</param>
        /// <param name="DateTime2">新日期</param>
        /// <param name="BackTypes">返回类型：天，小时，分钟，秒，毫秒，（X天或X小时或X分钟）</param>
        /// <returns>null或一个具体类型</returns>
        public static object DateDiff(DateTime DateTime1, DateTime DateTime2, BackType BackTypes)
        {
            object Diff = null;
            TimeSpan ts = DateTime2 - DateTime1;
            switch (BackTypes)
            {
                default:
                    Diff = ts.Days;
                    break;
                case BackType.GetDays:
                    Diff = ts.Days;
                    break;
                case BackType.GetHours:
                    Diff = ts.Minutes;
                    break;
                case BackType.GetSeconds:
                    Diff = ts.Seconds;
                    break;
                case BackType.GetMilliseconds:
                    Diff = ts.Milliseconds;
                    break;
                case BackType.GetString:
                    string OutDiff = null;
                    if (ts.Days >= 1)
                    {
                        OutDiff = ts.Days + "天";
                    }
                    else
                    {
                        if (ts.Hours >= 1)
                        {
                            OutDiff = ts.Hours + "小时";
                        }
                        else
                        {
                            OutDiff = ts.Minutes + "分钟";
                        }
                    }
                    Diff = OutDiff;
                    break;
            }
            return Diff;

        }
        #endregion

        #region 生成两个日期中的月份数组
        /// <summary>
        /// 生成两个日期中的月份数组
        /// </summary>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns></returns>
        public static IList<int[]> GetYearDateList(DateTime StartDate, DateTime EndDate)
        {
            List<int[]> YearMonthList = new List<int[]>();
            int SYear = StartDate.Year;
            int SMonth = StartDate.Month;
            int EYear = EndDate.Year;
            int EMonth = EndDate.Month;
            //HttpContext.Current.Response.Write(SMonth + "   " + EMonth+"<br><hr>");
            if (SYear == EYear)
            {
                for (int i = SMonth; i <= EMonth; i++)
                {
                    int[] arrs = new int[2];
                    arrs[0] = SYear;
                    arrs[1] = i;
                    YearMonthList.Add(arrs);
                    //HttpContext.Current.Response.Write(arrs[0].ToString() + "年" + arrs[1] + "月<br><hr>");
                }
            }
            else if (SYear < EYear)
            {
                for (int i = SYear; i <= EYear; i++)
                {
                    if (i == SYear)
                    {
                        for (int j = SMonth; j <= 12; j++)
                        {
                            int[] arrs = new int[2];
                            arrs[0] = i;
                            arrs[1] = j;
                            YearMonthList.Add(arrs);
                            // HttpContext.Current.Response.Write(arrs[0].ToString() + "年" + arrs[1] + "月<br><hr>");
                        }
                    }
                    else if (i > SYear && i < EYear)
                    {
                        for (int j = 1; j <= 12; j++)
                        {
                            int[] arrs = new int[2];
                            arrs[0] = i;
                            arrs[1] = j;
                            YearMonthList.Add(arrs);
                            //HttpContext.Current.Response.Write(arrs[0].ToString() + "年" + arrs[1] + "月<br><hr>");
                        }
                    }
                    else if (i == EYear)
                    {
                        for (int j = 1; j <= EMonth; j++)
                        {
                            int[] arrs = new int[2];
                            arrs[0] = i;
                            arrs[1] = j;
                            YearMonthList.Add(arrs);
                            //HttpContext.Current.Response.Write(arrs[0].ToString() + "年" + arrs[1] + "月<br><hr>");
                        }
                    }
                }
            }
            return YearMonthList;
        }
        #endregion

        #region 获取指定日期的星期数，获取当前日期的星期数，如星期一
        /// <summary>
        /// 获取指定日期的星期数，如星期一
        /// </summary>
        /// <param name="T">当前日期时间</param>
        /// <returns>星期一、星期二……</returns>
        public static string GetFullWeek(DateTime T)
        {
            string Week = "";
            switch (T.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    Week = "一";
                    break;
                case DayOfWeek.Tuesday:
                    Week = "二";
                    break;
                case DayOfWeek.Wednesday:
                    Week = "三";
                    break;
                case DayOfWeek.Thursday:
                    Week = "四";
                    break;
                case DayOfWeek.Friday:
                    Week = "五";
                    break;
                case DayOfWeek.Saturday:
                    Week = "六";
                    break;
                case DayOfWeek.Sunday:
                    Week = "日";
                    break;
            }
            return string.IsNullOrEmpty(Week) ? "" : "星期" + Week;
        }
        /// <summary>
        /// 获取当前日期的星期数，如星期一
        /// </summary>
        /// <returns>星期一、星期二……</returns>
        public static string GetFullWeek()
        {
            return GetFullWeek(DateTime.Now);
        }
        #endregion
    }
}

