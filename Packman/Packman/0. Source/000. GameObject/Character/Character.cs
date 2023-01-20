using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Character : GameObject
    {
        protected RenderManager _renderManager;

        public Character( int x, int y, string image, ConsoleColor color )
            : base( x, y, image, color )
        {
            _renderManager = RenderManager.Instance;
        }

        public override void Update()
        {
            base.Update();
        }

        public void MoveDirection( int dirX, int dirY )
        {
            // 움직일 필요가 없다면 리턴..
            if ( 0 == dirX && 0 == dirY )
            {
                return;
            }

            _renderManager.ReserveRenderRemove( _x, _y, 1 );

            _x += dirX;
            _y += dirY;
        }
    }
}
