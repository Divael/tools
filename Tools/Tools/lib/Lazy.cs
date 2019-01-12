using System;
using System.Threading;

namespace System
{
    // Token: 0x02000011 RID: 17
    internal abstract class Lazy
    {
        // Token: 0x060000D2 RID: 210 RVA: 0x00004EAA File Offset: 0x000030AA
        public static void Sleep(int MillenSenconds)
        {
            Lazy.sleep.sleep(MillenSenconds);
        }

        // Token: 0x060000D3 RID: 211 RVA: 0x00004EB7 File Offset: 0x000030B7
        public static void Sleep(TimeSpan ts)
        {
            Lazy.sleep.sleep(ts);
        }

        // Token: 0x04000025 RID: 37
        private static Laman sleep = new Laman();
    }

    // Token: 0x02000012 RID: 18
    internal sealed class Laman : Lazy
    {
        // Token: 0x060000D6 RID: 214 RVA: 0x00004ED0 File Offset: 0x000030D0
        public Laman()
        {
            Common.SetSleepTick();
        }

        // Token: 0x060000D7 RID: 215 RVA: 0x00004EDD File Offset: 0x000030DD
        public void sleep(TimeSpan ts)
        {
            Thread.Sleep(ts);
        }

        // Token: 0x060000D8 RID: 216 RVA: 0x00004EE5 File Offset: 0x000030E5
        public void sleep(int millenseconds)
        {
            Thread.Sleep(millenseconds);
        }

        // Token: 0x060000D9 RID: 217 RVA: 0x00004EF0 File Offset: 0x000030F0
        ~Laman()
        {
            Common.AbortSleepTick();
        }
    }
}
