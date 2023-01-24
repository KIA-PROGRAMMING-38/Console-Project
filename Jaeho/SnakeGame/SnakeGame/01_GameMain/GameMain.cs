
namespace SnakeGame
{
    class GameMain
    {
        static void Main()
        {

            GameManager.Instance.Initialize();

            GameManager.Instance.GameLoop();

            GameManager.Instance.Release();
        }
    }
}