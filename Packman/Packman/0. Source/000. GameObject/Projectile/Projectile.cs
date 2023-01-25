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
        public enum MoveKind
        {
            Straight,   // 일직선으로 나아가다 벽과 충돌 시 없어짐..
            Bend,       // 일직선으로 나아가다 벽과 충돌 시 다른 방향으로 휜다..
        }

        protected Map _map = null;
        protected GoldGroup _goldGroup = null;

        private MoveKind _moveKind = MoveKind.Straight;

        private int _prevX;
        private int _prevY;

        protected int _dirX;
        protected int _dirY;

        private float _elapsedTime = 0.0f;
        private float _moveInterval = 0.05f;

        private int _lifeTime = 0;

        public int PrevX { get { return _prevX; } }
        public int PrevY { get { return _prevY; } }

        public Projectile( int x, int y, string image, ConsoleColor color, int dirX, int dirY, int lifeTime = 0, MoveKind moveKind = MoveKind.Straight )
            : base( x, y, image, color, 5 )
        {
            _lifeTime = lifeTime;
            _moveKind = moveKind;

            _dirX = dirX;
            _dirY = dirY;

            _prevX = _x;
            _prevY = _y;

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

            _prevX = _x;
            _prevY = _y;

            Tile tile = _map.GetTile( moveDestinationX, moveDestinationY );
            if ( Tile.Kind.Empty != tile.MyKind )
            {
                switch ( _moveKind )
                {
                    case MoveKind.Straight:
                        RemoveObject();
                        break;

                    case MoveKind.Bend:
                        ComputeNextDir();
                        break;
                }
            }
            else
            {
                _x = moveDestinationX;
                _y = moveDestinationY;

                _goldGroup.OnProjectileMove( this );

                --_lifeTime;

                if ( 0 == _lifeTime )
                {
                    RemoveObject();
                }
            }
        }

        private void ComputeNextDir()
        {
            int moveDirX = _dirX;
            int moveDirY = _dirY;

            // 현재 방향 기준 왼쪽이 갈 수 있는 타일인지 검사..
            moveDirX = _dirY * -1;
            moveDirY = _dirX * -1;
            Tile tile = _map.GetTile(_x + moveDirX, _y + moveDirY);
            if ( null != tile && Tile.Kind.Empty == tile.MyKind )
            {
                _dirX = moveDirX;
                _dirY = moveDirY;

                return;
            }

            // 현재 방향 기준 오른쪽이 갈 수 있는 타일인지 검사..
            moveDirX = _dirY;
            moveDirY = _dirX;
            tile = _map.GetTile(_x + moveDirX, _y + moveDirY);
            if ( null != tile && Tile.Kind.Empty == tile.MyKind )
            {
                _dirX = moveDirX;
                _dirY = moveDirY;

                return;
            }

            // 현재 방향 기준 뒤쪽이 갈 수 있는 타일인지 검사..
            moveDirX = _dirX * -1;
            moveDirY = _dirY * -1;
            tile = _map.GetTile(_x + moveDirX, _y + moveDirY);
            if ( null != tile && Tile.Kind.Empty == tile.MyKind )
            {
                _dirX = moveDirX;
                _dirY = moveDirY;

                return;
            }
        }
    }
}
