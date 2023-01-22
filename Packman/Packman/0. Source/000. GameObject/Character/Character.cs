using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Character : GameObject
    {
        public Action<Character> OnMoveCharacterEvent;

        protected RenderManager _renderManager;
        protected Map _map;

        protected int _prevX;
        protected int _prevY;

        protected int _dirX;
        protected int _dirY;

		public int PrevX { get { return _prevX; } }
        public int PrevY { get { return _prevY; } }

        public Character( int x, int y, string image, ConsoleColor color, int renderOrder, Map map )
            : base( x, y, image, color, renderOrder )
        {
            _renderManager = RenderManager.Instance;
            _map = map;
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

            int moveDestinationX = _x + dirX;
            int moveDestinationY = _y + dirY;

			_renderManager.ReserveRenderRemove( _prevX, _prevY, 1 );

			Tile.Kind curPosTileKind = _map.GetTileKind( moveDestinationX, moveDestinationY );
            if( Tile.Kind.Empty == curPosTileKind )
            {
                _dirX = dirX;
                _dirY = dirY;

				_prevX = _x;
                _prevY = _y;

                _x = moveDestinationX;
                _y = moveDestinationY;

                OnMoveCharacterEvent?.Invoke( this );
			}
        }
    }
}
