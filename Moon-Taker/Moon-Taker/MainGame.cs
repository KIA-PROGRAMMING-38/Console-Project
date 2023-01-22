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
            Wall[] walls = new Wall[0];
            Enemy[] enemies = new Enemy[0];
            Block[] blocks = new Block[0];
            Trap[] traps = new Trap[0];
            Moon moon = new Moon();
            Key key = new Key();
            Door door = new Door();
            MapSize mapSize = new MapSize();
            Advice[] advice = new Advice[0];

            string[][] Stage = new string[StageSettings.stageNumber + 1][];
            for (int stageId = 1; stageId < Stage.Length; ++stageId)
            {
                Stage[stageId] = Functions.LoadFile(@$"Assets\Stage\Stage{stageId}.txt", stageId);
            }
            string[] advices = Functions.LoadFile(@"Assets\Advice\Advice.txt");
            Functions.ParseAdvice(advices, out advice);
            Scene.EnterSynopsisScene();
            Scene.EnterGameRulesScene();

            while (StageSettings.isGameStarted)
            {
                Console.Clear();

                if (StageSettings.isStageReseted)
                {
                    for (int stageId = 0; stageId < Stage[StageSettings.currentStage].Length - 1; ++stageId)
                    {
                        Console.WriteLine(Stage[StageSettings.currentStage][stageId]);
                    }
                    Functions.ParseStage(Stage[StageSettings.currentStage], out player, out walls, out enemies,
                        out blocks, out traps, out key, out door, out moon, out mapSize);
                    Functions.ResetStageSetting(StageSettings.doesKeyExist, StageSettings.stageMovePoint);
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
                    Functions.RenderAt(walls[wallId].X, walls[wallId].Y, Constants.wall, Constants.wallColor);
                }
                for (int trapId = 0; trapId < traps.Length; ++trapId)
                {
                    if (ObjectStatus.isTrapToggled)
                    {
                        Functions.RenderAt(traps[trapId].X, traps[trapId].Y, Constants.trap, Constants.trapColor);
                    }
                }
                for (int enemyId = 0; enemyId < enemies.Length; ++enemyId)
                {
                    if (enemies[enemyId].IsAlive)
                    {
                        if (enemies[enemyId].isOnTrap)
                        {
                            Functions.RenderAt(enemies[enemyId].X, enemies[enemyId].Y, Constants.enemyOnTrap, Constants.enemyColor);
                        }
                        else
                        {
                            Functions.RenderAt(enemies[enemyId].X, enemies[enemyId].Y, Constants.enemy, Constants.enemyColor);
                        }
                    }
                }
                for (int blockId = 0; blockId < blocks.Length; ++blockId)
                {
                    if (blocks[blockId].isOnTrap)
                    {
                        Functions.RenderAt(blocks[blockId].X, blocks[blockId].Y, Constants.blockOnTrap, Constants.blockColor);
                    }
                    else 
                    {
                        Functions.RenderAt(blocks[blockId].X, blocks[blockId].Y, Constants.block, Constants.blockColor);
                    }
                }
                if (StageSettings.doesKeyExist)
                {
                    if (false == ObjectStatus.hasKey)
                    {
                        Functions.RenderAt(key.X, key.Y, Constants.key, Constants.keyColor);
                    }
                    if (false == ObjectStatus.isDoorOpened)
                    {
                        Functions.RenderAt(door.X, door.Y, Constants.door, Constants.doorColor);
                    }
                }

                LookUpTable.objectColor[LookUpTable.objectColor.Length - 1] = Functions.RandomColor();
                Functions.RenderAt(moon.X, moon.Y, Constants.moon, LookUpTable.objectColor[LookUpTable.objectColor.Length - 1]);
                Functions.RenderAt(player.X, player.Y, Constants.player, Constants.playerColor);

                for (int stageId = 1; stageId < Stage.Length; ++stageId)
                {
                    if (stageId != StageSettings.currentStage)
                    {
                        continue;
                    }
                    for(int i = 0; i < LookUpTable.controlHelp.Length; ++i)
                    {
                        Functions.RenderAt(Stage[stageId][0].Length + 2, i, LookUpTable.controlHelp[i]);
                    }
                    for(int i = 0; i < LookUpTable.objectDescription.Length; ++i)
                    {
                        if(i % 2 == 0)
                        {
                            Functions.RenderAt(Stage[stageId][0].Length + 2, (i / 2) + LookUpTable.controlHelp.Length + 1, LookUpTable.objectDescription[i], LookUpTable.objectColor[i]);
                        }
                        else
                        {
                            Functions.Render(LookUpTable.objectDescription[i], LookUpTable.objectColor[i]);
                        }
                    }
                    Functions.RenderAt(mapSize.X / 2, mapSize.Y + 1, $"남은 행동 횟수 : {ObjectStatus.playerMovePoint}");
                }

                if (ObjectStatus.isAdviceToggled)
                {
                    int adviceNumber = -1;
                    Functions.PickAdviceNumber(advice, ref adviceNumber);
                    Functions.WriteAdvice(advice, mapSize, ref adviceNumber);
                    if (advice[adviceNumber].name == "최선문" && StageSettings.currentStage != StageSettings.stageNumber)
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

                switch (Input)
                {
                    case ConsoleKey.RightArrow:
                        Actions.MovePlayerToRight(ref player.X, mapSize.X);
                        --ObjectStatus.playerMovePoint;
                        ObjectStatus.isTrapToggled = true ^ ObjectStatus.isTrapToggled;
                        break;
                    case ConsoleKey.LeftArrow:
                        Actions.MovePlayerToLeft(ref player.X, mapSize.X);
                        --ObjectStatus.playerMovePoint;
                        ObjectStatus.isTrapToggled = true ^ ObjectStatus.isTrapToggled;
                        break;
                    case ConsoleKey.DownArrow:
                        Actions.MovePlayerToDown(ref player.Y, mapSize.Y);
                        --ObjectStatus.playerMovePoint;
                        ObjectStatus.isTrapToggled = true ^ ObjectStatus.isTrapToggled;
                        break;
                    case ConsoleKey.UpArrow:
                        Actions.MovePlayerToUp(ref player.Y, mapSize.Y);
                        --ObjectStatus.playerMovePoint;
                        ObjectStatus.isTrapToggled = true ^ ObjectStatus.isTrapToggled;
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

                for (int trapId = 0; trapId < traps.Length; ++trapId)
                {
                    if (ObjectStatus.isTrapToggled && Actions.IsCollided(traps[trapId].X, traps[trapId].Y, player.X, player.Y))
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
                    if (StageSettings.currentStage < StageSettings.stageNumber)
                    {
                        Scene.EnterStageClearScene(ref StageSettings.currentStage);
                        continue;
                    }
                    else if (StageSettings.currentStage == StageSettings.stageNumber)
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
