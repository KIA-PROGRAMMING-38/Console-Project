using System;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
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
                for (int wallId = 0; wallId < walls.Length; ++wallId)
                {
                    Functions.RenderObject(walls[wallId].X, walls[wallId].Y, Constants.wall);
                }
                for (int enemyId = 0; enemyId < enemies.Length; ++enemyId)
                {
                    if (enemies[enemyId].IsAlive)
                    Functions.RenderObject(enemies[enemyId].X, enemies[enemyId].Y, Constants.enemy);
                }
                for (int blockId = 0; blockId < blocks.Length; ++blockId)
                {
                    Functions.RenderObject(blocks[blockId].X, blocks[blockId].Y, Constants.block);
                }

                ConsoleKey Input = Console.ReadKey().Key;

                switch (Input)
                {
                    case ConsoleKey.RightArrow:
                        Actions.MovePlayerToRight(ref player.X, mapSize.X, Status.playerMoveDirection);
                        break;
                    case ConsoleKey.LeftArrow:
                        Actions.MovePlayerToLeft(ref player.X, mapSize.X, Status.playerMoveDirection);
                        break;
                    case ConsoleKey.DownArrow:
                        Actions.MovePlayerToDown(ref player.Y, mapSize.Y, Status.playerMoveDirection);
                        break;
                    case ConsoleKey.UpArrow:
                        Actions.MovePlayerToUp(ref player.Y, mapSize.Y, Status.playerMoveDirection);
                        break;
                }

                for (int enemyId = 0; enemyId < enemies.Length; ++enemyId)
                {
                    if (Actions.IsCollided(player.X, player.Y, enemies[enemyId].X, enemies[enemyId].Y))
                    {
                        Status.pushedEnemyId = enemyId;
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

                for (int blockId = 0; blockId < blocks.Length; ++blockId)
                {
                    if (Actions.IsCollided(player.X, player.Y, blocks[blockId].X, blocks[blockId].Y))
                    {
                        Status.pushedBlockId = blockId;
                        switch (Input)
                        {
                            case ConsoleKey.RightArrow:
                                Actions.PushRight(ref blocks[blockId].X, ref player.X);
                                Actions.CollidSolidOnLeft(ref player.X);
                                break;
                            case ConsoleKey.LeftArrow:
                                Actions.PushLeft(ref blocks[blockId].X, ref player.X);
                                Actions.CollidSolidOnRight(ref player.X);
                                break;
                            case ConsoleKey.DownArrow:
                                Actions.PushDown(ref blocks[blockId].Y, ref player.Y);
                                Actions.CollidSolidOnUp(ref player.Y);
                                break;
                            case ConsoleKey.UpArrow:
                                Actions.PushUp(ref blocks[blockId].Y, ref player.Y);
                                Actions.CollidSolidOnDown(ref player.Y);
                                break;
                        }
                    }
                }


                for (int wallId = 0; wallId < walls.Length; wallId++)
                {
                    if(Actions.IsCollided(player.X, player.Y, walls[wallId].X, walls[wallId].Y))
                    {
                        switch (Input)
                        {
                            case ConsoleKey.RightArrow:
                                Actions.CollidSolidOnLeft(ref player.X);
                                break;
                            case ConsoleKey.LeftArrow:
                                Actions.CollidSolidOnRight(ref player.X);
                                break;
                            case ConsoleKey.DownArrow:
                                Actions.CollidSolidOnUp(ref player.Y);
                                break;
                            case ConsoleKey.UpArrow:
                                Actions.CollidSolidOnDown(ref player.Y);
                                break;
                        }
                    }
                }

                for(int wallId = 0; wallId < walls.Length; wallId++)
                {
                    for (int enemyId = 0; enemyId < enemies.Length; ++enemyId)
                    {
                        if (Actions.IsCollided(walls[wallId].X, walls[wallId].Y, enemies[enemyId].X, enemies[enemyId].Y))
                        {
                            enemies[enemyId].X = 0; 
                            enemies[enemyId].Y = 0;
                            enemies[enemyId].IsAlive = false;
                        }
                    }
                }

                for (int collidedenemyId = 0; collidedenemyId < enemies.Length; ++collidedenemyId)
                {
                    if (Status.pushedEnemyId == collidedenemyId)
                    {
                        continue;
                    }

                    if (Actions.IsCollided(enemies[Status.pushedEnemyId].X, enemies[Status.pushedEnemyId].Y, enemies[collidedenemyId].X, enemies[collidedenemyId].Y))
                    {
                        enemies[Status.pushedEnemyId].X = 0;
                        enemies[Status.pushedEnemyId].Y = 0;
                        enemies[Status.pushedEnemyId].IsAlive = false;
                    }
                }

                for (int wallId = 0; wallId < walls.Length; wallId++)
                {
                    for (int blockId = 0; blockId < blocks.Length; ++blockId)
                    {
                        if (Actions.IsCollided(walls[wallId].X, walls[wallId].Y,blocks[blockId].X, blocks[blockId].Y))
                        {
                            switch (Input)
                            {
                                case ConsoleKey.RightArrow:
                                    Actions.CollidSolidOnLeft(ref blocks[blockId].X);
                                    break;
                                case ConsoleKey.LeftArrow:
                                    Actions.CollidSolidOnRight(ref blocks[blockId].X);
                                    break;
                                case ConsoleKey.DownArrow:
                                    Actions.CollidSolidOnUp(ref blocks[blockId].Y);
                                    break;
                                case ConsoleKey.UpArrow:
                                    Actions.CollidSolidOnDown(ref blocks[blockId].Y);
                                    break;
                            }
                        }
                    }
                }

                for (int collidedBlockId = 0; collidedBlockId < blocks.Length; ++collidedBlockId)
                {
                    if (Status.pushedBlockId == collidedBlockId)
                    {
                        continue;
                    }
                    
                    if (Actions.IsCollided(blocks[Status.pushedBlockId].X, blocks[Status.pushedBlockId].Y, blocks[collidedBlockId].X, blocks[collidedBlockId].Y))
                    {
                        switch (Input)
                        {
                            case ConsoleKey.RightArrow:
                                Actions.CollidSolidOnLeft(ref blocks[Status.pushedBlockId].X);
                                break;
                            case ConsoleKey.LeftArrow:
                                Actions.CollidSolidOnRight(ref blocks[Status.pushedBlockId].X);
                                break;
                            case ConsoleKey.DownArrow:
                                Actions.CollidSolidOnUp(ref blocks[Status.pushedBlockId].Y);
                                break;
                            case ConsoleKey.UpArrow:
                                Actions.CollidSolidOnDown(ref blocks[Status.pushedBlockId].Y);
                                break;
                        }
                    }
                }
            }
        }
    }
}