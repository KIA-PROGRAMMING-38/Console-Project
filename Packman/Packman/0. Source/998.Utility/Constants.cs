using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    public struct Point2D
    {
        public int X;
        public int Y;

        public Point2D()
        {
            X = 0;
            Y = 0;
        }

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    internal static class Constants
    {
        // Object RenderOrder 관련 상수들..
        public const int PLAYER_RENDER_ORDER = 10;
        public const int MONSTER_RENDER_ORDER = 5;
        public const int GOLD_RENDER_ORDER = 2;
        public const int TILE_RENDER_ORDER = 0;

        // Image 관련 상수들..
        public const string PLAYER_IMAGE = "☹";
        public const string MONSTER_IMAGE = "★";
        public const string GOLD_IMAGE = "⊙";

        // Color 관련 상수들..
        // 오브젝트 Color..
        public const ConsoleColor PLAYER_COLOR = ConsoleColor.Yellow;
        public const ConsoleColor MONSTER_COLOR = ConsoleColor.Red;
        public const ConsoleColor MAP_WALL_COLOR = ConsoleColor.Blue;
        public const ConsoleColor GOLD_COLOR = ConsoleColor.DarkYellow;
        // 맵 Color..
        public const ConsoleColor DEFAULT_BACKGROUND_COLOR = ConsoleColor.Black;
        public const ConsoleColor DEFAULT_FOREGROUND_COLOR = ConsoleColor.White;

        // 시간과 관련된 상수들..
        public const int FRAME_PER_SECOND = 30;
        // 맵과 관련된 상수들..
        public const int MAP_RANGE_MIN_X = 1;
        public const int MAP_RANGE_MIN_Y = 1;
        public const int MAP_RANGE_MAX_X = 60;
        public const int MAP_RANGE_MAX_Y = 25;
    }
}
