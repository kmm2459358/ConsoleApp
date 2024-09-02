//======================================
//	ドットイートゲーム　Main
//======================================
using GP2;
using System;
using Character = DotEatGame_CS.Character;
using Stage = DotEatGame_CS.Stage;
using Utility = GP2.Utility;

namespace DotEatGame_CS
{
    internal class DotEatGameMain
    {
        static int Main()
        {
            Utility.InitRand();

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
            return 0;
        }

        static void Game()
        {
            Character player = new Character(Chara.Player, 9, 13);
            Character monster1 = new Character(Chara.Random, 1, 1);
            Character monster2 = new Character(Chara.Chase, 17, 1);
            Character monster3 = new Character(Chara.Ambush, 1, 17);
            Character monster4 = new Character(Chara.Siege, 17, 17);
            IntervalTimer timer = new IntervalTimer();
            AI ai = new AI();

            Stage stage = new Stage(player);
            stage.RegistMonster(monster1);
            stage.RegistMonster(monster2);
            stage.RegistMonster(monster3);
            stage.RegistMonster(monster4);

            stage.DrawMaze();
            Utility.WaitKey();

            timer.StartTimer(2); // FPS=2
            while (true)
            {
                if (timer.IsInterval)
                {
                    ai.MoveMonsters(stage);
                    stage.DrawMaze();
                    if (stage.IsGameOver())
                    {
                        DrawGameOver();
                        Utility.WaitKey();
                        break;
                    }
                }
                if (Utility.KeyAvailable())
                {
                    UI.MovePlayer(stage);
                    stage.DrawMaze();
                    if (stage.IsComplete())
                    {
                        DrawCongratulations();
                        Utility.WaitKey();
                        break;
                    }
                }
            }
        }
        // 「ＧＡＭＥ　ＯＶＥＲ」表示
        protected static void DrawGameOver()
        {
            Utility.ClearScreen();
            for (int i = 0; i < Stage.MAZE_HEIGHT; i++)
            {
                if (i == Stage.MAZE_HEIGHT / 2)
                {
                    Utility.Printf("　　　　　ＧＡＭＥ　ＯＶＥＲ\n");
                }
                else
                {
                    Utility.Putchar('\n');
                }
            }
            Utility.PrintOut();
        }
        // 「ＣＯＮＧＲＡＴＵＬＡＴＩＯＮＳ！」表示
        protected static void DrawCongratulations()
        {
            Utility.ClearScreen();
            for (int i = 0; i < Stage.MAZE_HEIGHT; i++)
            {
                if (i == Stage.MAZE_HEIGHT / 2)
                {
                    Utility.Printf("　　ＣＯＮＧＲＡＴＵＬＡＴＩＯＮＳ！\n");
                }
                else
                {
                    Utility.Putchar('\n');
                }
            }
            Utility.PrintOut();
        }
    } // class
} // namespace