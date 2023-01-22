using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    enum MoveDirection
    {
        None,
        Left,
        Right
    };

    internal class Player
    {
        public int BeforeX;
        public int UpdateX;
        public int Y = 0;
        public string Icon = "▲";
        public MoveDirection moveDirection;

        public void Move()
        {
            switch(moveDirection)
            {
                case MoveDirection.Left:
                    UpdateX = Math.Max(0, BeforeX - 1);
                    break;

                case MoveDirection.Right:
                    UpdateX = Math.Min(BeforeX + 1, 10);
                    break;
            }
        }

        public void Render()
        {
            Console.SetCursorPosition(BeforeX, Y);
            Console.Write(" ");

            Console.SetCursorPosition(UpdateX, Y);
            Console.Write(Icon);

            BeforeX = UpdateX;
        }
    }
}
