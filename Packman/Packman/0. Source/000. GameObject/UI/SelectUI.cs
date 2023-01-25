using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class SelectUI : GameObject
    {
        private class SelectInfo
        {
            public string[] Message;
            public string OriginMessage;
            public Action Event;
        }

        // 많이 사용하는 싱글톤들은 미리 멤버로 받아두기..
        EventManager _eventManager = EventManager.Instance;

        private List<SelectInfo> _selectInfoes = new List<SelectInfo>();
        int _maxSelectPointCount = 0;
        int _curSelectPoint = 0;
        int _yDistance = 4;
        int _maxMessageLength = 0;

        public SelectUI( int x, int y )
            : base( x, y, 2000 )
        {

        }

        public void AddSelectList( string message, Action eventAction )
        {
            SelectInfo curSelectInfo = new SelectInfo();

            curSelectInfo.OriginMessage = message;
            curSelectInfo.Event = eventAction;

            _selectInfoes.Add( curSelectInfo );

            _maxMessageLength = Math.Max( _maxMessageLength, message.Length );

            for ( int i = 0; i < _selectInfoes.Count; i++ )
            {
                _selectInfoes[i].Message = ConvertText( _selectInfoes[i].OriginMessage, _maxMessageLength );
            }

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
                for( int messageIndex = 0; messageIndex < 3; ++messageIndex )
                {
                    Console.SetCursorPosition( _x, _y + index * _yDistance + messageIndex );
                    Console.Write( _selectInfoes[index].Message[messageIndex] );
                }
            }

            Console.SetCursorPosition( _x - 4, _y + _curSelectPoint * _yDistance + 1 );
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
            _eventManager.AddInputEvent( ConsoleKey.UpArrow, this.OnPressUpArrowKey );
            _eventManager.AddInputEvent( ConsoleKey.DownArrow, this.OnPressDownArrowKey );
            _eventManager.AddInputEvent( ConsoleKey.Enter, this.OnPressEnterKey );
        }

        /// <summary>
        /// Key 입력 이벤트를 제거해줍니다.
        /// </summary>
        private void RemoveKeyPressEvent()
        {
            _eventManager.RemoveInputEvent( ConsoleKey.UpArrow, this.OnPressUpArrowKey );
            _eventManager.RemoveInputEvent( ConsoleKey.DownArrow, this.OnPressDownArrowKey );
            _eventManager.RemoveInputEvent( ConsoleKey.Enter, this.OnPressEnterKey );
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

            Console.SetCursorPosition( _x - 4, _y + _curSelectPoint * _yDistance + 1 );
            Console.Write( "  " );

            _curSelectPoint = newSelectPoint;
        }

        /// <summary>
        /// 유저가 선택했을 때 호출되는 함수( 외부에서 받아온 함수를 호출합니다, Callback 함수 )..
        /// </summary>
        private void OnPressEnterKey()
        {
            _selectInfoes[_curSelectPoint].Event?.Invoke();
        }

        private string[] ConvertText( string text, int maxTextLength )
        {
            string[] convertText = new string[3] { "", "", "" };

            int curTextLength = text.Length;

            for ( int i = 0; i < maxTextLength + 2; ++i )
            {
                convertText[0] += "#";
                convertText[2] += "#";
            }

            convertText[1] = "#";

            int loopCount = (maxTextLength - curTextLength) / 2;
            for ( int i = 0; i < loopCount; ++i )
            {
                convertText[1] += " ";
            }

            convertText[1] += text;

            for ( int i = 0; i < (maxTextLength - curTextLength) - loopCount; ++i )
            {
                convertText[1] += " ";
            }

            convertText[1] += "#";

            return convertText;
        }
    }
}
