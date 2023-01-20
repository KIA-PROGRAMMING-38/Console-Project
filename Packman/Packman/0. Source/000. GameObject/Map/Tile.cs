using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Tile : GameObject
    {
        public enum Kind
        {
            Empty,
            Block
        }

        private Kind _kind;

        public Tile( int x, int y, Kind kind, ConsoleColor color )
        {
            _x = x;
            _y = y;
            _kind = kind;
            _color = color;

            switch ( _kind )
            {
                case Kind.Empty:
                    _image = " ";
                    break;

                case Kind.Block:
                    _image = "■";
                    break;
            }
        }
    }
}
