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
            InteractionObject[,] mapInteractionData = new InteractionObject[(MAP_OFFSET_X * 2) + MAP_MAX_X,
                (MAP_OFFSET_Y * 2) + MAP_MAX_Y];

            Stage currentScene = Stage.Default;
            Stage captureSCene = Stage.Default;

            int answerCount = 0;

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
            Interactive_A[] interactiveFieldA;
            Interactive_B[] interactiveFieldB;
            Interactive_C[] interactiveFieldC;
            Interactive_D[] interactiveFieldD;
            Interactive_E[] interactiveFieldE;
            Interactive_F[] interactiveFieldF;
            Interactive_G[] interactiveFieldG;
            Interactive_H[] interactiveFieldH;
            Interactive_I[] interactiveFieldI;
            Interactive_J[] interactiveFieldJ;
            Interactive_K[] interactiveFieldK;
            Interactive_L[] interactiveFieldL;


            // 초기 스테이지 룩업테이블 구성
            currentScene = Stage.Livingroom;
            string[] lines = StageFormat.LoadStageFormat((int)currentScene);
            StageFormat.ParseStage(lines, out walls);
            string[] dividedRoomDoor = InteractedObject.LoadDividedroomDoor();
            InteractedObject.ParseStageDoorID(dividedRoomDoor, out bedroomDoor, out toiletDoor,
                out utilityroomDoor, out frontDoor);
            string[] livingroomDoor = InteractedObject.LoadDLivingroomDoor();
            InteractedObject.ParseLivingroomDoorID(livingroomDoor, out firstLRDoor,
                out secondLRDoor, out thirdLRDoor);
            string[] interactionFields = InteractedObject.LoadInteractionStage((int)currentScene);
            InteractedObject.ParseInteractionID(interactionFields, out interactiveFieldA, 
                out interactiveFieldB, out interactiveFieldC, out interactiveFieldD,
                out interactiveFieldE, out interactiveFieldF, out interactiveFieldG,
                out interactiveFieldH, out interactiveFieldI, out interactiveFieldJ,
                out interactiveFieldK, out interactiveFieldL);
            GameSystem.MadeMapMetaData(mapMetaData, currentScene, player, walls, utilityroomDoor, toiletDoor,
                bedroomDoor, frontDoor, firstLRDoor, secondLRDoor, thirdLRDoor);
            GameSystem.MadeInteractionData();

            //ConsoleKey key = Console.ReadKey().Key;

            Console.WriteLine("check");

            while (true)
            {
                if (captureSCene == currentScene)
                {

                }

                answerCount = 0;
                captureSCene = currentScene;
                player.pastX = player.X;
                player.pastY = player.Y;

                // render



                // process input



                // update
                //Player.MovePlayer(key, ref player.X, ref player.Y, player.X, player.Y);


                // afterupdate



            }
        }
    }
}