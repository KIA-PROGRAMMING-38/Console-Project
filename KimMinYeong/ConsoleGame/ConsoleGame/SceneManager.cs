using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    internal class SceneManager
    {
        public int CurrentSceneId;
        public int CapturedSceneId;
        public Scene scene = new Scene();

        public Player player = new Player { BeforeX = 0, UpdateX = 0 };

        public bool IsSceneChange(int currentSceneId)
        {
            CurrentSceneId = currentSceneId;

            if(CurrentSceneId == CapturedSceneId)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ChangeScene()
        {
            CapturedSceneId = CurrentSceneId;
            Console.Clear();
        }

        public void RenderCurrentScene()
        {
            switch (CurrentSceneId)
            {
                case 0:
                    RenderTitleScene();
                    break;
                case 1:
                    //RenderMainScene();
                    break;
                case 2:
                    RenderEndScene();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"잘못된 SceneId 입니다. {CurrentSceneId}");
                    return;
            }
        }

        public void TestRender(ConsoleKey key)
        {
            switch (CurrentSceneId)
            {
                case 0:
                    RenderTitleScene();
                    break;
                case 1:
                    RenderMainScene(key);
                    break;
                case 2:
                    RenderEndScene();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"잘못된 SceneId 입니다. {CurrentSceneId}");
                    return;
            }
        }

        public void RenderTitleScene()
        {
            // 타이틀 씬 렌더 구현
            Console.Clear();
            Console.WriteLine(scene.TitleSceneData);
            Thread.Sleep(1000);
            
        }

        public void RenderMainScene(ConsoleKey key)
        {
            // 게임 진행 화면 렌더 구현
            switch(key)
            {
                case ConsoleKey.RightArrow:
                    player.moveDirection = MoveDirection.Right;
                    player.Move();
                    break;

                case ConsoleKey.LeftArrow:
                    player.moveDirection = MoveDirection.Left;
                    player.Move();
                    break;
            }

            player.Render();
            Thread.Sleep(1000);

        }

        public void RenderEndScene()
        {
            // 결과 화면 렌더 구현
            Console.Clear();
            Console.WriteLine(scene.EndSceneData);
            Thread.Sleep(1000);
        }
    }
}
