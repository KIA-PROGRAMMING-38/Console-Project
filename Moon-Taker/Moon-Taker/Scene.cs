using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon_Taker
{
    internal class Scene
    {
        public static void SetTitleScene()
        {
            Console.Clear();
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.Title = "Moon taker";
            Functions.Render("   ___  ___ _____  _____  _   _ \r\n" +
                   "   |  \\/  ||  _  ||  _  || \\ | |\r\n" +
                   "   | .  . || | | || | | ||  \\| |\r\n" +
                   "   | |\\/| || | | || | | || . ` |\r\n" +
                   "   | |  | |\\ \\_/ /\\ \\_/ /| |\\  |\r\n" +
                   "   \\_|  |_/ \\___/  \\___/ \\_| \\_/\r\n"
                   , ConsoleColor.Yellow);

            Functions.Render("_____   ___   _   __ _____ ______ \r\n" +
                   "|_   _| / _ \\ | | / /|  ___|| ___ \\\r\n" +
                   "  | |  / /_\\ \\| |/ / | |__  | |_/ /\r\n" +
                   "  | |  |  _  ||    \\ |  __| |    / \r\n" +
                   "  | |  | | | || |\\  \\| |___ | |\\ \\ \r\n" +
                   "  \\_/  \\_| |_/\\_| \\_/\\____/ \\_| \\_|\r\n");
            Functions.RenderAt(6, 14, "Press e to start game.\n");
            Functions.WaitForNextInput(ConsoleKey.E, GameStart);
            return;
        }
        public static void EnterSynopsisScene()
        {
            Console.Clear();
            Functions.Render("시놉시스\n\n\n" +
                   "경일게임아카데미 프로그래밍 38기의 교수인 최선문은\n" +
                   "그의 학생 유재광과 함께 그의 집에서 곱창파티를 하고 있었다.\n\n" +
                   "파티가 한창 이어지던 중, 어쩌다 튄 곱창 기름이 재광의 손에 묻었고,\n" +
                   "기름기를 닦기 위해 재광은 화장실로 갔다.\n" +
                   "그러나 손에 묻은 기름기 때문인지, 재광은 실수로 비누를 떨어뜨렸고, 이를 선문이 목격했다.\n\n" +
                   "왜인지 갑자기 잔뜩 겁에 질린 선문은 재광의 집에서 도망쳐 나왔고,\n" +
                   "곱창을 먹으며 물어볼 것이 산더미였던 재광은 선문의 흔적을 쫓아 가기 시작했다...\n\n\n" +
                   "계속하려면 E를 누르세요.");
            Functions.WaitForNextInput(ConsoleKey.E);
        }
        public static void EnterGameRulesScene()
        {
            Console.Clear();
            Functions.Render("게임 규칙\n\n\n" +
                   $"이 게임은 {GameSettings.stageNumber}개의 스테이지로 구성되어 있습니다.\n\n" +
                   "재광은 앞에 있는 적과 블록은 발로 찰 수 있습니다.\n\n" +
                   "적은 어딘가에 부딪히면 부서집니다.\n\n" +
                   "각 스테이지는 별도의 행동횟수가 주어지며, 움직이거나 무언가를 찰 때 1씩 감소합니다.\n\n" +
                   "행동횟수가 0이 되었는데도 교수님의 흔적을 찾지 못하면 실패입니다.\n\n" +
                   "가시 트랩은 재광이 움직일 때 마다 상태가 바뀌며,\n돌출된 가시를 밟으면 행동점수가 1 감소합니다.\n\n" +
                   "문은 열쇠가 없으면 열리지 않습니다. 열쇠를 찾아 문을 여세요\n\n" +
                   "행운을 빕니다!\n\n\n" +
                   "계속하려면 E를 누르세요.");
            Functions.WaitForNextInput(ConsoleKey.E);
        }
        public static void EnterGameOverScene(int playerMovePoint)
        {

            Console.Clear();
            Functions.Render("재광은 흔적을 열심히 쫓았지만, 마음이 꺾였습니다.\nR키를 눌러 스테이지를 재시작하세요.", ConsoleColor.Green);
            Functions.WaitForNextInput(ConsoleKey.R, StageReseted);
        }
        public static void EnterStageClearScene(ref int stageNumber)
        {
            Console.Clear();
            Functions.Render($"{StageSettings.currentStage} 스테이지 클리어!\nE키를 눌러 다음 스테이지로 이동하세요!", ConsoleColor.Yellow);
            Functions.WaitForNextInput(ConsoleKey.E, StageClear);
        }
        public static void EnterBlessedScene(Advice[] someAdvice, int adviceNumber)
        {
            Console.Clear();
            Console.WriteLine($"{someAdvice[adviceNumber].name}: {someAdvice[adviceNumber].advice}");
            Functions.Render(" _______  _______  _______ \r\n" +
                   "(       )(  ___  )(  ____ \\\r\n" +
                   "| () () || (   ) || (    \\/\r\n" +
                   "| || || || |   | || (__    \r\n" +
                   "| |(_)| || |   | ||  __)   \r\n" +
                   "| |   | || |   | || (      \r\n" +
                   "| )   ( || (___) || (____/\\\r\n" +
                   "|/     \\|(_______)(_______/\r\n", Functions.RandomColor());
            Functions.Render(" _______  _______  _______ \r\n" +
                   "(       )(  ___  )(  ____ \\\r\n" +
                   "| () () || (   ) || (    \\/\r\n" +
                   "| || || || |   | || (__    \r\n" +
                   "| |(_)| || |   | ||  __)   \r\n" +
                   "| |   | || |   | || (      \r\n" +
                   "| )   ( || (___) || (____/\\\r\n" +
                   "|/     \\|(_______)(_______/\r\n", Functions.RandomColor());
            Functions.Render("_                          _       \r\n" +
                   "| \\    /\\|\\     /||\\     /|( (    /|\r\n" +
                   "|  \\  / /( \\   / )| )   ( ||  \\  ( |\r\n" +
                   "|  (_/ /  \\ (_) / | |   | ||   \\ | |\r\n" +
                   "|   _ (    \\   /  | |   | || (\\ \\) |\r\n" +
                   "|  ( \\ \\    ) (   | |   | || | \\   |\r\n" +
                   "|  /  \\ \\   | |   | (___) || )  \\  |\r\n" +
                   "|_/    \\/   \\_/   (_______)|/    )_)\r\n" +
                   "                                    \r\n", Functions.RandomColor());
            string Message = "과거에 들었던 교수님의 목소리가 재광을 감쌉니다.\n" +
                             "재광은 따스한 온기를 느꼈습니다.\n" +
                             "힘을 낸 재광은 교수님의 흔적을 단번에 찾았습니다.\n\n\n" +
                             "다음 스테이지로 넘어가려면 E를 눌러주세요!";

            Functions.Render(Message);
            StageSettings.isBlessed = true;
            Functions.WaitForNextInput(ConsoleKey.E, StageClear);
            return;
        }
        public static void EnterGameClearScene()
        {
            Console.Clear();
            string gameClear = "축하합니다!\n\n";
            for (int i = 0; i < gameClear.Length; i++)
            {
                Functions.Render($"{gameClear[i]}", Functions.RandomColor());

            }
            Functions.Render("당신덕에 재광은 교수님의 흔적을 쫓아 교수님을 포획하는데 성공했습니다!\n" +
                   "재광은 교수님께 해명하는 것을 성공한 뒤 질문폭탄을 보냈고,\n" +
                   "잡힌 교수님은 질문에 전부 답한 후,\n" +
                   "재광의 집에서 행복하게 교안을 작성했습니다!\n\n\n" +
                   "ESC를 눌러 ");
            Functions.Render("둘만의 시간", ConsoleColor.Yellow);
            Functions.Render("을 갖게 해줍시다.");
            Functions.WaitForNextInput(ConsoleKey.Escape, GameClear);
        }
        public static void EnterErrorScene(string errorMessage, int errorCode)
        {
            Console.Clear();
            Console.WriteLine("에러가 발생하여 프로그램을 종료합니다.");
            Console.WriteLine($"에러내용: {errorMessage}");
            Console.WriteLine("나가시려면 ESC키를 눌러주세요");
            Functions.WaitForNextInput(ConsoleKey.Escape, FoundError);
        }

        public static Action GameStart = () => GameSettings.isGameStarted = true;
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
