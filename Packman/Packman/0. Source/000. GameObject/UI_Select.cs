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

        private void AddKeyPressEvent()
        {
            InputManager.Instance.AddEvent( ConsoleKey.UpArrow, this.OnPressUpArrowKey );
            InputManager.Instance.AddEvent( ConsoleKey.DownArrow, this.OnPressDownArrowKey );
            InputManager.Instance.AddEvent( ConsoleKey.Enter, this.OnPressEnterKey );
        }

        private void RemoveKeyPressEvent()
        {
            InputManager.Instance.RemoveEvent( ConsoleKey.UpArrow, this.OnPressUpArrowKey );
            InputManager.Instance.RemoveEvent( ConsoleKey.DownArrow, this.OnPressDownArrowKey );
            InputManager.Instance.RemoveEvent( ConsoleKey.Enter, this.OnPressEnterKey );
        }

        private void OnPressUpArrowKey()
        {
            _curSelectPoint = Math.Max( _curSelectPoint - 1, 0 );
        }

        private void OnPressDownArrowKey()
        {
            _curSelectPoint = Math.Min( _curSelectPoint + 1, _maxSelectPointCount - 1 );
        }

        private void OnPressEnterKey()
        {
            _selectInfoes[_curSelectPoint].Event?.Invoke();
        }
    }
}
