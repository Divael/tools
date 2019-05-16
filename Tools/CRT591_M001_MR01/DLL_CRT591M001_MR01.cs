using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CRT591_M001_MR01
{
    public class DLL_CRT591M001_MR01
    {
        /// <summary>
        /// 打开串口 默认9600
        /// </summary>
        /// <param name="port">串口号COM1</param>
        /// <returns>0打开失败</returns>
        [DllImport("CRT_591_M001.dll",EntryPoint = "CRT591MROpen")]
        public static extern IntPtr CRT591MROpen(string port);

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="port">串口号COM1</param>
        /// <param name="baudOption">波特率 38400</param>
        /// <returns></returns>
        [DllImport("CRT_591_M001.dll")]
        public static extern IntPtr CRT591MROpenWithBaut(string port, uint baudOption);

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <param name="ComHandle">打开串口获得的句柄</param>
        /// <returns>0成功</returns>
        [DllImport("CRT_591_M001.dll")]
        public static extern int CRT591MRClose(IntPtr ComHandle);

        /// <summary>
        /// 执行命令并返回执行结果
        /// 若 RxReplyType 值为 0x50 时，则 RxStCode0，RxStCode1，RxStCode2 为卡机状态码
        /// 若 RxReplyType 值为 0x4E 时，则 RxStCode1，RxStCode2 为失败原因码，RxStCode0 值无意义
        /// </summary>
        /// <param name="ComHandle">串口句柄</param>
        /// <param name="TxAddr">:卡机地址，取值范围 0x00~0x0f </param>
        /// <param name="TxCmCode">命令代码</param>
        /// <param name="TxPmCode">:参数代码</param>
        /// <param name="TxDataLen">命令附加的数据包长度</param>
        /// <param name="TxData">命令附加的数据包</param>
        /// <param name="RxReplyType">返回的应答类型  0x50 : 执行成功 0x4E : 执行失败 0x10 : 下位机取消通讯(NAK ) 0x20 : 通讯错误 0x30 : 上位机取消命令(DLE, EOT) </param>
        /// <param name="RxStCode0">返回的状态代码0 </param>
        /// <param name="RxStCode1">返回的状态代码1</param>
        /// <param name="RxStCode2">返回的状态代码2</param>
        /// <param name="RxDataLen">返回的数据包长度</param>
        /// <param name="RxData">返回的数据包</param>
        /// <returns>0成功</returns>
        [DllImport("CRT_591_M001.dll")]
        public static extern int RS232_ExeCommand(IntPtr ComHandle,byte TxAddr,byte TxCmCode,byte TxPmCode,int TxDataLen, byte[] TxData,
           ref byte RxReplyType, ref byte RxStCode0, ref byte RxStCode1, ref byte RxStCode2,ref int RxDataLen,byte[] RxData);



        /// <summary>
        /// 初始化智能读卡器环境，并返回电脑中有多少个本动态库支持的智能读卡器(列表读卡器个数) 
        /// </summary>
        /// <param name="ReaderCount">本动态库支持的智能读卡器个数</param>
        /// <returns>=0 成功</returns>
        [DllImport("CRT_591_M001.dll")]
        public static extern int InitializeContext(ref int ReaderCount);

        /// <summary>
        /// 获取智能读卡器的名称和卡状态
        /// </summary>
        /// <param name="ReaderSort">设定操作的智能读卡器的序号，0 为第 1 个，1 为第 2 个 ……. </param>
        /// <param name="ReaderName">获取的读卡器名称长度字节数</param>
        /// <param name="ReaderNameLen">ReaderName：获取的读卡器名称</param>
        /// <param name="CardState">CardState：读卡器中卡的状态</param>
        /// <param name="CardProtocol">CardProtocol：读卡器中卡的使用标准</param>
        /// <param name="ATR_Data">读卡器中卡的 ATR 数据</param>
        /// <param name="ATR_DataLen">ATR_DataLen：读卡器中卡的 ATR 数据长度</param>
        /// <returns>=0 成功</returns>
        [DllImport("CRT_591_M001.dll")]//[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 数组大小)] public string 数组名
        public static extern int GetSCardReaderStatus(int ReaderSort,ref ushort ReaderName,ref int ReaderNameLen,ref byte CardState,
            ref byte CardProtocol,byte[] ATR_Data,ref int ATR_DataLen);

        /// <summary>
        /// 使用智能读卡器进行 APDU 传输
        /// </summary>
        /// <param name="ReaderSort">ReaderSort：设定操作的智能读卡器的序号，0 为第 1 个，1 为第 2 个 ……. </param>
        /// <param name="TxDataLen">TxDataLen：发送的 APDU 数据长度</param>
        /// <param name="TxData">TxData：发送的 APDU 数据</param>
        /// <param name="RxDataLen">RxDataLen：返回的 RPDU 数据长度</param>
        /// <param name="RxData">RxData：返回的 RPDU 数据</param>
        /// <returns> =0 成功</returns>
        [DllImport("CRT_591_M001.dll")]
        public static extern int APDU_Transmit( int ReaderSort,int TxDataLen,byte[] TxData,ref int RxDataLen,byte[] RxData);

        /// <summary>
        /// 断开智能读卡器
        /// </summary>
        /// <param name="ReaderCount">ReaderSort：设定操作的智能读卡器的序号，0 为第 1 个，1 为第 2 个 ……. </param>
        /// <returns> =0 成功</returns>
        [DllImport("CRT_591_M001.dll")]
        public static extern int DisconnectSCardReader(int ReaderCount);

        /// <summary>
        /// 释放智能读卡器环境资源
        /// </summary>
        /// <returns> =0 成功</returns>
        [DllImport("CRT_591_M001.dll")]
        public static extern int ReleaseContext();

    }
}
