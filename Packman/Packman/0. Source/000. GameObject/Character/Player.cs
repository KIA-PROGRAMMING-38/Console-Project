using Packman.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Player : Character
    {
        public Player( int x, int y )
            : base( x, y, ObjectImageColor.PLAYER_IMAGE, ObjectImageColor.PLAYER_COLOR )
        {

        }
    }
}
