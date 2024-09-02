//======================================
//      落ち物バズル ステージ
//======================================
using System;
using BlockShape = FallingBlockPuzzle_CS.BlockShape;
using FallBlock = FallingBlockPuzzle_CS.FallBlock;
using Utility = GP2.Utility;

namespace FallingBlockPuzzle_CS
{
    enum Block
    {
        None,  // プロックなし
        Wall,  // 壁
        Fix,   // 定着ブロック
        Fall,  // 落下ブロック
        Max,
    }

    internal class Stage
    {
        const int FIELD_WIDTH = 12;
        const int FIELD_HEIGHT = 18;

        protected Block[,] m_field;       // フィールド
        protected FallBlock m_fallBlock; // 落ちブロック
        protected bool m_isGameOver;     // ゲームオーバ?
        protected Block[,] m_screen;     // 描画要ワーク

        // フォールブロックのプロパティ
        public FallBlock FallBlock
        {
            get { return m_fallBlock; }
            set { m_fallBlock = value; }
        }
        // ゲームオーバフラグのプロパティ
        public bool IsGameOver
        {
            get { return m_isGameOver; }
        }

        const Block W = Block.Wall;
        const Block _ = Block.None;
        static Block[,] s_defaultField = new Block[FIELD_HEIGHT, FIELD_WIDTH]
        {
            {W,_,_,_,_,_,_,_,_,_,_,W}, // 0
            {W,_,_,_,_,_,_,_,_,_,_,W}, // 1
            {W,_,_,_,_,_,_,_,_,_,_,W}, // 2
            {W,_,_,_,_,_,_,_,_,_,_,W}, // 3
            {W,_,_,_,_,_,_,_,_,_,_,W}, // 4
            {W,_,_,_,_,_,_,_,_,_,_,W}, // 5
            {W,_,_,_,_,_,_,_,_,_,_,W}, // 6
            {W,_,_,_,_,_,_,_,_,_,_,W}, // 7
            {W,_,_,_,_,_,_,_,_,_,_,W}, // 8
            {W,_,_,_,_,_,_,_,_,_,_,W}, // 9
            {W,_,_,_,_,_,_,_,_,_,_,W}, // 10
            {W,_,_,_,_,_,_,_,_,_,_,W}, // 11
            {W,W,_,_,_,_,_,_,_,_,W,W}, // 12
            {W,W,_,_,_,_,_,_,_,_,W,W}, // 13
            {W,W,_,_,_,_,_,_,_,_,W,W}, // 14
            {W,W,W,_,_,_,_,_,_,W,W,W}, // 15
            {W,W,W,W,_,_,_,_,W,W,W,W}, // 16
            {W,W,W,W,W,W,W,W,W,W,W,W}, // 17
        };

        // コンストラクタ
        public Stage()
        {
            m_field = s_defaultField.Clone() as Block[,];
            Array.Copy(s_defaultField, m_field, s_defaultField.GetLength(0));
            m_screen = new Block[FIELD_HEIGHT, FIELD_WIDTH];
            m_fallBlock = FallBlock.CreateRandomFallBlock();
            m_isGameOver = false;
        }

        // フィルードセッター
        public void SetField(int x, int y, Block value)
        {
            if (isInField(x, y))
            {
                m_field[y, x] = value;
            }
        }
        // フィールドゲッター
        public Block GetField(int x, int y)
        {
            if (isInField(x, y))
            {
                return m_field[y, x];
            }
            return Block.Wall;
        }
        // 落ちブロックがフィールド内で衝突か?
        public bool BlockIntersectField(FallBlock fallBlock)
        {
            BlockShape shape = fallBlock.Shape;
            for (int y = 0; y < shape.Size; y++)
            {
                int fieldY = fallBlock.Y + y;
                for (int x = 0; x < shape.Size; x++)
                {
                    if (shape.GetShapePattern(x, y))
                    {
                        int fieldX = fallBlock.X + x;
                        if (GetField(fieldX, fieldY) != Block.None)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        // 揃ったとき行を消して、上から詰める
        public void EraseLine()
        {
            for (int y = 0; y < FIELD_HEIGHT; y++)
            {
                bool completed = true;
                // 行が揃ったか?
                for (int x = 0; x < FIELD_WIDTH; x++)
                {
                    if (GetField(x, y) == Block.None)
                    {
                        completed = false;
                        break;
                    }
                }
                if (completed)
                {
                    // 行を消す
                    for (int x = 0; x < FIELD_WIDTH; x++)
                    {
                        if (GetField(x, y) == Block.Fix)
                        {
                            SetField(x, y, Block.None);
                        }
                    }
                    // 上から詰める
                    for (int x = 0; x < FIELD_WIDTH; x++)
                    {
                        for (int yy = y; yy >= 0; yy--)
                        {
                            if (GetField(x, yy) == Block.Wall)
                            {
                                break;
                            }
                            if (yy == 0)
                            {
                                // 最上段なら、空白を
                                SetField(x, yy, Block.None);
                            }
                            else
                            {
                                // 最上段でなければ、1つ上を
                                Block blk = GetField(x, yy - 1);
                                if (blk != Block.Wall)
                                {
                                    SetField(x, yy, blk);
                                }
                                else
                                {
                                    SetField(x, yy, Block.None);
                                }
                            }
                        }
                    }
                }
            }
        }
        static string[] s_blockAA = new string[]{
                "　",
                "＋",
                "◆",
                "◇",
            };
        // 画面描画
        public void DrawScreen()
        {
            // m_field => m_screen コピー
            // _.Length=_.GetLength(0)*_.GetLength(1)
            Array.Copy(m_field, m_screen, m_field.Length);
            writeFallBlockToField(m_screen, m_fallBlock, Block.Fall);
            // 描画
            Utility.ClearScreen();
            for (int y = 0; y < FIELD_HEIGHT; y++)
            {
                for (int x = 0; x < FIELD_WIDTH; x++)
                {
                    Block blk = m_screen[y, x];
                    Utility.Printf(s_blockAA[(int)blk]);
                }
                Utility.Putchar('\n');
            }
            Utility.PrintOut();
        }
        // ブロックを1つ落下させる
        public void MoveDownFallBlock()
        {
            FallBlock tmp = new FallBlock(m_fallBlock);
            tmp.Move(0, 1);
            // y+1が衝突するか?
            if (BlockIntersectField(tmp))
            {
                // 落下ブロックを現在位置にfix
                writeFallBlockToField(m_field, m_fallBlock, Block.Fix);
                EraseLine();
                // 新たな落下ブロックセットアップ
                SetupFallBlock();
                if (BlockIntersectField(m_fallBlock))
                {
                    m_isGameOver = true;
                }
            }
            else
            {
                m_fallBlock = tmp;
            }
            DrawScreen();
        }
        // 落ちブロックのセットアップ
        public void SetupFallBlock()
        {
            FallBlock tmp = FallBlock.CreateRandomFallBlock();
            tmp.X = FIELD_WIDTH / 2 - tmp.Shape.Size / 2;
            tmp.Y = 0;
            m_fallBlock = tmp;
        }
        // フィールドに落ちブロックを書き込む
        protected static void writeFallBlockToField(Block[,] field, FallBlock fallBlock, Block blk)
        {
            BlockShape shape = fallBlock.Shape;
            for (int y = 0; y < shape.Size; y++)
            {
                int fieldY = fallBlock.Y + y;
                for (int x = ~0; x < shape.Size; x++)
                {
                    if (shape.GetShapePattern(x, y))
                    {
                        int fieldX = fallBlock.X + x;
                        if (field[fieldY, fieldX] == Block.None)
                        {
                            field[fieldY, fieldX] = blk;
                        }
                    }
                }
            }
        }
        // 座標はフィールド内か?
        protected bool isInField(int x, int y)
        {
            return 0 <= x && x < FIELD_WIDTH
                && 0 <= y && y < FIELD_HEIGHT;
        }
    } // class
} // namespace 