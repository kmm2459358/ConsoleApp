//======================================
//      リバーシ　UI
//======================================
using System;
using Utility = GP2.Utility;
using Vector2 = GP2.Vector2;

namespace Reversi_CS
{
    internal class UI
    {
        static string[] modeName = new string[]
        {
                "１Ｐ　ＧＡＭＥ",
                "２Ｐ　ＧＡＭＥ",
                "ＷＡＴＣＨ",
        };
        // モード選択
        public static GameMode SelectMode()
        {
            int sel = 0;
            while (true)
            {
                Utility.ClearScreen();
                Utility.Puts("モードを　選択して\nください\n");
                for (int i = 0; i < modeName.Length; i++)
                {
                    string cur = (sel == i) ? "＞" : "　";
                    Utility.Printf("{0}{1}\n\n", cur, modeName[i]);
                }
                Utility.PrintOut();
                switch (Utility.GetKey())
                {
                    case ConsoleKey.UpArrow:
                        sel--;
                        if (sel < 0)
                        {
                            sel = modeName.Length - 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        sel++;
                        if (sel >= modeName.Length)
                        {
                            sel = 0;
                        }
                        break;
                    case ConsoleKey.Enter:
                        return (GameMode)sel;
                }
            }
        }
        // 位置入力
        public static Vector2 InputPosition(Reversi reversi)
        {
            Vector2 pos = new Vector2(3, 3);
            while (true)
            {
                reversi.DrawScreen(pos, DrawStatus.InPlay);
                switch (Utility.GetKey())
                {
                    case ConsoleKey.UpArrow:
                        pos.y--;
                        if (pos.y < 0)
                        {
                            pos.y = Reversi.BOARD_HEI - 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        pos.y++;
                        if (pos.y >= Reversi.BOARD_HEI)
                        {
                            pos.y = 0;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        pos.x--;
                        if (pos.x < 0)
                        {
                            pos.x = Reversi.BOARD_WID - 1;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        pos.x++;
                        if (pos.x >= Reversi.BOARD_WID)
                        {
                            pos.x = 0;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (reversi.CheckCanPlace(reversi.turn, pos, false) == false)
                        {
                            Utility.Printf("そこには　置けません\n");
                            Utility.PrintOut();
                            Utility.WaitKey();
                            break;
                        }
                        return pos;
                }
            }
        }
    } // class
} // namespace 