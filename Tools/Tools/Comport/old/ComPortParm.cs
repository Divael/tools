using System;
using System.ComponentModel;
using System.IO.Ports;

namespace Comport
{
    /// <summary 
    ///  ComPortParm.Parse :字符串转串口参数  var cpp = ComPortParm.Parse("5,9600,N,8,1");
    /// </summary>
    [Serializable]
    public class ComPortParm
    {
        public bool DiscardNull { get; set; }

        public bool DtrEnable { get; set; }

        public Handshake Handshake { get; set; }

        public bool RtsEnable { get; set; }

        [Category("串口参数")]
        public int 数据位
        {
            get
            {
                return this.databit;
            }
            set
            {
                this.databit = value;
            }
        }

        // Token: 0x170000F4 RID: 244
        // (get) Token: 0x06000B1E RID: 2846 RVA: 0x000248E6 File Offset: 0x00022AE6
        // (set) Token: 0x06000B1F RID: 2847 RVA: 0x000248EE File Offset: 0x00022AEE
        [Category("串口参数")]
        public StopBits 停止位
        {
            get
            {
                return this.sb;
            }
            set
            {
                this.sb = value;
            }
        }

        // Token: 0x170000F5 RID: 245
        // (get) Token: 0x06000B20 RID: 2848 RVA: 0x000248F7 File Offset: 0x00022AF7
        // (set) Token: 0x06000B21 RID: 2849 RVA: 0x000248FF File Offset: 0x00022AFF
        [Category("串口参数")]
        public Parity 校验位
        {
            get
            {
                return this.p;
            }
            set
            {
                this.p = value;
            }
        }

        // Token: 0x170000F6 RID: 246
        // (get) Token: 0x06000B22 RID: 2850 RVA: 0x00024908 File Offset: 0x00022B08
        // (set) Token: 0x06000B23 RID: 2851 RVA: 0x00024910 File Offset: 0x00022B10
        [Category("串口参数")]
        [TypeConverter(typeof(ComPortParm.BaruList))]
        public int 波特率
        {
            get
            {
                return this.baudRat;
            }
            set
            {
                this.baudRat = value;
            }
        }

        // Token: 0x170000F7 RID: 247
        // (get) Token: 0x06000B24 RID: 2852 RVA: 0x00024919 File Offset: 0x00022B19
        // (set) Token: 0x06000B25 RID: 2853 RVA: 0x00024921 File Offset: 0x00022B21
        [Category("串口参数")]
        [TypeConverter(typeof(ComPortParm.ComList))]
        public string 串口名称
        {
            get
            {
                return this.portName;
            }
            set
            {
                this.portName = value;
            }
        }

        // Token: 0x06000B26 RID: 2854 RVA: 0x0002492C File Offset: 0x00022B2C
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4}", new object[]
            {
                this.串口名称,
                this.波特率,
                this.校验位,
                this.数据位,
                this.停止位
            });
        }

        /// <summary>
        /// ComPortParm.Parse :字符串转串口参数  var cpp = ComPortParm.Parse("5,9600,N,8,1");
        /// </summary>
        /// <param name="parm">("5,9600,N,8,1")</param>
        /// <returns></returns>
		public static ComPortParm Parse(string parm)
        {
            ComPortParm cpp = new ComPortParm();
            string[] array = parm.Trim().ToUpper().Split(new char[]
            {
                ','
            });
            if (array.Length != 0)
            {
                cpp.串口名称 = "COM" + array[0];
            }
            if (array.Length > 1)
            {
                cpp.波特率 = int.Parse(array[1]);
            }
            string a = array[2];
            if (!(a == "N"))
            {
                if (!(a == "E"))
                {
                    if (!(a == "O"))
                    {
                        if (!(a == "M"))
                        {
                            if (a == "S")
                            {
                                cpp.校验位 = Parity.Space;
                            }
                        }
                        else
                        {
                            cpp.校验位 = Parity.Mark;
                        }
                    }
                    else
                    {
                        cpp.校验位 = Parity.Odd;
                    }
                }
                else
                {
                    cpp.校验位 = Parity.Even;
                }
            }
            else
            {
                cpp.校验位 = Parity.None;
            }
            if (array.Length > 3)
            {
                cpp.数据位 = int.Parse(array[3]);
            }
            a = array[4];
            if (!(a == "1"))
            {
                if (!(a == "2"))
                {
                    if (!(a == "1.5"))
                    {
                        if (a == "N")
                        {
                            cpp.停止位 = StopBits.None;
                        }
                    }
                    else
                    {
                        cpp.停止位 = StopBits.OnePointFive;
                    }
                }
                else
                {
                    cpp.停止位 = StopBits.Two;
                }
            }
            else
            {
                cpp.停止位 = StopBits.One;
            }
            System.Common.ForEach(array, delegate (string m)
             {
                 string a2 = m.Trim().ToLower();
                 if (a2 == "r")
                 {
                     cpp.RtsEnable = true;
                     return;
                 }
                 if (a2 == "d")
                 {
                     cpp.DtrEnable = true;
                     return;
                 }
                 if (a2 == "hno")
                 {
                     cpp.Handshake = Handshake.None;
                     return;
                 }
                 if (a2 == "hxx")
                 {
                     cpp.Handshake = Handshake.XOnXOff;
                     return;
                 }
                 if (a2 == "hrs")
                 {
                     cpp.Handshake = Handshake.RequestToSend;
                     return;
                 }
                 if (!(a2 == "hrx"))
                 {
                     return;
                 }
                 cpp.Handshake = Handshake.RequestToSendXOnXOff;
             });
            return cpp;
        }

        // Token: 0x170000F8 RID: 248
        // (get) Token: 0x06000B28 RID: 2856 RVA: 0x00024B27 File Offset: 0x00022D27
        [Browsable(false)]
        public ComPortParm clone
        {
            get
            {
                return base.MemberwiseClone() as ComPortParm;
            }
        }

        // Token: 0x04000429 RID: 1065
        private string portName = "COM1";

        // Token: 0x0400042A RID: 1066
        private int baudRat = 4800;

        // Token: 0x0400042B RID: 1067
        private Parity p;

        // Token: 0x0400042C RID: 1068
        private StopBits sb = StopBits.One;

        // Token: 0x0400042D RID: 1069
        private int databit = 8;

        // Token: 0x0200034D RID: 845
        public class ComList : StringConverter
        {
            // Token: 0x06001ABA RID: 6842 RVA: 0x000022EA File Offset: 0x000004EA
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            // Token: 0x06001ABB RID: 6843 RVA: 0x0006D871 File Offset: 0x0006BA71
            public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new TypeConverter.StandardValuesCollection(SerialPort.GetPortNames());
            }
        }

        // Token: 0x0200034E RID: 846
        public class BaruList : Int32Converter
        {
            // Token: 0x06001ABD RID: 6845 RVA: 0x000022EA File Offset: 0x000004EA
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            // Token: 0x06001ABE RID: 6846 RVA: 0x0006D885 File Offset: 0x0006BA85
            public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new TypeConverter.StandardValuesCollection(new int[]
                {
                    1200,
                    2400,
                    4800,
                    9600,
                    19200,
                    115200
                });
            }
        }
    }
}
