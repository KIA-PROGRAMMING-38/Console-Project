using System.Text;

namespace SnakeGame
{
    public class GameManager : LazySingleton<GameManager>
    {
        public GameManager()
        {
        }

        public bool IsGameSet = false;

        public void Initialize()
        {
            Console.OutputEncoding = Encoding.UTF8;         
            Console.CursorVisible = false;
            Console.SetBufferSize(1920, 1080);

            GameDataManager.Instance.Load();
            SoundManager.Instance.Load();
            SceneManager.Instance.Load();
        }

        public void GameLoop()
        {
            while (true)
            {
                TimeManager.Instance.Update();

                if (IsGameSet == true)
                {
                    break;
                }

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
