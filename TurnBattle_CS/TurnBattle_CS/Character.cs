//======================================
//      キャラクター
//======================================
using System;
using Utility = GP2.Utility;

namespace TurnBattle_CS
{
    class Character
    {
        const int SPELL_COST = 3;
        private int m_hp;
        private int m_maxHp;
        private int m_mp;
        private int m_maxMp;
        private int m_attack;
        private string m_name;
        private string m_aa;
        private bool m_isEscape;
        private bool m_isEraseAa;

        // コンストラクタ
        public Character(int hp, int mp, int attack, string name, string aa)
        {
            m_maxHp = hp;
            m_maxMp = mp;
            m_attack = attack;
            m_name = name;
            m_aa = aa;
        }
        // 戦闘開始
        public void Start()
        {
            m_hp = m_maxHp;
            m_mp = m_maxMp;
            m_isEscape = false;
            m_isEraseAa = false;
        }
        // 死亡か?
        public bool IsDead()
        {
            return m_hp == 0;
        }
        // ダメージ受ける
        public void Damage(int dmg)
        {
            m_hp -= dmg;
            if (m_hp < 0)
            {
                m_hp = 0;
            }
        }
        // 回復する
        public void Recover()
        {
            m_hp = m_maxHp;
        }
        // 呪文できるか?
        public bool CanSpell()
        {
            return m_mp >= SPELL_COST;
        }
        // 呪文を使う
        public void UseSpell()
        {
            m_mp -= SPELL_COST;
            if (m_mp < 0)
            {
                m_mp = 0;
            }
        }
        // プレーヤ表示
        public void IndicatePlayer()
        {
            Utility.Printf("{0}\n", m_name);
            Utility.Printf("ＨＰ：{0,3}／{1}　ＭＰ：{2,2}／{3}\n", m_hp, m_maxHp, m_mp, m_maxMp);
        }
        // 敵表示
        public void IndicateEnemy()
        {
            if (m_isEraseAa == false)
            {
                Utility.Printf("{0}", m_aa);
            }
            Utility.Printf("（ＨＰ：{0,3}／{1}）\n", m_hp, m_maxHp);
        }
        public int CalcDamage()
        {
            int dmg = Utility.GetRand(m_attack) + 1;
            return dmg;
        }
        // 名前を取得
        public String Name
        {
            get { return m_name; }
        }
        // にげたか?
        public bool IsEscape
        {
            get { return m_isEscape; }
            set { m_isEscape = value; }
        }
        // アスキーアート消去設定
        public bool IsEraseAa
        {
            get { return m_isEraseAa; }
            set { m_isEraseAa = value; }
        }
    } // class Character
} // namespace