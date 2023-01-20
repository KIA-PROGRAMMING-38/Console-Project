using Packman.Source;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Player : Character
    {
        // 많이 사용하는 싱글톤들은 미리 멤버로 받아두기..
        private InputManager _inputManager = InputManager.Instance;

        // 키 관련 상수 정의..
        private const ConsoleKey moveRightKey = ConsoleKey.RightArrow;
        private const ConsoleKey moveLeftKey = ConsoleKey.LeftArrow;
        private const ConsoleKey moveUpKey = ConsoleKey.UpArrow;
        private const ConsoleKey moveDownKey = ConsoleKey.DownArrow;

        public Player( int x, int y )
            : base( x, y, Constants.PLAYER_IMAGE, Constants.PLAYER_COLOR )
        {
        }

        /// <summary>
        /// 활성화 시 호출되는 함수..
        /// </summary>
        public override void OnEnable()
        {
            base.OnEnable();

            AddKeyPressEvent();
        }

        /// <summary>
        /// 비활성화 시 호출되는 함수..
        /// </summary>
        public override void OnDisable()
        {
            base.OnDisable();

            RemoveKeyPressEvent();
        }

        /// <summary>
        /// 플레이어를 초기화합니다..
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
            Debug.Assert( base.Initialize() );

            return true;
        }

        /// <summary>
        /// InputManager에게 키 입력 시 호출할 이벤트를 등록합니다..
        /// </summary>
        private void AddKeyPressEvent()
        {
            _inputManager.AddEvent( moveRightKey, OnMoveRightKeyPress );
            _inputManager.AddEvent( moveLeftKey, OnMoveLeftKeyPress );
            _inputManager.AddEvent( moveUpKey, OnMoveUpKeyPress );
            _inputManager.AddEvent( moveDownKey, OnMoveDownKeyPress );
        }

        /// <summary>
        /// InputManager에게 키 입력 시 등록된 이벤트를 해제시킵니다..
        /// </summary>
        private void RemoveKeyPressEvent()
        {
            _inputManager.RemoveEvent( moveRightKey, OnMoveRightKeyPress );
            _inputManager.RemoveEvent( moveLeftKey, OnMoveLeftKeyPress );
            _inputManager.RemoveEvent( moveUpKey, OnMoveUpKeyPress );
            _inputManager.RemoveEvent( moveDownKey, OnMoveDownKeyPress );
        }

        /// <summary>
        /// 오른쪽 움직임 키를 눌렀을 때 호출됩니다..
        /// </summary>
        private void OnMoveRightKeyPress()
        {
            MoveDirection( 1, 0 );
        }

        /// <summary>
        /// 왼쪽 움직임 키를 눌렀을 때 호출됩니다..
        /// </summary>
        private void OnMoveLeftKeyPress()
        {
            MoveDirection( -1, 0 );
        }

        /// <summary>
        /// 위쪽 움직임 키를 눌렀을 때 호출됩니다..
        /// </summary>
        private void OnMoveUpKeyPress()
        {
            MoveDirection( 0, -1 );
        }

        /// <summary>
        /// 아래쪽 움직임 키를 눌렀을 때 호출됩니다..
        /// </summary>
        private void OnMoveDownKeyPress()
        {
            MoveDirection( 0, 1 );
        }
    }
}
