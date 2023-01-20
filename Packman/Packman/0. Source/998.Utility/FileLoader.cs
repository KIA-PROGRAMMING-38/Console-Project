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
        /// <param name="fileName"></param>
        /// <param name="fileExtension"></param>
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

        private static string[] ReadFile(string path )
        {
            Debug.Assert( File.Exists( path ) );

            return File.ReadAllLines( path );
        }

        private static void PasingStageData( string[] lines )
        {
            string[] mapFileMetadata = lines[0].Split(' ');
            string[] playerFileMetadata = lines[1].Split(' ');

            PasingMapData( mapFileMetadata[0], mapFileMetadata[1] );
            PasingPlayerData( playerFileMetadata[0], playerFileMetadata[1] );
        }

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

                    switch ( lines[y][x] )
                    {
                        case '0':
                            curTileKind = Tile.Kind.Empty;
                            break;
                        case 'W':
                            curTileKind = Tile.Kind.Block;
                            break;
                        case ' ':
                            curTileKind = Tile.Kind.Empty;
                            break;
                        default:
                            Debug.Assert( false );
                            return;
                    }

                    tileKind[y - 1, x] = curTileKind;
                }
            }

            Debug.Assert( map.Initialize( mapWidth, mapHeight, tileKind ) );

            ObjectManager.Instance.AddGameObject( "Map", map );
        }

        private static void PasingPlayerData( string fileName, string fileExtension )
        {
            string path = MakePath(fileName, fileExtension);
            string[] lines = ReadFile(path);

            string[] playerMetadata = lines[0].Split(' ');

            int playerX = int.Parse(playerMetadata[0]);
            int playerY = int.Parse(playerMetadata[1]);

            Player player = new Player(playerX, playerY);
            Debug.Assert( player.Initialize() );

            ObjectManager.Instance.AddGameObject( "Player", player );
        }
    }
}
