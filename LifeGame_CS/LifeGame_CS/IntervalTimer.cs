//======================================
//      インターバル・タイマー
//======================================

namespace GP2
{
    internal class IntervalTimer
    {
        protected long m_interval;
        protected long m_nextClock;

        // スタートタイマー
        public void StartTimer(int fps)
        {
            m_interval = 1000 / fps;
            m_nextClock = clock() + m_interval;
        }
        // インターバル経過?
        public bool IsInterval
        {
            get
            {
                long nowClock = clock();
                if (nowClock >= m_nextClock)
                {
                    m_nextClock = nowClock + m_interval;
                    return true;
                }
                return false;
            }
        }
        // 現在clock取得
        private long clock()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}