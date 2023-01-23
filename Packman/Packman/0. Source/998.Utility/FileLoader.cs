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
        private static string MakePath(string fileName, string fileExtension)
        {
            return Path.Combine( "..\\..\\..\\Assets", "Data", fileName + "." + fileExtension );
        }

        /// <summary>
        /// 파일을 읽어와 모든 파일 안의 문자열들을 반환합니다.
        /// </summary>
        /// <param name="path"> 파일 경로 </param>
        /// <returns> 파일 안의 문자열들( 배열의 원소에는 한 줄의 문자열이 담겨있습니다 ) </returns>
        private static string[] ReadFile(string path )
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
            //string[] mapFileMetadata = lines[0].Split(' ');
            //string[] playerFileMetadata = lines[1].Split(' ');
            //string[] MonsterFileMetadata = lines[2].Split(' ');
            //
            //// 각 파일별로 파싱 진행..
            //PasingMapData( mapFileMetadata[0], mapFileMetadata[1] );
            //PasingPlayerData( playerFileMetadata[0], playerFileMetadata[1] );
            //PasingMonsterData( MonsterFileMetadata[0], MonsterFileMetadata[1] );

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
            const char BLOCK_TILE_SYMBOL = '#';
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
            
            // Gold 는 GoldGroup이 관리할 것이기 때문에 저장한 후 한번에 넘길 것..
            LinkedList<Gold> goldList = new LinkedList<Gold>();

            // WayPoint 는 WayPointGroup 에서 관리할 것이기 때문에 저장한 후 한번에 넘길 것..
            List<WayPoint> wayPoints = new List<WayPoint>();

            // 몬스터 관련 변수..
            int monsterCount = 0;

            int lineCount = lines.Length;
            for ( int y = 1; y < lineCount; ++y )
            {
                for ( int x = 0; x < mapWidth; ++x )
                {
                    // 파싱..
                    Tile.Kind curTileKind = Tile.Kind.Empty;

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

                            break;
                        case GOLD_TILE_SYMBOL:     // Gold가 있는 타일..
                            isCreateGold = true;

                            curTileKind = Tile.Kind.Empty;

                            break;
                        case MONSTER_TILE_SYMBOL:   // 몬스터가 있는 타일..
                            Monster monster = new Monster(curIndexX + mapPosX, curIndexY + mapPosY, map);
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
                            isCreateGold = true;

                            WayPoint wayPoint = new WayPoint( curIndexX + mapPosX, curIndexY + mapPosY, 0 );
                            Debug.Assert( wayPoint.Initialize() );

                            wayPoints.Add( wayPoint );

                            break;
                        default:                // 이 외는 파일 잘못 만든 것이라 강제종료..
                            Debug.Assert( false );
                            return;
                    }

                    tileKind[curIndexY, curIndexX] = curTileKind;

                    if ( true == isCreateGold )
                    {
                        Gold gold = new Gold(curIndexX + mapPosX, curIndexY + mapPosY);
                        Debug.Assert( gold.Initialize() );

                        goldList.AddLast( gold );
                    }
                }
            }

            // Map 초기화 및 ObjectManager 에 넣어주기..
            Debug.Assert( map.Initialize( mapWidth, mapHeight, tileKind ) );
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

        /// <summary>
        /// Player File 을 파싱합니다.
        /// </summary>
        /// <param name="fileName"> 플레이어 파일 이름 </param>
        /// <param name="fileExtension"> 플레이어 파일 확장자 </param>
        private static void PasingPlayerData( string fileName, string fileExtension )
        {
            string path = MakePath(fileName, fileExtension);
            string[] lines = ReadFile(path);

            string[] playerMetadata = lines[0].Split(' ');

            int playerX = int.Parse(playerMetadata[0]);
            int playerY = int.Parse(playerMetadata[1]);

            //Player player = new Player(playerX, playerY);
            //Debug.Assert( player.Initialize() );
            //
            //Debug.Assert( ObjectManager.Instance.AddGameObject( "Player", player ) );
        }

        /// <summary>
        /// Monster File 을 파싱합니다.
        /// </summary>
        /// <param name="fileName"> 몬스터 파일 이름 </param>
        /// <param name="fileExtension"> 몬스터 파일 확장자 </param>
        private static void PasingMonsterData( string fileName, string fileExtension )
        {
            Map map = ObjectManager.Instance.GetGameObject<Map>();
            Debug.Assert( null != map );

            string path = MakePath(fileName, fileExtension);
            string[] lines = ReadFile(path);

            for( int curLineIndex = 0; curLineIndex < lines.Length; ++curLineIndex )
            {
                string[] monsterMetadata = lines[curLineIndex].Split(' ');

                int monsterPosX = int.Parse(monsterMetadata[0]);
                int monsterPosY = int.Parse(monsterMetadata[1]);

                Monster monster = new Monster(monsterPosX, monsterPosY, map );
                monster.Initialize();

                Debug.Assert( ObjectManager.Instance.AddGameObject( $"Monster_{curLineIndex:D2}", monster ) );
            }
        }
    }
}
