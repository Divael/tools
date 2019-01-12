using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
//using ThoughtWorks.QRCode.Codec;

namespace Tools
{
    /// <summary>
    /// <para>　二维码转换</para>
    /// 如果要用到请nuget导入ThoughtWorks.QRCode包，然后复制以下方法
    /// <para>　-------------------------------------------------</para>
    /// <para>　 GetQRImage（string urlQRCode）： url转换二维码图片image</para>
    /// </summary>
    public class QRCodeConvert
    {
        /// <summary>
        /// url转换二维码图片image
        /// </summary>
        /// <param name="urlQRCode">url</param>
        /// <returns></returns>
        public static Image GetQRImage(string urlQRCode)
        {
            //QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //qrCodeEncoder.QRCodeScale = 4;
            //qrCodeEncoder.QRCodeVersion = 8;
            //qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //System.Drawing.Image image = qrCodeEncoder.Encode(urlQRCode);
            System.Drawing.Image image = null;
            return image;
        }
    }
}
