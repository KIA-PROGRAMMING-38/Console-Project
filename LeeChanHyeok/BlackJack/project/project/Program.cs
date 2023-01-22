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
    enum CardPattern
    {
        none,
        spade,
        diamond,
        hert,
        club
    }
    
    enum CardNumber
    {
        none,
        one, two, three, four, five, six, seven, eight, nine, ten, jack, queen, king
    }
    class Program
    {
        static void Main()
        {
            Title titleSceen = new Title();
            titleSceen.TitleMain();
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.CursorVisible = false;

            //맵 함수 부분
            void MapExtent(int x, int y, string mapObject)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(mapObject);
            }

            //카드
            CardPattern cardPattern;
            CardNumber cardNumber;

            //CardPattern[] 
            //CardNumber[] cards = new CardNumber[53];

            
            Direction player = Direction.none;
            
            
            while (true)
            {
                //------------------------------------랜더-------------------------------------
                Console.Clear();
                Map mapRender = new Map();
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
                
            }
        }
    }
}