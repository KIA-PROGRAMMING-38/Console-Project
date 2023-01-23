﻿using System.Diagnostics;
namespace SnakeGame
{
    public class TimeManager : LazySingleton<TimeManager>
    {
        public TimeManager() {
            stopwatch.Start();
        }

        public readonly int TimeScale = 1;
        public const int MS_PER_FRAME = 1000 / FPS;
        public const int FPS = 5;
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
