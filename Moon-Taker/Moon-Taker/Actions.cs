using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon_Taker
{
    internal class Actions
    {
        public static void MovePlayerToRight(ref int playerX, in int mapSizeX)
        {
            playerX = Math.Min(playerX + 1, mapSizeX);
        }
        public static void MovePlayerToLeft(ref int playerX, in int mapSizeX)
        {
            playerX = Math.Max(0, playerX - 1);
        }
        public static void MovePlayerToDown(ref int playerY, in int mapSizeY)
        {
            playerY = Math.Min(playerY + 1, mapSizeY);
        }
        public static void MovePlayerToUp(ref int playerY, in int mapSizeY)
        {
            playerY = Math.Max(0, playerY - 1);
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
        public static void PushRight(ref int enemyX, ref int playerX)
        {
            enemyX = ++playerX;
            --playerX;
            return;
        }
        public static void PushLeft(ref int enemyX, ref int playerX)
        {
            enemyX = --playerX;
            ++playerX;
            return;
        }
        public static void PushDown(ref int enemyY, ref int playerY)
        {
            enemyY = ++playerY;
            --playerY;
            return;
        }
        public static void PushUp(ref int enemyY, ref int playerY)
        {
            enemyY = --playerY;
            ++playerY;
            return;
        }
    }
}


