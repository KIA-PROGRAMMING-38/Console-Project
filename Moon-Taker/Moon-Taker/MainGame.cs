using System;
using System.Diagnostics;
using System.Media;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;

namespace Moon_Taker
{
    class MainGame
    {
        static void Main()
        {
            while (false == GameSettings.isGameStarted)
            {
                if (Scene.isFirstScene)
                {
                    Scene.SetTitleScene();
                }
                ConsoleKey moveMenu = Console.ReadKey().Key;

                Functions.SelectMenu(ref GameSettings.MenuNum, ref moveMenu);
                if (moveMenu == ConsoleKey.E)
                {
                    SoundPlayer soundPlayer = new SoundPlayer(@"Assets\Sound\Select.wav");
                    soundPlayer.PlaySync();
                    if (GameSettings.MenuNum == 0)
                    {
                        Scene.EnterSynopsisScene();
                    }
                    else if (GameSettings.MenuNum == 1)
                    {
                        Scene.EnterGameRulesScene();
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
            }

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
            Advice[] advices = new Advice[0];
            Trace[] traces = new Trace[0];

            string[][] Stage = new string[GameSettings.stageNumber + 1][];
            Functions.CheckStageNumber();
            Functions.NameStage(GameSettings.stageFilePath);
            for (int stageId = 1; stageId < Stage.Length; ++stageId)
            {
                Stage[stageId] = Functions.LoadFile(GameSettings.stageFilePath[stageId]);
            }
            string[] advice = Functions.LoadFile(GameSettings.adviceFilePath);
            string[] trace = Functions.LoadFile(GameSettings.traceFilePath);
            Functions.ParseAdvice(advice, out advices);
            Functions.ParseTrace(trace, out traces);

            while (GameSettings.isGameStarted)
            {
                if (false == GameSettings.isBGMPlaying)
                {
                    Functions.PlayBGM("Assets\\Sound\\BGM.wav");
                    GameSettings.isBGMPlaying = true;
                }
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

                if (false == Actions.IsCollided(player.x, player.y, previousPlayer.x, previousPlayer.y))
                {
                    Functions.ClearObject(previousPlayer.x, previousPlayer.y);
                }

                for (int enemyId = 0; enemyId < enemies.Length; enemyId++)
                {
                    if (Actions.IsCollided(enemies[enemyId].x, enemies[enemyId].y, previousEnemies[enemyId].x, previousEnemies[enemyId].y))
                    {
                        continue;
                    }
                    Functions.ClearObject(previousEnemies[enemyId].x, previousEnemies[enemyId].y);
                }

                for (int blockId = 0; blockId < blocks.Length; blockId++)
                {
                    if (Actions.IsCollided(blocks[blockId].x, blocks[blockId].y, previousBlocks[blockId].x, previousBlocks[blockId].y))
                    {
                        continue;
                    }
                    Functions.ClearObject(previousBlocks[blockId].x, previousBlocks[blockId].y);
                }

                for (int enemyId = 0; enemyId < enemies.Length; ++enemyId)
                {
                    for (int trapId = 0; trapId < traps.Length; ++trapId)
                    {
                        if (Actions.IsCollided(traps[trapId].x, traps[trapId].y, enemies[enemyId].x, enemies[enemyId].y))
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
                        if (Actions.IsCollided(traps[trapId].x, traps[trapId].y, blocks[blockId].x, blocks[blockId].y))
                        {
                            blocks[blockId].isOnTrap = true;
                            break;
                        }
                        blocks[blockId].isOnTrap = false;
                    }
                }

                for (int wallId = 0; wallId < walls.Length; ++wallId)
                {
                    Functions.Render(walls[wallId].x, walls[wallId].y, Constants.wall, Constants.wallColor);
                }
                for (int trapId = 0; trapId < traps.Length; ++trapId)
                {
                    if (traps[trapId].isActivated)
                    {
                        Functions.Render(traps[trapId].x, traps[trapId].y, Constants.activatedTrap, Constants.trapColor);
                    }
                    else
                    {
                        Functions.Render(traps[trapId].x, traps[trapId].y, Constants.deactivatedTrap, Constants.trapColor);
                    }
                }
                for (int enemyId = 0; enemyId < enemies.Length; ++enemyId)
                {
                    if (enemies[enemyId].isAlive)
                    {
                        if (enemies[enemyId].isOnTrap)
                        {
                            Functions.Render(enemies[enemyId].x, enemies[enemyId].y, Constants.enemyOnTrap, Constants.enemyColor);
                        }
                        else
                        {
                            Functions.Render(enemies[enemyId].x, enemies[enemyId].y, Constants.enemy, Constants.enemyColor);
                        }
                    }
                }
                for (int blockId = 0; blockId < blocks.Length; ++blockId)
                {
                    if (blocks[blockId].isOnTrap)
                    {
                        Functions.Render(blocks[blockId].x, blocks[blockId].y, Constants.blockOnTrap, Constants.blockColor);
                    }
                    else
                    {
                        Functions.Render(blocks[blockId].x, blocks[blockId].y, Constants.block, Constants.blockColor);
                    }
                }
                if (StageSettings.doesKeyExist)
                {
                    if (false == ObjectStatus.hasKey)
                    {
                        Functions.Render(key.x, key.y, Constants.key, Constants.keyColor);
                    }
                    if (false == ObjectStatus.isDoorOpened)
                    {
                        Functions.Render(door.x, door.y, Constants.door, Constants.doorColor);
                    }
                }

                LookUpTable.objectColor[LookUpTable.objectColor.Length - 1] = Functions.RandomColor();
                Functions.Render(moon.x, moon.y, Constants.moon, LookUpTable.objectColor[LookUpTable.objectColor.Length - 1]);
                Functions.Render(player.x, player.y, Constants.player, Constants.playerColor);


                for (int i = 0; i < LookUpTable.controlHelp.Length; ++i)
                {
                    Functions.Render(Stage[StageSettings.currentStage][0].Length + 4, i, LookUpTable.controlHelp[i]);
                }
                Functions.Render(Stage[StageSettings.currentStage][0].Length + 3, LookUpTable.controlHelp.Length + 2, "/------------------------------\\");
                for (int i = 0; i < LookUpTable.objectDescription.Length; ++i)
                {
                    if (i % 2 == 0)
                    {
                        Functions.Render(Stage[StageSettings.currentStage][0].Length + 3, (i / 2) + LookUpTable.controlHelp.Length + 3, "|");
                        Functions.Render(Stage[StageSettings.currentStage][0].Length + 4, (i / 2) + LookUpTable.controlHelp.Length + 3, LookUpTable.objectDescription[i], LookUpTable.objectColor[i]);
                    }
                    else
                    {
                        Functions.Render(LookUpTable.objectDescription[i], LookUpTable.objectColor[i]);
                        Functions.Render("|");
                    }
                }
                Functions.Render(Stage[StageSettings.currentStage][0].Length + 3, LookUpTable.objectDescription.Length + 2, "\\------------------------------/");
                Functions.Render(Stage[StageSettings.currentStage][0].Length + 3, LookUpTable.objectDescription.Length + 4, "남은 ");
                Functions.Render("행동 점수", Constants.movePointColor);
                Functions.Render($" : {ObjectStatus.playerMovePoint}".PadLeft(2, ' '));


                if (ObjectStatus.isAdviceToggled)
                {
                    int adviceNumber = -1;
                    Functions.PickAdviceNumber(advices, ref adviceNumber);
                    Functions.WriteAdvice(Stage[StageSettings.currentStage][0].Length + 3, LookUpTable.objectDescription.Length + 5, advices, ref adviceNumber);
                    if (advices[adviceNumber].name == "최선문" && StageSettings.currentStage != GameSettings.stageNumber)
                    {
                        Scene.EnterBlessedScene(advices, adviceNumber);
                    }
                    ObjectStatus.isAdviceToggled = false;
                    if (StageSettings.isBlessed)
                    {
                        goto LoopEnd;
                    }
                }

                ConsoleKey Input = Console.ReadKey().Key;
                Functions.Render("\b ");

                Functions.SavePreviousObject(out previousPlayer.x, out previousPlayer.y, player.x, player.y);
                for (int enemyId = 0; enemyId < enemies.Length; enemyId++)
                {
                    Functions.SavePreviousObject(out previousEnemies[enemyId].x, out previousEnemies[enemyId].y,
                        enemies[enemyId].x, enemies[enemyId].y);
                }
                for (int blockId = 0; blockId < blocks.Length; blockId++)
                {
                    Functions.SavePreviousObject(out previousBlocks[blockId].x, out previousBlocks[blockId].y,
                        blocks[blockId].x, blocks[blockId].y);
                }

                switch (Input)
                {
                    case ConsoleKey.RightArrow:
                        Actions.MovePlayerToRight(ref player.x, mapSize.x);
                        --ObjectStatus.playerMovePoint;
                        break;
                    case ConsoleKey.LeftArrow:
                        Actions.MovePlayerToLeft(ref player.x, mapSize.x);
                        --ObjectStatus.playerMovePoint;
                        break;
                    case ConsoleKey.DownArrow:
                        Actions.MovePlayerToDown(ref player.y, mapSize.y);
                        --ObjectStatus.playerMovePoint;
                        break;
                    case ConsoleKey.UpArrow:
                        Actions.MovePlayerToUp(ref player.y, mapSize.y);
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
                        traps[trapId].isActivated = true ^ traps[trapId].isActivated;
                    }
                }

                for (int enemyId = 0; enemyId < enemies.Length; ++enemyId)
                {
                    if (false == Actions.IsCollided(player.x, player.y, enemies[enemyId].x, enemies[enemyId].y))
                    {
                        continue;
                    }
                    ObjectStatus.pushedEnemyId = enemyId;
                    switch (Input)
                    {
                        case ConsoleKey.RightArrow:
                            Actions.PushRight(ref enemies[enemyId].x, ref player.x);
                            Actions.CollidSolidOnLeft(ref player.x);
                            break;
                        case ConsoleKey.LeftArrow:
                            Actions.PushLeft(ref enemies[enemyId].x, ref player.x);
                            Actions.CollidSolidOnRight(ref player.x);
                            break;
                        case ConsoleKey.DownArrow:
                            Actions.PushDown(ref enemies[enemyId].y, ref player.y);
                            Actions.CollidSolidOnUp(ref player.y);
                            break;
                        case ConsoleKey.UpArrow:
                            Actions.PushUp(ref enemies[enemyId].y, ref player.y);
                            Actions.CollidSolidOnDown(ref player.y);
                            break;
                    }
                }

                for (int blockId = 0; blockId < blocks.Length; ++blockId)
                {
                    if (false == Actions.IsCollided(player.x, player.y, blocks[blockId].x, blocks[blockId].y))
                    {
                        continue;
                    }
                    ObjectStatus.pushedBlockId = blockId;

                    switch (Input)
                    {
                        case ConsoleKey.RightArrow:
                            Actions.PushRight(ref blocks[blockId].x, ref player.x);
                            Actions.CollidSolidOnLeft(ref player.x);
                            break;
                        case ConsoleKey.LeftArrow:
                            Actions.PushLeft(ref blocks[blockId].x, ref player.x);
                            Actions.CollidSolidOnRight(ref player.x);
                            break;
                        case ConsoleKey.DownArrow:
                            Actions.PushDown(ref blocks[blockId].y, ref player.y);
                            Actions.CollidSolidOnUp(ref player.y);
                            break;
                        case ConsoleKey.UpArrow:
                            Actions.PushUp(ref blocks[blockId].y, ref player.y);
                            Actions.CollidSolidOnDown(ref player.y);
                            break;
                    }
                }

                for (int wallId = 0; wallId < walls.Length; wallId++)
                {
                    if (false == Actions.IsCollided(player.x, player.y, walls[wallId].x, walls[wallId].y))
                    {
                        continue;
                    }
                    switch (Input)
                    {
                        case ConsoleKey.RightArrow:
                            Actions.CollidSolidOnLeft(ref player.x);
                            break;
                        case ConsoleKey.LeftArrow:
                            Actions.CollidSolidOnRight(ref player.x);
                            break;
                        case ConsoleKey.DownArrow:
                            Actions.CollidSolidOnUp(ref player.y);
                            break;
                        case ConsoleKey.UpArrow:
                            Actions.CollidSolidOnDown(ref player.y);
                            break;
                    }
                }

                for (int wallId = 0; wallId < walls.Length; wallId++)
                {
                    if (enemies.Length == 0)
                    {
                        break;
                    }
                    if (enemies[ObjectStatus.pushedEnemyId].isAlive == false)
                    {
                        continue;
                    }
                    if (Actions.IsCollided(walls[wallId].x, walls[wallId].y, enemies[ObjectStatus.pushedEnemyId].x, enemies[ObjectStatus.pushedEnemyId].y))
                    {
                        enemies[ObjectStatus.pushedEnemyId].x = 0;
                        enemies[ObjectStatus.pushedEnemyId].y = 0;
                        enemies[ObjectStatus.pushedEnemyId].isAlive = false;
                        Console.Beep();
                    }
                }

                for (int wallId = 0; wallId < walls.Length; wallId++)
                {
                    if (blocks.Length == 0)
                    {
                        break;
                    }
                    if (Actions.IsCollided(walls[wallId].x, walls[wallId].y, blocks[ObjectStatus.pushedBlockId].x, blocks[ObjectStatus.pushedBlockId].y)
                        || Actions.IsCollided(blocks[ObjectStatus.pushedBlockId].x, blocks[ObjectStatus.pushedBlockId].y, moon.x, moon.y))
                    {
                        switch (Input)
                        {
                            case ConsoleKey.RightArrow:
                                Actions.CollidSolidOnLeft(ref blocks[ObjectStatus.pushedBlockId].x);
                                break;
                            case ConsoleKey.LeftArrow:
                                Actions.CollidSolidOnRight(ref blocks[ObjectStatus.pushedBlockId].x);
                                break;
                            case ConsoleKey.DownArrow:
                                Actions.CollidSolidOnUp(ref blocks[ObjectStatus.pushedBlockId].y);
                                break;
                            case ConsoleKey.UpArrow:
                                Actions.CollidSolidOnDown(ref blocks[ObjectStatus.pushedBlockId].y);
                                break;
                        }
                    }
                }

                for (int collidedenemyId = 0; collidedenemyId < enemies.Length; ++collidedenemyId)
                {
                    if (ObjectStatus.pushedEnemyId == collidedenemyId || enemies[collidedenemyId].isAlive == false)
                    {
                        continue;
                    }
                    if (Actions.IsCollided(enemies[ObjectStatus.pushedEnemyId].x, enemies[ObjectStatus.pushedEnemyId].y, enemies[collidedenemyId].x, enemies[collidedenemyId].y))
                    {
                        enemies[ObjectStatus.pushedEnemyId].x = 0;
                        enemies[ObjectStatus.pushedEnemyId].y = 0;
                        enemies[ObjectStatus.pushedEnemyId].isAlive = false;
                        Console.Beep();
                    }
                }

                for (int collidedBlockId = 0; collidedBlockId < blocks.Length; ++collidedBlockId)
                {
                    if (ObjectStatus.pushedBlockId == collidedBlockId)
                    {
                        continue;
                    }
                    if (Actions.IsCollided(blocks[ObjectStatus.pushedBlockId].x, blocks[ObjectStatus.pushedBlockId].y, blocks[collidedBlockId].x, blocks[collidedBlockId].y))
                    {
                        switch (Input)
                        {
                            case ConsoleKey.RightArrow:
                                Actions.CollidSolidOnLeft(ref blocks[ObjectStatus.pushedBlockId].x);
                                break;
                            case ConsoleKey.LeftArrow:
                                Actions.CollidSolidOnRight(ref blocks[ObjectStatus.pushedBlockId].x);
                                break;
                            case ConsoleKey.DownArrow:
                                Actions.CollidSolidOnUp(ref blocks[ObjectStatus.pushedBlockId].y);
                                break;
                            case ConsoleKey.UpArrow:
                                Actions.CollidSolidOnDown(ref blocks[ObjectStatus.pushedBlockId].y);
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
                    if (Actions.IsCollided(enemies[ObjectStatus.pushedEnemyId].x, enemies[ObjectStatus.pushedEnemyId].y, blocks[blockId].x, blocks[blockId].y))
                    {
                        enemies[ObjectStatus.pushedEnemyId].x = 0;
                        enemies[ObjectStatus.pushedEnemyId].y = 0;
                        enemies[ObjectStatus.pushedEnemyId].isAlive = false;
                        Console.Beep();
                    };
                }

                for (int enemyId = 0; enemyId < enemies.Length; ++enemyId)
                {
                    if (blocks.Length == 0)
                    {
                        break;
                    }
                    if (Actions.IsCollided(blocks[ObjectStatus.pushedBlockId].x, blocks[ObjectStatus.pushedBlockId].y, enemies[enemyId].x, enemies[enemyId].y))
                    {
                        switch (Input)
                        {
                            case ConsoleKey.RightArrow:
                                Actions.CollidSolidOnLeft(ref blocks[ObjectStatus.pushedBlockId].x);
                                break;
                            case ConsoleKey.LeftArrow:
                                Actions.CollidSolidOnRight(ref blocks[ObjectStatus.pushedBlockId].x);
                                break;
                            case ConsoleKey.DownArrow:
                                Actions.CollidSolidOnUp(ref blocks[ObjectStatus.pushedBlockId].y);
                                break;
                            case ConsoleKey.UpArrow:
                                Actions.CollidSolidOnDown(ref blocks[ObjectStatus.pushedBlockId].y);
                                break;
                        }
                    };
                }

                for (int trapId = 0; trapId < traps.Length; ++trapId)
                {
                    if (traps[trapId].isActivated == true && Actions.IsCollided(traps[trapId].x, traps[trapId].y, player.x, player.y))
                    {
                        ObjectStatus.playerMovePoint -= 1;
                        Console.Beep();
                    }
                }

                if (StageSettings.doesKeyExist)
                {
                    if (Actions.IsCollided(player.x, player.y, key.x, key.y))
                    {
                        ObjectStatus.hasKey = true;
                    }
                    if (Actions.IsCollided(player.x, player.y, door.x, door.y))
                    {
                        if (false == ObjectStatus.hasKey)
                        {
                            switch (Input)
                            {
                                case ConsoleKey.RightArrow:
                                    Actions.CollidSolidOnLeft(ref player.x);
                                    break;
                                case ConsoleKey.LeftArrow:
                                    Actions.CollidSolidOnRight(ref player.x);
                                    break;
                                case ConsoleKey.DownArrow:
                                    Actions.CollidSolidOnUp(ref player.y);
                                    break;
                                case ConsoleKey.UpArrow:
                                    Actions.CollidSolidOnDown(ref player.y);
                                    break;
                            }
                        }
                        else
                        {
                            ObjectStatus.isDoorOpened = true;
                        }
                    }
                    if (enemies.Length != 0 && Actions.IsCollided(enemies[ObjectStatus.pushedEnemyId].x, enemies[ObjectStatus.pushedEnemyId].y, door.x, door.y) && false == ObjectStatus.hasKey)
                    {
                        enemies[ObjectStatus.pushedEnemyId].x = 0;
                        enemies[ObjectStatus.pushedEnemyId].y = 0;
                        enemies[ObjectStatus.pushedEnemyId].isAlive = false;
                        Console.Beep();
                    }                  
                    if (blocks.Length != 0 && Actions.IsCollided(blocks[ObjectStatus.pushedBlockId].x, blocks[ObjectStatus.pushedBlockId].y, door.x, door.y) && false == ObjectStatus.hasKey)
                    {
                        switch (Input)
                        {
                            case ConsoleKey.RightArrow:
                                Actions.CollidSolidOnLeft(ref blocks[ObjectStatus.pushedBlockId].x);
                                break;
                            case ConsoleKey.LeftArrow:
                                Actions.CollidSolidOnRight(ref blocks[ObjectStatus.pushedBlockId].x);
                                break;
                            case ConsoleKey.DownArrow:
                                Actions.CollidSolidOnUp(ref blocks[ObjectStatus.pushedBlockId].y);
                                break;
                            case ConsoleKey.UpArrow:
                                Actions.CollidSolidOnDown(ref blocks[ObjectStatus.pushedBlockId].y);
                                break;
                        }
                    }
                }
                if (Actions.IsCollided(player.x, player.y, moon.x, moon.y))
                {
                    bool isYes = true;
                    Scene.EnterFoundTraceScene(traces);

                    while (false == StageSettings.isStageCleared)
                    {
                        Console.SetCursorPosition(0, 5);
                        ConsoleKey yesOrNo = Console.ReadKey().Key;
                        Console.Write("\b \b");
                        Functions.ChooseOX(yesOrNo, ref isYes);
                        if (yesOrNo == ConsoleKey.E)
                        {
                            if (isYes ^ traces[StageSettings.currentStage - 1].isUseful)
                            {
                                Scene.EnterBadChoiceScene(traces);
                                break;
                            }
                            if (StageSettings.currentStage < GameSettings.stageNumber)
                            {
                                Scene.EnterStageClearScene(traces);
                                break;
                            }
                            else if (StageSettings.currentStage == GameSettings.stageNumber)
                            {
                                Scene.EnterGameClearScene(traces);
                            }
                        }
                    }
                    continue;
                }

                if (ObjectStatus.playerMovePoint <= 0)
                {
                    Scene.EnterGameOverScene();
                }
            LoopEnd:;
            }

        }
    }
}


