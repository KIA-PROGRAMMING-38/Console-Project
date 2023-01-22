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
        // 많이 사용하는 싱글톤들은 미리 멤버로 받아두기..
        private CollisionManager _collisionManager = CollisionManager.Instance;
        private StageManager _stageManager = StageManager.Instance;

        int _stageNum = 0;

        public StageScene( int stageNum )
        {
            _stageNum = stageNum;
        }

        public override bool Initialize()
        {
            FileLoader.Load( $"Stage{_stageNum:D2}", "txt" );

            return true;
        }

        public override void Update()
        {
            base.Update();

            _collisionManager.Update();
            _stageManager.Update();
        }

        public override void Render()
        {
            base.Render();
        }

        private bool InitPlayer(int playerX, int playerY)
        {
            //Player player = new Player(playerX, playerY);
            //player.Initialize();
            //
            //_objectManager.AddGameObject( "Player", player );

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
