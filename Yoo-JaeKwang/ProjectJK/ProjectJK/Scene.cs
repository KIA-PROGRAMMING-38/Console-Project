using ProjectJK.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK
{
    public enum SceneKind
    {
        Title,
        InGame
    }
    public static class Scene
    {
        public static void Render(Player player, Wall[] walls, VillageNPC[] villageNPCs, StageUpPortal stageUpPortal, StageDownPortal stageDownPortal,
                                    Slime[] slimes, Fox[] foxes, Goblin[] goblins, KingSlime kingSlime, SelectCursor selectCursor)
        {
            switch (_currentScene)
            {
                case SceneKind.Title:
                    RenderTitle(selectCursor);
                    break;
                case SceneKind.InGame:
                    RenderInGame(player, walls, villageNPCs, stageUpPortal, stageDownPortal,
                         slimes, foxes, goblins, kingSlime, selectCursor);
                    break;
            }
        }
        public static void Update(Player player, Wall[] walls, VillageNPC[] villageNPCs, StageUpPortal stageUpPortal, StageDownPortal stageDownPortal,
                                    Slime[] slimes, Fox[] foxes, Goblin[] goblins, KingSlime kingSlime, SelectCursor selectCursor)
        {
            switch (_currentScene)
            {
                case SceneKind.Title:
                    UpdateTitle(selectCursor, player);
                    break;
                case SceneKind.InGame:
                    UpdateInGame(player, ref walls, ref villageNPCs, ref stageUpPortal, ref stageDownPortal,
                         ref slimes, ref foxes, ref goblins, kingSlime, selectCursor);
                    break;
            }
        }
        public static void InitTitle()
        {
            Console.Clear();
            _lines = LoadScene(SceneKind.Title);
            ParseScene(_lines);

        }
        private static string[] LoadScene(SceneKind sceneKind)
        {
            string sceneFilePath = Path.Combine("..\\..\\..\\Assets", "Scene", $"Scene{(int)sceneKind:D2}.txt");
            if (false == File.Exists(sceneFilePath))
            {
                Game.ExitWithError($"신 파일 로드 오류{sceneFilePath}");
            }
            return File.ReadAllLines(sceneFilePath);
        }
        private static string[] _lines = null;
        private static void ParseScene(string[] lines)
        {
            for (int i = 0; i < lines.Length; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(lines[i]);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static void RenderTitle(SelectCursor selectCursor)
        {
            Game.ObjRender(selectCursor.X, selectCursor.PastY, ">", ConsoleColor.White);
            Game.ObjRender(selectCursor.X, selectCursor.Y, ">", ConsoleColor.Black);
        }
        public static void UpdateTitle(SelectCursor selectCursor, Player player)
        {
            SelectCursor.Move(selectCursor, player);

            if (SelectCursor.SelectYes(selectCursor))
            {
                SetNextScene(SceneKind.InGame);
            }
            if (SelectCursor.SelectNo(selectCursor))
            {
                Console.Clear();
                Game.TitleExit();
                Environment.Exit(0);
            }
        }
        private static void InitInGame(Player player, out Wall[] walls, out VillageNPC[] villageNPCs, out StageUpPortal stageUpPortal, SelectCursor selectCursor)
        {
            Console.Clear();
            player.CanMove = true;
            Stage.InitStage00(out walls, out villageNPCs, out stageUpPortal);
            selectCursor.X = Game.DialogCursor_X;
        }
        public static void RenderInGame(Player player, Wall[] walls, VillageNPC[] villageNPCs, StageUpPortal stageUpPortal, StageDownPortal stageDownPortal,
                                    Slime[] slimes, Fox[] foxes, Goblin[] goblins, KingSlime kingSlime, SelectCursor selectCursor)
        {
            Stage.Render(player, walls, villageNPCs, stageUpPortal, stageDownPortal,
                         slimes, foxes, goblins, kingSlime, selectCursor);
        }
        public static void UpdateInGame(Player player, ref Wall[] walls, ref VillageNPC[] villageNPCs, ref StageUpPortal stageUpPortal, ref StageDownPortal stageDownPortal,
                                    ref Slime[] slimes, ref Fox[] foxes, ref Goblin[] goblins, KingSlime kingSlime, SelectCursor selectCursor)
        {
            if (Stage.IsStageChange())
            {
                Stage.ChangeStage(ref walls, ref villageNPCs, ref stageUpPortal, ref stageDownPortal,
                                   ref slimes, ref foxes, ref goblins, kingSlime);
            }

            Stage.Update(player, walls, villageNPCs, stageUpPortal, stageDownPortal,
                         slimes, foxes, goblins, kingSlime, selectCursor);
        }

        private static SceneKind _currentScene = SceneKind.Title;
        public static SceneKind GetCurrentScene()
        {
            return _currentScene;
        }
        private static bool _isSceneChange = true;
        public static bool IsSceneChange()
        {
            return _isSceneChange;
        }
        private static SceneKind _nextScene;
        public static void SetNextScene(SceneKind nextScene)
        {
            _nextScene = nextScene;
            _isSceneChange = true;
        }
        public static void ChangeScene(Player player, ref Wall[] walls, ref VillageNPC[] villageNPCs, ref StageUpPortal stageUpPortal, SelectCursor selectCursor)
        {
            if (_isSceneChange)
            {
                _isSceneChange = false;

                _currentScene = _nextScene;

                _nextScene = default;

                switch (_currentScene)
                {
                    case SceneKind.Title:
                        InitTitle();
                        break;
                    case SceneKind.InGame:
                        InitInGame(player, out walls, out villageNPCs, out stageUpPortal, selectCursor);
                        break;
                }
            }
        }
    }
}