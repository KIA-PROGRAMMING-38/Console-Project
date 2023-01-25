namespace SnakeGame
{
    public class MapShaker : LazySingleton<MapShaker>
    {
        private bool _shakeFlagOn = false;
        private long _millsecond = 0;
        private int _shakePowerX = 0;
        private int _shakePowerY = 0;

        /// <summary>
        /// 맵을 흔드는 플래그를 켜주는 함수
        /// </summary>
        /// <param name="flag">flag</param>
        /// <param name="millsecond">흔들 시간</param>
        /// <param name="shakePowerX">-X ~ X 범위로 흔들음</param>
        /// <param name="shakePowerY">-Y ~ Y 범위로 흔들음</param>
        public void SetShakeFlag(bool flag, long millsecond, int shakePowerX, int shakePowerY)
        {
            _shakeFlagOn = flag;
            _millsecond = millsecond;
            _shakePowerX = shakePowerX;
            _shakePowerY = shakePowerY;
        }

        /// <summary>
        /// 맵을 흔들어주는 함수
        /// </summary>
        public void RenderShakedMap()
        {
            if (_shakeFlagOn == true)
            {
                Vector2 startPos = new Vector2(GameDataManager.MAP_MIN_X, GameDataManager.MAP_MIN_Y);
                int mapWidth = GameDataManager.MAP_MAX_X - GameDataManager.MAP_MIN_X;
                int mapHeight = GameDataManager.MAP_MAX_Y - GameDataManager.MAP_MIN_Y;

                long time = _millsecond;
                while (time > 0)
                {
                    time -= TimeManager.Instance.ElapsedMs / 2;
                    int xRandomValue = RandomManager.Instance.GetRandomRangeInt(-_shakePowerX, _shakePowerX);
                    int yRandomValue = RandomManager.Instance.GetRandomRangeInt(-_shakePowerY, _shakePowerY);
                    Console.MoveBufferArea(startPos.X, startPos.Y, mapWidth, mapHeight, startPos.X + xRandomValue, startPos.Y + yRandomValue);
                    Thread.Sleep((int)TimeManager.Instance.ElapsedMs/4);
                    Console.MoveBufferArea(startPos.X + xRandomValue, startPos.Y + yRandomValue, mapWidth, mapHeight, startPos.X, startPos.Y);
                    Thread.Sleep((int)TimeManager.Instance.ElapsedMs/4);
                }
                
                _shakeFlagOn = false;
                _millsecond = 0;
                _shakePowerX = 0;
                _shakePowerY = 0;
            }
        }
    }
}
