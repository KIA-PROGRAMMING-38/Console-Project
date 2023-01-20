using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class TimeManager : Singleton<TimeManager>
    {
        public TimeManager() {
            stopwatch.Start();
        }

        public readonly int TimeScale = 1;
        public static int DeltaTime = 1000 / FPS;
        public const int FPS = 10;
        private Stopwatch stopwatch = new Stopwatch();
        private long _currentMs = 0;
        private long _elapsed = 0;

        public long ElapsedMs { get { return _elapsed; } }

        public void Update()
        {
            _elapsed = stopwatch.ElapsedMilliseconds - _currentMs;
            _currentMs = stopwatch.ElapsedMilliseconds;
        }

        public void Destroy()
        {
            stopwatch.Stop();
        }
    }
}
