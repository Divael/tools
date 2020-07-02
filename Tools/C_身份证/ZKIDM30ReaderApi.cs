using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace C_身份证
{
    #region 使用方法

    //private void Sfz_NewPerson(string arg1, string arg2, string arg3)
    //{
    //    this.Invoke(() => NewCardIn(arg1, arg2, arg3));
    //}

    //protected virtual void NewCardIn(string card, string name, string sex)
    //{
    //    var data = His.Get1004Ye(card);
    //    PayTask.sfz_id = card;
    //    PayTask.name = name;
    //    PayTask.sex = sex;
    //    if (data.success)
    //    {
    //        uMain.ToUI(new uks());
    //    }
    //    else
    //    {
    //        $"卡号{card} 姓名{name}，进入建档".LogThis();

    //        uMain.ToUI(new ujiandang(false));
    //    }
    //}

    #endregion

    /// <summary>
    ///         public static C_身份证.ZKIDM30ReaderApi sfz = new C_身份证.ZKIDM30ReaderApi();
    ///         PayTask.sfz.NewPerson += Sfz_NewPerson;
    ///         PayTask.sfz.NewPerson -= Sfz_NewPerson;
    ///             PayTask.sfz.StopRead();
    ///             PayTask.sfz.StartRead();
    /// </summary>
    public class ZKIDM30ReaderApi
    {
        public static int Port = 0;
        public static int ComPort = 0;
        public const int cbDataSize = 128;
        public const int GphotoSize = 256 * 1024;
        public static Boolean IsConnected = false;
        public static Boolean IsAuthenticate = false;
        public static Boolean IsRead_Content = false;

        [DllImport("termb.dll")]
        static extern int IC_WriteData(int iPort, int keyMode, int sector, int idx, StringBuilder key, StringBuilder data, int cbdata, ref int snr);//写数据
        [DllImport("termb.dll")]
        static extern int InitCommExt();//自动搜索身份证阅读器并连接身份证阅读器 
        [DllImport("termb.dll")]
        static extern int CloseComm();//断开与身份证阅读器连接 

        [DllImport("termb.dll")]
        static extern int Authenticate();//判断是否有放卡，且是否身份证 

        [DllImport("termb.dll")]
        public static extern int Read_Content(int index);//读卡操作,信息文件存储在dll所在下

        [DllImport("termb.dll")]
        static extern void GetPhotoJPGPathName(StringBuilder path, int cbPath);//获取jpg头像全路径名 


        [DllImport("termb.dll")]
        static extern int SetPhotoJPGPathName(string path);//设置jpg头像全路径名
        [DllImport("termb.dll")]
        static extern int getName(StringBuilder data, int cbData);//获取姓名

        [DllImport("termb.dll")]
        static extern int getSex(StringBuilder data, int cbData);//获取性别

        [DllImport("termb.dll")]
        static extern int getNation(StringBuilder data, int cbData);//获取民族

        [DllImport("termb.dll")]
        static extern int getBirthdate(StringBuilder data, int cbData);//获取生日(YYYYMMDD)

        [DllImport("termb.dll")]
        static extern int getAddress(StringBuilder data, int cbData);//获取地址

        [DllImport("termb.dll")]
        static extern int getIDNum(StringBuilder data, int cbData);//获取身份证号

        [DllImport("termb.dll")]
        static extern int getIssue(StringBuilder data, int cbData);//获取签发机关

        [DllImport("termb.dll")]
        static extern int getEffectedDate(StringBuilder data, int cbData);//获取有效期起始日期(YYYYMMDD)

        [DllImport("termb.dll")]
        static extern int getExpiredDate(StringBuilder data, int cbData);//获取有效期截止日期(YYYYMMDD) 
        [DllImport("termb.dll")]
        static extern int GetBmpPhoto(string PhotoPath);//解析身份证照片

        [DllImport("termb.dll")]
        static extern int GetBmpPhotoExt();//解析身份证照片


        public event Action<string, string, string> NewPerson;
        CTimer ticker = new CTimer { Interval = 1500.ToMilliseconds() };

        /// <summary>
        /// event.startread,stopread.
        /// </summary>
        public ZKIDM30ReaderApi()
        {
            ticker.Tick += Ticker_Tick;
            opcon();

        }

        private void Ticker_Tick()
        {
            //var s = suchKa();
            //if (!s) return;
            //await Task.Delay(500);

            common.RunInBackThread(() =>
            {
                var s = DoRead_Content();
                if (!s) return;
                common.Sleep(300);
                NewPerson?.Invoke(idNum(null, null), userName(null, null), sexSy(null, null));
                ticker.Stop();
            });



            //   closeCon();

        }

        //连接设备
        public bool opcon()
        {
            int AutoSearchReader = InitCommExt();
            if (AutoSearchReader > 0)
            {
                //MessageBox.Show
                ("连接设备成功").LogThis();
                return true;
            }
            else
            //MessageBox.Show
            {
                ("连接设备失败，请联系").LogThis();
                return false;
            }



        }
        //关闭设备
        public bool closeCon()
        {
            int closec = CloseComm();
            if (closec > 0)
            {
                //MessageBox.Show
                ("关闭设备成功").LogThis();
                IsRead_Content = false;
                IsAuthenticate = false;
            }
            else
            {
                //MessageBox.Show
                ("关闭设备失败请联系管理员").LogThis();
            }
            return closec > 0;
        }
        // 验证卡片是否放置
        public bool suchKa()
        {
            int FindCard = Authenticate();
            switch (FindCard)
            {
                case 1:
                    //  MessageBox.Show
                    ("Authenticate:寻卡成功 ").LogThis();
                    IsAuthenticate = true; break;
                case -1:
                    //MessageBox.Show
                    ("Authenticate:寻卡失败 ").LogThis();
                    IsAuthenticate = false; break;
                case -2:
                    //MessageBox.Show
                    ("Authenticate:选卡失败 ").LogThis();
                    IsAuthenticate = false; break;
                case 0:
                    //MessageBox.Show
                    ("Authenticate:其他错误 ").LogThis();
                    IsAuthenticate = false; break;
                default:
                    break;
            }
            return IsAuthenticate;
        }
        //卡片连接
        public bool DoRead_Content()
        {
            int index = 0;
            Boolean falg = suchKa();
            if (falg)
            {
                int rs = Read_Content(index + 1);
                if (rs > 0)
                {

                    IsRead_Content = true;
                    //MessageBox.Show
                    ("Read_Content: Success").LogThis();
                }
                else
                {

                    IsRead_Content = false;
                    //MessageBox.Show
                    ("Read_Content: Fail").LogThis();
                }

            }
            return falg;

        }
        //姓名
        public String userName(object sender, EventArgs e)
        {
            String uName = null;
            StringBuilder sb = new StringBuilder(cbDataSize);
            if (IsRead_Content && IsAuthenticate)
            {


                getName(sb, cbDataSize);
                uName = sb.ToString();

            }
            else
            {
                //MessageBox.Show
                ("未连接设备").LogThis();
                uName = "";

            }
            return uName;


        }

        public void StartRead() => ticker.Start();
        public void StopRead() => ticker.Stop();


        //性别
        public String sexSy(object sender, EventArgs e)
        {
            String sex = null;
            StringBuilder sb = new StringBuilder(cbDataSize);
            if (IsRead_Content && IsAuthenticate)
            {
                getSex(sb, cbDataSize);
                sex = sb.ToString();

            }
            else
            {


            }
            return sex;

        }

        //民族
        public String nationSy(object sender, EventArgs e)
        {
            String nation = null;
            StringBuilder sb = new StringBuilder(cbDataSize);
            if (IsAuthenticate && IsRead_Content)
            {
                getNation(sb, cbDataSize);
                nation = sb.ToString();

            }
            else
            {
                //MessageBox.Show
                ("未连接设备").LogThis();
                nation = "";
            }
            return nation;
            //System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3 = new ToolStripStatusLabel();
            //StringBuilder sb = new StringBuilder(cbDataSize);
            //if (IsAuthenticate && IsRead_Content)
            //{
            //    getNation(sb, cbDataSize);
            //    toolStripStatusLabel3.Text = sb.ToString();
            //    labelNation.Text = sb.ToString();
            //}
        }
        //出生
        public String birthdateSy(object sender, EventArgs e)
        {
            String birthdate = null;
            StringBuilder sb = new StringBuilder(cbDataSize);

            if (IsAuthenticate && IsRead_Content)
            {
                getBirthdate(sb, cbDataSize);
                birthdate = sb.ToString();

            }
            else
            {
                ("未连接设备").LogThis();
                birthdate = "";
            }
            return birthdate;
            //System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3 = new ToolStripStatusLabel();
            //StringBuilder sb = new StringBuilder(cbDataSize);
            //if (IsAuthenticate && IsRead_Content)
            //{
            //    getBirthdate(sb, cbDataSize);
            //    toolStripStatusLabel3.Text = sb.ToString();
            //    labelYear.Text = sb.ToString().Substring(0, 4);
            //    labelMonth.Text = sb.ToString().Substring(4, 2);
            //    labelDay.Text = sb.ToString().Substring(6, 2);
            //}
        }
        //住址
        public String addressSy(object sender, EventArgs e)
        {
            String address = null;
            StringBuilder sb = new StringBuilder(cbDataSize);
            if (IsAuthenticate && IsRead_Content)
            {
                getAddress(sb, cbDataSize);
                address = sb.ToString();

            }
            else
            {
                ("未连接设备").LogThis();
                address = "";
            }
            return address;
            //System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3 = new ToolStripStatusLabel();
            //StringBuilder sb = new StringBuilder(cbDataSize);
            //if (IsAuthenticate && IsRead_Content)
            //{
            //    getAddress(sb, cbDataSize);
            //    string ad = sb.ToString();
            //    toolStripStatusLabel3.Text = ad;
            //    if (ad.Length > 11)
            //    {
            //        labelAddress.Text = ad.Substring(0, 11);
            //        labelAddress2.Text = ad.Substring(11);
            //    }
            //    else
            //    {
            //        labelAddress.Text = ad;
            //    }


            //}
        }
        //身份证号码
        public String idNum(object sender, EventArgs e)
        {
            String idnum = null;
            StringBuilder sb = new StringBuilder(cbDataSize);

            if (IsAuthenticate && IsRead_Content)
            {
                getIDNum(sb, cbDataSize);
                idnum = sb.ToString();
            }
            else
            {
                ("未连接设备").LogThis();
                idnum = "";
            }
            return idnum;

            //System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3 = new ToolStripStatusLabel();
            //StringBuilder sb = new StringBuilder(cbDataSize);
            //if (IsAuthenticate && IsRead_Content)
            //{
            //    getIDNum(sb, cbDataSize);
            //    toolStripStatusLabel3.Text = sb.ToString();
            //    labelIDNum.Text = sb.ToString();
            //}
        }
        //签发机关
        public String issueSy(object sender, LinkLabelLinkClickedEventArgs e)
        {
            String issue = null;
            StringBuilder sb = new StringBuilder(cbDataSize);
            if (IsAuthenticate && IsRead_Content)
            {
                try
                {
                    getIssue(sb, cbDataSize);
                    issue = sb.ToString();
                }
                catch { }
            }
            else
            {
                //MessageBox.Show
                ("未连接设备").LogThis();
                issue = "";
            }
            return issue;

            //System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3 = new ToolStripStatusLabel();
            //StringBuilder sb = new StringBuilder(cbDataSize);
            //if (IsAuthenticate && IsRead_Content)
            //{
            //    getIssue(sb, cbDataSize);
            //    toolStripStatusLabel3.Text = sb.ToString();
            //    labelIssue.Text = sb.ToString();
            //}
        }
        //有效期
        public String expiredDateSy(object sender, LinkLabelLinkClickedEventArgs e)
        {
            String expiredDateSy = null;
            StringBuilder sb = new StringBuilder(cbDataSize);
            if (IsAuthenticate && IsRead_Content)
            {
                getExpiredDate(sb, cbDataSize);
                expiredDateSy = sb.ToString();
            }
            else
            {
                //MessageBox.Show
                ("未连接设备").LogThis();
                expiredDateSy = "";
            }
            return expiredDateSy;
            //System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3 = new ToolStripStatusLabel();
            //StringBuilder sb = new StringBuilder(cbDataSize);
            //if (IsAuthenticate && IsRead_Content)
            //{
            //    getEffectedDate(sb, cbDataSize);
            //    string aa = sb.ToString();
            //    getExpiredDate(sb, cbDataSize);
            //    toolStripStatusLabel3.Text = aa + sb.ToString();
            //    labelBegin.Text = aa;
            //    labelEnd.Text = sb.ToString();
            //}
        }
        //照片
        public byte[] photoSy(object sender, EventArgs e)
        {
            if (IsAuthenticate && IsRead_Content)
            {

                GetBmpPhotoExt();
                string PhotoPath = "";
                if (File.Exists("zp.jpg"))
                {
                    PhotoPath = "zp.jpg";
                }
                else if (File.Exists("zp.bmp"))
                {
                    PhotoPath = "zp.bmp";
                }
                return File.ReadAllBytes(PhotoPath);
                //FileStream fs = new FileStream(PhotoPath, FileMode.Open);
                ////把文件读取到字节数组 
                //byte[] data = new byte[fs.Length];
                //fs.Read(data, 0, data.Length);
                //fs.Close();

            }
            else
            {
                //MessageBox.Show
                ("未连接设备").LogThis();

            }
            return null;
        }




    }
}
