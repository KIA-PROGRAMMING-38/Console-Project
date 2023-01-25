using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Transactions;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Media;

namespace Moon_Taker
{
    public class Functions
    {
        public static void Render(string someString, ConsoleColor myColor = ConsoleColor.White)
        {
            Console.ForegroundColor = myColor;
            Console.Write(someString);
        }
        public static void Render(int x, int y, string someString, ConsoleColor myColor = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Render(someString, myColor);
        }
        public static void SelectMenu(ref int MenuNum, ref ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    MenuNum = (2 + MenuNum) % 3;
                    break;
                case ConsoleKey.DownArrow:
                    MenuNum = (4 + MenuNum) % 3;
                    break;
                default:
                    Console.Write("\b \b");
                    return;
            }

            if (MenuNum == 0)
            {
                Functions.Render(9, 14, ">");
                Functions.Render(9, 16, " ");
                Functions.Render(9, 18, " ");
            }
            else if (MenuNum == 1)
            {
                Functions.Render(9, 14, " ");
                Functions.Render(9, 16, ">");
                Functions.Render(9, 18, " ");
            }
            else if (MenuNum == 2)
            {
                Functions.Render(9, 14, " ");
                Functions.Render(9, 16, " ");
                Functions.Render(9, 18, ">");
            }
            else
            {
                Scene.EnterErrorScene("메뉴조작에 이상이 감지되었습니다.", -3);
            }
        }
        public static int CheckStageNumber()
        {
            int stageNumber = 1;
            while(File.Exists($@"Assets\Stage\Stage{stageNumber}.txt"))
            {
                ++stageNumber;
            }
            return stageNumber - 1;
        }
        public static void NameStage(string[] stageFilePath)
        {
            for(int stageId = 1; stageId < GameSettings.stageNumber + 1; stageId++)
            {
                stageFilePath[stageId] = $@"Assets\Stage\Stage{stageId}.txt";
            }
        }
        public static string[] LoadFile(string filePath, int stageNumber = 0)
        {
            string file = filePath;
            Debug.Assert(File.Exists(filePath));
            return File.ReadAllLines(filePath);
        }
        public static void ParseStage(string[] stage, out Player player, out PreviousPlayer previousPlayer,
            out Wall[] wall, out Enemy[] enemy, out PreviousEnemy[] previousEnemy, out Block[] block, out PreviousBlock[] previousBlock,
            out Trap[] trap, out Key key, out Door door, out Moon moon, out MapSize mapSize)
        {
            string[] objectNums = stage[stage.Length - 1].Split(" ");

            wall = new Wall[int.Parse(objectNums[0])];
            enemy = new Enemy[int.Parse(objectNums[1])];
            previousEnemy = new PreviousEnemy[int.Parse(objectNums[1])];
            block = new Block[int.Parse(objectNums[2])];
            previousBlock = new PreviousBlock[int.Parse(objectNums[2])];
            trap = new Trap[int.Parse(objectNums[3])];
            StageSettings.stageMovePoint = int.Parse(objectNums[4]);
            StageSettings.doesKeyExist = bool.Parse(objectNums[5]);

            key = null;
            door = null;
            player = null;
            previousPlayer = null;
            moon = null;
            mapSize = new MapSize { x = stage[0].Length, y = stage.Length };

            int wallId = 0;
            int enemyId = 0;
            int blockId = 0;
            int trapId = 0;

            for (int y = 0; y < stage.Length - 1; ++y)
            {
                for (int x = 0; x < stage[y].Length; ++x)
                {
                    switch ($"{stage[y][x]}")
                    {
                        case Constants.player:
                            player = new Player { x = x, y = y };
                            previousPlayer = new PreviousPlayer { x = x, y = y };
                            break;
                        case Constants.wall:
                            wall[wallId] = new Wall { x = x, y = y };
                            ++wallId;
                            break;
                        case Constants.enemy:
                            enemy[enemyId] = new Enemy { x = x, y = y, isAlive = true };
                            previousEnemy[enemyId] = new PreviousEnemy { x = x, y = y };
                            ++enemyId;
                            break;
                        case Constants.enemyOnTrap:
                            enemy[enemyId] = new Enemy { x = x, y = y, isAlive = true };
                            previousEnemy[enemyId] = new PreviousEnemy { x = x, y = y };
                            ++enemyId;
                            trap[trapId] = new Trap { x = x, y = y };
                            ++trapId;
                            break;
                        case Constants.block:
                            block[blockId] = new Block { x = x, y = y };
                            previousBlock[blockId] = new PreviousBlock { x = x, y = y };
                            ++blockId;
                            break;
                        case Constants.blockOnTrap:
                            block[blockId] = new Block { x = x, y = y };
                            previousBlock[blockId] = new PreviousBlock { x = x, y = y };
                            ++blockId;
                            trap[trapId] = new Trap { x = x, y = y };
                            ++trapId;
                            break;
                        case Constants.activatedTrap:
                            trap[trapId] = new Trap { x = x, y = y, isActivated = true};
                            ++trapId;
                            break;
                        case Constants.deactivatedTrap:
                            trap[trapId] = new Trap { x = x, y = y, isActivated = false};
                            ++trapId;
                            break;
                        case Constants.key:
                            key = new Key { x = x, y = y };
                            break;
                        case Constants.door:
                            door = new Door { x = x, y = y };
                            break;
                        case Constants.moon:
                            moon = new Moon { x = x, y = y };
                            break;
                        case Constants.blank:
                            break;
                        default:
                            Scene.EnterErrorScene($"스테이지 파일이 손상되었습니다. {y}번 행의 {x}번째 글자: {stage[y][x]}", -1);
                            break;
                    }
                }
            }
            return;
        }
        public static void ParseAdvice(string[] adviceFile, out Advice[] advice)
        {
            advice = new Advice[adviceFile.Length];
            for (int adviceId = 0; adviceId < adviceFile.Length; adviceId++)
            {
                advice[adviceId] = new Advice
                {
                    name = adviceFile[adviceId].Split("\t")[0],
                    advice = adviceFile[adviceId].Split("\t")[1],
                    weight = int.Parse(adviceFile[adviceId].Split("\t")[2])
                };
            }
            return;
        }
        public static void PickAdviceNumber(Advice[] someAdvice, ref int adviceNumber)
        {
            Random random = new Random();
            int totalWeight = 1;
            for(int i = 0; i <someAdvice.Length; i++)
            {
                totalWeight += someAdvice[i].weight;
            }
            int randomAdviceNum = random.Next(0, totalWeight);
            int weightSum = 0;
            for (int i = 0; i < someAdvice.Length; i++)
            {
                weightSum += someAdvice[i].weight;

                if (weightSum >= randomAdviceNum)
                {
                    adviceNumber = i;
                    return;
                }
            }
            return;
        }
        public static void WriteAdvice(Advice[] someAdvice, MapSize mapSize, ref int adviceNumber)
        {
            if (adviceNumber == -1)
            {
                Scene.EnterErrorScene($"Advice 파일의 형식이 잘못되었습니다.", -2);
            }
            
            string advice = $"{someAdvice[adviceNumber].name}: {someAdvice[adviceNumber].advice}".PadRight(100,' ');
            Render(mapSize.x / 2, mapSize.y + 2, advice);
            return;
        }
        public static void ParseTrace(string[] traceFile, out Trace[] trace)
        {
            trace = new Trace[traceFile.Length];
            for (int traceId = 0; traceId < traceFile.Length; traceId++)
            {
                trace[traceId] = new Trace
                {
                    name = traceFile[traceId].Split("\t")[0],
                    description = traceFile[traceId].Split("\t")[1],
                    isUseful = bool.Parse(traceFile[traceId].Split("\t")[2]),
                    SuccessString = traceFile[traceId].Split("\t")[3],
                    FailureString = traceFile[traceId].Split("\t")[4],
                };
            }
            return;
        }
        public static void ChooseOX(ConsoleKey yesOrNo, ref bool isO)
        {
            if (yesOrNo == ConsoleKey.DownArrow || yesOrNo == ConsoleKey.UpArrow)
            {
                isO = true ^ isO;
            }
            if(isO == true)
            {
                Render(0, 4, ">");
                Render(0, 5, " ");
                return;
            }
                Render(0, 4, " ");
                Render(0, 5, ">");
        }
        public static void WaitForNextInput(ConsoleKey someKey, Action action1)
        {
            Console.WriteLine();
            while (true)
            {
                ConsoleKey input = Console.ReadKey().Key;
                if (input == someKey)
                {
                    action1();
                    return;
                }
                else
                {
                    Console.Write("\b \b");
                }
            }
        }
        public static void WaitForNextInput(ConsoleKey someKey)
        {
            Console.WriteLine();
            while (true)
            {
                ConsoleKey input = Console.ReadKey().Key;
                if (input == someKey)
                { 
                    return;
                }
                else
                {
                    Console.Write("\b \b");
                }
            }
        }
        public static ConsoleColor RandomColor()
        {
            Random random = new Random();
            int colorIndex = random.Next(LookUpTable.allColor.Length);
            return LookUpTable.allColor[colorIndex];
        }
        public static void ResetStageSetting(bool doesKeyExist, int playerMovePoint)
        {
            StageSettings.isStageReseted = false;
            StageSettings.isBlessed = false;
            ObjectStatus.playerMovePoint = playerMovePoint;
            StageSettings.doesKeyExist = doesKeyExist;
            ObjectStatus.pushedBlockId = 0;
            ObjectStatus.pushedEnemyId = 0;
            if (doesKeyExist)
            {
                ObjectStatus.hasKey = false;
                ObjectStatus.isDoorOpened = false;
            }
            return;
        }
        public static void SavePreviousObject(out int previousx, out int previousy, int currentx, int currenty)
        {
            previousx = currentx;
            previousy = currenty;
            return;
        }
        public static void ClearObject(int x, int y)
        {
            Render(x, y, " ");
        }
        public static void PlayBGM(string musicPath)
        {
            SoundPlayer soundPlayer = new SoundPlayer(musicPath);
            soundPlayer.Load();
            soundPlayer.Play();
        }
    }
}
