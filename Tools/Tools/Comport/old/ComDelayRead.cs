using System;
using System.ComponentModel;
using System.IO.Ports;

namespace Comport
{
    // Token: 0x02000111 RID: 273
    public class ComDelayRead : Com
    {
        // Token: 0x14000028 RID: 40
        // (add) Token: 0x06000B0A RID: 2826 RVA: 0x00024668 File Offset: 0x00022868
        // (remove) Token: 0x06000B0B RID: 2827 RVA: 0x000246A0 File Offset: 0x000228A0
        public new event Action<ComDelayRead, byte[]> DataReceive;

        // Token: 0x06000B0C RID: 2828 RVA: 0x000246D8 File Offset: 0x000228D8
        public ComDelayRead(ComPortParm cpp, int ReceiveDelay = 150) : base(cpp)
        {
            base.DataReceive += this.ComPluss_dataReceive;
            this.bw.DoWork += this.bw_DoWork;
            this.ReceiveDelay = ReceiveDelay;
        }

        // Token: 0x06000B0D RID: 2829 RVA: 0x00024734 File Offset: 0x00022934
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            SerialPort serialPort = e.Argument as SerialPort;
            System.Common.Sleep(this.ReceiveDelay);
            byte[] array = new byte[serialPort.BytesToRead];
            serialPort.Read(array, 0, array.Length);
            Action<ComDelayRead, byte[]> dataReceive = this.DataReceive;
            if (dataReceive == null)
            {
                return;
            }
            dataReceive(this, array);
        }

        // Token: 0x06000B0E RID: 2830 RVA: 0x00024780 File Offset: 0x00022980
        private void ComPluss_dataReceive(object sender, SerialDataReceivedEventArgs e)
        {
            if (!this.bw.IsBusy)
            {
                this.bw.RunWorkerAsync(sender);
            }
        }

        // Token: 0x04000426 RID: 1062
        private BackgroundWorker bw = new BackgroundWorker();

        // Token: 0x04000427 RID: 1063
        public int ReceiveDelay = 150;
    }
}
