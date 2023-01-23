using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
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

            Scene.SetTitleScene();

            Player player = new Player();
            PreviousPlayer previousPlayer = new PreviousPlayer();
            Wall[] walls = new Wall[0];
            Enemy[] enemies = new Enemy[0];
            PreviousEnemy[] previousEnemies = new PreviousEnemy[0];
            Block[] blocks = new Block[0];
            PreviousBlock[] previousBlocks = new PreviousBlock[0];
            Trap[] traps = new Trap[0];
            Moon moon = new Moon();
            Key key = new Key();
            Door door = new Door();
            MapSize mapSize = new MapSize();
            Advice[] advice = new Advice[0];

            string[][] Stage = new string[GameSettings.stageNumber + 1][];
            Functions.CheckStageNumber();
            Functions.NameStage(GameSettings.stageFilePath);
            for (int stageId = 1; stageId < Stage.Length; ++stageId)
            {
                Stage[stageId] = Functions.LoadFile(GameSettings.stageFilePath[stageId]);
            }
            string[] advices = Functions.LoadFile(GameSettings.adviceFilePath);
            Functions.ParseAdvice(advices, out advice);
            Scene.EnterSynopsisScene();
            Scene.EnterGameRulesScene();

            while (GameSettings.isGameStarted)
            {
                if (StageSettings.isStageReseted)
                {
                    Console.Clear();
                    for (int stageId = 0; stageId < Stage[StageSettings.currentStage].Length - 1; ++stageId)
                    {
                        Console.WriteLine(Stage[StageSettings.currentStage][stageId]);
                    }
                    
                    Functions.ParseStage(Stage[StageSettings.currentStage], out player, out previousPlayer, out walls, 
                        out enemies, out previousEnemies, out blocks, out previousBlocks, out traps, out key, out door, out moon, out mapSize);

                    Functions.ResetStageSetting(StageSettings.doesKeyExist, StageSettings.stageMovePoint);
                }
                
                if (false == Actions.IsCollided(player.X, player.Y, previousPlayer.X, previousPlayer.Y))
                {
                    Functions.ClearObject(previousPlayer.X, previousPlayer.Y);
                }
                
                for (int enemyId = 0; enemyId < enemies.Length; enemyId++)
                {
                    if (Actions.IsCollided(enemies[enemyId].X, enemies[enemyId].Y, previousEnemies[enemyId].X, previousEnemies[enemyId].Y))
                    {
                        continue;
                    }
                    Functions.ClearObject(previousEnemies[enemyId].X, previousEnemies[enemyId].Y);
                }

                for (int blockId = 0; blockId < blocks.Length; blockId++)
                {
                    if (Actions.IsCollided(blocks[blockId].X, blocks[blockId].Y, previousBlocks[blockId].X, previousBlocks[blockId].Y))
                    {
                        continue;
                    }
                    Functions.ClearObject(previousBlocks[blockId].X, previousBlocks[blockId].Y);
                }

                for (int enemyId = 0; enemyId < enemies.Length; ++enemyId)
                {
                    for (int trapId = 0; trapId < traps.Length; ++trapId)
                    {
                        if (Actions.IsCollided(traps[trapId].X, traps[trapId].Y, enemies[enemyId].X, enemies[enemyId].Y))
                        {
                            enemies[enemyId].isOnTrap = true;
                            break;
                        }
                        enemies[enemyId].isOnTrap = false;
                    }
                }

                for (int blockId = 0; blockId < blocks.Length; ++blockId)
                {
                    for (int trapId = 0; trapId < traps.Length; ++trapId)
                    {
                        if (Actions.IsCollided(traps[trapId].X, traps[trapId].Y, blocks[blockId].X, blocks[blockId].Y))
                        {
                            blocks[blockId].isOnTrap = true;
                            break;
                        }
                        blocks[blockId].isOnTrap = false;
                    }
                }

                for (int wallId = 0; wallId < walls.Length; ++wallId)
                {
                    Functions.Render(walls[wallId].X, walls[wallId].Y, Constants.wall, Constants.wallColor);
                }
                for (int trapId = 0; trapId < traps.Length; ++trapId)
                {
                    if (traps[trapId].IsActivated)
                    {
                        Functions.Render(traps[trapId].X, traps[trapId].Y, Constants.activatedTrap, Constants.trapColor);
                    }
                    else
                    {
                        Functions.Render(traps[trapId].X, traps[trapId].Y, Constants.deactivatedTrap, Constants.trapColor);
                    }
                }
                for (int enemyId = 0; enemyId < enemies.Length; ++enemyId)
                {
                    if (enemies[enemyId].IsAlive)
                    {
                        if (enemies[enemyId].isOnTrap)
                        {
                            Functions.Render(enemies[enemyId].X, enemies[enemyId].Y, Constants.enemyOnTrap, Constants.enemyColor);
                        }
                        else
                        {
                            Functions.Render(enemies[enemyId].X, enemies[enemyId].Y, Constants.enemy, Constants.enemyColor);
                        }
                    }
                }
                for (int blockId = 0; blockId < blocks.Length; ++blockId)
                {
                    if (blocks[blockId].isOnTrap)
                    {
                        Functions.Render(blocks[blockId].X, blocks[blockId].Y, Constants.blockOnTrap, Constants.blockColor);
                    }
                    else
                    {
                        Functions.Render(blocks[blockId].X, blocks[blockId].Y, Constants.block, Constants.blockColor);
                    }
                }
                if (StageSettings.doesKeyExist)
                {
                    if (false == ObjectStatus.hasKey)
                    {
                        Functions.Render(key.X, key.Y, Constants.key, Constants.keyColor);
                    }
                    if (false == ObjectStatus.isDoorOpened)
                    {
                        Functions.Render(door.X, door.Y, Constants.door, Constants.doorColor);
                    }
                }

                LookUpTable.objectColor[LookUpTable.objectColor.Length - 1] = Functions.RandomColor();
                Functions.Render(moon.X, moon.Y, Constants.moon, LookUpTable.objectColor[LookUpTable.objectColor.Length - 1]);
                Functions.Render(player.X, player.Y, Constants.player, Constants.playerColor);

                for (int stageId = 1; stageId < Stage.Length; ++stageId)
                {
                    if (stageId != StageSettings.currentStage)
                    {
                        continue;
                    }
                    for (int i = 0; i < LookUpTable.controlHelp.Length; ++i)
                    {
                        Functions.Render(Stage[stageId][0].Length + 2, i, LookUpTable.controlHelp[i]);
                    }
                    for (int i = 0; i < LookUpTable.objectDescription.Length; ++i)
                    {
                        if (i % 2 == 0)
                        {
                            Functions.Render(Stage[stageId][0].Length + 2, (i / 2) + LookUpTable.controlHelp.Length + 1, LookUpTable.objectDescription[i], LookUpTable.objectColor[i]);
                        }
                        else
                        {
                            Functions.Render(LookUpTable.objectDescription[i], LookUpTable.objectColor[i]);
                        }
                    }
                    Functions.Render(mapSize.X / 2, mapSize.Y + 1, "남은 행동 횟수 : " + $"{ObjectStatus.playerMovePoint}".PadLeft(2,' '));
                }

                if (ObjectStatus.isAdviceToggled)
                {
                    int adviceNumber = -1;
                    Functions.PickAdviceNumber(advice, ref adviceNumber);
                    Functions.WriteAdvice(advice, mapSize, ref adviceNumber);
                    if (advice[adviceNumber].name == "최선문" && StageSettings.currentStage != GameSettings.stageNumber)
                    {
                        Scene.EnterBlessedScene(advice, adviceNumber);
                    }
                    ObjectStatus.isAdviceToggled = false;
                    if (StageSettings.isBlessed)
                    {
                        goto LoopEnd;
                    }
                }

                ConsoleKey Input = Console.ReadKey().Key;
                Functions.Render("\b ");

                Functions.SavePreviousObject(out previousPlayer.X, out previousPlayer.Y, player.X, player.Y);
                for (int enemyId = 0; enemyId < enemies.Length; enemyId++)
                {
                    Functions.SavePreviousObject(out previousEnemies[enemyId].X , out previousEnemies[enemyId].Y, 
                        enemies[enemyId].X, enemies[enemyId].Y);
                }
                for (int blockId = 0; blockId < blocks.Length; blockId++)
                {
                    Functions.SavePreviousObject(out previousBlocks[blockId].X, out previousBlocks[blockId].Y,
                        blocks[blockId].X, blocks[blockId].Y);
                }

                switch (Input)
                {
                    case ConsoleKey.RightArrow:
                        Actions.MovePlayerToRight(ref player.X, mapSize.X);
                        --ObjectStatus.playerMovePoint;
                        break;
                    case ConsoleKey.LeftArrow:
                        Actions.MovePlayerToLeft(ref player.X, mapSize.X);
                        --ObjectStatus.playerMovePoint;
                        break;
                    case ConsoleKey.DownArrow:
                        Actions.MovePlayerToDown(ref player.Y, mapSize.Y);
                        --ObjectStatus.playerMovePoint;
                        break;
                    case ConsoleKey.UpArrow:
                        Actions.MovePlayerToUp(ref player.Y, mapSize.Y);
                        --ObjectStatus.playerMovePoint;
                        break;
                    case ConsoleKey.R:
                        StageSettings.isStageReseted = true;
                        continue;
                    case ConsoleKey.A:
                        ObjectStatus.isAdviceToggled = true;
                        continue;
                    default:
                        break;
                }

                for (int trapId = 0; trapId < traps.Length; ++trapId)
                {
                    if (Input == ConsoleKey.RightArrow || Input == ConsoleKey.LeftArrow || Input == ConsoleKey.DownArrow || Input == ConsoleKey.UpArrow)
                    {
                        traps[trapId].IsActivated = true ^ traps[trapId].IsActivated;
                    }
                }

                for (int enemyId = 0; enemyId < enemies.Length; ++enemyId)
                {
                    if (false == Actions.IsCollided(player.X, player.Y, enemies[enemyId].X, enemies[enemyId].Y))
                    {
                        continue;
                    }
                    ObjectStatus.pushedEnemyId = enemyId;
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

                for (int blockId = 0; blockId < blocks.Length; ++blockId)
                {
                    if (false == Actions.IsCollided(player.X, player.Y, blocks[blockId].X, blocks[blockId].Y))
                    {
                        continue;
                    }
                    ObjectStatus.pushedBlockId = blockId;
                    
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

                for (int wallId = 0; wallId < walls.Length; wallId++)
                {
                    if (false == Actions.IsCollided(player.X, player.Y, walls[wallId].X, walls[wallId].Y))
                    {
                        continue;
                    }
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

                for (int wallId = 0; wallId < walls.Length; wallId++)
                {
                    if (enemies.Length == 0)
                    {
                        break;
                    }
                    if (enemies[ObjectStatus.pushedEnemyId].IsAlive == false)
                    {
                        continue;
                    }
                    if (Actions.IsCollided(walls[wallId].X, walls[wallId].Y, enemies[ObjectStatus.pushedEnemyId].X, enemies[ObjectStatus.pushedEnemyId].Y))
                    {
                        enemies[ObjectStatus.pushedEnemyId].X = 0;
                        enemies[ObjectStatus.pushedEnemyId].Y = 0;
                        enemies[ObjectStatus.pushedEnemyId].IsAlive = false;
                        Console.Beep();
                    }
                }

                for (int wallId = 0; wallId < walls.Length; wallId++)
                {
                    if (blocks.Length == 0)
                    {
                        break;
                    }
                    if (Actions.IsCollided(walls[wallId].X, walls[wallId].Y, blocks[ObjectStatus.pushedBlockId].X, blocks[ObjectStatus.pushedBlockId].Y)
                        || Actions.IsCollided(blocks[ObjectStatus.pushedBlockId].X, blocks[ObjectStatus.pushedBlockId].Y, moon.X, moon.Y))
                    {
                        switch (Input)
                        {
                            case ConsoleKey.RightArrow:
                                Actions.CollidSolidOnLeft(ref blocks[ObjectStatus.pushedBlockId].X);
                                break;
                            case ConsoleKey.LeftArrow:
                                Actions.CollidSolidOnRight(ref blocks[ObjectStatus.pushedBlockId].X);
                                break;
                            case ConsoleKey.DownArrow:
                                Actions.CollidSolidOnUp(ref blocks[ObjectStatus.pushedBlockId].Y);
                                break;
                            case ConsoleKey.UpArrow:
                                Actions.CollidSolidOnDown(ref blocks[ObjectStatus.pushedBlockId].Y);
                                break;
                        }
                    }
                }

                for (int collidedenemyId = 0; collidedenemyId < enemies.Length; ++collidedenemyId)
                {
                    if (ObjectStatus.pushedEnemyId == collidedenemyId || enemies[collidedenemyId].IsAlive == false)
                    {
                        continue;
                    }
                    if (Actions.IsCollided(enemies[ObjectStatus.pushedEnemyId].X, enemies[ObjectStatus.pushedEnemyId].Y, enemies[collidedenemyId].X, enemies[collidedenemyId].Y))
                    {
                        enemies[ObjectStatus.pushedEnemyId].X = 0;
                        enemies[ObjectStatus.pushedEnemyId].Y = 0;
                        enemies[ObjectStatus.pushedEnemyId].IsAlive = false;
                        Console.Beep();
                    }
                }

                for (int collidedBlockId = 0; collidedBlockId < blocks.Length; ++collidedBlockId)
                {
                    if (ObjectStatus.pushedBlockId == collidedBlockId)
                    {
                        continue;
                    }
                    if (Actions.IsCollided(blocks[ObjectStatus.pushedBlockId].X, blocks[ObjectStatus.pushedBlockId].Y, blocks[collidedBlockId].X, blocks[collidedBlockId].Y))
                    {
                        switch (Input)
                        {
                            case ConsoleKey.RightArrow:
                                Actions.CollidSolidOnLeft(ref blocks[ObjectStatus.pushedBlockId].X);
                                break;
                            case ConsoleKey.LeftArrow:
                                Actions.CollidSolidOnRight(ref blocks[ObjectStatus.pushedBlockId].X);
                                break;
                            case ConsoleKey.DownArrow:
                                Actions.CollidSolidOnUp(ref blocks[ObjectStatus.pushedBlockId].Y);
                                break;
                            case ConsoleKey.UpArrow:
                                Actions.CollidSolidOnDown(ref blocks[ObjectStatus.pushedBlockId].Y);
                                break;
                        }
                    }
                }

                for (int blockId = 0; blockId < blocks.Length; ++blockId)
                {
                    if (enemies.Length == 0)
                    {
                        break;
                    }
                    if (Actions.IsCollided(enemies[ObjectStatus.pushedEnemyId].X, enemies[ObjectStatus.pushedEnemyId].Y, blocks[blockId].X, blocks[blockId].Y))
                    {
                        enemies[ObjectStatus.pushedEnemyId].X = 0;
                        enemies[ObjectStatus.pushedEnemyId].Y = 0;
                        enemies[ObjectStatus.pushedEnemyId].IsAlive = false;
                        Console.Beep();
                    };
                }

                for (int trapId = 0; trapId < traps.Length; ++trapId)
                {
                    if (traps[trapId].IsActivated == true && Actions.IsCollided(traps[trapId].X, traps[trapId].Y, player.X, player.Y))
                    {
                        ObjectStatus.playerMovePoint -= 1;
                        Console.Beep();
                    }
                }

                if (StageSettings.doesKeyExist)
                {
                    if (Actions.IsCollided(player.X, player.Y, key.X, key.Y))
                    {
                        ObjectStatus.hasKey = true;
                    }

                    if (Actions.IsCollided(player.X, player.Y, door.X, door.Y))
                    {
                        if (false == ObjectStatus.hasKey)
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
                        else
                        {
                            ObjectStatus.isDoorOpened = true;
                        }
                    }
                }

                if (Actions.IsCollided(player.X, player.Y, moon.X, moon.Y))
                {
                    if (StageSettings.currentStage < GameSettings.stageNumber)
                    {
                        Scene.EnterStageClearScene(ref StageSettings.currentStage);
                        continue;
                    }
                    else if (StageSettings.currentStage == GameSettings.stageNumber)
                    {
                        Scene.EnterGameClearScene();
                    }
                }

                if (ObjectStatus.playerMovePoint <= 0)
                {
                    Scene.EnterGameOverScene(ObjectStatus.playerMovePoint);
                }
            LoopEnd:;
            }
        }
    }
}
