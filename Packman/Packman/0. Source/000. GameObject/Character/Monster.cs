using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private List<Tile> _paths = new List<Tile>();
        private int _curPathIndex = 0;

        private float _moveInterval = 5.0f;
        private bool _isWaitMove = false;
        

        public Monster( int x, int y, Map map )
            : base( x, y, Constants.MONSTER_IMAGE, Constants.MONSTER_COLOR, Constants.MONSTER_RENDER_ORDER )
        {
            _curState = State.Idle;
            _mapObject = map;
            _player = _objectManager.GetGameObject<Player>( "Player" );
        }

        public void Initialize()
        {
            Debug.Assert( base.Initialize() );

            _moveInterval *= TimeManager.Instance.ElaspedTime;
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

                    if(false == _isWaitMove )
                    {
                        EventManager.Instance.SetTimeOut( MoveToNextPath, _moveInterval );
                        _isWaitMove = true;
                    }
                    break;
            }
        }

        private void ActionFindPath(int destinationX, int destinationY)
        {
            _paths.Clear();

            if( true == PathFinder.ComputePath( _mapObject, _x, _y, destinationX, destinationY, ref _paths ) )
            {
                _curState = State.Chase;
                _curPathIndex = 0;
            }
        }

        private void MoveToNextPath()
        {
            if( 0 >= _paths.Count )
            {
                return;
            }

            int dirX = _paths[_curPathIndex].X - _x;
            int dirY = _paths[_curPathIndex].Y - _y;

            MoveDirection( dirX, dirY );

            _isWaitMove = false;
        }
    }
}
