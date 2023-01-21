using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Item : GameObject
    {
        public Item( int x, int y, string image, ConsoleColor color, int renderOrder )
            : base( x, y, image, color, renderOrder )
        {

        }

        public override void Render()
        {
            
        }
    }
}
