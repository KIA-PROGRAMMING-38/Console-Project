﻿
namespace SnakeGame
{
    class GameMain
    {
        
        static void Main()
        {
            Console.SetWindowSize(120, 40);
            Console.SetWindowPosition(0, 0);
            GameManager.Instance.Initialize();
            GameManager.Instance.GameLoop();
            GameManager.Instance.Release();
        }
    }
}