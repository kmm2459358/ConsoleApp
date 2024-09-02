//======================================
//      オーディオプレーヤ
//======================================
using System;
using System.Collections.Generic;  // List<T>
using System.Diagnostics;
using System.Media;     // SoundPlayer:

namespace GP2
{
    internal class AudioPlayer
    {
        List<SoundPlayer> m_bgmPlayer;
        List<SoundPlayer> m_sePlayer;
        int m_currentBgm = -1;

        // コンストラクタ
        public AudioPlayer(string[] bgmDataPath, string[] seDataPath)
        {
            try
            {
                string wavPath = searchWavPath();
                m_bgmPlayer = setup(bgmDataPath, wavPath);
                m_sePlayer = setup(seDataPath, wavPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                GP2.Utility.WaitKey();
            }
        }
        protected List<SoundPlayer> setup(string[] dataPath, string wavPath)
        {
            var playerList = new List<SoundPlayer>();
            if (dataPath != null)
            {
                for (int i = 0; i < dataPath.Length; i++)
                {
                    string path = System.IO.Path.Combine(wavPath, dataPath[i]);
                    var player = new SoundPlayer(path);
                    player.LoadAsync();
                    playerList.Add(player);
                }
            }
            return playerList;
        }
        // BGM再生        
        public void PlayBgm(int num)
        {
            StopBgm();
            SoundPlayer? player = GetBgmPlayer(num);
            if (player != null)
            {
                player.PlayLooping();
                m_currentBgm = num;
            }
        }
        // BGM停止
        public void StopBgm()
        {
            SoundPlayer? player = GetBgmPlayer(m_currentBgm);
            if (player != null)
            {
                player.Stop();
                m_currentBgm = -1;
            }
        }
        // BGMプレーヤを取得
        protected SoundPlayer? GetBgmPlayer(int num)
        {
            if (num >= 0 && num < m_bgmPlayer.Count)
            {
                return m_bgmPlayer[num];
            }
            return null;
        }
        // SE再生
        public void PlaySe(int num, bool isLoop = false)
        {
            SoundPlayer? player = GetSePlayer(num);
            if (player != null)
            {
                if (isLoop)
                {
                    player.PlayLooping();
                }
                else
                {
                    player.Play();
                }
            }
        }
        // SE停止
        public void StopSe(int num)
        {
            SoundPlayer? player = GetSePlayer(num);
            if (player != null)
            {
                player.Stop();
            }
        }
        protected SoundPlayer? GetSePlayer(int num)
        {
            if (0 <= num && num < m_sePlayer.Count)
            {
                return m_sePlayer[num];
            }
            return null;
        }
        // *.wavのパス取得.
        protected string searchWavPath()
        {
            // 実行ファイルのパスから、上にさかのぼっていく
            // *.wav があったら、そのディレクトリーを返す
            //
            //  プロジェクト/bin/Debug/.net8.0/*.exe というディレクトリーなので
            //  プロジェクト/に *.wavを置いている
            //
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            while (true)
            {
                var wavfiles = System.IO.Directory.EnumerateFiles(path, "*.wav");
                if (wavfiles.Count() > 0)
                {
                    break;
                }
                path = System.IO.Path.Combine(path, "..");
                path = System.IO.Path.GetFullPath(path);
            }
            return path;
        }
    } // class
} // namespace 