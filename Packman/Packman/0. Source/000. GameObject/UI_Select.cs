using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman._0._Source._000._GameObject
{
    internal class UI_Select : GameObject
    {
        private struct SelectInfo
        {
            public string Message;
            public Action Event;
        }

        private List<SelectInfo> _selectInfoes = new List<SelectInfo>();
        int _maxSelectPointCount = 0;
        int _curSelectPoint = 0;

        public UI_Select( int x, int y )
            : base( x, y )
        {

        }

        public void AddSelectList( string message, Action eventAction )
        {
            SelectInfo curSelectInfo = new SelectInfo();

            curSelectInfo.Message = message;
            curSelectInfo.Event = eventAction;

            _selectInfoes.Add( curSelectInfo );

            ++_maxSelectPointCount;
        }

        public override void OnEnable()
        {
            base.OnEnable();

            AddKeyPressEvent();
        }

        public override void OnDisable()
        {
            base.OnDisable();

            RemoveKeyPressEvent();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Render()
        {
            base.Render();

            for ( int index = 0; index < _maxSelectPointCount; ++index )
            {
                Console.SetCursorPosition( _x, _y + index );
                Console.Write( _selectInfoes[index].Message );
            }

            Console.SetCursorPosition( _x - 4, _y + _curSelectPoint );
            Console.Write( "→" );
        }

        public override void Release()
        {
            base.Release();

            RemoveKeyPressEvent();
        }

        /// <summary>
        /// Key 입력 이벤트를 추가해줍니다.
        /// </summary>
        private void AddKeyPressEvent()
        {
            InputManager.Instance.AddEvent( ConsoleKey.UpArrow, this.OnPressUpArrowKey );
            InputManager.Instance.AddEvent( ConsoleKey.DownArrow, this.OnPressDownArrowKey );
            InputManager.Instance.AddEvent( ConsoleKey.Enter, this.OnPressEnterKey );
        }

        /// <summary>
        /// Key 입력 이벤트를 제거해줍니다.
        /// </summary>
        private void RemoveKeyPressEvent()
        {
            InputManager.Instance.RemoveEvent( ConsoleKey.UpArrow, this.OnPressUpArrowKey );
            InputManager.Instance.RemoveEvent( ConsoleKey.DownArrow, this.OnPressDownArrowKey );
            InputManager.Instance.RemoveEvent( ConsoleKey.Enter, this.OnPressEnterKey );
        }

        /// <summary>
        /// 위 화살표 키가 눌렸을 때 호출됩니다( 이벤트 함수 )..
        /// </summary>
        private void OnPressUpArrowKey()
        {
            SetSelectPoint( Math.Max( _curSelectPoint - 1, 0 ) );
        }

        /// <summary>
        /// 아래 화살표 키가 눌렸을 때 호출됩니다( 이벤트 함수 )..
        /// </summary>
        private void OnPressDownArrowKey()
        {
            SetSelectPoint( Math.Min( _curSelectPoint + 1, _maxSelectPointCount - 1 ) );
        }

        /// <summary>
        /// 현재 가리키고 있는 선택 지점을 변경합니다.
        /// </summary>
        /// <param name="newSelectPoint"> 변경할 선택 지점 </param>
        private void SetSelectPoint( int newSelectPoint )
        {
            if ( _curSelectPoint == newSelectPoint )
            {
                return;
            }

            RenderManager.Instance.ReserveRenderRemove( _x - 4, _y + _curSelectPoint, 2 );

            _curSelectPoint = newSelectPoint;
        }

        /// <summary>
        /// 유저가 선택했을 때 호출되는 함수( 외부에서 받아온 함수를 호출합니다, Callback 함수 )..
        /// </summary>
        private void OnPressEnterKey()
        {
            _selectInfoes[_curSelectPoint].Event?.Invoke();
        }
    }
}
