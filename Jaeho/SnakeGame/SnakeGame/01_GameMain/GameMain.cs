
namespace SnakeGame
{
    class GameMain
    {
        
        static void Main()
        {
            Console.SetWindowSize(120, 34);
            Console.SetWindowPosition(0, 0);
            GameManager.Instance.Initialize();
            GameManager.Instance.GameLoop();
            GameManager.Instance.Release();
        }
    }
}