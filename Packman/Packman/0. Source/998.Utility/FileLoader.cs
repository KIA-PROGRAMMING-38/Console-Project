using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class FileLoader
    {
        private FileLoader() { }

        /// <summary>
        /// 파일을 Load합니다..
        /// </summary>
        /// <param name="fileName"> Load할 파일 이름 </param>
        /// <param name="fileExtension"> Load할 파일 확장자 </param>
        /// <returns></returns>
        public static void Load( string fileName, string fileExtension )
        {
            string path = MakePath(fileName, fileExtension);

            string[] lines = ReadFile(path);

            PasingStageData( lines );
        }

        /// <summary>
        /// 파일 경로를 만들어 반환합니다..
        /// </summary>
        /// <param name="fileName"> 파일 이름 </param>
        /// <param name="fileExtension"> 파일 확장자 </param>
        /// <returns></returns>
        public static string MakePath(string fileName, string fileExtension)
        {
            return Path.Combine( "..\\..\\..\\Assets", "Data", fileName + "." + fileExtension );
        }

        /// <summary>
        /// 파일을 읽어와 모든 파일 안의 문자열들을 반환합니다.
        /// </summary>
        /// <param name="path"> 파일 경로 </param>
        /// <returns> 파일 안의 문자열들( 배열의 원소에는 한 줄의 문자열이 담겨있습니다 ) </returns>
        public static string[] ReadFile(string path )
        {
            Debug.Assert( File.Exists( path ) );

            return File.ReadAllLines( path );
        }

        /// <summary>
        /// Stage File 을 파싱합니다.
        /// </summary>
        /// <param name="lines"> Steg File 의 문자열들 </param>
        private static void PasingStageData( string[] lines )
        {
            PasingMapData( lines );
        }

        /// <summary>
        /// Map File 을 파싱합니다.
        /// </summary>
        /// <param name="fileName"> 맵 파일 이름 </param>
        /// <param name="fileExtension"> 맵 파일 확장자 </param>
        //private static void PasingMapData(string fileName, string fileExtension)
        private static void PasingMapData( string[] lines )
        {
            //string path = MakePath(fileName, fileExtension);
            //string[] lines = ReadFile(path);

            // 파싱 시 각 문자열들이 어떤 의미를 가지는지 미리 선언..
            const char EMPTY_TILE_SYMBOL = '0';
            const char GOLD_TILE_SYMBOL = ' ';
            const char BLOCK_TILE_SYMBOL = '▮';
            const char MONSTER_TILE_SYMBOL = 'M';
            const char PLAYER_TILE_SYMBOL = 'P';
            const char WAYPOINT_TILE_SYMBOL = '*';

            // 맵 데이터 가져와 파싱..
            string[] mapMetadata = lines[0].Split(' ');
            int mapPosX = int.Parse(mapMetadata[0]);
            int mapPosY = int.Parse(mapMetadata[1]);
            int mapWidth = int.Parse(mapMetadata[2]);
            int mapHeight = int.Parse(mapMetadata[3]);
            // 맵 인스턴스 미리 생성..
            Map map = new Map(mapPosX, mapPosY);
            // 맵의 타일들에게 필요한 정보 미리 생성..
            Tile.Kind[,] tileKind = new Tile.Kind[mapHeight, mapWidth];
            string[,] tileImages = new string[mapHeight, mapWidth];

            // Gold 는 GoldGroup이 관리할 것이기 때문에 저장한 후 한번에 넘길 것..
            LinkedList<Gold> goldList = new LinkedList<Gold>();

            // WayPoint 는 WayPointGroup 에서 관리할 것이기 때문에 저장한 후 한번에 넘길 것..
            List<WayPoint> wayPoints = new List<WayPoint>();

            // 몬스터 관련 변수..
            int monsterCount = 0;
            string[] monsterStartWaitTime = lines[lines.Length - 1].Split(' ');

            int lineCount = lines.Length - 1;
            for ( int y = 1; y < lineCount; ++y )
            {
                for ( int x = 0; x < mapWidth; ++x )
                {
                    // 파싱..
                    Tile.Kind curTileKind = Tile.Kind.Empty;
                    string curTileImage = " ";

                    int curIndexX = x;
                    int curIndexY = y - 1;
                    bool isCreateGold = false;

                    switch ( lines[y][x] )
                    {
                        case EMPTY_TILE_SYMBOL:     // 아무것도 없는 타일..
                            isCreateGold = false;

                            curTileKind = Tile.Kind.Empty;

                            break;
                        case BLOCK_TILE_SYMBOL:     // 못 가는 타일..
                            isCreateGold = false;

                            curTileKind = Tile.Kind.Block;
                            curTileImage = "▮";

                            break;
                        case GOLD_TILE_SYMBOL:     // Gold가 있는 타일..
                            isCreateGold = true;

                            curTileKind = Tile.Kind.Empty;

                            break;
                        case MONSTER_TILE_SYMBOL:   // 몬스터가 있는 타일..
                            Monster monster = new Monster(curIndexX + mapPosX, curIndexY + mapPosY, map, float.Parse(monsterStartWaitTime[monsterCount]));
                            monster.Initialize();

                            Debug.Assert( ObjectManager.Instance.AddGameObject( $"Monster_{monsterCount:D2}", monster ) );
                            ++monsterCount;

                            break;
                        case PLAYER_TILE_SYMBOL:    // 플레이어가 있는 타일..
                            Player player = new Player(curIndexX + mapPosX, curIndexY + mapPosY, map );
                            Debug.Assert( player.Initialize() );

                            Debug.Assert( ObjectManager.Instance.AddGameObject( "Player", player ) );

                            break;
                        case WAYPOINT_TILE_SYMBOL:  // WayPoint 가 있는 타일..
                            isCreateGold = false;

                            WayPoint wayPoint = new WayPoint( curIndexX + mapPosX, curIndexY + mapPosY, 0 );
                            Debug.Assert( wayPoint.Initialize() );

                            wayPoints.Add( wayPoint );

                            break;
                        default:                // 이 외는 파일 잘못 만든 것이라 강제종료..
                            Debug.Assert( false );
                            
                            return;
                    }

                    tileKind[curIndexY, curIndexX] = curTileKind;
                    tileImages[curIndexY, curIndexX] = curTileImage;

                    if ( true == isCreateGold )
                    {
                        Gold gold = new Gold(curIndexX + mapPosX, curIndexY + mapPosY);
                        Debug.Assert( gold.Initialize() );

                        goldList.AddLast( gold );
                    }
                }
            }

            // Map 초기화 및 ObjectManager 에 넣어주기..
            Debug.Assert( map.Initialize( mapWidth, mapHeight, tileImages, tileKind ) );
            ObjectManager.Instance.AddGameObject( "Map", map );

            // Gold Group 객체 생성 및 ObjectManager 에 넣어주기..
            GoldGroup goldGroup = new GoldGroup(mapWidth + mapPosX, mapHeight + mapPosY);
            goldGroup.Initialize( goldList );
            ObjectManager.Instance.AddGameObject( "GoldGroup", goldGroup );

            // WayPoint Group 객체 생성 및 ObjectManager 에 넣어주기..
            WayPointGroup wayPointGroup = new WayPointGroup();
            wayPointGroup.Initialize( wayPoints );
            ObjectManager.Instance.AddGameObject( "WayPointGroup", wayPointGroup );
        }
    }
}
