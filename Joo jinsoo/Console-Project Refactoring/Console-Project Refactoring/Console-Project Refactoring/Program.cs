using Console_Project_Refactoring.Assets.StageData;

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
            MapIcon[,] mapMetaData = new MapIcon[(MAP_OFFSET_X * 2) + MAP_MAX_X,
                (MAP_OFFSET_Y * 2) + MAP_MAX_Y];

            Stage currentScene = Stage.Default;

            Player player = new Player
            {
                X = 15,
                Y = 15
            };
            Wall[] walls = null;
            Utilityroom[] utilityroomDoor = null;
            Toilet[] toiletDoor = null;
            Bedroom[] bedroomDoor = null;
            Frontdoor[] frontDoor = null;
            LivingroomDoor_First[] firstLRDoor = null;
            LivingroomDoor_Second[] secondLRDoor = null;
            LivingroomDoor_Third[] thirdLRDoor = null;


            // 초기 스테이지 룩업테이블 구성
            GameSystem.MadeMapMetaData(mapMetaData, currentScene, player, walls, utilityroomDoor, toiletDoor,
                bedroomDoor, frontDoor, firstLRDoor, secondLRDoor, thirdLRDoor);

            // 초기 상호작용 룩업테이블 구성
            string[] lines = StageFormat.LoadStageFormat(0);

            for (int i = 1; i < lines.Length; ++i)
            {
                Console.WriteLine(lines[i]);
            }

            int a = 0;

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