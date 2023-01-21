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
            return Path.Combine( "..\\..\\..\\Resources", "Data", fileName + "." + fileExtension );
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
            string[] mapFileMetadata = lines[0].Split(' ');
            string[] playerFileMetadata = lines[1].Split(' ');
            string[] MonsterFileMetadata = lines[2].Split(' ');

            // 각 파일별로 파싱 진행..
            PasingMapData( mapFileMetadata[0], mapFileMetadata[1] );
            PasingPlayerData( playerFileMetadata[0], playerFileMetadata[1] );
            PasingMonsterData( MonsterFileMetadata[0], MonsterFileMetadata[1] );
        }

        /// <summary>
        /// Map File 을 파싱합니다.
        /// </summary>
        /// <param name="fileName"> 맵 파일 이름 </param>
        /// <param name="fileExtension"> 맵 파일 확장자 </param>
        private static void PasingMapData(string fileName, string fileExtension)
        {
            string path = MakePath(fileName, fileExtension);
            string[] lines = ReadFile(path);

            string[] mapMetadata = lines[0].Split(' ');
            int mapPosX = int.Parse(mapMetadata[0]);
            int mapPosY = int.Parse(mapMetadata[1]);
            int mapWidth = int.Parse(mapMetadata[2]);
            int mapHeight = int.Parse(mapMetadata[3]);

            Map map = new Map(mapPosX, mapPosY);
            Tile.Kind[,] tileKind = new Tile.Kind[mapHeight, mapWidth];

            int lineCount = lines.Length;
            for ( int y = 1; y < lineCount; ++y )
            {
                for ( int x = 0; x < mapWidth; ++x )
                {
                    Tile.Kind curTileKind = Tile.Kind.Empty;
                    bool isCreateGoldObject = false;

                    int curIndexX = x;
                    int curIndexY = y - 1;

                    switch ( lines[y][x] )
                    {
                        case '0':
                            curTileKind = Tile.Kind.Empty;
                            isCreateGoldObject = false;

                            break;
                        case 'W':
                            curTileKind = Tile.Kind.Block;
                            isCreateGoldObject = false;

                            break;
                        case ' ':
                            curTileKind = Tile.Kind.Empty;
                            isCreateGoldObject = true;

                            break;
                        default:
                            Debug.Assert( false );
                            return;
                    }

                    tileKind[curIndexY, curIndexX] = curTileKind;

                    if( true == isCreateGoldObject )
                    {
                        Gold gold = new Gold(curIndexX + mapPosX, curIndexY + mapPosY);
                        Debug.Assert( gold.Initialize() );

                        Debug.Assert( ObjectManager.Instance.AddGameObject( $"Gold{x:D2}{y:D2}", gold ) );
                    }
                }
            }

            // Map 초기화 및 ObjectManager 에 넣어주기..
            Debug.Assert( map.Initialize( mapWidth, mapHeight, tileKind ) );
            ObjectManager.Instance.AddGameObject( "Map", map );

            // Gold Group 객체 생성 및 ObjectManager 에 넣어주기..
            GoldGroup goldGroup = new GoldGroup(mapWidth, mapHeight);
            goldGroup.Initialize();
            ObjectManager.Instance.AddGameObject( "GoldGroup", goldGroup );
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

            Player player = new Player(playerX, playerY);
            Debug.Assert( player.Initialize() );

            Debug.Assert( ObjectManager.Instance.AddGameObject( "Player", player ) );
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
