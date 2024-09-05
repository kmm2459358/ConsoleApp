//======================================
//      疑似3Dダンジョン　UI
//======================================
using Utility = GP2.Utility;
using Vector2 = GP2.Vector2;
using System;  // ConsoleKey

namespace _3DDungeonGame_CS
{
    internal static class UI
    {
        // プレーヤ移動( Exitフラグを返す)
        public static bool MovePlayer(Stage stage)
        {
            ConsoleKey c = Utility.GetKey();
            if (c == ConsoleKey.Spacebar)
            {
                stage.isForMap = !stage.isForMap;
            }
            else if (c == ConsoleKey.Escape)
            {
                return true;
            }
            else
            {
                if (stage.isForMap)
                {
                    MovePlayerForMap(stage, c);
                }
                else
                {
                    MovePlayerFor3D(stage, c);
                }
            }
            return false;
        }
        public static void DrawOperation(Stage stage)
        {
            Utility.Printf("SPACE : change UI\n");
            Utility.Printf("ESC : exit game\n");
            if (stage.isForMap)
            {
                Utility.Printf("↑:北へ向く/北へ前進\n");
                Utility.Printf("←:西へ向く/西へ前進\n");
                Utility.Printf("→:東へ向く/東へ前進\n");
                Utility.Printf("↓:南へ向く/南へ前進\n");
            }
            else
            {
                Utility.Printf("↑:前に進む\n");
                Utility.Printf("←:左を向く\n");
                Utility.Printf("→:右を向く\n");
                Utility.Printf("↓:後ろを向く\n");
            }
        }
        // プレーヤ移動(ForMap)
        static void MovePlayerForMap(Stage stage, ConsoleKey c)
        {
            Character player = stage.player;
            Direction d = Direction.Max;
            switch (c)
            {
                case ConsoleKey.UpArrow: d = Direction.North; break;
                case ConsoleKey.DownArrow: d = Direction.South; break;
                case ConsoleKey.LeftArrow: d = Direction.West; break;
                case ConsoleKey.RightArrow: d = Direction.East; break;
            }
            if (d != Direction.Max)
            {
                if (player.dir != d)
                {
                    player.dir = d;
                }
                else
                {
                    if (stage.GetMazeWall(player.pos, player.dir) == false)
                    {
                        Vector2 dir = Misc.GetDirVector2(d);
                        Vector2 newPos = player.pos + dir;
                        if (stage.IsInsideMaze(newPos))
                        {
                            player.pos = newPos;
                        }
                    }
                }
            }
        }
        // プレーヤ移動(For3D)
        static void MovePlayerFor3D(Stage stage, ConsoleKey c)
        {
            Character player = stage.player;
            switch (c)
            {
                case ConsoleKey.UpArrow:
                    if (stage.GetMazeWall(player.pos, player.dir) == false)
                    {
                        Vector2 dir = Misc.GetDirVector2(player.dir);
                        Vector2 newPos = player.pos + dir;
                        if (stage.IsInsideMaze(newPos))
                        {
                            player.pos = newPos;
                        }
                    }
                    break;
                case ConsoleKey.DownArrow:
                    player.TurnBack();
                    break;
                case ConsoleKey.LeftArrow:
                    player.TurnLeft();
                    break;
                case ConsoleKey.RightArrow:
                    player.TurnRight();
                    break;
            }
        }
    } // class
} // namespace 