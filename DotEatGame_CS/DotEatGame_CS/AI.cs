//======================================
//	ドットイートゲーム　AI
//======================================
using Vector2 = GP2.Vector2;
using Direction4 = GP2.Direction4;
using Utility = GP2.Utility;

using System;
using System.Collections.Generic;  // List<T>

namespace DotEatGame_CS
{
    internal class AI
    {
        const int MAZE_WIDTH = Stage.MAZE_WIDTH;
        const int MAZE_HEIGHT = Stage.MAZE_HEIGHT;
        const int INIT_DISTANCE = -1;
        const int LARGE_DISTANCE = 100;

        int[,] m_distance;
        List<Vector2> m_v2list;
        List<Vector2> m_v2temp;

        // コンストラクタ
        public AI()
        {
            m_distance = new int[MAZE_HEIGHT, MAZE_WIDTH];
            int capacity = MAZE_WIDTH * MAZE_HEIGHT;
            m_v2list = new List<Vector2>(capacity);
            m_v2temp = new List<Vector2>(capacity);
        }
        // モンスター達の移動
        public void MoveMonsters(Stage stage)
        {
            List<Character> monsters = stage.monsters;
            for (int i = 0; i < monsters.Count; i++)
            {
                Character monster = monsters[i];
                switch (monster.id)
                {
                    case Chara.Random: MoveMonsterRandom(stage, monster); break;
                    case Chara.Chase: MoveMonsterChase(stage, monster); break;
                    case Chara.Ambush: MoveMonsterAmbush(stage, monster); break;
                    case Chara.Siege: MoveMonsterSiege(stage, monster); break;
                }
            }
        }
        // ランダムモンスターの移動
        protected void MoveMonsterRandom(Stage stage, Character ch)
        {
            Vector2 newPos = GetRandomPosition(stage, ch);
            ch.Move(newPos);
        }
        // 追いかけモンスターの移動
        protected void MoveMonsterChase(Stage stage, Character ch)
        {
            Vector2 targetPos = stage.player.pos;
            Vector2 newPos = GetChasePosition(stage, ch, targetPos);
            ch.Move(newPos);
        }
        // 先回りモンスターの移動
        protected void MoveMonsterAmbush(Stage stage, Character ch)
        {
            Character player = stage.player;
            Vector2 playerDir = player.GetCharacterDir();
            Vector2 targetPos = playerDir * 3 + player.pos;
            targetPos = Stage.GetLoopPosition(targetPos);
            Vector2 newPos = GetChasePosition(stage, ch, targetPos);
            ch.Move(newPos);
        }
        // 挟み撃ちモンスターの移動
        protected void MoveMonsterSiege(Stage stage, Character ch)
        {
            Vector2 newPos;
            Character player = stage.player;
            Character chaser = stage.chase;
            if (chaser == null)
            {
                newPos = GetRandomPosition(stage, ch);
            }
            else
            {
                Vector2 chaseToPlayer = player.pos - chaser.pos;
                Vector2 targetPos = player.pos + chaseToPlayer;
                targetPos = Stage.GetLoopPosition(targetPos);
                newPos = GetChasePosition(stage, ch, targetPos);
            }
            ch.Move(newPos);
        }
        // キャラのランダムな移動先を取得
        protected Vector2 GetRandomPosition(Stage stage, Character ch)
        {
            m_v2list.Clear();
            // ４方向で行ける座標をリティング(後退はしない)
            for (int d = 0; d < (int)Direction4.MAX; d++)
            {
                Vector2 dir = Vector2.GetDirVector2((Direction4)d);
                Vector2 newPos = ch.pos + dir;
                newPos = Stage.GetLoopPosition(newPos);
                if (stage[newPos] != Maze.Wall && newPos != ch.lastPos)
                {
                    m_v2list.Add(newPos);
                }
            }
            // リストからランダムに選択
            int idx = Utility.GetRand(m_v2list.Count);
            return m_v2list[idx];
        }
        // キャラがターゲットへ移動するための移動先を取得
        protected Vector2 GetChasePosition(Stage stage, Character ch, Vector2 targetPos)
        {
            //
            //  distance[] を初期化します(INIT_DISTANCEで埋める)
            //
            for (int y = 0; y < MAZE_HEIGHT; y++)
            {
                for (int x = 0; x < MAZE_WIDTH; x++)
                {
                    m_distance[y, x] = INIT_DISTANCE;
                }
            }
            //
            // ScanDistance 初手(chの退路以外の経路をスキャン)
            //
            Vector2 dir;
            Vector2 newPos;
            setDistance(ch.pos, 0);
            for (int d = 0; d < (int)Direction4.MAX; d++)
            {
                dir = Vector2.GetDirVector2((Direction4)d);
                newPos = ch.pos + dir;
                newPos = Stage.GetLoopPosition(newPos);
                if (newPos != ch.lastPos)
                {
                    scanDistance(stage, newPos, 1);
                }
            }
            int dist = getDistance(targetPos);
            if (dist != INIT_DISTANCE)
            {
                //
                // targetPosからさかのぼって dist=1 までたどる
                //
                m_v2list.Clear();
                m_v2list.Add(targetPos);
                while (dist > 1)
                {
                    m_v2temp.Clear();
                    for (int i = 0; i < m_v2list.Count; i++)
                    {
                        Vector2 route = m_v2list[i];
                        for (int d = 0; d < (int)Direction4.MAX; d++)
                        {
                            Vector2 dir2 = Vector2.GetDirVector2((Direction4)d);
                            Vector2 pos2 = route + dir2;
                            pos2 = Stage.GetLoopPosition(pos2);
                            if (getDistance(pos2) == dist - 1)
                            {
                                m_v2temp.Add(pos2);
                            }
                        }
                    }
                    if (m_v2temp.Count == 0)
                    {
                        // 経路なしは、ありえないはず
                        Console.WriteLine(string.Format("dist:{0}", dist));
                        Vector2.PrintList(m_v2list);
                        printDistance(stage);
                    }
                    // リストコピー( m_v2temp=>m_v2list )
                    m_v2list.Clear();
                    for (int i = 0; i < m_v2temp.Count; i++)
                    {
                        m_v2list.Add(m_v2temp[i]);
                    }
                    dist--;
                }
                //
                // dist=1 が移動先
                //
                int idx = Utility.GetRand(m_v2list.Count);
                Vector2 routePos = m_v2list[idx];
                return routePos;
            }
            //
            // targetPosにつながる経路がなければ、ランダムな行先を
            //
            return GetRandomPosition(stage, ch);
        }
        // Mazeをスキャンしてdistance[][]に距離を書き込む(再帰)
        protected void scanDistance(Stage stage, Vector2 pos, int dist)
        {
            Vector2 dir;
            Vector2 newPos;
            pos = Stage.GetLoopPosition(pos);
            if (stage[pos] != Maze.Wall)
            {
                int tmp = getDistance(pos);
                if (tmp == INIT_DISTANCE || tmp > dist)
                {
                    setDistance(pos, dist);
                    for (int d = 0; d < (int)Direction4.MAX; d++)
                    {
                        dir = Vector2.GetDirVector2((Direction4)d);
                        newPos = pos + dir;
                        scanDistance(stage, newPos, dist + 1);
                    }
                }
            }
        }
        // distance[,]のセッター
        protected void setDistance(Vector2 pos, int value)
        {
            if (Stage.IsInMaze(pos))
            {
                m_distance[pos.y, pos.x] = value;
            }
        }
        // distance[,]のゲッター
        protected int getDistance(Vector2 pos)
        {
            if (Stage.IsInMaze(pos))
            {
                return m_distance[pos.y, pos.x];
            }
            return LARGE_DISTANCE;
        }
        // デバッグ(distance[] をプリント)
        protected void printDistance(Stage stage)
        {
            for (int y = 0; y < MAZE_HEIGHT; y++)
            {
                for (int x = 0; x < MAZE_WIDTH; x++)
                {
                    int dist = m_distance[y, x];
                    if (dist == INIT_DISTANCE)
                    {
                        Console.Write("■");
                    }
                    else
                    {
                        Console.Write(dist.ToString("D2"));
                    }
                }
                Console.WriteLine();
            }
            Utility.WaitKey();
            Utility.WaitKey();
            Utility.WaitKey();
            Utility.WaitKey();
        }
    } // class
} // namespace 