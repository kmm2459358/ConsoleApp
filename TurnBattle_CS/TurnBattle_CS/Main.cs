//======================================
//      ターン制バトル メイン
//======================================
using System;    // ConsoleKey
using System.ComponentModel.Design;
using Utility = GP2.Utility;

namespace TurnBattle_CS
{
    class TurnBattleMain
    {
        static int Main()
        {
            Utility.InitRand();
            Audio.Setup();

            ConsoleKey c;
            do
            {
                Game();
                Utility.Printf("もう一度(y/n)?");
                Utility.PrintOut();
                while (true)
                {
                    c = Utility.GetKey();
                    if (c == ConsoleKey.Y || c == ConsoleKey.N)
                    {
                        break;
                    }
                }
            } while (c == ConsoleKey.Y);

            Audio.Remove();
            return 0;
        }

        static void Game()
        {
            Character player = new Character(
                100,        // HP
                15,         // MP
                30,         // 攻撃力
                "ゆうしゃ", // 名前
                ""          // アスキーアート
                );
            Character boss = new Character(
                255,        // HP
                 0,         // MP
                 50,        // 攻撃力
                 "まおう",  // 名前
                "　　Ａ＠Ａ\n" +   // アスキーアート
                "ψ（▼皿▼）ψ"
                );
            Character zako = new Character(
                3,         // HP
                0,         // MP
                2,         // 攻撃力
                "スライム",// 名前
                "／・Д・＼\n" + // アスキーアート
                "～～～～～"
                );
            TurnBattle btl = new TurnBattle(player, boss);

            btl.Start();
            btl.Intro();
            bool isEnd = false;
            Command cmd;
            while (true)
            {
                cmd = CommandUI.GetPlayerCommand(btl);
                isEnd = btl.ExecPlayerTurn(cmd);
                if (isEnd)
                {
                    break;
                }
                cmd = CommandAI.GetEnemyCommand();
                isEnd = btl.ExecEnemyTurn(cmd);
                if (isEnd)
                {
                    break;
                }
                btl.NextTurn();
            }
        }
    } // class
} // namespace 