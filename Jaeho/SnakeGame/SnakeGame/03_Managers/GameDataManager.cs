using System.Diagnostics;
using SnakeGame;

namespace SnakeGame
{
    public class GameDataManager : LazySingleton<GameDataManager>
    {
        public struct MapInfo
        {
            public int NeedFeedCount;
            public int SpawnInterval;
            public int Min_X;
            public int Min_Y;

            public int Max_X;
            public int Max_Y;

            public Vector2 PlayerPosition;
            public Vector2[] WallPosisions;
            public bool[,] MapSpawnableTable;
        }

        //◯☺ ▢ 𐍈 ꔮ ё Ö
        public const char PLAYER_ICON = 'Ö';
        public const char SNAKEBODY_ICON = '▢';

        //▮ ▢ ◯▮
        public  const char WALL_ICON = '▮';
        public  const char FEED_ICON = '☺';

        public  static readonly string ResourcePath = Path.Combine(@"..\..\..\","07_Assets");

        private int _needStageClearFeedCount = 0;
        private int _currentFeedCount = 0;

        public  int CurrentFeedCount  { get { return _currentFeedCount; } set { _currentFeedCount = value; } }
        public  int NeedClearFeedCount { get { return _needStageClearFeedCount; } }

        public const ConsoleColor DEFAULT_FOREGROUND_COLOR = ConsoleColor.White;

        public const int DEFAULT_SCREEN_WIDTH = 120;
        public const int DEFAULT_SCREEN_HEIGHT = 34;

        public static int _mapMinX = 0;
        public static int _mapMinY = 0;
        public static int _mapMaxX = 0;
        public static int _mapMaxY = 0;


        public const  int ANCHOR_LEFT = 36;
        public const  int ANCHOR_TOP = 10;

        public static int MAP_MAX_X { get { return ANCHOR_LEFT + _mapMaxX; } }
        public static int MAP_MIN_X { get  { return ANCHOR_LEFT + _mapMinX; } }
       
        public static int MAP_MAX_Y { get  { return ANCHOR_TOP + _mapMaxY; } }
        public static int MAP_MIN_Y { get { return ANCHOR_TOP + _mapMinY; } }

        public static int MAP_HEIGHT { get { return MAP_MAX_Y - MAP_MIN_Y; } }
        public static int MAP_WIDTH { get { return MAP_MAX_X - MAP_MIN_X; } }

        private Dictionary<string, MapInfo> _mapData  = new Dictionary<string, MapInfo>();

        /// <summary>
        /// 맵 정보를 가져옵니다.
        /// </summary>
        /// <param name="mapName">맵 이름</param>
        /// <returns></returns>
        public MapInfo GetMapData(string mapName)
        {
            MapInfo info;
            bool isSuccess = _mapData.TryGetValue(mapName, out info);
            Debug.Assert(isSuccess, $"GameDataManager._mapData Invalid Key : {mapName} !");

            SetData(info);
            return info;
        }

        /// <summary>
        /// 맵데이터를 불러옵니다.
        /// </summary>
        public void Load()
        {
            Dictionary<string, Scene> scenes = SceneManager.Instance.Scenes;

            foreach(KeyValuePair<string, Scene> sceneInfo in scenes)
            {
                // 씬중 스테이지만 불러온다
                if(sceneInfo.Value is Stage)
                {
                    _mapData.Add(sceneInfo.Key, ReadMapFile(sceneInfo.Key));
                }
            }
            
        }

        /// <summary>
        /// 게임 데이터를 재설정해줍니다.
        /// </summary>
        /// <param name="mapInfo"></param>
        public void SetData(MapInfo mapInfo)
        {
            _mapMaxX = mapInfo.Max_X;
            _mapMinX = mapInfo.Min_X;
            _mapMaxY = mapInfo.Max_Y;
            _mapMinY = mapInfo.Min_Y;
            _needStageClearFeedCount = mapInfo.NeedFeedCount;
            _currentFeedCount = 0;
        }

        /// <summary>
        /// 텍스트파일로부터 맵 정보 추출
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public MapInfo ReadMapFile(string fileName)
        {
            using (StreamReader textMapDataReader = new StreamReader(Path.Combine(ResourcePath, "MapData", fileName + ".txt")))
            {
                MapInfo mapInfo = new MapInfo();
                mapInfo.NeedFeedCount = int.Parse(textMapDataReader.ReadLine().Split()[1]);
                mapInfo.SpawnInterval= int.Parse(textMapDataReader.ReadLine().Split()[1]);
                int currentY = 0;
                List<Vector2> wallPositions = new List<Vector2>();

                int maxX = int.MinValue;

                while (!textMapDataReader.EndOfStream)
                {
                    string line = textMapDataReader.ReadLine();
                    maxX = Math.Max(maxX, line.Length);
                    for (int currentX = 0; currentX < line.Length; currentX++)
                    {
                        if ('P' == line[currentX])
                        {
                            mapInfo.PlayerPosition = new Vector2(ANCHOR_LEFT + currentX, ANCHOR_TOP + currentY);
                        }
                        if ('B' == line[currentX])
                        {
                            wallPositions.Add(new Vector2(ANCHOR_LEFT + currentX, ANCHOR_TOP + currentY));
                        }
                    }
                    currentY++;
                }

                //mapInfo.Max_X = currentX;
                mapInfo.Max_X = maxX;
                mapInfo.Max_Y = currentY;
                mapInfo.WallPosisions = wallPositions.ToArray();
                mapInfo.MapSpawnableTable = new bool[currentY, maxX];

                for (int i = 0; i < currentY; ++i)
                {
                    for (int j = 0; j < maxX; ++j)
                    {
                        mapInfo.MapSpawnableTable[i, j] = true;
                    }
                }

                for (int i = 0; i < wallPositions.Count; ++i)
                {
                    mapInfo.MapSpawnableTable[wallPositions[i].Y - ANCHOR_TOP, wallPositions[i].X - ANCHOR_LEFT] = false;
                }

                return mapInfo;
            }
        }
    }
}
