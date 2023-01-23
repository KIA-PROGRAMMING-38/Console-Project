using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Map : GameObject
    {
        private Tile[,]? _tiles = null;
        private int _width = 0;
        private int _height = 0;

        public int Width { get { return _width; } }
        public int Height { get { return _height; } }

        public Map(int x, int y)
            : base( x, y, 0 )
        {

        }

        /// <summary>
        /// Map 을 초기화합니다..
        /// </summary>
        /// <param name="width"> Map Width </param>
        /// <param name="height"> Map Height </param>
        /// <param name="tileKindTable"> Tile의 상태를 저장하는 룩업테이블 </param>
        /// <returns></returns>
        public bool Initialize( int width, int height, Tile.Kind[,]? tileKindTable )
        {
            base.Initialize();

            // 맵 가로, 세로 사이즈가 0이하일 경우..
            if ( width <= 0 || height <= 0 )
            {
                return false;
            }

            _width = width;
            _height = height;

            Debug.Assert( null != tileKindTable );

            _tiles = new Tile[_height, _width];
            for ( int y = 0; y < _height; ++y )
            {
                for ( int x = 0; x < _width; ++x )
                {
                    int tileX = x + _x;
                    int tileY = y + _y;

                    Tile newTile = new Tile( tileX, tileY, tileKindTable[y, x], Constants.MAP_WALL_COLOR );
                    Debug.Assert( newTile.Initialize() );

                    _tiles[y, x] = newTile;
                }
            }

            Render();

            return true;
        }

        /// <summary>
        /// Map Update..
        /// </summary>
        public override void Update()
        {
            //base.Update();

            //for ( int y = 0; y < _height; ++y )
            //{
            //    for ( int x = 0; x < _width; ++x )
            //    {
            //        _tiles?[y, x].Update();
            //    }
            //}
        }

        public override void Render()
        {
            //base.Render();

            for ( int y = 0; y < _height; ++y )
            {
                for ( int x = 0; x < _width; ++x )
                {
                    _tiles?[y, x].Render();
                }
            }
        }

        public Tile GetTile(int x, int y)
        {
            return _tiles[y - _y, x - _x];
        }

        public Tile.Kind GetTileKind(int x, int y)
        {
            return _tiles[y - _y, x - _x].MyKind;
        }
    }
}
