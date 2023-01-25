using Console_ReProject.StageRender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_ReProject;
using System.Numerics;

namespace Console_ReProject.Object
{
    class Player
    {
        public int X;
        public int Y;
        public int pastX;
        public int pastY;
        public bool IsOnInteraction = false;


        // 이동 함수
        public static void MoveToLeftOfTarget(out int x, int targetX) =>
            x = targetX + 1;
        public static void MoveToRightOfTarget(out int x, int targetX) =>
            x = targetX - 1;
        public static void MoveToUpOfTarget(out int y, int targetY) =>
            y = targetY + 1;
        public static void MoveToDownOfTarget(out int y, int targetY) =>
            y = targetY - 1;

        // 플레이어 이동 모듈화
        public static void MovePlayer(ConsoleKey key, ref int playerX, ref int playerY, int targetX, int targetY)
        {
            if (key == ConsoleKey.LeftArrow)
                MoveToRightOfTarget(out playerX, targetX);
            if (key == ConsoleKey.RightArrow)
                MoveToLeftOfTarget(out playerX, targetX);
            if (key == ConsoleKey.UpArrow)
                MoveToDownOfTarget(out playerY, targetY);
            if (key == ConsoleKey.DownArrow)
                MoveToUpOfTarget(out playerY, targetY);
        }

        public static string[] LoadMessage(int stageNumber)
        {
            string stageFilePath = Path.Combine("Assets", "InGameMessage", $"InteractionMessage{stageNumber:D2}.txt");

            if (false == File.Exists(stageFilePath))
            {
                Console.WriteLine($"상호작용 스테이지 파일이 없습니다. 스테이지 번호{stageNumber}.txt");
            }

            return File.ReadAllLines(stageFilePath);
        }

        
    }
}
