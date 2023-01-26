using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public static class SceneData
    {
        // Title Data
        public static string[] gameTitle = File.ReadAllLines("Assets\\Title.txt");
        public static string titleOption1 = "1. 게임 시작";
        public static string titleOption2 = "2. 게임 정보";
        public static string titleOption3 = "3. 게임 종료";
        public static string cursorIcon = "▶";

        public const int titleOptionsX = 54;
        public const int titleOption1Y = 15;
        public const int titleOption2Y = 17;
        public const int titleOption3Y = 19;

        public static int preTitleCursorY = 15;
        public static int titleCursorY = 15;

        // GameInfo Data
        public static string[] infos = { "게임인포입니다.", "Title로 돌아가려면 엔터를 누르세요." };

        // InGame Data
        public const int MIN_OF_INGAME_X = 0;
        public const int MAX_OF_INGAME_X = 48;
        public const int MIN_OF_INGAME_Y = 0;
        public const int MAX_OF_INGAME_Y = 19;
        public const int X_OF_DEADCOUNT = 1;
        public const int Y_OF_DEADCOUNT = 25;

        // Ending Data
        public static string[] gifts = { "", "1등상", "2등상" };
        public static string[] endInfo = { "Title로 돌아가려면 엔터를 누르세요.", "게임을 종료하려면 스페이스바를 누르세요." };
    }
}
