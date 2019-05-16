using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRT591_M001_MR01
{
    public class CRT591MR01_Controller
    {
        /// <summary>
        /// 串口句柄
        /// </summary>
        public IntPtr comHandler;

        /// <summary>
        /// 读卡器数量
        /// </summary>
        public int ReaderCount;

        /// <summary>
        /// 从机地址
        /// </summary>
        public byte TxAddr = 0x00;


        /// <summary>
        /// 传输的信息输出
        /// </summary>
        public event Action<Msg> msg;

        public CRT591MR01_Controller() {

        }

        /// <summary>
        /// COM2
        /// </summary>
        /// <param name="com"></param>
        public bool OpenCom(string com) {
            var handle = DLL_CRT591M001_MR01.CRT591MROpen(com);
            if ((int)handle == 0)
            {
                return false;
            }
            this.comHandler = handle;
            return true;
        }

        public void SetTxAddr(byte v)
        {
            TxAddr = v;
        }

        /// <summary>
        /// COM2
        /// </summary>
        /// <param name="com"></param>
        public bool OpenCom(string com, string route)
        {
            var handle = DLL_CRT591M001_MR01.CRT591MROpenWithBaut(com, uint.Parse(route));
            if (handle.Equals(0))
            {
                return false;
            }
            this.comHandler = handle;
            return true;
        }

        public bool CloseCom() {
            int a = DLL_CRT591M001_MR01.CRT591MRClose(comHandler);
            return a == 0 ? true : false;
        }

        /// <summary>
        /// 处理发送的数据包
        /// </summary>
        public void DealData() {

        }

        /// <summary>
        /// 获取所有连接的读卡器状态，先初始化
        /// </summary>
        /// <returns></returns>
        public List<ReaderCardandState> GetSCardReaderStatus() {
            List<ReaderCardandState> list = new List<ReaderCardandState>();
            for (int i = 0; i < ReaderCount; i++)
            {
                ReaderCardandState reader = new ReaderCardandState();
                DLL_CRT591M001_MR01.GetSCardReaderStatus(i, ref reader.ReaderName,ref reader.ReaderNameLen,ref reader.CardState,ref reader.CardProtocol,reader.ATR_DATA,ref reader.ATR_DataLen);
                list.Add(reader);
            }
            return list;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <returns></returns>
        internal bool ReleaseContext()
        {
            int a = DLL_CRT591M001_MR01.ReleaseContext();
            return a == 0 ? true : false;
        }

        /// <summary>
        /// 读卡器复位
        /// </summary>
        /// <returns></returns>
        public bool Reset(enum_InitPm _InitPm)
        {

            RxRes rxRes = new RxRes();
            byte[] vs = "00".ToHex();
            int res  = DLL_CRT591M001_MR01.RS232_ExeCommand(comHandler, TxAddr,(byte)0x30, (byte)_InitPm, 0x00, vs, ref rxRes.RxReplyType,ref rxRes.RxStCode0,ref  rxRes.RxStCode1,ref  rxRes.RxStCode2,ref rxRes.RxDataLen, rxRes.RxData);
            rxRes.ToString().logThis();
            if (res.Equals(0))
            {
                if ((enum_RxReplyType)rxRes.RxReplyType ==enum_RxReplyType.执行成功)
                {
                    return true;
                }
            }
            return false;
            
        }

        /// <summary>
        /// 读卡器复位
        /// </summary>
        /// <returns></returns>
        public bool PollWithReader()
        {

            RxRes rxRes = new RxRes();
            byte[] vs = "00".ToHex();
            int res = DLL_CRT591M001_MR01.RS232_ExeCommand(comHandler, TxAddr, 0x31, 0x31, 0x00, vs, ref rxRes.RxReplyType, ref rxRes.RxStCode0, ref rxRes.RxStCode1, ref rxRes.RxStCode2, ref rxRes.RxDataLen, rxRes.RxData);
            rxRes.ToString().logThis();
            if (res.Equals(0))
            {
                if ((enum_RxReplyType)rxRes.RxReplyType == enum_RxReplyType.执行成功)
                {
                    return true;
                }
            }
            return false;

        }

        public bool MoveCard(enum_MoveCardPm pm)
        {
            RxRes rxRes = new RxRes();
            byte[] vs = "00".ToHex();
            int res = DLL_CRT591M001_MR01.RS232_ExeCommand(comHandler, TxAddr, 0x32, (byte)pm, 0x00, vs, ref rxRes.RxReplyType, ref rxRes.RxStCode0, ref rxRes.RxStCode1, ref rxRes.RxStCode2, ref rxRes.RxDataLen, rxRes.RxData);
            rxRes.ToString().logThis();
            if (res.Equals(0))
            {
                if ((enum_RxReplyType)rxRes.RxReplyType == enum_RxReplyType.执行成功)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 读卡器复位
        /// </summary>
        /// <returns></returns>
        public bool Poll()
        {

            RxRes rxRes = new RxRes();
            byte[] vs = "00".ToHex();
            int res = DLL_CRT591M001_MR01.RS232_ExeCommand(comHandler, TxAddr, 0x31, 0x30, 0x00, vs, ref rxRes.RxReplyType, ref rxRes.RxStCode0, ref rxRes.RxStCode1, ref rxRes.RxStCode2, ref rxRes.RxDataLen, rxRes.RxData);
            rxRes.ToString().logThis();
            if (res.Equals(0))
            {
                if ((enum_RxReplyType)rxRes.RxReplyType == enum_RxReplyType.执行成功)
                {
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// 初始化读卡器环境
        /// </summary>
        public bool InitializeContext() {
            int count = 0;
            int res = DLL_CRT591M001_MR01.InitializeContext(ref count);
            if (res.Equals(0))
            {
                ReaderCount = count;
                return true;
            }
            else
            {
                return false;
            }
        }

        internal byte[] ReadRFCard()
        {
            RxRes rxRes = new RxRes(15);
            byte[] vs = "41 42".ToHex();
            int res = DLL_CRT591M001_MR01.RS232_ExeCommand(comHandler, TxAddr, 0x60, 0x30, 0x02, vs, ref rxRes.RxReplyType, ref rxRes.RxStCode0, ref rxRes.RxStCode1, ref rxRes.RxStCode2, ref rxRes.RxDataLen, rxRes.RxData);
            rxRes.ToString().logThis();
            if (res.Equals(0))
            {
                if ((enum_RxReplyType)rxRes.RxReplyType == enum_RxReplyType.执行成功)
                {
                    byte[] bt = null;
                    switch ((enum_RF_Rtype)rxRes.RxData[0])
                    {
                        case enum_RF_Rtype.MifareOneCard:
                            int a = rxRes.RxData[3];
                            bt = new byte[a];
                            for (int i = 0; i < a; i++)
                            {
                                bt[i] = rxRes.RxData[4 + i];
                            }
                            break;
                        case enum_RF_Rtype.TypeA:
                            break;
                        case enum_RF_Rtype.TypeB:
                            break;
                        default:
                            break;
                    }
                    return bt;
                }
            }
            return null;
        }

        /// <summary>
        /// 断开只能读卡器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool DisconnectSCardReader(int index) {
            int res =DLL_CRT591M001_MR01.DisconnectSCardReader(index);
            if (res.Equals(0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    public enum Msg
    {
        未执行 = 99,
        串口关闭 = 0,
        串口打开 = 1,


    }

    public class ReaderCardandState {

        public ushort ReaderName;
        public int ReaderNameLen;
        public byte CardState;
        public byte CardProtocol;
        public byte[] ATR_DATA;
        public int ATR_DataLen;

        public ReaderCardandState()
        {
            ReaderName = 0;
            ReaderNameLen = 0;
            CardState = 0;
            CardProtocol = 0;
            ATR_DATA = new byte[10];
            ATR_DataLen = 0;
        }

        public override string ToString()
        {
            string bytes ="";
            foreach (var item in ATR_DATA)
            {
                bytes += item + " ";
            }
            return "读卡器名称 = "+ReaderName+ " 读卡器名称长度="+ ReaderNameLen+" 卡状态=" + CardState+ " 读卡器中卡的使用标准=" + CardProtocol+ " 读卡器中卡的 ATR 数据=" + bytes+ " 读卡器中卡的 ATR 数据长度=" + ATR_DataLen;
        }
    }

    public class RxRes
    {

        /// <summary>
        /// 返回的应答类型  0x50 : 执行成功 0x4E : 执行失败 0x10 : 下位机取消通讯(NAK ) 0x20 : 通讯错误 0x30 : 上位机取消命令(DLE, EOT) </param>
        /// </summary>
        public byte RxReplyType ;
        public byte RxStCode0 ;
        public byte RxStCode1;
        public byte RxStCode2;
        public int RxDataLen;
        public byte[] RxData;

        public RxRes(int count = 10)
        {
            RxReplyType = 0;
            RxStCode0 = 0;
            RxStCode1 = 0;
            RxStCode2 = 0;
            RxDataLen = 0;
            RxData = new byte[count];
        }

        public override string ToString()
        {
            string bytes = "";
            foreach (var item in RxData)
            {
                bytes += item + " ";
            }
            return "返回的应答类型 = " + (enum_RxReplyType)RxReplyType + " RxStCode0=" + RxStCode0 + " RxStCode1=" + RxStCode1 + " RxStCode2=" + RxStCode2 + " RxDataLen=" + bytes + " RxData=" + bytes;
        }
    }

    public enum enum_RxReplyType {
        执行成功 = 0x50,
        执行失败 = 0x4E,
        下位机取消通讯Nak = 0x10,
        通讯错误 = 0x20,
        上位机取消命令DLE_EOT = 0x30
    }

    public enum enum_InitPm
    {
        移动卡片到卡口 = 0x30,
        回收卡片到回收箱中 = 0x31,
        不移动卡 = 0x33,
        同Pm30H启动回收卡计数 = 0x34,
        同Pm31H启动回收卡计数 = 0x35,
        同Pm33H启动回收卡计数 = 0x37,
        //卡机软件版本信息Rev_type
    }

    public enum enum_MoveCardPm
    {
        移动卡片到持卡位 = 0x30,
        移动卡片到IC卡位 = 0x31,
        移动卡片到RF卡位 = 0x32,
        移动卡片到回收盒 = 0x33,
        移动卡片到出卡口 = 0x39,
    }

    public enum enum_RF_Rtype
    {
        TypeA = 0x41,
        TypeB = 0x42,
        MifareOneCard = 0x4D
    }
    
}
