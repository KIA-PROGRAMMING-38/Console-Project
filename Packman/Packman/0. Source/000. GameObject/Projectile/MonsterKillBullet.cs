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
            : base( x, y, Constants.MONSTER_KILL_PROJECTILE_IMAGE, Constants.MONSTER_KILL_PROJECTILE_COLOR, dirX, dirY, 5 )
        {

        }

        public override void OnCollision( GameObject collisionObjectInst )
        {
            base.OnCollision( collisionObjectInst );

            Monster monster = (Monster)collisionObjectInst;
            if(null != monster)
            {
                monster.OnDead();
            }
        }
    }
}
