using System.Diagnostics;
namespace SnakeGame
{
    public class TimeManager : Singleton<TimeManager>
    {
        public TimeManager() {
            stopwatch.Start();
        }

        public readonly int TimeScale = 1;
        public static int MS_PER_FRAME = 1000 / FPS;
        public const int FPS = 15;
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
