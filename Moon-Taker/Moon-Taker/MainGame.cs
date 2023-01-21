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
            
            Player player;
            Wall[] walls;
            Enemy[] enemies;
            Block[] blocks;
            Moon moon;
            Key key;
            Door door;

            string[] Stage1 = Functions.LoadStage(1, out player, out walls, out enemies, out blocks, out moon, out key, out door);
            Functions.ParseStage(Stage1, out player, out walls, out enemies, out blocks, out moon, out key, out door);
        }
    }
}