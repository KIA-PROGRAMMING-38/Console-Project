using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Projectile : GameObject
    {
        protected Map _map = null;

        private int _dirX;
        private int _dirY;

        public Projectile( int x, int y, int dirX, int dirY )
            : base( x, y, 5 )
        {
            _dirX = dirX;
            _dirY = dirY;

            _map = _objectManager.GetGameObject<Map>();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Release()
        {
            base.Release();

            _objectManager.RemoveObject( this );
        }

        private void UpdateMovement()
        {
            int moveDestinationX = _x + _dirX;
            int moveDestinationY = _y + _dirY;

            Tile tile = _map.GetTile( moveDestinationX, moveDestinationY );
            if ( Tile.Kind.Empty != tile.MyKind )
            {
                CollisionManager.Instance.RenewObjectInstance();
            }
        }
    }
}
