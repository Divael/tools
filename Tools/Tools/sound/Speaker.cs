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
    public static class Speaker
    {
        public static void Speak(string data)
        {
            // speraker.Speak(data);
            System.Common.invoke(() => SpeakAsy(data));
        }
        private static SpeechSynthesizer man = new SpeechSynthesizer();
        /// <summary>
        /// 语音合成
        /// </summary>
        /// <param name="words"></param>
        public static void SpeakAsy(string words)
        {
            Speaker.man.SpeakAsync(words);
        }

        
    }
}
