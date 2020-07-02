using System;
using System.Collections.Generic;

namespace Comport
{
    public class ComResponse : Com
    {
        public ComResponse(ComPortParm cpp, int delay = 100) : base(cpp)
        {
            this.responsedelay = delay;
        }

        public byte[] SendResponse(byte[] data)
        {
            List<byte> obj = this.cache;
            byte[] result;
            lock (obj)
            {
                this.cache.Clear();
                if (base.Send(data))
                {
                    System.Common.Sleep(this.responsedelay);
                    int bytesToRead;
                    while ((bytesToRead = this.handle.BytesToRead) > 0)
                    {
                        byte[] array = new byte[bytesToRead];
                        this.handle.Read(array, 0, bytesToRead);
                        this.cache.AddRange(array);
                        System.Common.Sleep(Math.Max(50, this.responsedelay / 2));
                    }
                }
                result = this.cache.ToArray();
            }
            return result;
        }

        // Token: 0x04000432 RID: 1074
        public int responsedelay = 100;

        // Token: 0x04000433 RID: 1075
        private List<byte> cache = new List<byte>();
    }
}
