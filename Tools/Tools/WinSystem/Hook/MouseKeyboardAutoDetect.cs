// Decompiled with JetBrains decompiler
// Type: System.WinSystem.Hook.MouseKeyboardAutoDetect
// Assembly: Tool, Version=7.0.0.0, Culture=neutral, PublicKeyToken=6a796b320d947832
// MVID: 393BF990-191A-488B-9DBB-114ACC0A58DC
// Assembly location: E:\C#开发程序包\Tools\Tool.dll

using System.Windows.Forms;
using Tools;

namespace System.WinSystem.Hook
{
    public class MouseKeyboardAutoDetect
    {
        private CTimer tick;
        private MouseHook mousehook;
        private DateTime lastopr;

        public int Delay { get; set; }

        public event Action Nobody;

        public MouseKeyboardAutoDetect()
        {
            CTimer ctimer = new CTimer();
            TimeSpan timeSpan = TimeSpan.FromSeconds(1.0);
            ctimer.Interval = timeSpan;
            this.tick = ctimer;
            this.mousehook = new MouseHook();
            this.lastopr = DateTime.Now;
            // ISSUE: reference to a compiler-generated field
            this.Delay = 60000;
            // ISSUE: explicit constructor call
            base.MemberwiseClone();
            this.mousehook.MouseDown += (MouseEventHandler)((param0, param1) => this.reset());
            this.tick.Tick += new Action(this.Tick_Tick);
        }

        private void Tick_Tick()
        {
            // ISSUE: reference to a compiler-generated field
            if (!(this.lastopr.AddMilliseconds((double)this.Delay) < DateTime.Now) || this.Nobody == null)
                return;
            // ISSUE: reference to a compiler-generated field
            this.Nobody();
            this.lastopr = DateTime.Now;
        }

        private void reset()
        {
            this.lastopr = DateTime.Now;
        }

        public void Start()
        {
            this.lastopr = DateTime.Now;
            this.mousehook.Start();
            this.tick.Start(false);
        }

        public void Stop()
        {
            this.mousehook.Stop();
            this.tick.Stop();
        }
    }
}
