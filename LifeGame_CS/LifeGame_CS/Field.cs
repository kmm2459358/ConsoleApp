//======================================
//      ライフゲーム　フィールド
//======================================
using System;
using Utility = GP2.Utility;

namespace LifeGame_CS
{
    internal class Field
    {
        protected int m_width;
        protected int m_height;
        protected bool[][] m_cells;
        protected int m_currentIdx;
        protected bool m_isLoopCells;

        // コンストラクター
        public Field(int width, int height, bool isLoopCells)
        {
            m_width = width;
            m_height = height;
            int cellsSize = width * m_height;
            m_cells = new bool[][]
            {
                new bool[cellsSize],
                new bool[cellsSize],
            };
            m_currentIdx = 0;
            m_isLoopCells = isLoopCells;
        }

        // フィールドを描画
        public void DrawField()
        {
            var cells = m_cells[m_currentIdx];
            drawCells(cells);
            Utility.PrintOut();
        }

        // シミュレーションを１ステップ進める
        public void StepSimulation()
        {
            int currentIdx = m_currentIdx;
            int nextIdx = (currentIdx != 0) ? 0 : 1;
            bool[] now = m_cells[currentIdx];
            bool[] next = m_cells[nextIdx];
            for (int y = 0; y < m_height; y++)
            {
                for (int x = 0; x < m_width; x++)
                {
                    int count = getLivingCount(now, x, y);
                    bool isLive = false;
                    if (count <= 1)
                    {
                        isLive = false;
                    }
                    else if (count == 2)
                    {
                        isLive = getCells(now, x, y);
                    }
                    else if (count == 3)
                    {
                        isLive = true;
                    }
                    else
                    {// if(count>=4
                        isLive = false;
                    }
                    setCells(next, x, y, isLive);
                }
            }
            m_currentIdx = nextIdx;
        }

        // パターン転送する
        public void TransferPattern(Pattern pattern)
        {
            m_isLoopCells = pattern.isLoopCells;
            int dstX = m_width / 2 - pattern.width / 2;
            int dstY = m_height / 2 - pattern.height / 2;
            clearCells(m_cells[m_currentIdx]);
            transferPattern(dstX, dstY, pattern.width, pattern.height, pattern.data);
        }
        // パターンを転送する( TransferPattern()の下請け)
        protected void transferPattern(int dstX, int dstY, int srcWid, int srcHei, byte[] srcPat)
        {
            bool[] cells = m_cells[m_currentIdx];
            for (int y = 0; y < srcHei; y++)
            {
                for (int x = 0; x < srcWid; x++)
                {
                    bool isLive = (srcPat[y * srcWid + x] != 0);
                    setCells(cells, dstX + x, dstY + y, isLive);
                }
            }
        }
        // 指定位置周辺の生存数を数える
        protected int getLivingCount(bool[] cells, int x, int y)
        {
            int count = 0;
            if (getCells(cells, x - 1, y - 1)) count++;
            if (getCells(cells, x + 0, y - 1)) count++;
            if (getCells(cells, x + 1, y - 1)) count++;
            if (getCells(cells, x - 1, y + 0)) count++;
            // (x+0,y+0) はスキップ
            if (getCells(cells, x + 1, y + 0)) count++;
            if (getCells(cells, x - 1, y + 1)) count++;
            if (getCells(cells, x + 0, y + 1)) count++;
            if (getCells(cells, x + 1, y + 1)) count++;
            return count;
        }

        // cells[]をクリア.
        protected void clearCells(bool[] cells)
        {
            for (int y = 0; y < m_height; y++)
            {
                for (int x = 0; x < m_width; x++)
                {
                    setCells(cells, x, y, false);
                }
            }
        }
        // cells[x,y]をセット
        protected void setCells(bool[] cells, int x, int y, bool value)
        {
            if (isInCells(x, y) == false)
            {
                if (m_isLoopCells == false)
                {
                    return;
                }
                // 左右、上下ループ時
                x = (x + m_width) % m_width;
                y = (y + m_height) % m_height;
            }
            cells[y * m_width + x] = value;
        }
        // cells[x,y]を取得
        protected bool getCells(bool[] cells, int x, int y)
        {
            if (isInCells(x, y) == false)
            {
                if (m_isLoopCells == false)
                {
                    // 外側は全て not live
                    return false;
                }
                // 左右、上下ループ時
                x = (x + m_width) % m_width;
                y = (y + m_height) % m_height;
            }
            return cells[y * m_width + x];
        }
        // 
        protected bool isInCells(int x, int y)
        {
            return 0 <= x && x < m_width
                && 0 <= y && y < m_height;
        }
        // cells[]を描画
        protected void drawCells(bool[] cells)
        {
            Utility.ClearScreen();
            for (int y = 0; y < m_height; y++)
            {
                for (int x = 0; x < m_width; x++)
                {
                    bool isLive = getCells(cells, x, y);
                    Utility.Printf("{0}", isLive ? "■" : "・");
                }
                Utility.Putchar('\n');
            }
        }
    }
}