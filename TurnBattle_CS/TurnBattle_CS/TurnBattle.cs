//======================================
//      ターン制バトル
//======================================
using System;
using Utility = GP2.Utility;

namespace TurnBattle_CS
{
    class TurnBattle
    {
        private Character m_player;
        private Character m_enemy;
        private int m_turn;

        // コンストラクタ
        public TurnBattle(Character player, Character enemy)
        {
            m_player = player;
            m_enemy = enemy;
        }
        // イントロ「～が現れた」を表示
        public void Intro()
        {
            DrawBattleScreen();
            Utility.Printf("{0}が　あらわれた!\n", m_enemy.Name);
            Utility.PrintOut();
            Utility.WaitKey();
        }
        // 戦闘開始
        public void Start()
        {
            m_turn = 1;
            m_player.Start();
            m_enemy.Start();
        }
        // 画面表示
        public void DrawBattleScreen()
        {
            Utility.ClearScreen();
            m_player.IndicatePlayer();
            Utility.Putchar('\n');
            m_enemy.IndicateEnemy();
            Utility.Putchar('\n');
        }
        // プレーヤのターン実行
        public bool ExecPlayerTurn(Command cmd)
        {
            execCommand(cmd, m_player, m_enemy);
            if (m_enemy.IsDead())
            {
                m_enemy.IsEraseAa = true;
                DrawBattleScreen();
                Utility.Printf("{0}を　たおした!\n", m_enemy.Name);
                Utility.PrintOut();
                Utility.WaitKey();
                return true;
            }
            return m_player.IsEscape;
        }
        // 敵のターン実行
        public bool ExecEnemyTurn(Command cmd)
        {
            execCommand(cmd, m_enemy, m_player);
            if (m_player.IsDead())
            {
                DrawBattleScreen();
                Utility.Printf("あなたは　しにました\n");
                Utility.PrintOut();
                Utility.WaitKey();
                return true;
            }
            return false;
        }
        // コマンド実行
        private void execCommand(Command cmd, Character offense, Character defense)
        {
            int dmg = 0;
            switch (cmd)
            {
                case Command.Fight:
                    DrawBattleScreen();
                    Utility.Printf("{0}の　こうげき!\n", offense.Name);
                    Utility.PrintOut();
                    Utility.WaitKey();

                    dmg = offense.CalcDamage();
                    defense.Damage(dmg);
                    DrawBattleScreen();
                    Utility.Printf("{0}に　{1}の　ダメージ!\n", defense.Name, dmg);
                    Utility.PrintOut();
                    Utility.WaitKey();

                    break;
                case Command.Spell:
                    if (offense.CanSpell() == false)
                    {
                        DrawBattleScreen();
                        Utility.Printf("ＭＰが　たりない!\n");
                        Utility.PrintOut();
                        Utility.WaitKey();
                        break;
                    }
                    offense.UseSpell();
                    DrawBattleScreen();
                    Utility.Printf("{0}は　ヒールを　となえた!\n", offense.Name);
                    Utility.PrintOut();
                    Utility.WaitKey();

                    offense.Recover();
                    DrawBattleScreen();
                    Utility.Printf("{0}のきずが　かいふくした!\n", offense.Name);
                    Utility.PrintOut();
                    Utility.WaitKey();
                    break;

                case Command.Escape:
                    DrawBattleScreen();
                    Utility.Printf("{0}は　にげだした!\n", offense.Name);
                    Utility.PrintOut();
                    Utility.WaitKey();
                    offense.IsEscape = true;
                    break;
            }
        }
        // 次のターンへ
        public void NextTurn()
        {
            m_turn++;
        }
    } // class TurnBattle
} // namespace