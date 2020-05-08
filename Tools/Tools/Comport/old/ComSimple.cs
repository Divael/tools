using System;
using System.IO.Ports;

namespace Comport
{
	// Token: 0x02000116 RID: 278
	public class ComSimple : Com
	{
		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000B2C RID: 2860 RVA: 0x00024C38 File Offset: 0x00022E38
		// (remove) Token: 0x06000B2D RID: 2861 RVA: 0x00024C70 File Offset: 0x00022E70
		public new event Action<ComSimple, byte[]> DataReceive;

		// Token: 0x06000B2E RID: 2862 RVA: 0x00024CA5 File Offset: 0x00022EA5
		public ComSimple(ComPortParm cpp) : base(cpp)
		{
			base.DataReceive += this.ComSimple_DataReceive;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00024CC0 File Offset: 0x00022EC0
		private void ComSimple_DataReceive(object sender, SerialDataReceivedEventArgs e)
		{
			byte[] array = new byte[this.handle.BytesToRead];
			this.handle.Read(array, 0, array.Length);
			if (this.DataReceive != null)
			{
				this.DataReceive(this, array);
			}
		}
	}
}
