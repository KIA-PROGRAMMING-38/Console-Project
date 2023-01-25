using System.Text;

namespace SnakeGame
{
    public class GameManager : Singleton<GameManager>
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

            Console.SetWindowSize(GameDataManager.SCREEN_WIDTH, GameDataManager.SCREEN_HEIGHT);
            Console.SetWindowPosition(0, 0);

            Console.OutputEncoding = Encoding.UTF8;         
            Console.CursorVisible = false;

            // 게임오브젝트 매니저 인스턴스 생성
            GameObjectManager.Instance.Start();

            // 1. 씬 정보 로드
            SceneManager.Instance.Load();

            // 2. 씬 정보에서 데이터 추출
            GameDataManager.Instance.Load();

            // 3. 사운드 로드
            SoundManager.Instance.Load();
            SoundManager.Instance.AddSound("CollisionSound", new System.Media.SoundPlayer(Path.Combine(GameDataManager.ResourcePath, "Sound", "CollisionSound.wav")));

            // 시작 씬 설정
            SceneManager.Instance.SetStartScene("Stage_4");
        }

        /// <summary>
        /// 게임루프
        /// </summary>
        public void GameLoop()
        {
            while (true)
            {
                TimeManager.Instance.Update();
                InputManager.Instance.Update();
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
