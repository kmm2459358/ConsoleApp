//======================================
//      落ち物バズル ブロック形状
//======================================using System;
using System;

namespace FallingBlockPuzzle_CS
{
    internal class BlockShape
    {
        int m_size;         // パターンのサイズ
        bool[,] m_pattern;  // パターン
        bool[,] m_work;     // 回転ワーク

        // サイズのプロパティ
        public int Size
        {
            get { return m_size; }
        }

        const bool X = true;
        const bool _ = false;
        // ピース配列
        static BlockShape[] s_blockShapes = new BlockShape[]
        {
            new BlockShape(3,
                new bool[3,3]{
                {_,X,_},
                {_,X,_},
                {_,X,_},
                }),
            new BlockShape(3,
                new bool[3,3]{
                {_,X,_},
                {_,X,X},
                {_,_,_},
                }),
        };
        // シォイプテーブルサイズ
        public static int BlockShapesSize
        {
            get { return s_blockShapes.Length; }
        }

        // コンストラクター
        public BlockShape(int size, bool[,] pat)
        {
            setup(size, pat);
        }

        // コンストラクター(idx)
        public BlockShape(int idx)
        {
            if (idx < 0 || s_blockShapes.Length <= idx)
            {
                idx = 0;
            }
            BlockShape tmp = s_blockShapes[idx];
            setup(tmp.m_size, tmp.m_pattern);
        }
        // コピーコンストラクター
        public BlockShape(BlockShape src)
        {
            setup(src.m_size, src.m_pattern);
        }

        // セットアップ
        protected void setup(int size, bool[,] pat)
        {
            m_size = size;
            m_pattern = new bool[size, size];
            m_work = new bool[size, size];

            int pat_hei = pat.GetLength(0);
            int pat_wid = pat.GetLength(1);
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    bool value = (x < pat_wid && y < pat_hei) ? pat[y, x] : false;
                    m_pattern[y, x] = value;
                }
            }
        }
        // 回転
        public void Rotate()
        {
            // 時計回り
            //  x         X=size-1-y
            // +-----+    +-----+
            //y|0,1,2|    |6,3,0|Y=x
            // |3,4,5| => |7,4,1|
            // |6,7,8|    |8.5.2|
            // +-----+    +-----+
            //　★反時計回り
            //  x         X=y
            // +-----+    +-----+
            //y|0,1,2|    |2,5,8|Y=size-1-x
            // |3,4,5| => |1,4,7|
            // |6,7,8|    |0.3.6|
            // +-----+    +-----+
            //
            for (int y = 0; y < m_size; y++)
            {
                for (int x = 0; x < m_size; x++)
                {
                    bool value = m_pattern[y, x];
                    m_work[m_size - 1 - x, y] = value;
                }
            }
            // m_patternとm_workをスワップ
            bool[,] tmp = m_pattern;
            m_pattern = m_work;
            m_work = tmp;
        }
        // バターンのセッター
        public void SetShapePattern(int x, int y, bool value)
        {
            if (isInShapePattern(x, y))
            {
                m_pattern[y, x] = value;
            }
        }
        // ﾊﾟﾀｰﾝのゲッター
        public bool GetShapePattern(int x, int y)
        {
            if (isInShapePattern(x, y))
            {
                return m_pattern[y, x];
            }
            return false;
        }
        // 座標が pattern内か?
        protected bool isInShapePattern(int x, int y)
        {
            return 0 <= x && x < m_size
            && 0 <= y && y < m_size;
        }
    } // class
} // namespace