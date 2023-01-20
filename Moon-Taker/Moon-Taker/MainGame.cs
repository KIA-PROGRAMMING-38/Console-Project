using System;

namespace Moon_Taker
{
    class MainGame
    {
        static void Main()
        {
            
            Functions.InitialSettings();
            Functions.Render("\n\n Press e to start game.");
            Functions.StartGame(out GameRules.isGameStarted);
            
        }
    }
}