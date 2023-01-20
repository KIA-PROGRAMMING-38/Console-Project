using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Monster : Character
    {
        public enum State
        {
            Idle,
            Chase,
        }

        private State _curState;
        private Map _mapObject;
        private Player _player;

        private List<Tile> paths = new List<Tile>();
        private int curPathIndex = 0;

        public Monster( int x, int y, Map map )
            : base( x, y, Constants.MONSTER_IMAGE, Constants.MONSTER_COLOR )
        {
            _curState = State.Idle;
            _mapObject = map;
            _player = _objectManager.GetGameObject<Player>( "Player" );
        }

        public override void Update()
        {
            base.Update();

            switch ( _curState )
            {
                case State.Idle:
                    ActionFindPath( _player.X, _player.Y );
                    break;

                case State.Chase:
                    ActionFindPath( _player.X, _player.Y );

                    MoveToNextPath();
                    break;
            }
        }

        private void ActionFindPath(int destinationX, int destinationY)
        {
            paths.Clear();

            if( true == PathFinder.ComputePath( _mapObject, _x, _y, destinationX, destinationY, ref paths ) )
            {
                _curState = State.Chase;
                curPathIndex = 0;
            }
        }

        private void MoveToNextPath()
        {
            if( 0 >= paths.Count )
            {
                return;
            }

            int dirX = paths[curPathIndex].X - _x;
            int dirY = paths[curPathIndex].Y - _y;

            MoveDirection( dirX, dirY );
        }
    }
}
