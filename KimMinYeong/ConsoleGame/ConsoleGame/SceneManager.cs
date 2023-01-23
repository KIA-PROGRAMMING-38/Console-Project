using System;
using System.Collections.Generic;
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
        public static SceneKind CurrentScene;
        public static SceneKind CapturedScene;

        public static bool IsSceneChange()
        {
            if(CurrentScene == CapturedScene)
            {
                return false;
            }
            else
            {
                CapturedScene = CurrentScene;
                return true;
            }
        }

        public static void ChangeScene()
        {
            CapturedScene = CurrentScene;
            Console.Clear();

            switch (CurrentScene)
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
                    Console.WriteLine($"잘못된 SceneKind 입니다. {CurrentScene}");
                    return;
            }
        }

        public static void RenderCurrentScene()
        {
            switch (CurrentScene)
            {
                case SceneKind.Title:
                    UpdateTitle();
                    RenderTitle();
                    break;
                case SceneKind.InGame:
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
                    Console.WriteLine($"잘못된 SceneId 입니다. {CurrentScene}");
                    return;
            }
        }

        public static void InitTitle()
        {
            Console.SetWindowSize(120, 30);
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
            //Thread.Sleep(1000);
        }

        public static void UpdateTitle()
        {
            SceneData.preTitleCursorY = SceneData.titleCursorY;

            switch (Input.CheckInputKey())
            {
                case ConsoleKey.UpArrow:
                    SceneData.titleCursorY -= 2;
                    break;

                case ConsoleKey.DownArrow:
                    SceneData.titleCursorY += 2;
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
                case 15:
                    CurrentScene = SceneKind.InGame;
                    break;

                case 17:
                    CurrentScene = SceneKind.GameInfo;
                    break;

                case 19:
                    Console.Clear();
                    Console.WriteLine("게임을 종료하였습니다.");
                    Environment.Exit(1);
                    return;
            }
        }

        public static void InitInGame()
        {
            Console.SetWindowSize(50, 30);
        }

        public static void RenderInGame()
        {
            // 게임 진행 화면 렌더 구현
            switch(Input.CheckInputKey())
            {
                case ConsoleKey.RightArrow:
                    Player.moveDirection = MoveDirection.Right;
                    Player.Move();
                    break;

                case ConsoleKey.LeftArrow:
                    Player.moveDirection = MoveDirection.Left;
                    Player.Move();
                    break;
            }

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
                CurrentScene = SceneKind.Title;
            }
        }

        public static void InitEnding()
        {

        }

        public static void RenderEnding()
        {
            // 결과 화면 렌더 구현
            Console.Clear();
            Console.WriteLine("결과 화면 입니다.");
            Thread.Sleep(1000);
        }
    }
}
