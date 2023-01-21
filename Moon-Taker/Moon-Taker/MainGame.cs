using System;
using System.Runtime.CompilerServices;
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
            Console.SetCursorPosition(6, 14);
            Functions.Render("Press e to start game.");
            Functions.StartStage(out StageSettings.isGameStarted, ref StageSettings.stageNum);

            Player player = new Player();
            Wall[] walls;
            Enemy[] enemies;
            Block[] blocks;
            Trap[] traps;
            Moon moon;
            Key key;
            Door door;
            MapSize mapSize = new MapSize();
            Advice[] advice = new Advice[5]
            {
                new Advice {name = "김윤하", advice = "이기지 못하면 합류하라"},
                new Advice {name = "김도익", advice = "최고의 가라는 FM이다"},
                new Advice {name = "엄종하", advice = "헉"},
                new Advice {name = "김창연", advice = "(블루투스 동글이를 빼간다)"},
                new Advice {name = "김규법", advice = "겐지가 함께한다"},
            };

            string[] Stage1 = Functions.LoadStage(1, out player, out walls, out enemies, out blocks, out traps, out moon, out key, out door);
            string[] Stage2 = Functions.LoadStage(2, out player, out walls, out enemies, out blocks, out traps, out moon, out key, out door);
       

            while (StageSettings.isGameStarted)
            {
                Console.Clear();

                if (StageSettings.stageNum == 1 && StageSettings.isStageReseted)
                {
                    string[] Stage1Reset = Functions.LoadStage(1, out player, out walls, out enemies, out blocks, out traps, out moon, out key, out door);

                    for (int i = 0; i < Stage1.Length - 1; ++i)
                    {
                        Console.WriteLine(Stage1[i]);
                    }

                    Functions.ParseStage(Stage1, out player, out walls, out enemies, out blocks, out traps, out moon, out key, out door, out mapSize);
                    StageSettings.isStageReseted = false;
                    ObjectsStatus.playerMovePoint = 23;
                    ObjectsStatus.isTrapToggled = true;
                }

                else if (StageSettings.stageNum == 2 && StageSettings.isStageReseted)
                {
                    string[] Stage2Reset = Functions.LoadStage(2, out player, out walls, out enemies, out blocks, out traps, out moon, out key, out door);

                    for (int i = 0; i < Stage2.Length - 1; ++i)
                    {
                        Console.WriteLine(Stage2[i]);
                    }

                    Functions.ParseStage(Stage2, out player, out walls, out enemies, out blocks, out traps, out moon, out key, out door, out mapSize);
                    StageSettings.isStageReseted = false;
                    ObjectsStatus.playerMovePoint = 24;
                    ObjectsStatus.isTrapToggled = true;
                }

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
                for (int trapId = 0; trapId < traps.Length; ++trapId)
                {
                    if (ObjectsStatus.isTrapToggled)
                    {
                        Functions.RenderObject(traps[trapId].X, traps[trapId].Y, Constants.trap);
                    }
                }
                Functions.RenderObject(moon.X, moon.Y, Constants.moon);
                Console.SetCursorPosition(Stage1[0].Length + 3, Stage1.Length / 2);
                Functions.Render($"Your Move Point : {ObjectsStatus.playerMovePoint}");
                if (ObjectsStatus.isAdviceToggled)
                {
                    Functions.WriteAdvice(advice, mapSize);
                    ObjectsStatus.isAdviceToggled = false;
                }
                ConsoleKey Input = Console.ReadKey().Key;

                switch (Input)
                {
                    case ConsoleKey.RightArrow:
                        Actions.MovePlayerToRight(ref player.X, mapSize.X, ObjectsStatus.playerMoveDirection);
                        --ObjectsStatus.playerMovePoint;
                        ObjectsStatus.isTrapToggled = true ^ ObjectsStatus.isTrapToggled;
                        break;
                    case ConsoleKey.LeftArrow:
                        Actions.MovePlayerToLeft(ref player.X, mapSize.X, ObjectsStatus.playerMoveDirection);
                        --ObjectsStatus.playerMovePoint;
                        ObjectsStatus.isTrapToggled = true ^ ObjectsStatus.isTrapToggled;
                        break;
                    case ConsoleKey.DownArrow:
                        Actions.MovePlayerToDown(ref player.Y, mapSize.Y, ObjectsStatus.playerMoveDirection);
                        --ObjectsStatus.playerMovePoint;
                        ObjectsStatus.isTrapToggled = true ^ ObjectsStatus.isTrapToggled;
                        break;
                    case ConsoleKey.UpArrow:
                        Actions.MovePlayerToUp(ref player.Y, mapSize.Y, ObjectsStatus.playerMoveDirection);
                        --ObjectsStatus.playerMovePoint;
                        ObjectsStatus.isTrapToggled = true ^ ObjectsStatus.isTrapToggled;
                        break;
                    case ConsoleKey.R:
                        StageSettings.isStageReseted = true;
                        continue;
                    case ConsoleKey.A:
                        ObjectsStatus.isAdviceToggled = true;
                        continue;
                }

                for (int enemyId = 0; enemyId < enemies.Length; ++enemyId)
                {
                    if (Actions.IsCollided(player.X, player.Y, enemies[enemyId].X, enemies[enemyId].Y))
                    {
                        ObjectsStatus.pushedEnemyId = enemyId;
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
                        ObjectsStatus.pushedBlockId = blockId;
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
                    if (Actions.IsCollided(player.X, player.Y, walls[wallId].X, walls[wallId].Y))
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

                for (int wallId = 0; wallId < walls.Length; wallId++)
                {
                    for (int enemyId = 0; enemyId < enemies.Length; ++enemyId)
                    {
                        if (enemies[enemyId].IsAlive == false)
                        {
                            continue;
                        }
                        if (Actions.IsCollided(walls[wallId].X, walls[wallId].Y, enemies[enemyId].X, enemies[enemyId].Y))
                        {
                            enemies[enemyId].X = 0;
                            enemies[enemyId].Y = 0;
                            enemies[enemyId].IsAlive = false;
                            Console.Beep();
                        }
                    }
                }

                for (int collidedenemyId = 0; collidedenemyId < enemies.Length; ++collidedenemyId)
                {
                    if (ObjectsStatus.pushedEnemyId == collidedenemyId || enemies[collidedenemyId].IsAlive == false)
                    {
                        continue;
                    }
                    if (Actions.IsCollided(enemies[ObjectsStatus.pushedEnemyId].X, enemies[ObjectsStatus.pushedEnemyId].Y, enemies[collidedenemyId].X, enemies[collidedenemyId].Y))
                    {
                        enemies[ObjectsStatus.pushedEnemyId].X = 0;
                        enemies[ObjectsStatus.pushedEnemyId].Y = 0;
                        enemies[ObjectsStatus.pushedEnemyId].IsAlive = false;
                        Console.Beep();
                    }
                }

                for (int wallId = 0; wallId < walls.Length; wallId++)
                {
                    for (int blockId = 0; blockId < blocks.Length; ++blockId)
                    {
                        if (Actions.IsCollided(walls[wallId].X, walls[wallId].Y, blocks[blockId].X, blocks[blockId].Y)
                            || Actions.IsCollided(blocks[blockId].X, blocks[blockId].Y, moon.X, moon.Y))
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
                    if (ObjectsStatus.pushedBlockId == collidedBlockId)
                    {
                        continue;
                    }

                    if (Actions.IsCollided(blocks[ObjectsStatus.pushedBlockId].X, blocks[ObjectsStatus.pushedBlockId].Y, blocks[collidedBlockId].X, blocks[collidedBlockId].Y))
                    {
                        switch (Input)
                        {
                            case ConsoleKey.RightArrow:
                                Actions.CollidSolidOnLeft(ref blocks[ObjectsStatus.pushedBlockId].X);
                                break;
                            case ConsoleKey.LeftArrow:
                                Actions.CollidSolidOnRight(ref blocks[ObjectsStatus.pushedBlockId].X);
                                break;
                            case ConsoleKey.DownArrow:
                                Actions.CollidSolidOnUp(ref blocks[ObjectsStatus.pushedBlockId].Y);
                                break;
                            case ConsoleKey.UpArrow:
                                Actions.CollidSolidOnDown(ref blocks[ObjectsStatus.pushedBlockId].Y);
                                break;
                        }
                    }
                }

                for(int trapId = 0; trapId < traps.Length; ++trapId)
                {
                    if (ObjectsStatus.isTrapToggled && Actions.IsCollided(traps[trapId].X, traps[trapId].Y, player.X, player.Y))
                    {
                        ObjectsStatus.playerMovePoint -= 2;
                    }
                }

                if (Actions.IsCollided(player.X, player.Y, moon.X, moon.Y))
                {
                    Functions.EnterStageClearScene(ref StageSettings.stageNum);
                    continue;
                }
                if (ObjectsStatus.playerMovePoint <= 0)
                {
                    Functions.EnterGameOverScene(ObjectsStatus.playerMovePoint);
                }
            }
        }
    }
}
