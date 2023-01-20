using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK
{
    internal class Player
    {
        public int X;
        public int Y;
        public int pastX;
        public int pastY;
        public int MaxHP;
        public int RemainHP;
        public int ATK;
        public int DEF;
        public Direction MoveDirection;

        public static class Function
        {
            public static void Render(Player player)
            {
                Game.Function.ObjRender(player.pastX, player.pastY, "☺", ConsoleColor.White);
                Game.Function.ObjRender(player.X, player.Y, "☺", ConsoleColor.Black);
            }
            public static void Move(ConsoleKey key, Player player)
            {
                if (ConsoleKey.LeftArrow == key)
                {
                    player.pastX = player.X;
                    player.pastY = player.Y;
                    player.X = Math.Max(Game.MAP_MIN_X, player.X - 1);
                    player.MoveDirection = Direction.Left;
                }
                if (ConsoleKey.RightArrow == key)
                {
                    player.pastX = player.X;
                    player.pastY = player.Y;
                    player.X = Math.Min(player.X + 1, Game.MAP_MAX_X);
                    player.MoveDirection = Direction.Right;
                }
                if (ConsoleKey.UpArrow == key)
                {
                    player.pastX = player.X;
                    player.pastY = player.Y;
                    player.Y = Math.Max(Game.MAP_MIN_Y, player.Y - 1);
                    player.MoveDirection = Direction.Up;
                }
                if (ConsoleKey.DownArrow == key)
                {
                    player.pastX = player.X;
                    player.pastY = player.Y;
                    player.Y = Math.Min(player.Y + 1, Game.MAP_MAX_Y);
                }
            }

        }
    }
}
