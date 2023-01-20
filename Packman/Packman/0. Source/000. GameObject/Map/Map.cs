using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Map : GameObject
    {
        private Tile[,] _tiles;
        private int _width;
        private int _height;

        public bool Initialize( int width, int height, Tile.Kind[] tileKindTable, ConsoleColor tileColor )
        {
            // 맵 가로, 세로 사이즈가 0이하일 경우..
            if ( width <= 0 || height <= 0 )
            {
                return false;
            }

            _width = width;
            _height = height;

            _tiles = new Tile[_height, _width];
            for ( int y = 0; y < _height; ++y )
            {
                for ( int x = 0; x < _width; ++x )
                {
                    _tiles[y, x] = new Tile(x, y, tileKindTable[x + y * _width], tileColor );
                }
            }

            return true;
        }

        public override void Update()
        {

        }

        public override void Render()
        {

        }
    }
}
