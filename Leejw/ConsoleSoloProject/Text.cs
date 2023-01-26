using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoloProject
{
    internal class Text
    {

        public static int timeX = 58;
        public static int timeY = 2;

        public static int stageX = 22;
        public static int stageY = 5;

        public static int monsterNumberX = 58;
        public static int monsterHpY = 3;
        public static int monsterNumberY = 1;

        public static int damageX = 58;
        public static int damageY = 4;
        public static int damageTextX = 72;
        public static int damageTextY = 4;

        public static int goldX = 58;
        public static int goldY = 5;
        public static int goldTextX = 68;
        public static int goldTextY = 5;

        public static int itemTextX = 58;
        public static int itemTextY = 9;

        public static int itemMessageX = 15;
        public static int itemMessageY = 14;

        public static void Position(int X, int Y, string Icon)
        {
            Console.SetCursorPosition(X, Y);
            Console.WriteLine(Icon);
        }

    }
}
