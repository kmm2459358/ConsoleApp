//======================================
//      リバーシ AI(CPUプレイヤー思考)
//======================================
using System;
using System.Collections.Generic;  // List<T>
using Utility = GP2.Utility;
using Vector2 = GP2.Vector2;

namespace Reversi_CS
{
    internal class AI
    {
        // CPUプレーヤの打つ場所を得る
        public static Vector2 GetCpuPlayerPosition(Reversi reversi)
        {
            List<Vector2> list = new List<Vector2>(100);
            // 打てるリストからランダムに選ぶ.
            reversi.ListCanPlaceAll(reversi.turn, list);
            int idx = Utility.GetRand(list.Count);
            return list[idx];
        }
    } // class
} // namespace