//======================================
//      ライフゲーム　パターン
//======================================
using Utility = GP2.Utility;

namespace LifeGame_CS
{
    internal class Pattern
    {
        protected int m_width;        // パターンの横幅
        protected int m_height;       // パターンの縦幅
        protected byte[] m_data;      // パターンデータ
        protected string m_name;      // パターンの名前
        protected bool m_isLoopCells; // フィールドの左右・上下がループしているか?

        // コンストラクター
        public Pattern(int width, int height, byte[] data, string name, bool isLoopCells)
        {
            m_width = width;
            m_height = height;
            m_data = data;
            m_name = name;
            m_isLoopCells = isLoopCells;
        }
        // 横幅ゲッター
        public int width
        {
            get { return m_width; }
        }
        // 縦幅ゲッター
        public int height
        {
            get { return m_height; }
        }
        // データゲッター
        public byte[] data
        {
            get { return m_data; }
        }
        // isLoopCellsゲッター
        public bool isLoopCells
        {
            get { return m_isLoopCells; }
        }

        protected static byte[] _block = new byte[]
        {
            0,0,0,0,
            0,1,1,0,
            0,1,1,0,
            0,0,0,0,
        };
        protected static byte[] _tab = new byte[]
        {
            0,0,0,0,0,
            0,0,1,0,0,
            0,1,0,1,0,
            0,0,1,0,0,
            0,0,0,0,0,
        };
        protected static byte[] _boat = new byte[]
        {
            0,0,0,0,0,
            0,1,1,0,0,
            0,1,0,1,0,
            0,0,1,0,0,
            0,0,0,0,0,
        };
        protected static byte[] _snake = new byte[]
        {
            0,0,0,0,0,0,
            0,1,1,0,1,0,
            0,1,0,1,1,0,
            0,0,0,0,0,0,
        };
        protected static byte[] _ship = new byte[]
        {
            0,0,0,0,0,
            0,1,1,0,0,
            0,1,0,1,0,
            0,0,1,1,0,
            0,0,0,0,0,
        };
        protected static byte[] _aircarrier = new byte[]
        {
            0,0,0,0,0,0,
            0,1,1,0,0,0,
            0,1,0,0,1,0,
            0,0,0,1,1,0,
            0,0,0,0,0,0,
        };
        protected static byte[] _beehive = new byte[]
        {
            0,0,0,0,0,0,
            0,0,1,1,0,0,
            0,1,0,0,1,0,
            0,0,1,1,0,0,
            0,0,0,0,0,0,
        };
        protected static byte[] _barge = new byte[]
        {
            0,0,0,0,0,0,
            0,0,1,0,0,0,
            0,1,0,1,0,0,
            0,0,1,0,1,0,
            0,0,0,1,0,0,
            0,0,0,0,0,0,
        };
        protected static byte[] _pond = new byte[]
        {
            0,0,0,0,0,0,
            0,0,1,1,0,0,
            0,1,0,0,1,0,
            0,1,0,0,1,0,
            0,0,1,1,0,0,
            0,0,0,0,0,0,
        };
        //        protected static byte[] _blinker = new byte[]
        //        { 
        //        };
        //        protected static byte[] _frog = new byte[]
        //        { 
        //        };
        //        protected static byte[] _beecon = new byte[]
        //        { 
        //        };
        //        protected static byte[] _clock = new byte[]
        //        { 
        //        };
        //        protected static byte[] _pulsar = new byte[]
        //        { 
        //        };
        //        protected static byte[] _octagon = new byte[]
        //        { 
        //        };
        //        protected static byte[] _galaxy = new byte[]
        //        { 
        //        };
        //        protected static byte[] _pentadecathlon = new byte[]
        //        { 
        //        };
        //        protected static byte[] _r_pentomono = new byte[]
        //       { 
        //        };
        //        protected static byte[] _diehard = new byte[]
        //        { 
        //        };
        //        protected static byte[] _acorn = new byte[]
        //        { 
        //        };
        //        protected static byte[] _glider = new byte[]
        //        { 
        //        };
        //        protected static byte[] _lightship = new byte[]
        //        { 
        //        };
        //        protected static byte[] _midship = new byte[]
        //        { 
        //        };
        //        protected static byte[] _largeship = new byte[]
        //        { 
        //        };
        //        protected static byte[] _griderGun1 = new byte[]
        //        { 
        //        };
        //        protected static byte[] _griderGun2 = new byte[]
        //        { 
        //        };
        //        protected static byte[] _breeding_10cell = new byte[]
        //        { 
        //        };
        //        protected static byte[] _breeding_5x5 = new byte[]
        //        { 
        //        };
        //        protected static byte[] _breeding_12x2 = new byte[]
        //        { 
        //        };
        //        protected static byte[] _breeding_h1 = new byte[]
        //        { 
        //        };
        //        protected static byte[] _shushupopo = new byte[]
        //        { 
        //        };
        //        protected static byte[] _max = new byte[]
        //        { 
        //        };
        //        protected static byte[] _straight = new byte[]
        //        { 
        //        };

        // パターンテーブル
        protected static Pattern[] patterns = new Pattern[]
        {
            new Pattern(4, 4, _block, "ブロック", true),
            new Pattern(5, 5, _tab, "タブ", true),
            new Pattern(5, 5, _boat, "ボート", true),
            new Pattern(6, 4, _snake, "へび", true),
            new Pattern(5, 5, _ship, "船", true),
            new Pattern(6, 5, _aircarrier, "空母", true),
            new Pattern(6, 5, _beehive, "蜂の巣", true),
            new Pattern(6, 6, _barge,"はしけ", true),
            new Pattern(6, 6, _pond, "池", true),
            	// 振動子
//            new Pattern(5,5,_blinker,"ブリンカー",true),
//            new Pattern(6,6,_frog,"ヒキガエル",true),
//            new Pattern(6,6,_beecon,"ビーコン",true),
//            new Pattern(6,6,_clock,"時計",true),
	        // パルサー
//            new Pattern(17,17,_pulsar,"パルサー",true),
	        // 八角形
//            new Pattern(10,10, _octagon,"八角形",true),
//            new Pattern(15,15, _galaxy,"銀河",true),
//            new Pattern(11,18, _pentadecathlon,"ペンタデカスロン",true),
//            new Pattern(5,5, _r_pentomono,"Rペントミノ",true),
//            new Pattern(10,5, _diehard,"ダイ・ハード" ,true),
//            new Pattern(9,5, _acorn,"どんぐり",true),
//            new Pattern(6,6, _glider,"グライダー",true),
//            new Pattern(9,7, _lightship,"軽量宇宙船",true),
//            new Pattern(10,9, _midship,"中量級宇宙船",true),
//            new Pattern(11,9, _largeship,"重量級宇宙船",true),
//            new Pattern(38,11, _griderGun1,"ゴスパーのグライダー銃",false),  // ループなし
//            new Pattern(35,23, _griderGun2,"シムキンのグライダー銃",false),  // ループなし
//            new Pattern(10,8, _breeding_10cell,"繁殖型10セル",true),
//            new Pattern(7,7, _breeding_5x5,"繁殖型5x5の矩形",true),
//            new Pattern(14,4, _breeding_12x2,"繁殖型12x2の矩形",true),
//            new Pattern(41,3, _breeding_h1,"繁殖型高さ1",true),
//            new Pattern(7,20, _shushupopo,"シュシュポッポ",false),
//            new Pattern(29,29, _max,"マックス",false),
//            new Pattern(32,1, _straight,"直線",false),
        };

        protected static int sel = 0;
        public static Pattern SelectPattern()
        {
            while (true)
            {
                Utility.ClearScreen();
                for (int i = 0; i < patterns.Length; i++)
                {
                    string cur = (sel == i) ? "＞" : "　";
                    Utility.Printf("{0}{1}\n", cur, patterns[i].m_name);
                }
                Utility.PrintOut();
                switch (Utility.GetKey())
                {
                    case ConsoleKey.UpArrow:
                        sel--;
                        if (sel < 0)
                        {
                            sel = patterns.Length - 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        sel++;
                        if ((sel >= patterns.Length))
                        {
                            sel = 0;
                        }
                        break;
                    case ConsoleKey.Enter:
                        return patterns[sel];
                    case ConsoleKey.Escape:
                        return null;
                }
            }
        }
    } // class
} // namespace 