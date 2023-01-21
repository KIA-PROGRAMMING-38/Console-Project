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

namespace Moon_Taker
{
    public class Functions
    {
        public static void InitialSettings()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.Title = "Moon taker";
            Render("   ___  ___ _____  _____  _   _ \r\n" +
                   "   |  \\/  ||  _  ||  _  || \\ | |\r\n" +
                   "   | .  . || | | || | | ||  \\| |\r\n" +
                   "   | |\\/| || | | || | | || . ` |\r\n" +
                   "   | |  | |\\ \\_/ /\\ \\_/ /| |\\  |\r\n" +
                   "   \\_|  |_/ \\___/  \\___/ \\_| \\_/\r"
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

        public static void StartGame(out bool isGameStarted)
        {
            while (true)
            {
                ConsoleKey key = Console.ReadKey().Key;
                if (key != ConsoleKey.E)
                {
                    Console.Write("\b \b");
                    continue;
                }
                isGameStarted = true;
                return;
            }
        }

        public static string[] LoadStage(int stageNumber, out Player player, out Wall[] wall,
            out Enemy[] enemy, out Block[] block, out Moon moon, out Key key, out Door door)
        {
            string stageFilePath = Path.Combine("Assets", "Stage", $"Stage{stageNumber}.txt");
            Console.WriteLine(stageFilePath);
            Debug.Assert(File.Exists(stageFilePath));

            player = null;
            wall = null;
            enemy = null;
            block = null;
            moon = null;
            key = null;
            door = null;

            return File.ReadAllLines(stageFilePath);
        }

        public static void ParseStage(string[] stage, out Player player, out Wall[] wall,
            out Enemy[] enemy, out Block[] block, out Moon moon, out Key key, out Door door)
        {
            string[] objectNums = stage[stage.Length - 1].Split(" ");
            
            wall = new Wall[int.Parse(objectNums[0])];
            enemy = new Enemy[int.Parse(objectNums[1])];
            block = new Block[int.Parse(objectNums[2])];
            player = null;
            moon = null;
            key = null;
            door = null;

            int wallId = 0;
            int enemyId = 0;
            int blockId = 0;

            for (int y = 0; y < stage.Length - 1; ++y)
            {
                for(int x = 0; x < stage[y].Length; ++x)
                {
                    switch (stage[y][x])
                    {
                        case ObjectSymbol.player:
                            player = new Player { X = x, Y = y };
                            break;
                            case ObjectSymbol.wall:
                            wall[wallId] = new Wall { X = x, Y = y };
                            ++wallId;
                            break;
                        case ObjectSymbol.enemy:
                            enemy[enemyId] = new Enemy { X = x, Y = y };
                            ++enemyId;
                            break;
                        case ObjectSymbol.block:
                            block[blockId] = new Block { X = x, Y = y };
                            ++blockId;
                            break;
                        case ObjectSymbol.moon:
                            moon = new Moon { X = x, Y = y };
                            break;
                        case ObjectSymbol.key:
                            key = new Key { X = x, Y = y };
                            break;
                        case ObjectSymbol.door:
                            door = new Door { X = x, Y = y };
                            break;
                        default:
                            Environment.Exit(-1);
                            break;
                    }
                }
            }
        }
    }
}
