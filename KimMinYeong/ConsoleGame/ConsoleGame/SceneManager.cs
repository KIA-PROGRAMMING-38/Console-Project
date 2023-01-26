using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public enum SceneKind
    {
        Title,
        InGame,
        GameInfo,
        Ending
    }
    public static class SceneManager
    {
        public static SceneKind _currentScene;
        public static SceneKind _capturedScene;

        public static Thread obstacleCreate = new Thread(() => Obstacle.Create());
        public static Thread obstacleFly = new Thread(() => Obstacle.Fly());

        public static bool IsSceneChange()
        {
            if(_currentScene == _capturedScene)
            {
                return false;
            }
            else
            {
                _capturedScene = _currentScene;
                return true;
            }
        }

        public static void ChangeScene()
        {
            _capturedScene = _currentScene;
            Console.Clear();

            switch (_currentScene)
            {
                case SceneKind.Title:
                    InitTitle();
                    break;
                case SceneKind.InGame:
                    InitInGame();
                    break;
                case SceneKind.GameInfo:
                    InitGameInfo();
                    break;
                case SceneKind.Ending:
                    InitEnding();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"잘못된 SceneKind 입니다. {_currentScene}");
                    return;
            }
        }

        public static void RenderCurrentScene()
        {
            switch (_currentScene)
            {
                case SceneKind.Title:
                    UpdateTitle();
                    RenderTitle();
                    break;
                case SceneKind.InGame:
                    UpdateInGame();
                    RenderInGame();
                    break;
                case SceneKind.GameInfo:
                    UpdateGameInfo();
                    RenderGameInfo();
                    break;
                case SceneKind.Ending:
                    RenderEnding();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"잘못된 SceneId 입니다. {_currentScene}");
                    return;
            }
        }

        public static void InitTitle()
        {
            Console.SetWindowSize(1920, 1080);
        }

        public static void RenderTitle()
        {
            // 타이틀 씬 렌더 구현
            for(int i = 0; i < SceneData.gameTitle.Length; ++i)
            {
                Console.SetCursorPosition(30, i + 3);
                Console.WriteLine(SceneData.gameTitle[i]);
            }

            Console.SetCursorPosition(SceneData.titleOptionsX, SceneData.titleOption1Y);
            Console.WriteLine(SceneData.titleOption1);

            Console.SetCursorPosition(SceneData.titleOptionsX, SceneData.titleOption2Y);
            Console.WriteLine(SceneData.titleOption2);

            Console.SetCursorPosition(SceneData.titleOptionsX, SceneData.titleOption3Y);
            Console.WriteLine(SceneData.titleOption3);

            if(SceneData.preTitleCursorY != SceneData.titleCursorY)
            {
                Console.SetCursorPosition(SceneData.titleOptionsX - 4, SceneData.preTitleCursorY);
                Console.Write("  ");

            }
            
            Console.SetCursorPosition(SceneData.titleOptionsX - 4, SceneData.titleCursorY);
            Console.Write(SceneData.cursorIcon);
        }

        public static void UpdateTitle()
        {
            SceneData.preTitleCursorY = SceneData.titleCursorY;

            switch (Input.CheckInputKey())
            {
                case ConsoleKey.UpArrow:
                    SceneData.titleCursorY = Math.Max(15, SceneData.titleCursorY - 2);
                    break;

                case ConsoleKey.DownArrow:
                    SceneData.titleCursorY = Math.Min(SceneData.titleCursorY + 2, 19);
                    break;

                case ConsoleKey.Enter:
                    CheckTitleToWhere();
                    break;


            }
        }

        public static void CheckTitleToWhere()
        {
            switch(SceneData.titleCursorY)
            {
                case SceneData.titleOption1Y:
                    _currentScene = SceneKind.InGame;
                    break;

                case SceneData.titleOption2Y:
                    _currentScene = SceneKind.GameInfo;
                    break;

                case SceneData.titleOption3Y:
                    Console.Clear();
                    Console.WriteLine("게임을 종료하였습니다.");
                    Environment.Exit(1);
                    return;
            }
        }

        public static Stopwatch createTargetWatch = new Stopwatch();
        public static Stopwatch flyTargetWatch = new Stopwatch();
        public static Stopwatch bulletWatch = new Stopwatch();
        public static void InitInGame()
        {
            Console.SetWindowSize(50, 30);
            obstacleCreate.Start();
            obstacleFly.Start();
            //targetCreate.Start();
            //targetFly.Start();

            // 스탑워치 start 여기서 해야할듯?
            createTargetWatch.Restart();
            flyTargetWatch.Restart();
            bulletWatch.Restart();
            Target.Create();
            Target.Fly();
            Bullet.Shoot();
            Bullet.Fly();
            Bullet.IsCollisionWithSomething();
        }

        public static void UpdateInGame()
        {
            switch (Input.CheckInputKey())
            {
                case ConsoleKey.RightArrow:
                    Player._moveDirection = MoveDirection.Right;
                    Player.Move();
                    break;

                case ConsoleKey.LeftArrow:
                    Player._moveDirection = MoveDirection.Left;
                    Player.Move();
                    break;
            }

            if(createTargetWatch.ElapsedMilliseconds > 5000)
            {
                Target.Create();
                createTargetWatch.Restart();
            }

            if(flyTargetWatch.ElapsedMilliseconds > 500)
            {
                Target.Fly();
                flyTargetWatch.Restart();
            }

            if(bulletWatch.ElapsedMilliseconds > 300)
            {
                Bullet.Shoot();
                Bullet.Fly();
                Bullet.IsCollisionWithSomething();
                bulletWatch.Restart();
            }

            Target.IsEnoughDeadNumber();
        }

        public static void RenderInGame()
        {
            // 게임 진행 화면 렌더 구현
            Bullet.Render();
            Target.Render();
            Obstacle.Render();
            Player.Render();
        }

        public static void InitGameInfo()
        {
            Console.SetWindowSize(118, 30);
        }

        public static void RenderGameInfo()
        {
            Console.SetCursorPosition(0, 0);

            for(int i = 0; i < SceneData.infos.Length; ++i)
            {
                Console.WriteLine(SceneData.infos[i]);
            }
        }

        public static void UpdateGameInfo()
        {
            if(Input.IsKeyDown(ConsoleKey.Enter))
            {
                _currentScene = SceneKind.Title;
            }
        }

        public static Random random = new Random();
        public static int _selectedGift;
        public static void InitEnding()
        {
            _selectedGift = random.Next(SceneData.gifts.Length);
        }

        public static void RenderEnding()
        {
            // 결과 화면 렌더 구현
            Console.Clear();
            Console.WriteLine(SceneData.gifts[_selectedGift]);
            Thread.Sleep(1000);
        }
    }
}
