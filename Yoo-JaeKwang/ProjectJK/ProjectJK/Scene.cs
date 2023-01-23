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
        private static void RenderTitle()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 0);
            Console.Write("Title");
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
