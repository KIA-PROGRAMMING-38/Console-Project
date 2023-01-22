namespace ConsoleGame
{
    internal class Program
    {
        static void Main()
        {
            SceneManager sceneManager = new SceneManager();
            Scene scene = sceneManager.scene;  // 처음엔 타이틀 화면으로 시작
            Player player = sceneManager.player;

            while(true)
            {
                
                ConsoleKey key = default;

                sceneManager.TestRender(key);

                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey().Key;
                }

                switch (key)
                {
                    case ConsoleKey.Enter:
                        scene.SceneId = 1;
                        break;

                    case ConsoleKey.Spacebar:
                        scene.SceneId = 2;
                        break;

                    

                    default:
                        break;
                }

                if (sceneManager.IsSceneChange(scene.SceneId))
                {
                    sceneManager.ChangeScene();
                }


            }
        }
    }
}