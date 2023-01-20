using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public GameManager()
        {
        }

        public bool IsGameSet = false;

        public void Initialize()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            SceneManager.Instance.AddScene("TitleScene", new TitleScene("Stage_1"));
            SceneManager.Instance.AddScene("EndingScene", new EndingScene("TitleScene"));
            SceneManager.Instance.AddScene("Stage_1", new StageOne("Stage_2"));
            SceneManager.Instance.AddScene("Stage_2", new StageTwo("Stage_2"));
            SceneManager.Instance.SetCurrentScene("TitleScene");
            SceneManager.Instance.Start();
        }

        public void GameLoop()
        {
            while (true)
            {
                Console.Clear();
                TimeManager.Instance.Update();
                if (IsGameSet == true) break;

                SceneManager.Instance.Update();
                SceneManager.Instance.Render();
                Thread.Sleep(TimeManager.DeltaTime);
            }
        }

        public void Release()
        {
            ColliderManager.Instance.Release();
            RenderManager.Instance.Release();
            GameObjectManager.Instance.Release();
            SceneManager.Instance.Release();
            SoundManager.Instance.Release();
        }

    }
}
