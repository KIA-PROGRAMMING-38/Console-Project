using System.Text;

namespace SnakeGame
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

            GameDataManager.Instance.LoadMapData();
            SceneManager.Instance.LoadScenes();
        }

        public void GameLoop()
        {
            while (true)
            {
                //Console.Clear();
                TimeManager.Instance.Update();
                if (IsGameSet == true) break;
                //SceneManager.Instance.Render();
                SceneManager.Instance.Update();
                SceneManager.Instance.Render();
                Thread.Sleep(TimeManager.MS_PER_FRAME);
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
