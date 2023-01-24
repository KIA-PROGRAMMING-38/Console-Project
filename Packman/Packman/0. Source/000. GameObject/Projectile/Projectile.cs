using Packman.Source;
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
        protected GoldGroup _goldGroup = null;

        private int _dirX;
        private int _dirY;

        private float _elapsedTime = 0.0f;
        private float _moveInterval = 0.05f;

        public int PrevX { get { return _x - _dirX; } }
        public int PrevY { get { return _y - _dirY; } }

        public Projectile( int x, int y, string image, ConsoleColor color, int dirX, int dirY )
            : base( x, y, image, color, 5 )
        {
            _dirX = dirX;
            _dirY = dirY;

            _map = _objectManager.GetGameObject<Map>();

            _goldGroup = ObjectManager.Instance.GetGameObject<GoldGroup>();
        }

        public override void Update()
        {
            base.Update();

            UpdateMovement();
        }

        public override void Release()
        {
            base.Release();
        }

        public override void OnCollision( GameObject collisionObjectInst )
        {
            base.OnCollision( collisionObjectInst );

            RemoveObject();
        }

        private void RemoveObject()
        {
            _objectManager.RemoveObject( this );
            CollisionManager.Instance.RenewObjectInstance();
            RenderManager.Instance.RemoveRenderer( GetComponent<Renderer>( "Renderer" ) );

            _dirX = _dirY = 0;
            _goldGroup.OnProjectileMove( this );
        }

        private void UpdateMovement()
        {
            _elapsedTime += _timer.ElaspedTime;
            if(_elapsedTime < _moveInterval )
            {
                return;
            }

            _elapsedTime = 0.0f;

            int moveDestinationX = _x + _dirX;
            int moveDestinationY = _y + _dirY;

            RenderManager.Instance.ReserveRenderRemove( _x, _y, 1 );

            Tile tile = _map.GetTile( moveDestinationX, moveDestinationY );
            if ( Tile.Kind.Empty != tile.MyKind )
            {
                RemoveObject();
            }
            else
            {
                _x = moveDestinationX;
                _y = moveDestinationY;

                _goldGroup.OnProjectileMove( this );
            }
        }
    }
}
