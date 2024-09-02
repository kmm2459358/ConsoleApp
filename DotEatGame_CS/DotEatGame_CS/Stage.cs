//======================================
//	ドットイートゲーム　Stage
//======================================using System;
using Vector2 = GP2.Vector2;
using Utility = GP2.Utility;
using System.Diagnostics;          // Debug
using System.Collections.Generic;  // List<T>
using System;

namespace DotEatGame_CS
{
    enum Maze
    {
        None = ' ',  // 空白
        Wall = '#',  // 壁
        Dot = 'o',   // ドット
        Player = Chara.Player,   // プレーヤ
        Random = Chara.Random,  // 気まぐれモンスター
        Chase = Chara.Chase,   // 追いかけモンスター
        Ambush = Chara.Ambush, // 先回りモンスター
        Siege = Chara.Siege,   // 挟み撃ちモンスター
    }

    internal class Stage
    {
        public const int MAZE_WIDTH = 19;
        public const int MAZE_HEIGHT = 19;

        Maze[,] m_maze;             // 迷路
        Character m_player;         // プレーヤ
        List<Character> m_monsters; // モンスターリスト
        Character m_chase;          // 追いかけモンター
        Maze[,] m_drawMaze;         // 描画ワーク

        static string[] s_defaultMaze = new string[]
        {
            "#########o#########",
            "#ooooooo#o#ooooooo#",
            "#o###o#o#o#o#o###o#",
            "#o# #o#ooooo#o# #o#",
            "#o###o###o###o###o#",
            "#ooooooooooooooooo#",
            "#o###o###o###o###o#",
            "#ooo#o#ooooo#o#ooo#",
            "###o#o#o###o#o#o###",
            "oooooooo# #oooooooo",
            "###o#o#o###o#o#o###",
            "#ooo#o#ooooo#o#ooo#",
            "#o###o###o###o###o#",
            "#oooooooo oooooooo#",
            "#o###o###o###o###o#",
            "#o# #o#ooooo#o# #o#",
            "#o###o#o#o#o#o###o#",
            "#ooooooo#o#ooooooo#",
            "#########o#########"
        };
        // プロパティ
        public Character player
        {
            get { return m_player; }
        }
        public Character chase
        {
            get { return m_chase; }
        }
        public List<Character> monsters
        {
            get { return m_monsters; }
        }
        // インデクサー
        public Maze this[Vector2 pos]
        {
            get
            {
                if (IsInMaze(pos))
                {
                    return m_maze[pos.y, pos.x];
                }
                return Maze.Wall;
            }
            set
            {
                if (IsInMaze(pos))
                {
                    m_maze[pos.y, pos.x] = value;
                }
            }
        }
        // コンストラクタ
        public Stage(Character player)
        {
            m_player = player;
            m_monsters = new List<Character>(4);
            m_maze = new Maze[MAZE_HEIGHT, MAZE_WIDTH];
            setupMaze(s_defaultMaze);
            m_drawMaze = new Maze[MAZE_HEIGHT, MAZE_WIDTH];
        }
        // mazeセットアップ
        protected void setupMaze(string[] defaultMaze)
        {
            for (int y = 0; y < MAZE_HEIGHT; y++)
            {
                string s = (y < defaultMaze.Length) ? defaultMaze[y] : "";
                for (int x = 0; x < MAZE_WIDTH; x++)
                {
                    char c = (x < s.Length) ? s[x] : ' ';
                    m_maze[y, x] = (Maze)c;
                }
            }
        }
        // モンスター登録
        public void RegistMonster(Character monster)
        {
            m_monsters.Add(monster);
            if (monster.id == Chara.Chase)
            {
                m_chase = monster;
            }
        }
        // 迷路描画
        public void DrawMaze()
        {
            Array.Copy(m_maze, m_drawMaze, m_maze.Length);
            setCharaToDrawMaze(m_player);
            for (int i = 0; i < m_monsters.Count; i++)
            {
                setCharaToDrawMaze(m_monsters[i]);
            }

            Utility.ClearScreen();
            for (int y = 0; y < MAZE_HEIGHT; y++)
            {
                for (int x = 0; x < MAZE_WIDTH; x++)
                {
                    Maze c = m_drawMaze[y, x];
                    Utility.Printf("{0}", getMazeAA(c));
                }
                Utility.Putchar('\n');
            }
            Utility.PrintOut();
        }
        // ゲームオーバ?
        public bool IsGameOver()
        {
            for (int i = 0; i < m_monsters.Count; i++)
            {
                if (m_player.pos == m_monsters[i].pos)
                {
                    return true;
                }
            }
            return false;
        }
        // ゲーム完了(ドット完食)?
        public bool IsComplete()
        {
            for (int y = 0; y < MAZE_HEIGHT; y++)
            {
                for (int x = 0; x < MAZE_WIDTH; x++)
                {
                    if (m_maze[y, x] == Maze.Dot)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        // 座標のワープ処理
        public static Vector2 GetLoopPosition(Vector2 pos)
        {
            pos.x = (pos.x + MAZE_WIDTH) % MAZE_WIDTH;
            pos.y = (pos.y + MAZE_HEIGHT) % MAZE_HEIGHT;
            return pos;
        }
        // 描画ワークにキャラを書き込む
        protected void setCharaToDrawMaze(Character ch)
        {
            m_drawMaze[ch.pos.y, ch.pos.x] = (Maze)ch.id;
        }
        protected string getMazeAA(Maze c)
        {
            switch (c)
            {
                case Maze.None: return "　";
                case Maze.Wall: return "■";
                case Maze.Dot: return "・";
                case Maze.Player: return "〇";
                case Maze.Random: return "☆";
                case Maze.Chase: return "凸";
                case Maze.Ambush: return "◇";
                case Maze.Siege: return "凹";
            }
            Debug.Assert(false);
            return "??";
        }

        public static bool IsInMaze(Vector2 pos)
        {
            return 0 <= pos.x && pos.x < MAZE_WIDTH
                && 0 <= pos.y && pos.y < MAZE_HEIGHT;
        }
    }
}