using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal static class Constants
    {
        // Image 관련 상수들..
        public const string PLAYER_IMAGE = "☹";
        public const string MONSTER_IMAGE = "★";
        public const string MONEY_IMAGE = "㉾";

        // Color 관련 상수들..
        public const ConsoleColor PLAYER_COLOR = ConsoleColor.Yellow;
        public const ConsoleColor MONSTER_COLOR = ConsoleColor.Red;
        public const ConsoleColor MAP_WALL_COLOR = ConsoleColor.Blue;
        public const ConsoleColor DEFAULT_BACKGROUND_COLOR = ConsoleColor.Black;
        public const ConsoleColor DEFAULT_FOREGROUND_COLOR = ConsoleColor.White;

        // 시간과 관련된 상수들..
        public const int FRAME_PER_SECOND = 10;
        // 맵과 관련된 상수들..
        public const int MAP_RANGE_MIN_X = 1;
        public const int MAP_RANGE_MIN_Y = 1;
        public const int MAP_RANGE_MAX_X = 60;
        public const int MAP_RANGE_MAX_Y = 25;
    }
}
