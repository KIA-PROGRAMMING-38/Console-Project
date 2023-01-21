using System;
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

            Functions.InitialSettings();
            Console.SetCursorPosition(6, 14);
            Functions.Render("Press e to start game.");
            Functions.StartStage();

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
            for(int stageId = 1; stageId < Stage.Length; ++stageId)
            {
                Stage[stageId] = Functions.LoadStage(stageId);
            }

            string[] advices = Functions.LoadAdvice();
            Functions.ParseAdvice(advices, out advice);
            Functions.ShowSynopsis();
            Functions.ShowGameRules();
            
            while (StageSettings.isGameStarted)
            {
                Console.Clear();

                if (StageSettings.isStageReseted)
                {
                    string[] Stage1Reset = Functions.LoadStage(StageSettings.currentStage);

                    for (int i = 0; i < Stage[StageSettings.currentStage].Length - 1; ++i)
                    {
                        Console.WriteLine(Stage[StageSettings.currentStage][i]);
                    }
                    Functions.ParseStage(Stage[StageSettings.currentStage], out player, out walls, out enemies, out blocks, out traps, out moon, out key, out door, out mapSize);
                    Functions.ResetStageSetting(StageSettings.isStageKeyNull[StageSettings.currentStage], StageSettings.stageMovePoint[StageSettings.currentStage]);
                }

                    for (int wallId = 0; wallId < walls.Length; ++wallId)
                    {
                        Functions.RenderObject(walls[wallId].X, walls[wallId].Y, Constants.wall, ConsoleColor.DarkGray);
                    }
                    for (int enemyId = 0; enemyId < enemies.Length; ++enemyId)
                    {
                        if (enemies[enemyId].IsAlive)
                            Functions.RenderObject(enemies[enemyId].X, enemies[enemyId].Y, Constants.enemy, ConsoleColor.Blue);
                    }
                    for (int blockId = 0; blockId < blocks.Length; ++blockId)
                    {
                        Functions.RenderObject(blocks[blockId].X, blocks[blockId].Y, Constants.block, ConsoleColor.Cyan);
                    }
                    for (int trapId = 0; trapId < traps.Length; ++trapId)
                    {
                        if (ObjectStatus.isTrapToggled)
                        {
                            Functions.RenderObject(traps[trapId].X, traps[trapId].Y, Constants.trap, ConsoleColor.Red);
                        }
                    }
                    if (false == StageSettings.isKeyNull)
                    {
                        if (false == ObjectStatus.hasKey)
                        {
                            Functions.RenderObject(key.X, key.Y, Constants.key, ConsoleColor.Yellow);
                        }
                        if (false == ObjectStatus.isDoorOpened)
                        {
                            Functions.RenderObject(door.X, door.Y, Constants.door, ConsoleColor.DarkYellow);
                        }
                    }
                ConsoleColor moonColor = Functions.RandomColor();
                    Functions.RenderObject(moon.X, moon.Y, Constants.moon, moonColor);
                    Functions.RenderObject(player.X, player.Y, Constants.player);

                for (int stageId = 1; stageId < Stage.Length; ++stageId)
                {
                    if(stageId != StageSettings.currentStage)
                    {
                        continue;
                    }
                    Console.SetCursorPosition(Stage[stageId][0].Length + 2, 0);
                    Functions.Render("방향키로 움직이세요!");
                    Console.SetCursorPosition(Stage[stageId][0].Length + 2, 1);
                    Functions.Render("A키를 눌러 동기들의 응원을 확인하세요!");
                    Console.SetCursorPosition(Stage[stageId][0].Length + 2, 2);
                    Functions.Render("막혔을 땐 R키로 재시작해봐요!");
                    Console.SetCursorPosition(Stage[stageId][0].Length + 2, 4);
                    Functions.Render($"{Constants.player}",ConsoleColor.White);
                    Console.Write(" : 유재광   ");
                    Functions.Render($"{Constants.enemy}",ConsoleColor.Blue);
                    Console.Write(" : 적");
                    Console.SetCursorPosition(Stage[stageId][0].Length + 2, 5);
                    Functions.Render($"{Constants.block}", ConsoleColor.Cyan);
                    Console.Write(" : 블록     ");
                    Functions.Render($"{Constants.wall}", ConsoleColor.Gray);
                    Console.Write(" : 벽");
                    Console.SetCursorPosition(Stage[stageId][0].Length + 2, 6);
                    Functions.Render($"{Constants.trap}", ConsoleColor.Red);
                    Console.Write(" : 가시함정 ");
                    Functions.Render($"{Constants.moon}", moonColor);
                    Console.Write(" : 교수님의 흔적");
                    Console.SetCursorPosition(Stage[stageId][0].Length + 2, 7);
                    Functions.Render($"{Constants.key}", ConsoleColor.Yellow);
                    Console.Write(" : 열쇠     ");
                    Functions.Render($"{Constants.door}", ConsoleColor.DarkYellow);
                    Console.Write(" : 문");
                    Console.SetCursorPosition(Stage[stageId][0].Length + 2, 9);
                    Functions.Render($"남은 행동 횟수 : {ObjectStatus.playerMovePoint}");
                }

                if (ObjectStatus.isAdviceToggled)
                {
                    int adviceNumber = -1; 
                    Functions.PickAdviceNumber(advice, ref adviceNumber);
                    Functions.WriteAdvice(advice, mapSize, ref adviceNumber);
                    Functions.BlessPlayer(advice, adviceNumber);
                    ObjectStatus.isAdviceToggled = false;
                    if(StageSettings.isBlessed)
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
                        ObjectStatus.playerMovePoint -= 2;
                        Console.Beep();
                    }
                }

                if (false == StageSettings.isKeyNull)
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
                    if (StageSettings.currentStage < 3)
                    {
                        Functions.EnterStageClearScene(ref StageSettings.currentStage);
                        continue;
                    }
                    else if (StageSettings.currentStage == 3)
                    {
                        Functions.EnterGameClearScene();
                    }
                }
                if (ObjectStatus.playerMovePoint <= 0)
                {
                    Functions.EnterGameOverScene(ObjectStatus.playerMovePoint);
                }
            LoopEnd:;
            }
        }
    }
}
