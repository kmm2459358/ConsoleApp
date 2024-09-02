//======================================
//      オーディオ
//======================================
using System;
using AudioPlayer = GP2.AudioPlayer;

namespace TurnBattle_CS
{
    enum BgmId
    {
        None = -1,
    }
    enum SeId
    {
        None = -1,
        Cursor,
        Decide,
        Error,
    }

    internal static class Audio
    {
        static AudioPlayer? s_audioPlayer = null;
        // セットアップ
        public static void Setup()
        {
            string[] bgmDataPath = new string[]
            {
                /* nothing */
            };
            string[] seDataPath = new string[]
            {
                "cursor.wav",
                "decide.wav",
                "error.wav"
            };

            s_audioPlayer = new AudioPlayer(bgmDataPath, seDataPath);
        }
        // リムーブ
        public static void Remove()
        {
            s_audioPlayer = null;
        }
        // BGM再生
        public static void PlayBgm(BgmId id)
        {
            if (s_audioPlayer != null)
            {
                s_audioPlayer.PlayBgm((int)id);
            }
        }
        // BGM停止
        public static void StopBgm()
        {
            if (s_audioPlayer != null)
            {
                s_audioPlayer.StopBgm();
            }
        }
        // SE再生
        public static void PlaySe(SeId id, bool isLoop = false)
        {
            if (s_audioPlayer != null)
            {
                s_audioPlayer.PlaySe((int)id, isLoop);
            }
        }
        // SE停止
        public static void StopSe(SeId id)
        {
            if (s_audioPlayer != null)
            {
                s_audioPlayer.StopSe((int)id);
            }
        }
    } // class
} // namespace