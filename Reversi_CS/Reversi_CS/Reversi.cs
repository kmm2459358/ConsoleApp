//======================================
//      リバーシ　
//======================================
using System.Diagnostics;  // Debug
using System.Collections.Generic; // List<T>
using Vector2 = GP2.Vector2;
using Direction8 = GP2.Direction8;
using Utility = GP2.Utility;

namespace Reversi_CS
{
    // ゲームモード
    enum GameMode
    {
        _1P,
        _2P,
        _Watch,
    }
    // ボードのセル
    enum Cell
    {
        None,   // 空白
        Black,  // 黒のコマ
        White,  // 白のコマ
        Out,    // 盤の外
    }
    // 描画ステート
    enum DrawStatus
    {
        InPlay,   // プレイ中
        NoSpace,  // 打てるところがない
        Result,   // リザルト
    }

    internal class Reversi
    {
        public const int BOARD_WID = 8;
        public const int BOARD_HEI = 8;
        protected Cell[,] m_board;
        protected Cell m_turn;
        protected GameMode m_mode;

        // コンストラクター
        public Reversi(GameMode mode)
        {
            m_mode = mode;

            m_board = new Cell[BOARD_HEI, BOARD_WID];
            // 盤を初期化
            for (int y = 0; y < BOARD_HEI; y++)
            {
                for (int x = 0; x < BOARD_WID; x++)
                {
                    SetBoard(x, y, Cell.None);
                }
            }
            SetBoard(3, 4, Cell.Black);
            SetBoard(4, 3, Cell.Black);
            SetBoard(3, 3, Cell.White);
            SetBoard(4, 4, Cell.White);
            m_turn = Cell.Black;
        }
        // プロパティ turn
        public Cell turn
        {
            get { return m_turn; }
        }
        // 次のターンへ
        public void NextTurn()
        {
            m_turn = GetOpponent(m_turn);
        }
        // 指定位置に置けるか?
        public bool CheckCanPlace(Cell myself, Vector2 pos, bool turnover)
        {
            bool canPlace = false;
            // 既に置かれている?
            if (GetBoard(pos.x, pos.y) != Cell.None)
            {
                return false;
            }

            Cell opponent = GetOpponent(myself);
            // ８方向について、
            for (int d = 0; d < (int)Direction8.MAX; d++)
            {
                Vector2 scan = pos;
                Vector2 dir = Vector2.GetDirVector2((Direction8)d);
                // 隣は相手コマ?
                scan += dir;
                if (GetBoard(scan.x, scan.y) != opponent)
                {
                    continue;
                }
                Cell tmp;
                do
                {
                    scan += dir;
                    tmp = GetBoard(scan.x, scan.y);
                } while (tmp == opponent);
                // 自分のコマなら、place可
                if (tmp == myself)
                {
                    canPlace = true;
                    if (turnover)
                    {
                        scan = pos;
                        do
                        {
                            scan += dir;
                            tmp = GetBoard(scan.x, scan.y);
                            if (tmp == opponent)
                            {
                                SetBoard(scan.x, scan.y, myself);
                            }
                        } while (tmp == opponent);
                    }
                }
                if (canPlace && turnover == false)
                {
                    break;
                }
            }
            return canPlace;
        }
        // 盤上のコマ数を数える
        int getDiskCount(Cell myself)
        {
            int count = 0;
            for (int y = 0; y < BOARD_HEI; y++)
            {
                for (int x = 0; x < BOARD_WID; x++)
                {
                    if (GetBoard(x, y) == myself)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public bool CheckCanPlaceAll(Cell myself)
        {
            Vector2 pos = new Vector2(0, 0);
            for (int y = 0; y < BOARD_HEI; y++)
            {
                for (int x = 0; x < BOARD_WID; x++)
                {
                    pos.Set(x, y);
                    if (CheckCanPlace(myself, pos, false))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        // 打てるところをリスティング
        public void ListCanPlaceAll(Cell myself, List<Vector2> list)
        {
            Vector2 pos = new Vector2(0, 0);
            for (int y = 0; y < BOARD_HEI; y++)
            {
                for (int x = 0; x < BOARD_WID; x++)
                {
                    pos.Set(x, y);
                    if (CheckCanPlace(myself, pos, false))
                    {
                        list.Add(pos);
                    }
                }
            }
        }

        protected static string[] diskAA = new string[]
        {
                "・",  // NONE
                "●",  // BLACK
                "〇",  // WHITE
                "■",  // OUT
        };

        // 画面描画
        public void DrawScreen(Vector2 pos, DrawStatus stat)
        {
            Utility.ClearScreen();
            bool isCursor = (stat == DrawStatus.InPlay) && IsHumanPlayer();
            for (int y = 0; y < BOARD_HEI; y++)
            {
                for (int x = 0; x < BOARD_WID; x++)
                {
                    Cell tmp = GetBoard(x, y);
                    Utility.Printf("{0}", diskAA[(int)tmp]);
                }
                if (isCursor && pos.y == y)
                {
                    Utility.Printf("←");
                }
                Utility.Putchar('\n');
            }
            for (int x = 0; x < BOARD_WID; x++)
            {
                if (isCursor && pos.x == x)
                {
                    Utility.Printf("↑");
                }
                else
                {
                    Utility.Printf("　");
                }
            }
            Utility.Putchar('\n');

            switch (stat)
            {
                case DrawStatus.InPlay:
                case DrawStatus.NoSpace:
                    Utility.Printf("{0}のターンです\n", getTurnName(m_turn));
                    if (stat == DrawStatus.NoSpace)
                    {
                        Utility.Printf("打てるところがありません\n");
                    }
                    break;
                case DrawStatus.Result:
                    {
                        int blackCount = getDiskCount(Cell.Black);
                        int whiteCount = getDiskCount(Cell.White);
                        Utility.Printf("{0}{1}-{2}{3}\n\n"
                            , getTurnName(Cell.Black), blackCount
                            , getTurnName(Cell.White), whiteCount
                            );
                        if (blackCount == whiteCount)
                        {
                            Utility.Puts("引き分けです");
                        }
                        else
                        {
                            Cell winner = blackCount >= whiteCount ? Cell.Black : Cell.White;
                            Utility.Printf("{0}の勝ちです\n", getTurnName(winner));
                        }
                    }
                    break;
            }
            Utility.PrintOut();
        }
        // 人間プレーヤのターンか?
        public bool IsHumanPlayer()
        {
            switch (m_mode)
            {
                case GameMode._1P:
                    return m_turn == Cell.Black;
                case GameMode._2P:
                    return true;
                case GameMode._Watch:
                    return false;
            }
            Debug.Assert(false);
            return false;
        }

        // 相手を取得
        public static Cell GetOpponent(Cell cell)
        {

            switch (cell)
            {
                case Cell.Black: return Cell.White;
                case Cell.White: return Cell.Black;
            }
            Debug.Assert(false);
            return Cell.None;
        }

        // ボードのセッター
        public void SetBoard(int x, int y, Cell value)
        {
            if (inBoard(x, y))
            {
                m_board[y, x] = value;
            }
        }
        // ボードのゲッター
        public Cell GetBoard(int x, int y)
        {
            if (inBoard(x, y))
            {
                return m_board[y, x];
            }
            return Cell.Out;
        }
        protected static string getTurnName(Cell turn)
        {
            switch (turn)
            {
                case Cell.Black: return "黒";
                case Cell.White: return "白";
            }
            Debug.Assert(false);
            return "";
        }

        // ボード内か?
        protected static bool inBoard(int x, int y)
        {
            return 0 <= x && x < BOARD_WID
                && 0 <= y && y < BOARD_HEI;
        }
    }
}