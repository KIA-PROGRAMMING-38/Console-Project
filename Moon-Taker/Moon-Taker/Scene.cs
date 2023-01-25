using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
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
            Functions.Render(9, 14, ">");
            Functions.Render(13, 14, "게임시작\n");
            Functions.Render(13, 16, "게임정보\n");
            Functions.Render(13, 18, "종료하기\n");
            Scene.isFirstScene = false;
            GameSettings.MenuNum = 0;
        }
        public static void EnterSynopsisScene()
        {
            Console.Clear();
            Functions.PlayBGM("BGM.wav");
            GameSettings.isBGMPlaying = true;
            Functions.Render("시놉시스\n\n\n" +
                   "어느 화창한 날, 경일게임아카데미 프로그래밍 38기의 최선문 교수님은\n" +
                   "그의 학생인 유재광과 함께 재광의 자취방에서 곱창파티를 하고 있었습니다.\n\n" +
                   "곱창을 굽는 도중, 곱창 기름이 재광의 손에 묻었고," +
                   "그는 기름기를 닦기 위해 화장실로 갔습니다.\n" +
                   "그러나 손에 묻은 기름기 때문인지, 그는 실수로 비누를 떨어뜨렸고, 이를 교수님이 목격하고 말았습니다...\n\n" +
                   "왜인지 갑자기 잔뜩 겁에 질린 교수님은 자취방에서 도망쳐 나왔고,\n" +
                   "곱창을 먹으며 물어볼 것이 산더미였던 재광은 교수님의 흔적을 쫓아 가기 시작했습니다...\n\n\n" +
                   "계속하려면 E를 누르세요...");
            Functions.WaitForNextInput(ConsoleKey.E, GameStart);
        }
        public static void EnterGameRulesScene()
        {
            Console.Clear();
            Functions.Render("게임 정보\n\n\n");
            Functions.Render("적", Constants.enemyColor);
            Functions.Render("과 ");
            Functions.Render("블록", Constants.blockColor);
            Functions.Render("은 발로 찰 수 있습니다.\n\n");
            Functions.Render("적", Constants.enemyColor);
            Functions.Render("은 어딘가에 부딪히면 부서집니다.\n\n" +
                             "각 스테이지는 별도의 ");
            Functions.Render("행동 점수", Constants.movePointColor);
            Functions.Render("가 주어지며, 움직이거나 무언가를 찰 때 1씩 감소합니다.\n\n");
            Functions.Render("행동 점수", Constants.movePointColor);
            Functions.Render("가 0이 되었는데도 교수님의 흔적을 찾지 못하면 실패입니다.\n\n");
            Functions.Render("가시 함정", Constants.trapColor);
            Functions.Render("은 재광이 행동할 때 마다 상태가 바뀌며, ");
            Functions.Render("돌출된 가시", Constants.trapColor);
            Functions.Render("를 밟으면 행동점수가 1 감소합니다.\n\n");
            Functions.Render("적", Constants.enemyColor);
            Functions.Render("과 ");
            Functions.Render("블록", Constants.blockColor);
            Functions.Render("은 ");
            Functions.Render("가시 함정", Constants.trapColor);
            Functions.Render(" 위에서 대문자로 표시됩니다!\n\n");
            Functions.Render("문", Constants.doorColor);
            Functions.Render("은 ");
            Functions.Render("열쇠", Constants.keyColor);
            Functions.Render("가 없으면 열리지 않습니다.");
            Functions.Render("행운을 빕니다!\n\n" +
                             "이 프로젝트는 VanRipper 디자이너가 제작한 게임,");
            Functions.Render("'HELL TAKER'", ConsoleColor.DarkRed);
            Functions.Render("의 모작입니다.\n\n\n" +
                   "메인메뉴로 돌아가려면 E를 누르세요.");
            Functions.WaitForNextInput(ConsoleKey.E, GoToTitle);
        }
        public static void EnterGameOverScene(int playerMovePoint)
        {
            Functions.PlayBGM("GameOver.wav");
            GameSettings.isBGMPlaying = false;
            Console.Clear();
            Functions.Render("재광은 흔적을 열심히 쫓았지만, 마음이 꺾였습니다.\n\n");
            Functions.Render("      :::::::::           :::        :::::::::        ::::::::::       ::::    :::       ::::::::: \r\n" +
                             "     :+:    :+:        :+: :+:      :+:    :+:       :+:              :+:+:   :+:       :+:    :+: \r\n" +
                             "    +:+    +:+       +:+   +:+     +:+    +:+       +:+              :+:+:+  +:+       +:+    +:+  \r\n" +
                             "   +#++:++#+       +#++:++#++:    +#+    +:+       +#++:++#         +#+ +:+ +#+       +#+    +:+   \r\n" +
                             "  +#+    +#+      +#+     +#+    +#+    +#+       +#+              +#+  +#+#+#       +#+    +#+    \r\n" +
                             " #+#    #+#      #+#     #+#    #+#    #+#       #+#              #+#   #+#+#       #+#    #+#     \r\n" +
                             "#########       ###     ###    #########        ##########       ###    ####       #########       \r\n\n\n", ConsoleColor.Magenta);
            Functions.Render("R키를 눌러 재시작하세요.");
            Functions.WaitForNextInput(ConsoleKey.R, StageReseted);
        }
        public static void EnterStageClearScene(ref int stageNumber)
        {
            Functions.PlayBGM("StageClear.wav");
            GameSettings.isBGMPlaying = false;
            Console.Clear();
            Functions.Render($"{StageSettings.currentStage} 스테이지 클리어!\nE키를 눌러 다음 스테이지로 이동하세요!", ConsoleColor.Yellow);
            Functions.WaitForNextInput(ConsoleKey.E, StageClear);
        }
        public static void EnterBlessedScene(Advice[] someAdvice, int adviceNumber)
        {
            Console.Clear();
            Functions.PlayBGM("Blessing.wav");
            GameSettings.isBGMPlaying = false;
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
            Functions.Render("당신 덕에 재광은 교수님의 흔적을 쫓아 교수님을 포획하는데 성공했습니다!\n" +
                   "재광은 교수님께 해명하는 것을 성공한 뒤 질문폭탄을 보냈고,\n" +
                   "잡힌 교수님은 질문에 전부 답한 후,\n" +
                   "재광의 자취방에서 식은 곱창을 먹으며 행복하게 교안을 작성했습니다!\n\n\n");
            Functions.Render("      :::    :::           :::        :::::::::       :::::::::    :::   :::        ::::::::::       ::::    :::       ::::::::: \r\n" +
                             "     :+:    :+:         :+: :+:      :+:    :+:      :+:    :+:   :+:   :+:        :+:              :+:+:   :+:       :+:    :+: \r\n" +
                             "    +:+    +:+        +:+   +:+     +:+    +:+      +:+    +:+    +:+ +:+         +:+              :+:+:+  +:+       +:+    +:+  \r\n" +
                             "   +#++:++#++       +#++:++#++:    +#++:++#+       +#++:++#+      +#++:          +#++:++#         +#+ +:+ +#+       +#+    +:+   \r\n" +
                             "  +#+    +#+       +#+     +#+    +#+             +#+             +#+           +#+              +#+  +#+#+#       +#+    +#+    \r\n" +
                             " #+#    #+#       #+#     #+#    #+#             #+#             #+#           #+#              #+#   #+#+#       #+#    #+#     \r\n" +
                             "###    ###       ###     ###    ###             ###             ###           ##########       ###    ####       #########       \r\n\n\n", ConsoleColor.Yellow);
            Functions.Render("ESC를 눌러 ");
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
                EnterErrorScene("메뉴조작에 이상이 감지되었습니다.", -3);
            }
        }

        public static bool isFirstScene = true;

        public static Action GameStart = () => GameSettings.isGameStarted = true;
        public static Action StageReseted = () => StageSettings.isStageReseted = true;
        public static Action GameClear = () => Environment.Exit(1);
        public static Action FoundError = () => Environment.Exit(-1);
        public static Action StageClear = () =>
        {
            ++StageSettings.currentStage;
            StageSettings.isStageReseted = true;
        };
        public static Action GoToTitle = () => isFirstScene = true;
    }
}
