using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman.Source
{
    internal class Character : GameObject
    {
        public Character( int x, int y, string image, ConsoleColor color )
            : base( x, y )
        {

        }

        public void MoveDirection(int dirX, int dirY)
        {
            _x += dirX;
            _y += dirY;
        }
    }
}
