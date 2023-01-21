using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Moon_Taker.Enum;

namespace Moon_Taker
{
    internal class Actions
    {
        public static void MovePlayerToRight(ref int playerX, in int mapSizeX, Enum.Direction direction)
        {
            playerX = Math.Min(playerX + 1, mapSizeX);
            direction = Enum.Direction.Right;
        }
        public static void MovePlayerToLeft(ref int playerX, in int mapSizeX, Enum.Direction direction)
        {
            playerX = Math.Max(0, playerX - 1);
            direction = Enum.Direction.Left;
        }
        public static void MovePlayerToDown(ref int playerY, in int mapSizeY, Enum.Direction direction)
        {
            playerY = Math.Min(playerY + 1, mapSizeY);
            direction = Enum.Direction.Down;
        }
        public static void MovePlayerToUp(ref int playerY, in int mapSizeY, Enum.Direction direction)
        {
            playerY = Math.Max(0, playerY - 1);
            direction = Enum.Direction.Up;
        }
        public static bool IsCollided(int x1, int y1, int x2, int y2)
        {
            if (x1 == x2 && y1 == y2)
            {
                return true;
            }
            return false;
        }
        public static void CollidSolidOnRight(ref int x)
        {
            ++x;
            return;
        }
        public static void CollidSolidOnLeft(ref int x)
        {
            --x;
            return;
        }
        public static void CollidSolidOnDown(ref int y)
        {
            ++y;
            return;
        }
        public static void CollidSolidOnUp(ref int y)
        {
            --y;
            return;
        }
        public static void PushRight(ref int pushedObjectX, ref int pushingObjectX)
        {
            pushedObjectX = ++pushingObjectX;
        }
        public static void PushLeft(ref int pushedObjectX, ref int pushingObjectX)
        {
            pushedObjectX = --pushingObjectX;
        }
        public static void PushDown(ref int pushedObjectY, ref int pushingObjectY)
        {
            pushedObjectY = ++pushingObjectY;
        }
        public static void PushUp(ref int pushedObjectY, ref int pushingObjectY)
        {
            pushedObjectY = --pushingObjectY;
        }
    }
}
    

