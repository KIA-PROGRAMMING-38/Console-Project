using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class MonsterKillBullet : Projectile
    {
        public MonsterKillBullet( int x, int y, int dirX, int dirY )
            : base( x, y, "", Constants.MONSTER_KILL_PROJECTILE_COLOR, dirX, dirY, 6 )
        {
            if ( 1 == dirX )
            {
                _image = Constants.MONSTER_KILL_PROJECTILE_IMAGE[0].ToString();
            }
            else if ( -1 == dirX )
            {
                _image = Constants.MONSTER_KILL_PROJECTILE_IMAGE[1].ToString();
            }
            else if ( 1 == dirY )
            {
                _image = Constants.MONSTER_KILL_PROJECTILE_IMAGE[2].ToString();
            }
            else
            {
                _image = Constants.MONSTER_KILL_PROJECTILE_IMAGE[3].ToString();
            }
        }

        public override void OnCollision( GameObject collisionObjectInst )
        {
            base.OnCollision( collisionObjectInst );

            Monster monster = (Monster)collisionObjectInst;
            if ( null != monster )
            {
                monster.OnDead();
            }
        }
    }
}
