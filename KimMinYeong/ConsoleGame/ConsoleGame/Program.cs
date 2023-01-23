namespace ConsoleGame
{
    internal class Program
    {
        static void Main()
        {

            while(true)
            {

                SceneManager.RenderCurrentScene();

                Input.Process();

                switch (Input.CheckInputKey())
                {
                    case ConsoleKey.Enter:
                        Scene.SceneId = 1;
                        break;

                    case ConsoleKey.Spacebar:
                        Scene.SceneId = 2;
                        break;

                    

                    default:
                        break;
                }

                if (SceneManager.IsSceneChange(Scene.SceneId))
                {
                    SceneManager.ChangeScene();
                }


            }
        }
    }
}