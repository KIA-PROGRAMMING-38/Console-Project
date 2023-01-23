using System.Security.Cryptography.X509Certificates;

namespace project
{
    enum Direction
    {
        none,
        up,
        down,
        enter
    }
    
  
    class Program
    {
        static void Main()
        {
            //Title titleSceen = new Title();
            //titleSceen.TitleMain();
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.CursorVisible = false;

            //카드
            const int CARD_COUNT = 13;
            string[] pattern = { "♠", "◆", "♥", "♣" };
            

            Direction player = Direction.none;

            Map mapRender = new Map();
            Random random = new Random();

            // 덱 초기화
            int user = 0;
            int dealer = 0;
            // 덱 정보 저장
            int saveDealer = 0;
            int saveruser = 0;

            while (true)
            {
                //------------------------------------랜더-------------------------------------
                Console.Clear();
                mapRender.MapRender();
                
                //------------------------------------ProcessInput ----------------------------
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                ConsoleKey key = keyinfo.Key;

                //------------------------------------update-----------------------------------

                // 메뉴 선택
                if(key == ConsoleKey.UpArrow)
                {
                    mapRender.playerY = Math.Max(mapRender.MIN_Y, mapRender.playerY - 3);
                    player = Direction.up;
                }

                if(key == ConsoleKey.DownArrow)
                {
                    mapRender.playerY = Math.Min(mapRender.playerY + 3, mapRender.MAX_Y);
                    player = Direction.down;
                }

                //게임 진행

                // 딜러
                int patternIndex = random.Next(pattern.Length);
                string patterns = pattern[patternIndex];
                Console.SetCursorPosition(51, 9);
                Console.Write(patterns);
                Console.SetCursorPosition(60, 15);
                Console.Write(patterns);
                
                dealer = random.Next(1, 13);
                Console.SetCursorPosition(56, 12);
                if(dealer < 11)
                {
                    Console.Write(dealer);
                }
                else if(dealer == 11)
                {
                    Console.Write("J");
                }
                else if(dealer == 12)
                {
                    Console.Write("Q");
                }
                else if(dealer == 13)
                {
                    Console.Write("K");
                }
                saveDealer += dealer;

                //유저
                patternIndex = random.Next(pattern.Length);
                patterns = pattern[patternIndex];
                Console.SetCursorPosition(51, 24);
                Console.Write(patterns);
                Console.SetCursorPosition(60, 30);
                Console.Write(patterns);
                user = random.Next(1, 13);
                Console.SetCursorPosition(56, 27);
                if(user < 11)
                {
                    Console.Write(user);
                }
                else if(user == 11) 
                {
                    Console.Write("J");
                }
                else if(user == 12)
                {
                    Console.Write("Q");
                }
                else if(user == 13)
                {
                    Console.Write("K");
                }
                
                saveruser += user;

                




            }
        }
    }
}