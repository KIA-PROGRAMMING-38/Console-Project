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
            Functions.StartStage(out StageSettings.isGameStarted, ref StageSettings.stageNum);

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

            string[][] Stage = new string[4][];
            for(int stageId = 1; stageId < Stage.Length; ++stageId)
            {
                Stage[stageId] = Functions.LoadStage(stageId);
            }

            string[] advices = Functions.LoadAdvice();
            Functions.ParseAdvice(advices, out advice);

            while (StageSettings.isGameStarted)
            {
                Console.Clear();

                if (StageSettings.stageNum == 1 && StageSettings.isStageReseted)
                {
                    string[] Stage1Reset = Functions.LoadStage(1);

                    for (int i = 0; i < Stage[1].Length - 1; ++i)
                    {
                        Console.WriteLine(Stage[1][i]);
                    }

                    Functions.ParseStage(Stage[1], out player, out walls, out enemies, out blocks, out traps, out moon, out key, out door, out mapSize);
                    StageSettings.isStageReseted = false;
                    StageSettings.isKeyNull = true;
                    ObjectStatus.playerMovePoint = 23;
                    ObjectStatus.isTrapToggled = true;
                }
                else if (StageSettings.stageNum == 2 && StageSettings.isStageReseted)
                {
                    string[] Stage2Reset = Functions.LoadStage(2);

                    for (int i = 0; i < Stage[2].Length - 1; ++i)
                    {
                        Console.WriteLine(Stage[2][i]);
                    }

                    Functions.ParseStage(Stage[2], out player, out walls, out enemies, out blocks, out traps, out moon, out key, out door, out mapSize);
                    StageSettings.isStageReseted = false;
                    StageSettings.isKeyNull = true;
                    ObjectStatus.playerMovePoint = 18;
                    ObjectStatus.isTrapToggled = true;
                }
                else if (StageSettings.stageNum == 3 && StageSettings.isStageReseted)
                {
                    string[] Stage3Reset = Functions.LoadStage(3);

                    for (int i = 0; i < Stage[3].Length - 1; ++i)
                    {
                        Console.WriteLine(Stage[3][i]);
                    }

                    Functions.ParseStage(Stage[3], out player, out walls, out enemies, out blocks, out traps, out moon, out key, out door, out mapSize);
                    StageSettings.isStageReseted = false;
                    StageSettings.isKeyNull = false;
                    ObjectStatus.playerMovePoint = 33;
                    ObjectStatus.isTrapToggled = true;
                }

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
                        if (ObjectStatus.isTrapToggled)
                        {
                            Functions.RenderObject(traps[trapId].X, traps[trapId].Y, Constants.trap);
                        }
                    }
                    if (false == StageSettings.isKeyNull)
                    {
                        if (false == ObjectStatus.hasKey)
                        {
                            Functions.RenderObject(key.X, key.Y, Constants.key);
                        }
                        if (false == ObjectStatus.isDoorOpened)
                        {
                            Functions.RenderObject(door.X, door.Y, Constants.door);
                        }
                    }
                    Functions.RenderObject(moon.X, moon.Y, Constants.moon);
                    Functions.RenderObject(player.X, player.Y, Constants.player);

                Console.SetCursorPosition(Stage[1][0].Length + 3, Stage[1].Length / 2);
                Functions.Render($"Your Move Point : {ObjectStatus.playerMovePoint}");

                if (ObjectStatus.isAdviceToggled)
                {
                    int adviceNumber = -1; 
                    Functions.PickAdviceNumber(advice, ref adviceNumber);
                    Functions.WriteAdvice(advice, mapSize, adviceNumber);
                    ObjectStatus.isAdviceToggled = false;
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
                        continue;
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
                    if (StageSettings.stageNum < 3)
                    {
                        Functions.EnterStageClearScene(ref StageSettings.stageNum);
                        continue;
                    }
                    else if (StageSettings.stageNum == 3)
                    {
                        Functions.EnterGameClearScene();
                    }
                }
                if (ObjectStatus.playerMovePoint <= 0)
                {
                    Functions.EnterGameOverScene(ObjectStatus.playerMovePoint);
                }
            }
        }
    }
}
