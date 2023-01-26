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
        public const int GOLD_RENDER_ORDER = 1;
        public const int TILE_RENDER_ORDER = 0;

        // Image 관련 상수들..
        public const string PLAYER_IMAGE = "^";
		//public const string MONSTER_IMAGE = "★";
		public const string MONSTER_IMAGE = "?";
		public const string MONSTER_STUN_STATE_IMAGE = MONSTER_IMAGE;
		public const string MONSTER_DEAD_STATE_IMAGE = "_";
        //public const string MONSTER_IMAGE = "M";
        //public const string GOLD_IMAGE = "⊙";
        //public const string GOLD_IMAGE = "☹";
        //public const string GOLD_IMAGE = "⊙";
        public const string GOLD_IMAGE = "*";
        // 아이템 관련 이미지..
        public const string TRAP_IMAGE = ":";
        // 투사체 관련 이미지..
        public const string STUN_IMAGE = "ż";
        public const string PUNCH_IMAGE = "-";
        //public const string MONSTER_KILL_PROJECTILE_IMAGE = "→←↓↑";
        public const string MONSTER_KILL_PROJECTILE_IMAGE = "=";

        //public const string COLLECT_GOLD_PROJECTILE_IMAGE = "c";
        //public const string COLLECT_GOLD_PROJECTILE_IMAGE = "∞";
        public const string COLLECT_GOLD_PROJECTILE_IMAGE = "$";

        // Color 관련 상수들..
        // 오브젝트 Color..
        public const ConsoleColor PLAYER_COLOR = ConsoleColor.DarkMagenta;
        public const ConsoleColor MONSTER_COLOR = ConsoleColor.Cyan;
        public const ConsoleColor MONSTER_CHASE_PATTERN_COLOR = ConsoleColor.DarkCyan;
        public const ConsoleColor MONSTER_STUN_STATE_COLOR = ConsoleColor.DarkGray;
        public const ConsoleColor MONSTER_FORCED_PUSH_STATE_COLOR = ConsoleColor.DarkGreen;
        public const ConsoleColor MONSTER_DEAD_STATE_COLOR = ConsoleColor.DarkRed;
        public const ConsoleColor MAP_WALL_COLOR = ConsoleColor.Blue;
        public const ConsoleColor GOLD_COLOR = ConsoleColor.DarkYellow;
        // 맵 Color..
        public const ConsoleColor DEFAULT_BACKGROUND_COLOR = ConsoleColor.Black;
        public const ConsoleColor DEFAULT_FOREGROUND_COLOR = ConsoleColor.White;
        // 아이템 관련 Color..
        public const ConsoleColor TRAP_COLOR = ConsoleColor.DarkMagenta;
        // 투사체 Color..
        public const ConsoleColor STUN_COLOR = ConsoleColor.Yellow;
        public const ConsoleColor PUNCH_COLOR = ConsoleColor.DarkRed;
        public const ConsoleColor MONSTER_KILL_PROJECTILE_COLOR = ConsoleColor.Red;
        public const ConsoleColor COLLECT_GOLD_PROJECTILE_COLOR = ConsoleColor.DarkCyan;

        // 시간과 관련된 상수들..
        public const int FRAME_PER_SECOND = 60;
        // 맵과 관련된 상수들..
        public const int MAP_RANGE_MIN_X = 1;
        public const int MAP_RANGE_MIN_Y = 1;
        public const int MAP_RANGE_MAX_X = 60;
        public const int MAP_RANGE_MAX_Y = 25;

        // 움직임 스피드와 관련된 상수들..
        public const float PLAYER_MOVE_DELAY = 0.12f;
        public const float MONSTER_MOVE_DELAY = PLAYER_MOVE_DELAY * 2.5f;
    }
}
