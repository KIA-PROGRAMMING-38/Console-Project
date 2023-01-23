using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class CharacterMovement : Component
    {
        private Action<int, int> _moveAction;

        private float _moveDelay = 0.0f;
        private bool _isCharacterWaitMove = false;
        private bool _isCharacterFirstMove = false;
        private bool _isIgnoreMoveEvent = false;

        public bool IsWaitMove { get { return _isCharacterWaitMove; } }

        public CharacterMovement( Character character, Action<int, int> moveAction, float moveDelay )
        {
            Debug.Assert( null != character );

            _moveAction = moveAction;
            _moveDelay = moveDelay;
        }

        public void Reset()
        {
            _isCharacterFirstMove = true;
            _isIgnoreMoveEvent = true;
        }

        public void Action( int dirX, int dirY )
        {
            if ( false == _isCharacterFirstMove )    // 첫 움직임이 아닐 때(움직임에 Delay를 준다)..
            {
                if ( true == _isCharacterWaitMove )
                {
                    return;
                }

                _isCharacterWaitMove = true;

                EventManager.Instance.SetTimeOut(
                    () => {
                        if ( false == _isIgnoreMoveEvent )
                        {
                            _moveAction.Invoke( dirX, dirY );
                        }

                        OnCharacterMove();
                    }
                    , _moveDelay );
            }
            else        // 첫 움직임일 때..
            {
                _moveAction.Invoke( dirX, dirY );
                _isCharacterFirstMove = false;
            }
        }

        private void OnCharacterMove()
        {
            _isCharacterWaitMove = false;
            _isIgnoreMoveEvent = false;
        }
    }
}
