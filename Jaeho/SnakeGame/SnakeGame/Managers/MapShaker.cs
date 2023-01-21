using ConsoleGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class MapShaker : Singleton<MapShaker>
    {
        private bool _shakeFlagOn = false;
        private long _millsecond = 0;
        private int _shakePowerX = 0;
        private int _shakePowerY = 0;
        public void SetShakeFlag(bool flag, long millsecond, int shakePowerX, int shakePowerY)
        {
            _shakeFlagOn = flag;
            _millsecond= millsecond;
            _shakePowerX = shakePowerX;
            _shakePowerY = shakePowerY;
        }

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
                    time -= TimeManager.Instance.ElapsedMs;
                    int xRandomValue = RandomManager.Instance.GetRandomRangeInt(-_shakePowerX, _shakePowerX);
                    int yRandomValue = RandomManager.Instance.GetRandomRangeInt(-_shakePowerY, _shakePowerY);
                    Console.MoveBufferArea(startPos.X, startPos.Y, mapWidth, mapHeight, startPos.X + xRandomValue, startPos.Y + yRandomValue);
                    Thread.Sleep(TimeManager.MS_PER_FRAME);
                    Console.MoveBufferArea(startPos.X + xRandomValue, startPos.Y + yRandomValue, mapWidth, mapHeight, startPos.X, startPos.Y);
                }
                _shakeFlagOn = false;
                _millsecond = 0;
                _shakePowerX = 0;
                _shakePowerY = 0;
            }
        }
    }
}
