using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class PunchMissile : Projectile
    {
        public PunchMissile( int x, int y, int dirX, int dirY )
            : base( x, y, Constants.PUNCH_IMAGE, Constants.PUNCH_COLOR, dirX, dirY )
        {

        }

        public override void OnCollision( GameObject collisionObjectInst )
        {
            Monster monster = (Monster)collisionObjectInst;

            monster.OnCHangedForcedToPushState( _dirX, _dirY, 100.0f );

            base.OnCollision( collisionObjectInst );
        }
    }
}
