namespace ConsoleGame
{
    public class EndingScene : Scene
    {
        public EndingScene(string nextScene)
        {
            _nextSceneName = nextScene;
        }

        public InputKeyComponent InputKey;

        public override void Start()
        {
             InputKey = new InputKeyComponent();
            SoundManager.Instance.Play("EndingBackgroundMusic");
        }

        public override void Update()
        {
            InputKey.Update();
            switch (InputKey.Key)
            {
                case ConsoleKey.Enter:
                    SceneManager.Instance.ChangeScene(_nextSceneName);
                    return;
                    break;
            }
        }

        public override void Render()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("엔딩화면...");
            Console.SetCursorPosition(0, 1);
            Console.Write("Enter누르면 종료");
        }

    }
}
