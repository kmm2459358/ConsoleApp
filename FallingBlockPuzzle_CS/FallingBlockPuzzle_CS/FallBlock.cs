//======================================
//      落ち物バズル 落ちプロック
//======================================
using Utility = GP2.Utility;

namespace FallingBlockPuzzle_CS
{
    internal class FallBlock
    {
        int m_x;            // x座標
        int m_y;            // y座標
        BlockShape m_shape; // シェイプ

        // x座標のプロパティ
        public int X
        {
            get { return m_x; }
            set { m_x = value; }
        }
        // y座標のプロパティ
        public int Y
        {
            get { return m_y; }
            set { m_y = value; }
        }
        // シェイプのプロパティ
        public BlockShape Shape
        {
            get { return m_shape; }
        }
        // コンストラクター
        public FallBlock(int idx)
        {
            m_x = m_y = 0;
            m_shape = new BlockShape(idx);
        }
        // コピーコンストラクター
        public FallBlock(FallBlock src)
        {
            m_x = src.m_x;
            m_y = src.m_y;
            m_shape = new BlockShape(src.m_shape);
        }
        // 移動
        public void Move(int ofsx, int ofsy)
        {
            m_x += ofsx;
            m_y += ofsy;
        }
        // 回転
        public void Rotate()
        {
            m_shape.Rotate();
        }
        // ランダムなFallBlockを生成する
        public static FallBlock CreateRandomFallBlock()
        {
            // ランダムなシェイプ
            int idx = Utility.GetRand(BlockShape.BlockShapesSize);
            FallBlock ret = new FallBlock(idx);
            // 0～3 回の回転
            int count = Utility.GetRand(4);
            while (count-- > 0)
            {
                ret.Rotate();
            }
            return ret;
        }
    } // class
} // namespace