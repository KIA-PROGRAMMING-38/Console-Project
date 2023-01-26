using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleSoloProject
{
    internal class Scene
    {
        public static int X = 45;
        public static int Y = 16;
        public static string Icon = "▶";
        public static bool titleScene = true;
        public static bool gameScene = true;
        public static bool waitScene = true;
        public static bool exit = true;
        public static ConsoleKey key;

        public static void TitleScene()
        {
            while (titleScene)
            {
                Console.Clear();

                Console.SetCursorPosition(0, 5);
                Console.WriteLine($"  _    _ _____   _____ _____            _____  ______         _____  ______ ______ ______ _   _  _____ ______ \r\n | |  | |  __ \\ / ____|  __ \\     /\\   |  __ \\|  ____|       |  __ \\|  ____|  ____|  ____| \\ | |/ ____|  ____|\r\n | |  | | |__) | |  __| |__) |   /  \\  | |  | | |__          | |  | | |__  | |__  | |__  |  \\| | |    | |__   \r\n | |  | |  ___/| | |_ |  _  /   / /\\ \\ | |  | |  __|         | |  | |  __| |  __| |  __| | . ` | |    |  __|  \r\n | |__| | |    | |__| | | \\ \\  / ____ \\| |__| | |____        | |__| | |____| |    | |____| |\\  | |____| |____ \r\n  \\____/|_|     \\_____|_|  \\_\\/_/    \\_\\_____/|______|       |_____/|______|_|    |______|_| \\_|\\_____|______|\r\n                                                                                                              \r\n                                                                                                             ");

                Console.SetCursorPosition(50, 15);
                Console.WriteLine("게임 시작");
                Console.SetCursorPosition(50, 16);
                Console.WriteLine("게임 설명");
                Console.SetCursorPosition(50, 17);
                Console.WriteLine("게임 종료");
                Console.SetCursorPosition(43, 20);
                Console.WriteLine("조작법 : ↑ ↓ 스페이스바");

                Console.SetCursorPosition(X, Y);
                Console.WriteLine(Icon);

                TitleSceneMove();

            }
        }

        public static void TitleSceneMove()


        {
            key = Console.ReadKey().Key;

            if (key == ConsoleKey.UpArrow)
            {
                Y = Math.Max(Y - 1, 15);
            }
            if (key == ConsoleKey.DownArrow)
            {
                Y = Math.Min(Y + 1, 17);

            }
            if (key == ConsoleKey.Spacebar && Y == 15)
            {
                waitScene = true;
                titleScene = false;
            }
            if (key == ConsoleKey.Spacebar && Y == 16)
            {
                GameExplanation();
            }

            if (key == ConsoleKey.Spacebar && Y == 17)
            {
                EndScene();
            }


        }
        public static void GameExplanation()
        {
            Console.Clear();
            string hello = "환영합니다 게임의 조작키는 D, G, R, 스페이스바로 구성되어 있습니다.";
            string gameclear = "모든 몬스터를 처치하면 스테이지 클리어, 제한시간이 지나가면 실패 입니다.";
            string returnGame = "타이틀 화면으로 돌아가시려면 R 키를 눌러주십시오.";
            string gameStart = "게임을 시작하시려면 아무키나 입력해 주십시오.";


            if (Console.KeyAvailable)

                key = Console.ReadKey().Key;


            for (int i = 0; i < hello.Length; ++i)
            {
                Console.SetCursorPosition(9 + (i * 2), 3);
                Console.Write(hello[i]);
                Thread.Sleep(1000 / 50);

            }
            for (int i = 0; i < gameclear.Length; ++i)
            {
                Thread.Sleep(1000 / 50);

                Console.SetCursorPosition(9 + (i * 2), 5);
                Console.Write(gameclear[i]);
            }
            for (int i = 0; i < returnGame.Length; ++i)
            {
                Thread.Sleep(1000 / 50);

                Console.SetCursorPosition(9 + (i * 2), 7);
                Console.Write(returnGame[i]);
            }
            for (int i = 0; i < gameStart.Length; ++i)
            {
                Thread.Sleep(1000 / 50);

                Console.SetCursorPosition(9 + (i * 2), 9);
                Console.Write(gameStart[i]);
            }

            waitScene = true;
            exit = false;
            Game.gameWait();

            if (key == ConsoleKey.R)
            {

                TitleScene();

            }



        }

        public static void TitleReLoad()
        {
            titleScene = true;
            waitScene = true;

            TitleScene();

        }

        public static void EndScene()
        {
            Console.Clear();
            waitScene = false;
            titleScene = false;
            Console.SetCursorPosition(0, 7);
            Console.WriteLine($"   _____                       _      _____                               _ \r\n  / ____|                     | |    / ____|                             | |\r\n | |  __    ___     ___     __| |   | |  __    __ _   _ __ ___     ___   | |\r\n | | |_ |  / _ \\   / _ \\   / _` |   | | |_ |  / _` | | '_ ` _ \\   / _ \\  | |\r\n | |__| | | (_) | | (_) | | (_| |   | |__| | | (_| | | | | | | | |  __/  |_|\r\n  \\_____|  \\___/   \\___/   \\__,_|    \\_____|  \\__,_| |_| |_| |_|  \\___|  (_)\r\n                                                                            \r\n                                                                            ");

        }



    }
}
