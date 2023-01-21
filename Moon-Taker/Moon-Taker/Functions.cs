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

namespace Moon_Taker
{
    public class Functions
    {
        public static void InitialSettings()
        {
            Console.Clear();
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.Title = "Moon taker";
            Render("   ___  ___ _____  _____  _   _ \r\n" +
                   "   |  \\/  ||  _  ||  _  || \\ | |\r\n" +
                   "   | .  . || | | || | | ||  \\| |\r\n" +
                   "   | |\\/| || | | || | | || . ` |\r\n" +
                   "   | |  | |\\ \\_/ /\\ \\_/ /| |\\  |\r\n" +
                   "   \\_|  |_/ \\___/  \\___/ \\_| \\_/\r\n"
                   , ConsoleColor.Yellow);

            Render("_____   ___   _   __ _____ ______ \r\n" +
                   "|_   _| / _ \\ | | / /|  ___|| ___ \\\r\n" +
                   "  | |  / /_\\ \\| |/ / | |__  | |_/ /\r\n" +
                   "  | |  |  _  ||    \\ |  __| |    / \r\n" +
                   "  | |  | | | || |\\  \\| |___ | |\\ \\ \r\n" +
                   "  \\_/  \\_| |_/\\_| \\_/\\____/ \\_| \\_|\r\n");
        }
        public static void Render(string someString, ConsoleColor myColor = ConsoleColor.White)
        {
            Console.ForegroundColor = myColor;
            Console.Write(someString);
        }

        public static void RenderObject(int x, int y, char icon, ConsoleColor myColor = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = myColor;
            Console.Write(icon);
        }

        public static void StartStage(out bool isStageStarted, ref int stageNum)
        {
            while (true)
            {
                ConsoleKey key = Console.ReadKey().Key;
                if (((int)key >= 48 && (int)key <= 57) || ((int)key >= 65 && (int)key <= 90))
                {
                    Console.Write("\b \b");
                    if (key == ConsoleKey.E)
                    {
                        isStageStarted = true;
                        return;
                    }
                    continue;
                }
            }
        }

        public static string[] LoadStage(int stageNumber, out Player player, out Wall[] wall,
            out Enemy[] enemy, out Block[] block, out Trap[] trap, out Moon moon, out Key key, out Door door)
        {
            string stageFilePath = Path.Combine("Assets", "Stage", $"Stage{stageNumber}.txt");
            Debug.Assert(File.Exists(stageFilePath));

            player = null;
            wall = null;
            enemy = null;
            block = null;
            trap = null;
            moon = null;
            key = null;
            door = null;

            return File.ReadAllLines(stageFilePath);
        }

        public static void ParseStage(string[] stage, out Player player, out Wall[] wall,
            out Enemy[] enemy, out Block[] block, out Trap[] trap, out Moon moon, out Key key, out Door door, out MapSize mapSize)
        {
            string[] objectNums = stage[stage.Length - 1].Split(" ");

            wall = new Wall[int.Parse(objectNums[0])];
            enemy = new Enemy[int.Parse(objectNums[1])];
            block = new Block[int.Parse(objectNums[2])];
            trap = new Trap[int.Parse(objectNums[3])];
            player = null;
            moon = null;
            key = null;
            door = null;
            mapSize = new MapSize { X = stage[0].Length, Y = stage.Length };

            int wallId = 0;
            int enemyId = 0;
            int blockId = 0;
            int trapId = 0;

            for (int y = 0; y < stage.Length - 1; ++y)
            {
                for (int x = 0; x < stage[y].Length; ++x)
                {
                    switch (stage[y][x])
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
                        case Constants.block:
                            block[blockId] = new Block { X = x, Y = y };
                            ++blockId;
                            break;
                        case Constants.trap:
                            trap[trapId] = new Trap { X = x, Y = y };
                            ++trapId;
                            break;
                        case Constants.moon:
                            moon = new Moon { X = x, Y = y };
                            break;
                        case Constants.key:
                            key = new Key { X = x, Y = y };
                            break;
                        case Constants.door:
                            door = new Door { X = x, Y = y };
                            break;
                        case Constants.blank:
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"스테이지 파일이 손상되었습니다.{y}번 행의 {x}번째 글자: {stage[y][x]}");
                            Environment.Exit(-1);
                            break;
                    }
                }
            }
        }

        public static void EnterGameOverScene(int playerMovePoint)
        {

            Console.Clear();
            Console.WriteLine("Game Over! Press R to restart. Press ESC to Exit Game.");
            while (true)
            {
                ConsoleKey resetKey = Console.ReadKey().Key;
                if (resetKey == ConsoleKey.R)
                {
                    StageSettings.isStageReseted = true;
                    break;
                }
                else if (resetKey == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                    return;
                }
                else
                {
                    Console.Write("\b \b");
                }
            }

        }

        public static void EnterStageClearScene(ref int stageNumber)
        {
            Console.Clear();
            Console.WriteLine("Stage Clear! Press E to goto next Stage");
            while (true)
            {
                ConsoleKey resetKey = Console.ReadKey().Key;
                if (resetKey == ConsoleKey.E)
                {
                    ++StageSettings.stageNum;
                    StageSettings.isStageReseted = true;
                    break;
                }
                else
                {
                    Console.Write("\b \b");
                }
            }
        }

        public static void WriteAdvice(Advice[] someAdvice, MapSize mapSize)
        {
            Random random = new Random();
            int adviceNum = random.Next(0, someAdvice.Length - 1);
            string advice = $"{someAdvice[adviceNum].name}: {someAdvice[adviceNum].advice}";
            Console.SetCursorPosition(0, mapSize.Y + 3);
            Console.Write(advice);
        }
    }
}
