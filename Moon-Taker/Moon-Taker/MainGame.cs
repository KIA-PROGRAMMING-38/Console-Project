using System;
using System.Security.Cryptography.X509Certificates;

namespace Moon_Taker
{
    class MainGame
    {
        static void Main()
        {
            
            Functions.InitialSettings();
            Functions.Render("\n\n Press e to start game.");
            Functions.StartStage(out GameRules.isGameStarted, ref GameRules.stageNum);

            Player player = new Player();
            Wall[] walls;
            Enemy[] enemies;
            Block[] blocks;
            Moon moon;
            Key key;
            Door door;
            MapSize mapSize = new MapSize();
            PreviousPlayer prevPlayer = new PreviousPlayer();
            PreviousEnemy[] prevEnemy;
            GameRules.stageSettingNum = GameRules.stageNum;

            Console.Clear();

            string[] Stage1 = Functions.LoadStage(1, out player, out walls, out enemies, out blocks, out moon, out key, out door);

            for (int i = 0; i < Stage1.Length - 1; ++i)
            {
                Console.WriteLine(Stage1[i]);
            }

            Functions.ParseStage(Stage1, out player, out walls, out enemies, out blocks, out moon, out key, out door, out mapSize);

            ++GameRules.stageSettingNum;
            
            while (GameRules.stageNum == 1)
            {

                    Console.Clear();

                Functions.RenderObject(player.X, player.Y, Constants.player);
                for(int enemyId = 0; enemyId < enemies.Length; ++enemyId)
                {
                    Functions.RenderObject(enemies[enemyId].X, enemies[enemyId].Y, Constants.enemy);
                }

                ConsoleKey Input = Console.ReadKey().Key;

                switch (Input)
                {
                    case ConsoleKey.RightArrow:
                        Actions.MovePlayerToRight(ref player.X, mapSize.X, GameRules.playerMoveDirection);
                        break;
                    case ConsoleKey.LeftArrow:
                        Actions.MovePlayerToLeft(ref player.X, mapSize.X, GameRules.playerMoveDirection);
                        break;
                    case ConsoleKey.DownArrow:
                        Actions.MovePlayerToDown(ref player.Y, mapSize.Y, GameRules.playerMoveDirection);
                        break;
                    case ConsoleKey.UpArrow:
                        Actions.MovePlayerToUp(ref player.Y, mapSize.Y, GameRules.playerMoveDirection);
                        break;
                }

                for (int enemyId = 0; enemyId < enemies.Length; ++enemyId)
                {
                    if (Actions.IsCollided(player.X, player.Y, enemies[enemyId].X, enemies[enemyId].Y))
                    {
                        switch (Input)
                        {
                            case ConsoleKey.RightArrow:
                                Actions.PushRight(ref enemies[enemyId].X, ref player.X);
                                Actions.CollidSolidOnLeft(ref player.X);
                                break;
                            case ConsoleKey.LeftArrow:
                                Actions.PushLeft(ref enemies[enemyId].X, ref player.X);
                                Actions.CollidSolidOnRight(ref player.X);
                                break;
                            case ConsoleKey.DownArrow:
                                Actions.PushDown(ref enemies[enemyId].Y, ref player.Y);
                                Actions.CollidSolidOnUp(ref player.Y);
                                break;
                            case ConsoleKey.UpArrow:
                                Actions.PushUp(ref enemies[enemyId].Y, ref player.Y);
                                Actions.CollidSolidOnDown(ref player.Y);
                                break;
                        }
                    }
                }
            }
        }
    }
}