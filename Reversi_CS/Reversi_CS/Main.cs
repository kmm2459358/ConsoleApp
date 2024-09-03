//======================================
//      リバーシ　メイン
//======================================
using GP2;

namespace Reversi_CS
{
    class ReversiMain
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
            Vector2 dummyPos = new Vector2(-1, -1);
            GameMode mode = UI.SelectMode();
            Reversi reversi = new Reversi(mode);
            while (true)
            {
                Cell turn = reversi.turn;
                // 打てるところがあるか?
                if (reversi.CheckCanPlaceAll(turn))
                {
                    Vector2 placePos;
                    if (reversi.IsHumanPlayer())
                    {
                        placePos = UI.InputPosition(reversi);
                    }
                    else
                    {
                        reversi.DrawScreen(dummyPos, DrawStatus.InPlay);
                        Utility.WaitKey();
                        placePos = AI.GetCpuPlayerPosition(reversi);
                    }
                    reversi.CheckCanPlace(turn, placePos, true);
                    reversi.SetBoard(placePos.x, placePos.y, turn);
                }
                else
                {
                    // 相手も打てないなら終了
                    Cell opponent = Reversi.GetOpponent(turn);
                    if (reversi.CheckCanPlaceAll(opponent) == false)
                    {
                        break;
                    }
                    reversi.DrawScreen(dummyPos, DrawStatus.NoSpace);
                    Utility.WaitKey();
                }
                reversi.NextTurn();
            }
            // リザルト表示
            reversi.DrawScreen(dummyPos, DrawStatus.Result);
            Utility.WaitKey();
        }
    } // class ReversiMain
} // namespace