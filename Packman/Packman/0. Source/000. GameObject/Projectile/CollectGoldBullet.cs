using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class CollectGoldBullet : Projectile
    {
        public CollectGoldBullet( int x, int y, int dirX, int dirY )
            : base( x, y, Constants.COLLECT_GOLD_PROJECTILE_IMAGE, Constants.COLLECT_GOLD_PROJECTILE_COLOR, dirX, dirY, 40, MoveKind.Bend )
        {

        }

        public override void Update()
        {
            base.Update();
        }

        public override void Render()
        {
            base.Render();
        }
    }
}
