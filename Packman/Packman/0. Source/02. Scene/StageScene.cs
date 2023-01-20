using Packman._0._Source._000._GameObject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class StageScene : Scene
    {
        public override bool Initialize()
        {
            Debug.Assert( true == InitMap( 0, 0, 70, 30 ) );
            Debug.Assert( true == InitPlayer( 10, 2 ) );
            Debug.Assert( true == InitMonster() );

            return true;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Render()
        {
            base.Render();
        }

        private bool InitPlayer(int playerX, int playerY)
        {
            Player player = new Player(playerX, playerY);
            player.Initialize();

            _objectManager.AddGameObject( "Player", player );

            return true;
        }

        private bool InitMonster()
        {
            Monster monster = new Monster(65, 28, _objectManager.GetGameObject<Map>("Map"));
            monster.Initialize();

            _objectManager.AddGameObject( "Monster_00", monster );

            return true;
        }

        private bool InitMap( int mapStartX, int mapStartY, int mapWidth, int mapHeight )
        {
            Tile.Kind[,] tileKinds = new Tile.Kind[mapHeight, mapWidth];
            for ( int y = 0; y < mapHeight; ++y )
            {
                for ( int x = 0; x < mapWidth; ++x )
                {
                    tileKinds[y, x] = Tile.Kind.Empty;
                }
            }

            for( int y = 0; y < mapHeight; ++y )
            {
                tileKinds[y, 0] = Tile.Kind.Block;
                tileKinds[y, mapWidth - 1] = Tile.Kind.Block;
            }

            for ( int x = 0; x < mapWidth; ++x )
            {
                tileKinds[0, x] = Tile.Kind.Block;
                tileKinds[mapHeight - 1, x] = Tile.Kind.Block;
            }

            for( int i = 0; i < 200; ++i )
            {
                int randomX = 0;
                int randomY = 0;

                int playerX = 10;
                int playerY = 2;
                int monsterX = 65;
                int monsterY = 28;

                while ( true )
                {
                    randomX = RandomManager.Instance.GetRandomNumber( mapWidth - 1 );
                    randomY = RandomManager.Instance.GetRandomNumber( mapHeight - 1 );

                    if ( tileKinds[randomY, randomX] == Tile.Kind.Block )
                        continue;
                    if ( (playerX == randomX && playerY == randomY) || (monsterX == randomX && monsterY == randomY) )
                        continue;

                    break;
                }

                tileKinds[randomY, randomX] = Tile.Kind.Block;
            }

            Map map = new Map(mapStartX, mapStartY);

            map.Initialize( mapWidth, mapHeight, tileKinds );

            _objectManager.AddGameObject( "Map", map );

            return true;
        }
    }
}
