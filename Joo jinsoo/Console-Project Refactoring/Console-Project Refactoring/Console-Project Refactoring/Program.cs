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

            Stage currentScene = Stage.Default;
            Stage captureSCene = Stage.Default;

            int answerCount = 0;

            Player player = new Player
            {
                X = 12,
                Y = 6
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
            GameSystem.MadeMapMetaData(GameSystem.mapMetaData, currentScene, player, walls, utilityroomDoor, toiletDoor,
                bedroomDoor, frontDoor, firstLRDoor, secondLRDoor, thirdLRDoor);
            GameSystem.MadeInteractionData(GameSystem.mapInteractionData, currentScene,
               interactiveFieldA, interactiveFieldB, interactiveFieldC,
               interactiveFieldD, interactiveFieldE, interactiveFieldF,
               interactiveFieldG, interactiveFieldH, interactiveFieldI,
               interactiveFieldJ, interactiveFieldK, interactiveFieldL);

            

            Console.WriteLine("check");

            ConsoleKey key = Console.ReadKey().Key;

            while (true)
            {
                if (captureSCene == currentScene)
                {
                    
                }

                Console.Clear();

                answerCount = 0;
                captureSCene = currentScene;
                player.pastX = player.X;
                player.pastY = player.Y;

                GameSystem.Render(currentScene, player, walls, utilityroomDoor,
                    toiletDoor, bedroomDoor, frontDoor, firstLRDoor, 
                    secondLRDoor, thirdLRDoor);

                key = Console.ReadKey().Key;

                Player.MovePlayer(key, ref player.X, ref player.Y, player.X, player.Y);


                // afterupdate



            }
        }
    }
}