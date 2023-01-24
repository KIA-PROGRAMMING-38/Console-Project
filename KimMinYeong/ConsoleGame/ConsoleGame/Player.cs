using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public enum MoveDirection
    {
        None,
        Left,
        Right
    };

    public static class Player
    {
        public static int _beforeX;
        public static int _currentX;
        public static int _y = 20;
        public static string _icon = "A";
        public static MoveDirection _moveDirection;

        public static void Move()
        {
            _beforeX = _currentX;

            switch(_moveDirection)
            {
                case MoveDirection.Left:
                    _currentX = Math.Max(0, _currentX - 1);
                    break;

                case MoveDirection.Right:
                    _currentX = Math.Min(_currentX + 1, 35);
                    break;
            }
        }

        public static void Render()
        {
            if(_beforeX != _currentX)
            {
                Console.SetCursorPosition(_beforeX, _y);
                Console.Write(" ");
            }

            Console.SetCursorPosition(_currentX, _y);
            Console.Write(_icon);
        }
    }
}
