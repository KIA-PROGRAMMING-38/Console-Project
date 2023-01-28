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
    public enum ExitKind
    {
        TitleExit,
        GameClear,
    }
    public class Game
    {
        public const int Level_HP_Money_X = 31;
        public const int EXP_X = 40;
        public const int Status_X = 47;
        public const int Battle_X = 62;
        public const int Level_EXP_Battle_Y = 1;
        public const int HP_STATUS_Y = 5;
        public const int Money_STATUS_Y = 9;
        public const int DialogCursor_X = 2;
        public const int DialogCursor_Y = 18;
        public const int BattleCursor_X = 63;
        public const int BattleCursor_Y = 10;
        public const int Center_X = 12;
        public const int Center_Y = 6;
               
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
        public static void Run(Player player, Wall[] walls, VillageNPC[] villageNPCs, StageUpPortal stageUpPortal, StageDownPortal stageDownPortal,
                                Slime[] slimes, Fox[] foxes, Goblin[] goblins, KingSlime kingSlime, SelectCursor selectCursor)
        {
            while (_isGameDoing)
            {
                Thread.Sleep(20);
                if (Scene.IsSceneChange())
                {
                    Scene.ChangeScene(player, ref walls, ref villageNPCs, ref stageUpPortal, selectCursor);
                }

                Render(player, walls, villageNPCs, stageUpPortal, stageDownPortal,
                     slimes, foxes, goblins, kingSlime, selectCursor);
                ProcessInput();
                Update(player, ref walls, ref villageNPCs, ref stageUpPortal, ref stageDownPortal,
                     ref slimes, ref foxes, ref goblins, kingSlime, selectCursor);
            }
        }

        private static void Render(Player player, Wall[] walls, VillageNPC[] villageNPCs, StageUpPortal stageUpPortal, StageDownPortal stageDownPortal,
                                Slime[] slimes, Fox[] foxes, Goblin[] goblins, KingSlime kingSlime, SelectCursor selectCursor)
        {
            switch (Scene.GetCurrentScene())
            {
                case SceneKind.Title:
                    Scene.RenderTitle(selectCursor);

                    break;
                case SceneKind.InGame:
                    Scene.RenderInGame(player, walls, villageNPCs, stageUpPortal, stageDownPortal,
                     slimes, foxes, goblins, kingSlime, selectCursor);

                    break;
            }
        }

        private static void ProcessInput()
        {
            Input.Process();
        }

        private static void Update(Player player, ref Wall[] walls, ref VillageNPC[] villageNPCs, ref StageUpPortal stageUpPortal, ref StageDownPortal stageDownPortal,
                               ref Slime[] slimes, ref Fox[] foxes, ref Goblin[] goblins, KingSlime kingSlime, SelectCursor selectCursor)
        {
            switch (Scene.GetCurrentScene())
            {
                case SceneKind.Title:
                    Scene.UpdateTitle(selectCursor, player);

                    break;

                case SceneKind.InGame:
                    Scene.UpdateInGame(player, ref walls, ref villageNPCs, ref stageUpPortal, ref stageDownPortal,
                     ref slimes, ref foxes, ref goblins, kingSlime, selectCursor);

                    break;
            }
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

        public static void TitleExit()
        {
            Console.Clear();
            _lines = LoadExit(ExitKind.TitleExit);
            ParseExit(_lines);
            Environment.Exit(0);
        }
        public static void GameClear()
        {
            Console.Clear();
            _lines = LoadExit(ExitKind.GameClear);
            ParseExit(_lines);
            Environment.Exit(0);
        }

        private static string[] _lines = null;
        private static string[] LoadExit(ExitKind exitKind)
        {
            string exitFilePath = Path.Combine("..\\..\\..\\Assets", "Exit", $"Exit{(int)exitKind:D2}.txt");
            if (false == File.Exists(exitFilePath))
            {
                ExitWithError($"종료 파일 로드 오류{exitFilePath}");
            }
            return File.ReadAllLines(exitFilePath);
        }
        private static void ParseExit(string[] lines)
        {
            for (int i = 0; i < lines.Length; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(lines[i]);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}