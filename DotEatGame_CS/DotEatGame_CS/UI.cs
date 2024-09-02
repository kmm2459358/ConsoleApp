//======================================
//	ドットイートゲーム　UI
//======================================
using Vector2 = GP2.Vector2;
using Utility = GP2.Utility;

namespace DotEatGame_CS
{
    static class UI
    {
        public static void MovePlayer(Stage stage)
        {
            Character player = stage.player;
            Vector2 newPos = player.pos;
            switch (Utility.GetKey())
            {
                case ConsoleKey.UpArrow: newPos.y--; break;
                case ConsoleKey.DownArrow: newPos.y++; break;
                case ConsoleKey.LeftArrow: newPos.x--; break;
                case ConsoleKey.RightArrow: newPos.x++; break;
                default:
                    return;
            }
            newPos = Stage.GetLoopPosition(newPos);
            Maze c = stage[newPos];
            if (c != Maze.Wall)
            {
                player.Move(newPos);
                if (c == Maze.Dot)
                {
                    stage[newPos] = Maze.None;
                }
            }
        }
    } // class
} // namespace