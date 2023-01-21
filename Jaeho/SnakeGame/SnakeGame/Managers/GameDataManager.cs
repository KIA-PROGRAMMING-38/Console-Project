using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class GameDataManager : Singleton<GameDataManager>
    {
        public struct MapInfo
        {
            public int NeedFeedCount;
            public int Min_X;
            public int Min_Y;

            public int Max_X;
            public int Max_Y;

            public Vector2 PlayerPosition;
            public Vector2[] WallPosisions;
            public bool[,] MapSpawnableTable;
        }

        private int _needStageClearFeedCount = 0;
        private int _currentFeedCount = 0;

        public  int CurrentFeedCount { get { return _currentFeedCount; } set { _currentFeedCount = value; } }
        public  int NeedClearFeedCount { get { return _needStageClearFeedCount; } set { _needStageClearFeedCount = value; } }


        public static int _mapMinX = 0;
        public static int _mapMinY = 0;
        public static int _mapMaxX = 0;
        public static int _mapMaxY = 0;


        public const int ANCHOR_LEFT = 30;
        public const int ANCHOR_TOP = 7;

        public static int MAP_MAX_X { get { return ANCHOR_LEFT + _mapMaxX; } }
        public static int MAP_MIN_X { get  { return ANCHOR_LEFT + _mapMinX; } }
       
        public static int MAP_MAX_Y { get  { return ANCHOR_TOP + _mapMaxY; } }
        public static int MAP_MIN_Y { get { return ANCHOR_TOP + _mapMinY; } }

        public void SetData(MapInfo mapInfo)
        {
            _mapMaxX = mapInfo.Max_X;
            _mapMinX = mapInfo.Min_X;
            _mapMaxY = mapInfo.Max_Y;
            _mapMinY = mapInfo.Min_Y;
            _needStageClearFeedCount = mapInfo.NeedFeedCount;
            _currentFeedCount = 0;
        }

        public MapInfo ReadMapFile(string fileName)
        {
            string resourcePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            using (StreamReader textMapDataReader = new StreamReader(Path.Combine(resourcePath,"Assets", "MapData", fileName + ".txt")))
            {
                MapInfo mapInfo = new MapInfo();
                mapInfo.NeedFeedCount = int.Parse(textMapDataReader.ReadLine());
                int currentX = 0;
                int currentY = 0;
                List<Vector2> wallPositions = new List<Vector2>();
                while (!textMapDataReader.EndOfStream)
                {
                    string line = textMapDataReader.ReadLine();
                    for (currentX = 0; currentX < line.Length; currentX++)
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

                mapInfo.Max_X = currentX;
                mapInfo.Max_Y = currentY;
                mapInfo.WallPosisions = wallPositions.ToArray();
                mapInfo.MapSpawnableTable = new bool[currentY, currentX];

                for (int i = 0; i < currentY; ++i)
                {
                    for (int j = 0; j < currentX; ++j)
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
