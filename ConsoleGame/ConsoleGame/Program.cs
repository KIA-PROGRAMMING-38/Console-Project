using System.Media;
using System.Runtime.CompilerServices;

namespace ConsoleGame
{
    internal class Program
    {
        static void Main()
        {
            // 초기 설정
            GameManager.Initialize();

            SceneManager._sceneType = Scene.Title;

            while (true)
            {
                switch (SceneManager._sceneType)
                {
                    case Scene.Title:
                        SceneManager.Title();
                        break;
                    case Scene.Story:
                        SceneManager.Story();
                        break;
                    case Scene.Exit:
                        SceneManager.Exit();
                        break;
                    case Scene.Town:
                        SceneManager.Town();
                        break;
                    case Scene.RaceTrack:
                        SceneManager.RaceScene();
                        break;
                    case Scene.Shop:
                        SceneManager.ShopScene();
                        break;
                    default:
                        GameManager.ExitWithError($"잘못된 Scene타입입니다. {SceneManager._sceneType}");
                        return;
                }
            }
        }
    }
}