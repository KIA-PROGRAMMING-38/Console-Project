using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class StunBullet : Projectile
    {
        public StunBullet( int x, int y, int dirX, int dirY )
            : base( x, y, Constants.STUN_IMAGE, Constants.STUN_COLOR, dirX, dirY )
        {

        }

        public override void OnCollision( GameObject collisionObjectInst )
        {
            base.OnCollision( collisionObjectInst );

            Monster monster = (Monster)collisionObjectInst;
            monster.OnChangedStunState( 1.0f );
        }
    }
}
