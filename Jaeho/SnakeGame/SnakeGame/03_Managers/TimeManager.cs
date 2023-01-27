using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
namespace SnakeGame
{
    public class TimeManager : LazySingleton<TimeManager>
    {
        public TimeManager() {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        private static int _timeScale = 1;
        public  static int FPS = 5;
        public  static int MS_PER_FRAME = 1000 / (FPS * _timeScale);

        public  static int TimeScale { get { return _timeScale; }
            set
            {
                _timeScale = value;
                MS_PER_FRAME = 1000 / (FPS * _timeScale);
            } }

        private Stopwatch _stopwatch;
        private long _currentMs = 0;
        private long _elapsed = 0;

        public long ElapsedMs { get { return _elapsed; } }

        public void ResetTimeScale()
        {
            TimeScale = 1;
        }

        public void Update()
        {
            _elapsed = _stopwatch.ElapsedMilliseconds - _currentMs;
            _currentMs = _stopwatch.ElapsedMilliseconds;
        }

        public void Release()
        {
            _stopwatch.Stop();
        }
    }
}
