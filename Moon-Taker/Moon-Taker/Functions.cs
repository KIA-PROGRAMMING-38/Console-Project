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

        public static void ShowSynopsis()
        {
            Console.Clear();
            Render("시놉시스\n\n\n" +
                   "경일게임아카데미 프로그래밍 38기의 교수 최선문은 그의 학생 유재광의 집에서 곱창파티를 하고 있었다.\n" +
                   "파티가 한창 이어지던 중, 곱창 기름이 재광의 손에 튀었고, 기름기를 닦기 위해 재광은 화장실로 갔다.\n" +
                   "그러나 손에 묻은 기름기 때문인지, 실수로 비누를 떨어뜨렸고, 이를 선문이 목격했다.\n" +
                   "겁에 질린 선문은 재광의 집에서 도망쳐 나왔고,\n" +
                   "곱창을 먹으며 물어볼 것이 산더미였던 재광은 교수님의 흔적을 쫓아 가기 시작했다...\n\n\n" +
                   "계속하려면 E를 누르세요.");
            WaitForNextInput(ConsoleKey.E);
        }
        public static void ShowGameRules()
        {
            Console.Clear();
            Render("게임 규칙\n\n\n" +
                   "이 게임은 3개의 스테이지로 구성되어 있습니다.\n" +
                   "재광은 앞에 있는 적과 블록은 발로 찰 수 있습니다.\n" +
                   "적은 어딘가에 부딪히면 부서집니다.\n" +
                   "각 스테이지는 별도의 행동횟수가 주어지며, 움직이거나 무언가를 찰 때 1씩 감소합니다.\n" +
                   "행동횟수가 0이 되었는데도 교수님의 흔적을 찾지 못하면 실패입니다.\n" +
                   "가시 트랩은 재광이 움직일 때 마다 상태가 바뀌며,\n돌출된 가시를 밟으면 행동점수가 2 감소합니다.\n" +
                   "문은 열쇠가 없으면 열리지 않습니다. 열쇠를 찾아 문을 여세요\n" +
                   "행운을 빕니다!\n\n\n" +
                   "계속하려면 E를 누르세요.");
            WaitForNextInput(ConsoleKey.E);
        }
        public static void EnterGameOverScene(int playerMovePoint)
        {

            Console.Clear();
            Render("재광은 흔적을 열심히 쫓았지만, 마음이 꺾였습니다.\nR키를 눌러 스테이지를 재시작하세요.", ConsoleColor.Green);
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
                string Message = "과거에 들었던 교수님의 목소리가 재광을 감쌉니다.\n" +
                                 "재광은 따스한 온기를 느꼈습니다.\n" +
                                 "힘을 낸 재광은 교수님의 흔적을 단번에 찾았습니다.\n\n\n" +
                                 "다음 스테이지로 넘어가려면 E를 눌러주세요!";

                Render(Message);
                StageSettings.isBlessed = true;
                WaitForNextInput(ConsoleKey.E, StageClear);
                return;
            }
        }
        public static void EnterGameClearScene()
        {
            Console.Clear();
            string gameClear = "당신덕에 재광은 교수님의 흔적을 쫓아 교수님을 포획하는데 성공했습니다!\n" +
                               "재광은 교수님께 해명하는 것을 성공한 뒤 질문폭탄을 보냈고,\n" +
                               "잡힌 교수님은 재광의 집에서 행복하게 교안을 작성했습니다!\n\n\n" +
                               "ESC를 눌러 ";
            Render("축하합니다!\n\n",RandomColor());
            Render(gameClear);
            Render("둘만의 시간", ConsoleColor.Yellow);
            Render("을 갖게 해줍시다.");
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

        public static ConsoleColor RandomColor(int a = 0, int b = 15)
        {
            Random random = new Random();
            int colorIndex = random.Next(a, b);
            return LookUpTable.consoleColor[colorIndex];
        }

        public static void ResetStageSetting(bool isKeyNull, int playerMovePoint)
        {
            StageSettings.isStageReseted = false;
            StageSettings.isBlessed = false;
            ObjectStatus.playerMovePoint = playerMovePoint;
            ObjectStatus.isTrapToggled = true;
            StageSettings.isKeyNull = isKeyNull;
            if (false == isKeyNull)
            {
                ObjectStatus.hasKey = false;
                ObjectStatus.isDoorOpened = false;
            }
            return;
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
