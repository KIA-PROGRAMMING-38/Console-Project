using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Character : GameObject
    {
        public Action<Character> OnMoveCharacterEvent = null;
        public Action<Character> OnCharacterMoveFailedEvent = null;

        // 필요한 인스턴스들 변수에 저장..
        protected RenderManager _renderManager = null;
        protected Map _map = null;

        // 캐릭터를 움직이기 위한 컴포넌트..
        protected CharacterMovement _movementComponent = null;

        // 이전 좌표..
        protected int _prevX;
        protected int _prevY;
        // 움직이는 방향..
        protected int _dirX;
        protected int _dirY;

		public int PrevX { get { return _prevX; } }
        public int PrevY { get { return _prevY; } }

        public Character( int x, int y, string image, ConsoleColor color, int renderOrder, Map map, float moveDelay )
            : base( x, y, image, color, renderOrder )
        {
            _renderManager = RenderManager.Instance;
            _map = map;

            _movementComponent = new CharacterMovement( this, OnMoveDirection, moveDelay );
        }

        public override void Update()
        {
            base.Update();
        }

        public void SendMoveDirOrder(int dirX, int dirY )
        {
            _movementComponent.Action( dirX, dirY );
        }

        protected bool IsCanGoPosition(int posX, int posY)
        {
            Tile.Kind curPosTileKind = _map.GetTileKind( posX, posY );
            if ( Tile.Kind.Empty == curPosTileKind )
            {
                return true;
            }

            return false;
        }

        private void OnMoveDirection( int dirX, int dirY )
        {
            // 움직일 필요가 없다면 리턴..
            if ( 0 == dirX && 0 == dirY )
            {
                return;
            }

            int moveDestinationX = _x + dirX;
            int moveDestinationY = _y + dirY;

            // 이동할 지점이 갈 수 있는 곳인지 검사..
            if ( IsCanGoPosition( moveDestinationX, moveDestinationY ) )
            {
                _dirX = dirX;
                _dirY = dirY;

                _renderManager.ReserveRenderRemove( _x, _y, 1 );

                _prevX = _x;
                _prevY = _y;

                _x = moveDestinationX;
                _y = moveDestinationY;

                OnMoveCharacterEvent?.Invoke( this );
            }
            else
            {
                OnCharacterMoveFailedEvent?.Invoke( this );
            }
        }
    }
}
