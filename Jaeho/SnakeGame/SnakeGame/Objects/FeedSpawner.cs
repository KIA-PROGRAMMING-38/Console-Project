using System.Diagnostics;

namespace ConsoleGame
{
    public class FeedSpawner : Singleton<FeedSpawner>
    {
        public FeedSpawner() 
        {
        }
        private List<Feed> _feedList = new List<Feed>();
        private HashSet<Feed> _removePendingList = new HashSet<Feed>();
        private bool[,] _currentSpawnAbleArea;
        private int _spawnInterval;
        long time = 0;

        public void StartSpawn(bool[,] spawnAbleArea, int spawnInterval)
        {
            time = 0;
            _feedList.Clear();
            _currentSpawnAbleArea = spawnAbleArea;
            Debug.Assert(_currentSpawnAbleArea != null);
            _spawnInterval = spawnInterval;
        }
        public void FeedClear()
        {
            foreach (var feed in _feedList)
            {
                if (feed.IsAlive == false)
                    _removePendingList.Add(feed);
            }
            _feedList.RemoveAll(_removePendingList.Contains);
            _removePendingList.Clear();
        }
        public void Update()
        {
            FeedClear();
            if (_feedList.Count >= 1) { return; }
            time += TimeManager.Instance.ElapsedMs;
            FeedSpawn(time);
        }
       
        public void FeedSpawn(long elapsed)
        {
            if (elapsed > _spawnInterval)
            {
                int x = 0;
                int y = 0;
                while (true)
                {
                    x = RandomManager.Instance.GetRandomRangeInt(GameDataManager.MAP_MIN_X + 1, GameDataManager.MAP_MAX_X - 1);
                    y = RandomManager.Instance.GetRandomRangeInt(GameDataManager.MAP_MIN_Y + 1, GameDataManager.MAP_MAX_Y - 1);
                    if (_currentSpawnAbleArea[y - GameDataManager.ANCHOR_TOP, x - GameDataManager.ANCHOR_LEFT] == true)
                    {
                        break;
                    }
                }
                Vector2 feedPos = new Vector2(x, y);
                Feed feed = new Feed();
                feed.Start();
                feed.Position = feedPos;

                _feedList.Add(feed);
                time = 0;
            }
        }
    }
}
