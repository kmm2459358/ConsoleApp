//======================================
//      ライフゲーム　メイン
//======================================
using System;  // ConsoleKey
using Utility = GP2.Utility;
using IntervalTimer = GP2.IntervalTimer;

namespace LifeGame_CS
{
    class LifeGameMain
    {
        const int FIELD_WIDTH = 48;
        const int FIELD_HEIGHT = 48;

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
        // ゲーム処理
        static void Game()
        {
            const int patternWidth = 10;
            const int patternHeight = 8;
            const int FPS = 10;

            byte[] _pattern = new byte[]
            {
                0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,1,0,0,
                0,0,0,0,0,1,0,1,1,0,
                0,0,0,0,0,1,0,1,0,0,
                0,0,0,0,0,1,0,0,0,0,
                0,0,0,1,0,0,0,0,0,0,
                0,1,0,1,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,
            };

            Pattern pattern = new Pattern(patternWidth, patternHeight, _pattern, "", true);
            IntervalTimer timer = new IntervalTimer();

            Field field = new Field(FIELD_WIDTH, FIELD_HEIGHT, true);
            field.TransferPattern(pattern);
            field.DrawField();

            timer.StartTimer(FPS);
            while (true)
            {
                if (timer.IsInterval)
                {
                    field.StepSimulation();
                    field.DrawField();
                }

                if (Utility.KeyAvailable())
                {
                    ConsoleKey c = Utility.GetKey();
                    if (c == ConsoleKey.Escape)
                    {
                        break;
                    }
                    else if (c == ConsoleKey.P)
                    {
                        Pattern pat = Pattern.SelectPattern();
                        if (pat != null)
                        {
                            field.TransferPattern(pat);
                            field.DrawField();
                            Utility.WaitKey();
                        }
                    }
                    else
                    {
                        do
                        {
                            field.StepSimulation();
                            field.DrawField();
                        } while (Utility.GetKey() == ConsoleKey.Spacebar);
                    }
                }
            }
        } // game
    } // class LifeGame
} // namespace