using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Tools.sound
{
    public static class Wav
    {
        /// <summary>
        /// 播放音频文件wav格式的
        /// </summary>
        /// <param name="wavfile">wav格式地址</param>
        public static void WavPlayer(string wavfile)
        {
            using (SoundPlayer sound = new SoundPlayer(wavfile))
            {
                sound.Play();
            }
        }

        /// <summary>
        /// 播放音频文件wav格式的
        /// </summary>
        /// <param name="wavfile">wav格式地址</param>
        public static void WavPlayerInvoke(string wavfile)
        {
            Common.invoke(()=> {
                WavPlayer(wavfile);
            });
        }
    }
}
