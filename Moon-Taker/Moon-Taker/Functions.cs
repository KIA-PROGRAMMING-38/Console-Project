using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Transactions;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;

namespace Moon_Taker
{
    public class Functions
    {
        public static void Render(string someString, ConsoleColor myColor = ConsoleColor.White)
        {
            Console.ForegroundColor = myColor;
            Console.Write(someString);
        }
        public static void RenderAt(int x, int y, string someString, ConsoleColor myColor = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Render(someString, myColor);
        }
        public static string[] LoadFile(string filePath, int stageNumber = 0)
        {
            string file = filePath;
            Debug.Assert(File.Exists(filePath));
            return File.ReadAllLines(filePath);
        }
        public static void ParseStage(string[] stage, out Player player, out Wall[] wall,
            out Enemy[] enemy, out Block[] block, out Trap[] trap, out Key key, out Door door, out Moon moon, out MapSize mapSize)
        {
            string[] objectNums = stage[stage.Length - 1].Split(" ");

            wall = new Wall[int.Parse(objectNums[0])];
            enemy = new Enemy[int.Parse(objectNums[1])];
            block = new Block[int.Parse(objectNums[2])];
            trap = new Trap[int.Parse(objectNums[3])];
            StageSettings.stageMovePoint = int.Parse(objectNums[4]);
            StageSettings.doesKeyExist = bool.Parse(objectNums[5]);

            key = null;
            door = null;
            player = null;
            moon = null;
            mapSize = new MapSize { X = stage[0].Length, Y = stage.Length };

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
                            player = new Player { X = x, Y = y };
                            break;
                        case Constants.wall:
                            wall[wallId] = new Wall { X = x, Y = y };
                            ++wallId;
                            break;
                        case Constants.enemy:
                            enemy[enemyId] = new Enemy { X = x, Y = y, IsAlive = true };
                            ++enemyId;
                            break;
                        case Constants.enemyOnTrap:
                            enemy[enemyId] = new Enemy { X = x, Y = y, IsAlive = true };
                            ++enemyId;
                            trap[trapId] = new Trap { X = x, Y = y };
                            ++trapId;
                            break;
                        case Constants.block:
                            block[blockId] = new Block { X = x, Y = y };
                            ++blockId;
                            break;
                        case Constants.blockOnTrap:
                            block[blockId] = new Block { X = x, Y = y };
                            ++blockId;
                            trap[trapId] = new Trap { X = x, Y = y };
                            ++trapId;
                            break;
                        case Constants.activatedTrap:
                            trap[trapId] = new Trap { X = x, Y = y, IsActivated = true};
                            ++trapId;
                            break;
                        case Constants.deactivatedTrap:
                            trap[trapId] = new Trap { X = x, Y = y, IsActivated = false};
                            ++trapId;
                            break;
                        case Constants.key:
                            key = new Key { X = x, Y = y };
                            break;
                        case Constants.door:
                            door = new Door { X = x, Y = y };
                            break;
                        case Constants.moon:
                            moon = new Moon { X = x, Y = y };
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
            
            string advice = $"{someAdvice[adviceNumber].name}: {someAdvice[adviceNumber].advice}";
            RenderAt(mapSize.X / 2, mapSize.Y + 2, advice);
            return;
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
    }
}
