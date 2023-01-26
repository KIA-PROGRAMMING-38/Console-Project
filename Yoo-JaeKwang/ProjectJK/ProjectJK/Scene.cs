using ProjectJK.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public static void Render()
        {
            switch (_currentScene)
            {
                case SceneKind.Title:
                    RenderTitle();
                    break;
                case SceneKind.InGame:
                    RenderInGame();
                    break;
            }
        }
        public static void Update()
        {
            switch (_currentScene)
            {
                case SceneKind.Title:
                    UpdateTitle();
                    break;
                case SceneKind.InGame:
                    UpdateInGame();
                    break;
            }
        }
        private static void InitTitle()
        {
            Console.Clear();
        }
        private static string[] LoadScene(SceneKind sceneKind)
        {
            string sceneFilePath = Path.Combine("..\\..\\..\\Assets", "Scene", $"Scene{(int)sceneKind:D2}.txt");
            if (false == File.Exists(sceneFilePath))
            {
                Game.Function.ExitWithError($"신 파일 로드 오류{sceneFilePath}");
            }
            return File.ReadAllLines(sceneFilePath);
        }
        private static string[] _lines = LoadScene(SceneKind.Title);
        private static void ParseScene(string[] lines)
        {
            for(int i = 0; i < lines.Length; ++i)
            {
                Console.WriteLine(lines[i]);
            }
        }
        private static void RenderTitle()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            LoadScene(SceneKind.Title);
            ParseScene(_lines);
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void UpdateTitle()
        {
            if(Input.IsKeyDown(ConsoleKey.E))
            {
                SetNextScene(SceneKind.InGame);
            }
        }
        private static void InitInGame()
        {
            Console.Clear();
        }
        private static void RenderInGame()
        {
            Stage.Render();
        }
        private static void UpdateInGame()
        {
            if (Stage.IsStageChange())
            {
                Stage.ChangeStage();
            }

            Stage.Update();
        }

        private static SceneKind _currentScene = SceneKind.Title;
        public static SceneKind GetCurrentScene()
        {
            return _currentScene;
        }
        private static bool _isSceneChange = false;
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
        public static void ChangeScene()
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
                        InitInGame();
                        break;
                }
            }
        }
    }
}
