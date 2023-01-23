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
        public static int BeforeX;
        public static int CurrentX;
        public static int Y = 20;
        public static string Icon = "A";
        public static MoveDirection moveDirection;

        public static void Move()
        {
            BeforeX = CurrentX;

            switch(moveDirection)
            {
                case MoveDirection.Left:
                    CurrentX = Math.Max(0, CurrentX - 1);
                    break;

                case MoveDirection.Right:
                    CurrentX = Math.Min(CurrentX + 1, 35);
                    break;
            }
        }

        public static void Render()
        {
            Console.SetCursorPosition(BeforeX, Y);
            Console.Write(" ");

            Console.SetCursorPosition(CurrentX, Y);
            Console.Write(Icon);
        }
    }
}
