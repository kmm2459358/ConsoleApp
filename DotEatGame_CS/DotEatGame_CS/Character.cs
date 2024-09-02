//======================================
//	ドットイートゲーム　Character
//======================================
using Vector2 = GP2.Vector2;

namespace DotEatGame_CS
{
    enum Chara
    {
        Player, // プレーヤ
        Random, // きまぐれモンスター
        Chase,  // 追いかけモンスター
        Ambush, // 先回りモンスター
        Siege,  // 挟み撃ちモンスター
    }

    internal class Character
    {
        Chara m_id;        // 誰?
        Vector2 m_pos;     // 座標
        Vector2 m_lastPos; // 前回の座標
        Vector2 m_tmp;     // ワーク

        // プロパティ
        public Chara id
        {
            get { return m_id; }
        }
        public Vector2 pos
        {
            get { return m_pos; }
        }
        public Vector2 lastPos
        {
            get { return m_lastPos; }
        }
        // コンストラクタ
        public Character(Chara id, int initX, int initY)
        {
            m_id = id;
            m_pos.x = initX;
            m_pos.y = initY;
            m_lastPos = m_pos;
        }
        // キャラクター移動
        public void Move(Vector2 pos)
        {
            m_lastPos = m_pos;
            m_pos = pos;
        }
        // キャラクターの向きベクターを取得(ワープ対応)
        public Vector2 GetCharacterDir()
        {
            int x = m_pos.x - m_lastPos.x;
            if (x < -1 || 1 < x)
            {
                // 右端→左端、または 右端←左端
                x = (m_pos.x == 0) ? +1 : -1;
            }
            int y = m_pos.y - m_lastPos.y;
            if (y < -1 || 1 < y)
            {
                // 下端→上端、または、下端←上端
                y = (m_pos.y == 0) ? +1 : -1;
            }
            m_tmp.x = x;
            m_tmp.y = y;
            return m_tmp;
        }
    } // class
} // namespace