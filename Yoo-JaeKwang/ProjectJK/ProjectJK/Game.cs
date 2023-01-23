using ProjectJK.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK
{
    internal class Game
    {
        public const int Level_HP_Money_X = 31;
        public const int EXP_X = 40;
        public const int Status_X = 47;
        public const int Battle_X = 62;
        public const int Level_EXP_Battle_Y = 1;
        public const int HP_STATUS_Y = 5;
        public const int Money_Y = 9;
        public const int DialogCursor_X = 2;
        public const int DialogCursor_Y = 18;
        public const int BattleCursor_X = 63;
        public const int BattleCursor_Y = 10;

        public static class Function
        {
            public static void Initializing()
            {
                Console.ResetColor();
                Console.CursorVisible = false;
                Console.Title = "RPG";
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.White;
                Console.OutputEncoding = Encoding.UTF8;
                Console.Clear();
            }

            private static bool _isGameDoing = true;
            public static void Run()
            {
                while (_isGameDoing)
                {

                    Render();
                    ProcessInput();
                    Update();
                }
            }

            private static void Render()
            {
                Scene.Render();
            }

            private static void ProcessInput()
            {
                Input.Process();
            }

            private static void Update()
            {
                if (Scene.IsSceneChange())
                {
                    Scene.ChangeScene();
                }
                Scene.Update();
            }

            public static void ObjRender(int objX, int objY, string icon, ConsoleColor color)
            {
                Console.SetCursorPosition(objX, objY);
                Console.ForegroundColor = color;
                Console.Write(icon);
                Console.ForegroundColor = ConsoleColor.White;
            }

            public static void ExitWithError(string errorMsg)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(errorMsg);
                Environment.Exit(1);
            }
        }
    }
}
