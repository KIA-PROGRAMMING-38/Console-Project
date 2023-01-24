namespace SnakeGame
{
    public class DeadScene : Scene
    {
        public override void Start()
        {
            SoundManager.Instance.Play(_soundName, true);
        }

        public override void Update()
        {
            if (InputManager.Instance.IsKeyDown(ConsoleKey.Enter))
            {
                SceneManager.Instance.ChangeFlagOn(_nextSceneName);
            }
        }

        public override void Render()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("Die...");
            Console.SetCursorPosition(0, 1);
            Console.Write("Enter누르면 종료");
        }

    }
}
