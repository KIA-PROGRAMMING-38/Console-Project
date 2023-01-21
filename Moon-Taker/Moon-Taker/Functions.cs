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

        public static void StartStage()
        {
            WaitForNextInput(ConsoleKey.E, GameStart);
            return;
        }

        public static string[] LoadStage(int stageNumber)
        {
            string stageFilePath = Path.Combine("Assets", "Stage", $"Stage{stageNumber}.txt");
            Debug.Assert(File.Exists(stageFilePath));

            return File.ReadAllLines(stageFilePath);
        }

        public static string[] LoadAdvice()
        {
            string stageFilePath = Path.Combine("Assets", "Advice", "Advice.txt");
            Debug.Assert(File.Exists(stageFilePath));

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
                            ExitWithError($"스테이지 파일이 손상되었습니다. {y}번 행의 {x}번째 글자: {stage[y][x]}", -1);
                            break;
                    }
                }
            }
        }

        public static void ParseAdvice(string[] advices, out Advice[] advice)
        {
            advice = new Advice[advices.Length];
            for (int adviceId = 0; adviceId < advices.Length; adviceId++)
            {
                advice[adviceId] = new Advice
                {
                    name = advices[adviceId].Split("\t")[0],
                    advice = advices[adviceId].Split("\t")[1],
                    weight = int.Parse(advices[adviceId].Split("\t")[2])
                };
            }
        }

        public static void EnterGameOverScene(int playerMovePoint)
        {

            Console.Clear();
            Render("당신은 흔적을 열심히 쫓았지만, 마음이 꺾였습니다.\nR키를 눌러 스테이지를 재시작하세요.", ConsoleColor.Green);
            WaitForNextInput(ConsoleKey.R, StageReseted);
        }

        public static void EnterStageClearScene(ref int stageNumber)
        {
            Console.Clear();
            Render($"{StageSettings.currentStage} 스테이지 클리어!\nE키를 눌러 다음 스테이지로 이동하세요!", ConsoleColor.Yellow);
            WaitForNextInput(ConsoleKey.E, StageClear);
        }

        public static void PickAdviceNumber(Advice[] someAdvice, ref int adviceNumber)
        {
            Random random = new Random();
            int randomAdviceNum = random.Next(1, 101);
            int totalWeight = someAdvice[0].weight;
            adviceNumber = 1;
            for (int i = 1; i < someAdvice.Length; i++)
            {
                if (totalWeight < randomAdviceNum)
                {
                    totalWeight += someAdvice[i].weight;
                    continue;
                }
                adviceNumber = i;
                return;
            }
            return;
        }
        public static void WriteAdvice(Advice[] someAdvice, MapSize mapSize, ref int adviceNumber)
        {
            if (adviceNumber == -1)
            {
                ExitWithError($"Advice 파일의 형식이 잘못되었습니다.", -2);
            }
            string advice = $"{someAdvice[adviceNumber].name}: {someAdvice[adviceNumber].advice}";

                Console.SetCursorPosition(mapSize.X / 2, mapSize.Y + 3);
                Console.Write(advice);
                return;
        }
        public static void BlessPlayer(Advice[] someAdvice, int adviceNumber)
        {
            if (someAdvice[adviceNumber].name == "최선문" && StageSettings.currentStage != StageSettings.stageNumber)
            {
                Console.Clear();
                Console.WriteLine($"{someAdvice[adviceNumber].name}: {someAdvice[adviceNumber].advice}");
                Render(" _______  _______  _______ \r\n" +
                       "(       )(  ___  )(  ____ \\\r\n" +
                       "| () () || (   ) || (    \\/\r\n" +
                       "| || || || |   | || (__    \r\n" +
                       "| |(_)| || |   | ||  __)   \r\n" +
                       "| |   | || |   | || (      \r\n" +
                       "| )   ( || (___) || (____/\\\r\n" +
                       "|/     \\|(_______)(_______/\r\n", RandomColor());
                Render(" _______  _______  _______ \r\n" +
                       "(       )(  ___  )(  ____ \\\r\n" +
                       "| () () || (   ) || (    \\/\r\n" +
                       "| || || || |   | || (__    \r\n" +
                       "| |(_)| || |   | ||  __)   \r\n" +
                       "| |   | || |   | || (      \r\n" +
                       "| )   ( || (___) || (____/\\\r\n" +
                       "|/     \\|(_______)(_______/\r\n", RandomColor());
                Render("_                          _       \r\n" +
                       "| \\    /\\|\\     /||\\     /|( (    /|\r\n" +
                       "|  \\  / /( \\   / )| )   ( ||  \\  ( |\r\n" +
                       "|  (_/ /  \\ (_) / | |   | ||   \\ | |\r\n" +
                       "|   _ (    \\   /  | |   | || (\\ \\) |\r\n" +
                       "|  ( \\ \\    ) (   | |   | || | \\   |\r\n" +
                       "|  /  \\ \\   | |   | (___) || )  \\  |\r\n" +
                       "|_/    \\/   \\_/   (_______)|/    )_)\r\n" +
                       "                                    \r\n", RandomColor());
                string Message = "교수님의 축복이 당신을 감쌉니다. 다음 스테이지로 넘어가려면 E를 눌러주세요!";
                for (int i = 0; i < Message.Length; i++)
                {
                    Console.ForegroundColor = RandomColor();
                    Console.Write(Message[i]);
                }
                StageSettings.isBlessed = true;
                WaitForNextInput(ConsoleKey.E, StageClear);
                return;
            }
        }
        public static void EnterGameClearScene()
        {
            Console.Clear();
            string gameClear = "축하합니다! 당신은 훌륭하게 교수님의 흔적을 쫓아 교수님을 포획하는데 성공했습니다! \n잡힌 교수님은 재광의 집에서 행복하게 교안을 작성했습니다!";
            for (int i = 0; i < gameClear.Length; ++i)
            {
                Console.ForegroundColor = RandomColor(13, 15);
                Console.Write(gameClear[i]);
            }
            WaitForNextInput(ConsoleKey.Escape, GameClear);
        }
        public static void ExitWithError(string errorMessage, int errorCode)
        {
            Console.Clear();
            Console.WriteLine("에러가 발생하여 프로그램을 종료합니다.");
            Console.WriteLine($"에러내용: {errorMessage}");
            Console.WriteLine("나가시려면 ESC키를 눌러주세요");
            WaitForNextInput(ConsoleKey.Escape, FoundError);
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

        public static ConsoleColor RandomColor(int a = 0, int b = 15)
        {
            Random random = new Random();
            int colorIndex = random.Next(a, b);
            return LookUpTable.consoleColor[colorIndex];
        }

        public static Action GameStart = () => StageSettings.isGameStarted = true;
        public static Action StageReseted = () => StageSettings.isStageReseted = true;
        public static Action GameClear = () => Environment.Exit(1);
        public static Action FoundError = () => Environment.Exit(-1);
        public static Action StageClear = () =>
        {
            ++StageSettings.currentStage;
            StageSettings.isStageReseted = true;
        };

    }
}
