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

        public Character( int x, int y, string image, ConsoleColor color, int renderOrder )
            : base( x, y, image, color, renderOrder )
        {
            _renderManager = RenderManager.Instance;
            _map = _objectManager.GetGameObject<Map>();
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

            Tile.Kind curPosTileKind = _map.GetTileKind( moveDestinationX, moveDestinationY );
            if( Tile.Kind.Empty == curPosTileKind )
            {
                _renderManager.ReserveRenderRemove( _x, _y, 1 );

                _x = moveDestinationX;
                _y = moveDestinationY;
            }
        }
    }
}
