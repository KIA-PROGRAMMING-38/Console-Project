namespace Console_Project_Refactoring
{
    class Program
    {
        static void Main()
        {
            Console.Clear();
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.Title = "Dying Message";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Clear();

            // 초기 변수와 상수 선언
            // 맵 크기 X = 10 24 10 / Y = 3 12 3
            const int MAP_OFFSET_X = 10;
            const int MAP_OFFSET_Y = 3;
            const int MAP_MAX_X = 24;
            const int MAP_MAX_Y = 12;

            // 초기 스테이지 룩업테이블 구성
            // 초기 상호작용 룩업테이블 구성

            while (true)
            {
                // if 씬이 바뀔 때마다 설정 초기화



                // render



                // process input


                
                // update



                // afterupdate



            }

        }
    }
}