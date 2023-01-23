using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class StunItem : Item
    {
        public StunItem(int x, int y)
            : base(x, y, Constants.STUN_IMAGE, ConsoleColor.DarkYellow, 2)
        {

        }

        public override void OnCollision( GameObject collisionObjectInst )
        {
            Player player = (Player)collisionObjectInst;

            _objectManager.RemoveObject( this );

            base.OnCollision( collisionObjectInst );
        }
    }
}
