namespace project
{
    class Program
    {
        static void Main()
        {
            //Title titleSceen = new Title();
            //titleSceen.TitleMain();
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.Title = "블랙잭";
            Console.CursorVisible= false;
            

            //맵 함수 부분
            void MapExtent(int x, int y, string mapObject)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(mapObject);
            }
            
            while (true)
            {
                //------------------------------------랜더-------------------------------------
                Console.Clear();
                //블랙잭 맵
                for(int i = 0; i < 125; ++i)
                {
                    MapExtent(30 + i, 3, "#");
                    MapExtent(30 + i, 37, "#");
                    
                }
                for(int i = 0; i < 35; ++i)
                {
                    MapExtent(30, 3 + i, "#");
                    MapExtent(155, 3 + i, "#");

                }
                
                //명령어 맵
                for(int i = 0; i < 35; ++i)
                {
                    MapExtent(119 + i, 5, "-");
                    MapExtent(119 + i, 7, "-");
                }

                for(int i = 0; i < 3; ++i)
                {
                    MapExtent(119, 5 + i, "l");
                    MapExtent(153, 5 + i, "l");
                }

                // 소지금
                MapExtent(120, 6, "소지금 : {}");

                //메뉴 테두리
                for(int i = 0; i < 35; ++i)
                {
                    MapExtent(119 + i, 10, "#");
                    MapExtent(119 + i, 20, "#");
                }
                for(int i = 0; i < 10; ++i)
                {
                    MapExtent(119, 10 + i, "#");
                    MapExtent(153, 10 + i, "#");
                }

                //메뉴
                MapExtent(126, 12, "▶ " + "Raise");
                MapExtent(126, 15, "▶ " + "Check");
                MapExtent(126, 18, "▶ " + "Fold");

                //딜러 카드
                for(int i = 0; i < 12; ++i)
                {
                    MapExtent(36, 8, "< Dealer >");
                    MapExtent(50 + i, 8, "-");
                    MapExtent(50 + i, 16, "-");
                    MapExtent(65 + i, 8, "-");
                    MapExtent(65 + i, 16, "-");
                    MapExtent(80 + i, 8, "-");
                    MapExtent(80 + i, 16, "-");
                }
                
                for(int i = 0; i < 9; ++i)
                {
                    MapExtent(50, 8 + i, "I");
                    MapExtent(62, 8 + i, "I");
                    MapExtent(65, 8 + i, "I");
                    MapExtent(77, 8 + i, "I");
                    MapExtent(80, 8 + i, "I");
                    MapExtent(92, 8 + i, "I");
                }

                //플레이어 카드
                for(int i = 0; i < 12; ++i)
                {
                    MapExtent(36, 23, "< player >");
                    MapExtent(50 + i, 23, "-");
                    MapExtent(50 + i, 31, "-");
                    MapExtent(65 + i, 23, "-");
                    MapExtent(65 + i, 31, "-");
                    MapExtent(80 + i, 23, "-");
                    MapExtent(80 + i, 31, "-");
                    
                }

                for(int i = 0; i < 9; ++i)
                {
                    MapExtent(50, 23 + i, "I");
                    MapExtent(62, 23 + i, "I");
                    MapExtent(65, 23 + i, "I");
                    MapExtent(77, 23 + i, "I");
                    MapExtent(80, 23 + i, "I");
                    MapExtent(92, 23 + i, "I");
                }

                //------------------------------------ProcessInput ----------------------------
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                ConsoleKey key = keyinfo.Key;

                //------------------------------------update-----------------------------------
                
            }
        }
    }
}