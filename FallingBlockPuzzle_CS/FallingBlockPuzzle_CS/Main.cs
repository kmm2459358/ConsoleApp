//======================================
//      落ち物バズル メイン
//======================================
using System;  // ConsoleKey
using Stage = FallingBlockPuzzle_CS.Stage;
using FallBlock = FallingBlockPuzzle_CS.FallBlock;
using Utility = GP2.Utility;
using IntervalTimer = GP2.IntervalTimer;

namespace FallingBlockPuzzle_CS
{
    class FallingBlockPuzzleMain
    {
        static int Main()
        {
            Utility.InitRand();

            ConsoleKey c;
            do
            {
                game();
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
        // ゲーム
        static void game()
        {
            const int FPS = 1;

            Stage stage = new Stage();
            IntervalTimer timer = new IntervalTimer();
            stage.SetupFallBlock();
            stage.DrawScreen();

            timer.StartTimer(FPS);
            while (stage.IsGameOver == false)
            {
                if (timer.IsInterval)
                {
                    stage.MoveDownFallBlock();
                }

                if (Utility.KeyAvailable())
                {
                    OperateFallBlock(stage);
                }
            }
            // 積みあがった状態を表示
            stage.DrawScreen();
        }
        // 落ちプロックの操作
        static void OperateFallBlock(Stage stage)
        {
            FallBlock tmp = new FallBlock(stage.FallBlock);
            bool change = false;
            switch (Utility.GetKey())
            {
                case ConsoleKey.DownArrow:
                    tmp.Move(0, 1);
                    change = true;
                    break;
                case ConsoleKey.LeftArrow:
                    tmp.Move(-1, 0);
                    change = true;
                    break;
                case ConsoleKey.RightArrow:
                    tmp.Move(1, 0);
                    change = true;
                    break;
                case ConsoleKey.Spacebar:
                    tmp.Rotate();
                    change = true;
                    break;
            }
            if (change)
            {
                // フィールドと衝突なければ更新
                if (stage.BlockIntersectField(tmp) == false)
                {
                    stage.FallBlock = tmp;
                    stage.DrawScreen();
                }
            }
        }
    } // class
} // namespace