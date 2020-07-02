﻿using System;
using System.Media;
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
            Common.invoke(() =>
            {
                WavPlayer(wavfile);
            });
        }

        private static Task task = null;

        /// <summary>
        /// 播放音频文件wav格式的,播完完整的一段
        /// </summary>
        /// <param name="wavfile">wav格式地址</param>
        public static void WavPlayerTask(string wavfile)
        {
            if (task != null)
            {
                if (!task.IsCompleted)
                {
                    return;
                }
            }
            task = Task.Run(() =>
            {
                WavPlayerInvoke(wavfile);
            });
        }
    }
}
