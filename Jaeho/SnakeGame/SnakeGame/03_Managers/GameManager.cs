using System.Text;

namespace SnakeGame
{
    public class GameManager : LazySingleton<GameManager>
    {
        public GameManager()
        {
        }

        public bool IsGameSet = false;

        /// <summary>
        /// 게임 초기화
        /// </summary>
        public void Initialize()
        {
            Console.OutputEncoding = Encoding.UTF8;         
            Console.CursorVisible = false;
            Console.SetBufferSize(1920, 1080);

            // 1. 씬 정보 로드
            SceneManager.Instance.Load();

            // 2. 씬 정보에서 데이터 추출
            GameDataManager.Instance.Load();

            // 3. 사운드 로드
            SoundManager.Instance.Load();
        }

        /// <summary>
        /// 게임루프
        /// </summary>
        public void GameLoop()
        {
            while (true)
            {
                TimeManager.Instance.Update();
                //x InputManager.Instance.Update(); 

                if (IsGameSet == true)
                {
                    break;
                }

                SceneManager.Instance.Update();
                SceneManager.Instance.Render();

                Thread.Sleep(TimeManager.MS_PER_FRAME);
            }
        }

        /// <summary>
        /// 리소스 해제
        /// </summary>
        public void Release()
        {
            ColliderManager.Instance.Release();
            RenderManager.Instance.Release();
            GameObjectManager.Instance.Release();
            SceneManager.Instance.Release();
            SoundManager.Instance.Release();
            TimeManager.Instance.Release();
        }

    }
}
