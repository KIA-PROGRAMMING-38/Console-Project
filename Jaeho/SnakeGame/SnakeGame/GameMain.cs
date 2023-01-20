
using ConsoleGame;
using ConsoleGame.Managers;
using System.Text;

class GameMain
{
    static void Main()
    {
        GameManager.Instance.Initialize();

        GameManager.Instance.GameLoop();

        GameManager.Instance.Release();
    }
}