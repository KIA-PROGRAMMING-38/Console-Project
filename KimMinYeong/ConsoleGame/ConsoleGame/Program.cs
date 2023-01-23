namespace ConsoleGame
{
    internal class Program
    {
        static void Main()
        {
            SceneManager sceneManager = new SceneManager();
            Scene scene = sceneManager.scene;  // 처음엔 타이틀 화면으로 시작

            while(true)
            {

                sceneManager.RenderCurrentScene();

                Input.Process();

                switch (Input.CheckInputKey())
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