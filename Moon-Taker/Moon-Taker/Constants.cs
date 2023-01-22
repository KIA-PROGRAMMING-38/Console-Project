using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon_Taker
{
    internal class Constants
    {
        public const string player = "8";
        public const string wall = "#";
        public const string enemy = "e";
        public const string enemyOnTrap = "E";
        public const string block = "b";
        public const string blockOnTrap = "B";
        public const string activatedTrap = "M";
        public const string deactivatedTrap = "_";
        public const string key = "k";
        public const string door = "X";
        public const string blank = " ";
        public const string moon = "O";

        public const ConsoleColor playerColor = ConsoleColor.White;
        public const ConsoleColor wallColor = ConsoleColor.DarkGray;
        public const ConsoleColor enemyColor = ConsoleColor.Blue;
        public const ConsoleColor blockColor = ConsoleColor.Cyan;
        public const ConsoleColor trapColor = ConsoleColor.Red;
        public const ConsoleColor keyColor = ConsoleColor.Yellow;
        public const ConsoleColor doorColor = ConsoleColor.DarkYellow;
    }
}
