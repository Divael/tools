using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace Tools.sound
{
    /// <summary>
    /// 语音
    /// </summary>
    public class Speaker
    {
        private SpeechSynthesizer man;
        private static Speaker speaker;
        private static object obj = new object();
        private Speaker() {
            man = new SpeechSynthesizer();
        }

        public static Speaker getInstance() {
            if (speaker == null)
            {
                lock (obj)
                {
                    if (speaker == null)
                    {
                        speaker = new Speaker();
                    }
                }
            }
            return speaker;
        }
        /// <summary>
        /// 用委托的方式播放声音，可以让声音说完
        /// </summary>
        /// <param name="data"></param>
        private void Speak(string data)
        {
            // speraker.Speak(data);
            System.Common.invoke(() => SpeakAsy(data));
        }

        /// <summary>
        /// 语音合成,直接播放声音，会覆盖
        /// </summary>
        /// <param name="words"></param>
        private void SpeakAsy(string words)
        {
            man.SpeakAsync(words);
        }

        
    }
}
