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
            Stage captureScene = Stage.Livingroom;
            

            GameSystem.murdererList = GameSystem.RandomCrimePick(GameSystem.CRIME_EVIDENCE,
                GameSystem.MURDERER_COUNT);
            GameSystem.weaponList = GameSystem.RandomCrimePick(GameSystem.CRIME_EVIDENCE,
                GameSystem.WEAPON_COUNT);
            GameSystem.motiveList = GameSystem.RandomCrimePick(GameSystem.CRIME_EVIDENCE,
                GameSystem.MOTIVE_COUNT);

            int[] answerNumber = new int[GameSystem.ANSWER_LIST];
            answerNumber[0] = GameSystem.correctAnswer(GameSystem.murdererList);
            answerNumber[1] = GameSystem.correctAnswer(GameSystem.weaponList);
            answerNumber[2] = GameSystem.correctAnswer(GameSystem.motiveList);
            string[] corretAnswerMurderer = GameSystem.LoadMurderer(answerNumber[0]);
            string[] corretAnswerWeapon = GameSystem.LoadWeapon(answerNumber[1]);
            string[] corretAnswerMotive = GameSystem.LoadMotive(answerNumber[2]);
            string[] hintString = new string[GameSystem.ANSWER_LIST];
            int[] mixedHintStringNumber = new int[GameSystem.ANSWER_LIST];
            mixedHintStringNumber = GameSystem.RandomCrimePick(GameSystem.ANSWER_LIST, GameSystem.ANSWER_LIST);
            hintString[0] = GameSystem.OutputTextToSecondLine(GameSystem.LoadMurderer(answerNumber[0]));
            hintString[1] = GameSystem.OutputTextToSecondLine(GameSystem.LoadWeapon(answerNumber[1]));
            hintString[2] = GameSystem.OutputTextToSecondLine(GameSystem.LoadMotive(answerNumber[2]));
            string[] mixedHintString = new string[GameSystem.ANSWER_LIST];
            mixedHintString[0] = hintString[mixedHintStringNumber[0] - 1];
            mixedHintString[1] = hintString[mixedHintStringNumber[1] - 1];
            mixedHintString[2] = hintString[mixedHintStringNumber[2] - 1];
            string[] addHintString = new string[GameSystem.ADD_HINT_LIST];
            addHintString[0] = GameSystem.OutputTextToThirdLine(GameSystem.LoadMurderer(answerNumber[0]));
            addHintString[1] = GameSystem.OutputTextToThirdLine(GameSystem.LoadMotive(answerNumber[2]));

            InteractionObject[] existHint = GameClear.RandomExistHint(answerNumber);
            int appearHintStringCount = 0;
            int answerCount = 0;
            bool[] checkHint = new bool[existHint.Length];
            for (int i = 0; i - 1 < checkHint.Length - 1; ++i)
            {
                if (existHint[i] != InteractionObject.Default)
                {
                    checkHint[i] = true;
                }
            }
            bool[] alreadySearchHint = new bool[GameSystem.ANSWER_LIST];

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
            ExceptionObj_01[] exceptionObj_01;
            ExceptionObj_02[] exceptionObj_02;
            ExceptionObj_03[] exceptionObj_03;
            ExceptionObj_04[] exceptionObj_04;
            ExceptionObj_05[] exceptionObj_05;
            ExceptionObj_06[] exceptionObj_06;
            ExceptionObj_07[] exceptionObj_07;
            ExceptionObj_08[] exceptionObj_08;
            ExceptionObj_09[] exceptionObj_09;
            ExceptionObj_10[] exceptionObj_10;
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

            string[] prologue = GameSystem.LoadPrologue(0);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            for (int i = 0; i < prologue.Length; ++i)
            {
                Console.WriteLine(prologue[i]);
            }

            ConsoleKey key = Console.ReadKey().Key;

            //for (int i = 1; i < 4; ++i)
            //{
            //    Console.Clear();
            //
            //    prologue = GameSystem.LoadPrologue(i);
            //    for (int j = 0; j < prologue.Length; ++j)
            //    {
            //        Console.WriteLine(prologue[j]);
            //    }
            //
            //    if (i == 1)
            //    {
            //        ++i;
            //        prologue = GameSystem.LoadPrologue(i);
            //        for (int j = 0; j < prologue.Length; ++j)
            //        {
            //            Console.WriteLine(prologue[j]);
            //
            //            Thread.Sleep(5000);
            //        }
            //    }
            //
            //    if (i == 3)
            //    {
            //        key = Console.ReadKey().Key;
            //    }
            //}

            // 초기 스테이지 룩업테이블 구성
            currentScene = Stage.Livingroom;
            string[] lines = StageFormat.LoadStageFormat((int)currentScene);
            StageFormat.ParseStage(lines, out walls, out exceptionObj_01, out exceptionObj_02,
                out exceptionObj_03, out exceptionObj_04, out exceptionObj_05, out exceptionObj_06,
                out exceptionObj_07, out exceptionObj_08, out exceptionObj_09, out exceptionObj_10);
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

            while (true)
            {
                if (captureScene != currentScene)
                {
                    GameSystem.MapMetaDataClear(GameSystem.mapMetaData, player, walls, utilityroomDoor,
                    toiletDoor, bedroomDoor, frontDoor, firstLRDoor,
                    secondLRDoor, thirdLRDoor);
                    GameSystem.MapInteractionDataClear(GameSystem.mapInteractionData, currentScene,
                    interactiveFieldA, interactiveFieldB, interactiveFieldC,
                    interactiveFieldD, interactiveFieldE, interactiveFieldF,
                    interactiveFieldG, interactiveFieldH, interactiveFieldI,
                    interactiveFieldJ, interactiveFieldK, interactiveFieldL);

                    lines = StageFormat.LoadStageFormat((int)currentScene);
                    StageFormat.ParseStage(lines, out walls, out exceptionObj_01, out exceptionObj_02,
                out exceptionObj_03, out exceptionObj_04, out exceptionObj_05, out exceptionObj_06,
                out exceptionObj_07, out exceptionObj_08, out exceptionObj_09, out exceptionObj_10);
                    interactionFields = InteractedObject.LoadInteractionStage((int)currentScene);
                    InteractedObject.ParseInteractionID(interactionFields, out interactiveFieldA,
                        out interactiveFieldB, out interactiveFieldC, out interactiveFieldD,
                        out interactiveFieldE, out interactiveFieldF, out interactiveFieldG,
                        out interactiveFieldH, out interactiveFieldI, out interactiveFieldJ,
                        out interactiveFieldK, out interactiveFieldL);
                    GameSystem.MadeInteractionData(GameSystem.mapInteractionData, currentScene,
                    interactiveFieldA, interactiveFieldB, interactiveFieldC,
                    interactiveFieldD, interactiveFieldE, interactiveFieldF,
                    interactiveFieldG, interactiveFieldH, interactiveFieldI,
                    interactiveFieldJ, interactiveFieldK, interactiveFieldL);

                }

                GameSystem.MadeMapMetaData(GameSystem.mapMetaData, currentScene, player, walls, utilityroomDoor, toiletDoor,
                bedroomDoor, frontDoor, firstLRDoor, secondLRDoor, thirdLRDoor);
                GameSystem.MadeInteractionData(GameSystem.mapInteractionData, currentScene,
                interactiveFieldA, interactiveFieldB, interactiveFieldC,
                interactiveFieldD, interactiveFieldE, interactiveFieldF,
                interactiveFieldG, interactiveFieldH, interactiveFieldI,
                interactiveFieldJ, interactiveFieldK, interactiveFieldL);

                Console.Clear();

                answerCount = 0;
                captureScene = currentScene;
                player.pastX = player.X;
                player.pastY = player.Y;

                GameSystem.StageStatus();
                GameSystem.Render(currentScene, player, walls, utilityroomDoor,
                    toiletDoor, bedroomDoor, frontDoor, firstLRDoor, 
                    secondLRDoor, thirdLRDoor, exceptionObj_01, exceptionObj_02,
                    exceptionObj_03, exceptionObj_04, exceptionObj_05, exceptionObj_06,
                    exceptionObj_07, exceptionObj_08, exceptionObj_09, exceptionObj_10,
                    alreadySearchHint, mixedHintString, GameClear.answerOpportunity, addHintString);

                key = Console.ReadKey().Key;

                Player.MovePlayer(key, ref player.X, ref player.Y, player.X, player.Y);

                GameSystem.AfterUpdate(ref currentScene, player, walls, utilityroomDoor,
                    toiletDoor, bedroomDoor, firstLRDoor,
                    secondLRDoor, thirdLRDoor);

                GameSystem.Interaction(currentScene, player, key, ref checkHint,
                    ref appearHintStringCount, ref alreadySearchHint);

                GameClear.InputAnswer(player, frontDoor, corretAnswerMurderer[0],
                    corretAnswerWeapon[0], corretAnswerMotive[0], answerCount, key);

                

            }
        }
    }
}