using System;
using System.Threading;

namespace Tools
{
    /// <summary>
    ///   用法：
    ///         CTimer tick = new CTimer { Interval = TimeSpan.FromSeconds(1) };
    ///         CTimer tick = new CTimer { Interval = 4000.ToMilliseconds() };
    ///             tick.Tick += delegate
    ///             tick.start stop
    /// 
    /// </summary>
    public sealed class CTimer
    {
        private Timer t;

        public TimeSpan Interval { get; set; } = TimeSpan.FromMilliseconds(1000.0);

        public bool IsEnabled { get; private set; }

        public object Tag { get; set; }

        public event Action Tick;

        public CTimer()
        {
            this.t = new Timer(delegate (object p)
            {
                System.Common.invoke(delegate ()
                {
                    Action tick = this.Tick;
                    if (tick == null)
                    {
                        return;
                    }
                    tick();
                }, null);
            }, this, -1, 0);
        }

        public void Start(bool RunNow = false)
        {
            this.IsEnabled = true;
            this.t.Change(RunNow ? TimeSpan.Zero : this.Interval, this.Interval);
        }

        public void Stop()
        {
            this.IsEnabled = false;
            this.t.Change(-1, 0);
        }

        public void Dispose() 
        {
            this.t.Dispose();
        }
    }
}
