using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK
{
    enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down,
    }
    internal class Game
    {
        public const int MAP_MIN_X = 20;
        public const int MAP_MIN_Y = 10;
        public const int MAP_MAX_X = 40;
        public const int MAP_MAX_Y = 20;
        public const int UI_HP_X = 4;
        public const int UI_HP_Y = 10;
        public const int UI_STATUS_X = 4;
        public const int UI_STATUS_Y = 12;
        public const int UI_MONEY_X = 4;
        public const int UI_MONEY_Y = 16;
        public const int UI_KEY_X = 4;
        public const int UI_Key_Y = 17;
        public const int UI_STAGE_X = 4;
        public const int UI_STAGE_Y = 20;



        public bool IsGameDoing;
        public bool IsTitleDoing;
        public bool IsStageOneDoing;

        public static class Function
        {
            public static void Initializing()
            {
                Console.ResetColor();
                Console.CursorVisible = false;
                Console.Title = "던전 탈출";
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.White;
                Console.OutputEncoding = Encoding.UTF8;
                Console.Clear();
            }

            public static void DefaultMapUI()
            {
                // 맵
                for (int i = -2; i <= MAP_MAX_X - MAP_MIN_X + 2; ++i)
                {
                    ObjRender(MAP_MIN_X + i, MAP_MIN_Y - 1, "▒", ConsoleColor.DarkMagenta);
                    ObjRender(MAP_MIN_X + i, MAP_MAX_Y + 1, "▒", ConsoleColor.DarkMagenta);
                }
                for (int i = 0; i <= MAP_MAX_Y - MAP_MIN_Y; ++i)
                {
                    ObjRender(MAP_MIN_X - 1, MAP_MIN_Y + i, "▒", ConsoleColor.DarkMagenta);
                    ObjRender(MAP_MIN_X - 2, MAP_MIN_Y + i, "▒", ConsoleColor.DarkMagenta);
                    ObjRender(MAP_MAX_X + 1, MAP_MIN_Y + i, "▒", ConsoleColor.DarkMagenta);
                    ObjRender(MAP_MAX_X + 2, MAP_MIN_Y + i, "▒", ConsoleColor.DarkMagenta);
                }
                // UI
                ObjRender(UI_HP_X, UI_HP_Y, "HP", ConsoleColor.Blue);
                ObjRender(UI_STATUS_X, UI_STATUS_Y, "Status", ConsoleColor.Blue);
                ObjRender(UI_MONEY_X, UI_MONEY_Y, "Money", ConsoleColor.Blue);
                ObjRender(UI_KEY_X,UI_Key_Y,"Key", ConsoleColor.Blue);
                ObjRender(UI_STAGE_X, UI_STAGE_Y, "Stage", ConsoleColor.Blue);
            }

            public static void StageMove()
            {
                for(int i = 0; i < MAP_MAX_X - MAP_MIN_X; ++i)
                {
                    for(int j = 0; j < MAP_MAX_Y - MAP_MIN_Y; ++j)
                    {
                        Console.SetCursorPosition(MAP_MIN_X + i, MAP_MIN_Y + j);
                        Console.Write(" ");
                    }
                }
            }



            public static void ObjRender(int objX, int objY, string icon, ConsoleColor color)
            {
                Console.SetCursorPosition(objX, objY);
                Console.ForegroundColor = color;
                Console.Write(icon);
                Console.ForegroundColor = ConsoleColor.White;
            }

        }
    }
}
