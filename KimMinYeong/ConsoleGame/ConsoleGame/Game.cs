using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public static class Game
    {
        /// <summary>
        /// 게임 시작에 필요한 초기화 진행
        /// </summary>
        public static void Init()
        {
            Console.SetWindowSize(118, 30);
            Console.CursorVisible = false;
            Console.Title = "Shoot";
        }

        /// <summary>
        /// 게임 진행
        /// </summary>
        public static void Run()
        {
            while (true)
            {
                if(SceneManager.IsSceneChange())
                {
                    SceneManager.ChangeScene();
                }

                SceneManager.RenderCurrentScene();

                Input.Process();
            }
        }

        public static void End()
        {
            Console.Clear();
            Console.WriteLine("게임을 종료하였습니다.");
            Environment.Exit(1);
        }
    }
}
