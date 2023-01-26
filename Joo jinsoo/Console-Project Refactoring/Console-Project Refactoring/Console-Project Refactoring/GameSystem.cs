using Console_Project_Refactoring.Assets.StageData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Console_Project_Refactoring
{
    enum Stage
    {
        Default,
        Livingroom,
        Utilityroom,
        Toilet,
        Bedroom
    }

    enum MapIcon
    {
        Default,
        Player,
        Wall,
        Utility,
        Toilet,
        Bed,
        Front,
        Living_1,
        Living_2,
        Living_3
    }

    enum InteractionObject
    {
        Default,
        LivingroomTable,
        LivingroomKitchen,
        LivingroomCloset,
        UtilityroomCloset,
        UtilityroomSecretroom,
        UtilityroomMirror,
        ToiletBathtub,
        ToiletSink,
        ToiletLavatory,
        BedroomBookshelf,
        BedroomPillow,
        BedroomDesk
    }

    class MapSymbol
    {
        public const char player = 'P';

        public const char wall = '#';
        public const char utilityroomDoor = 'U';
        public const char bedroomDoor = 'B';
        public const char toiletDoor = 'T';
        public const char frontDoor = 'F';

        public const char firstLivingroomDoor = 'F';
        public const char secondLivingroomDoor = 'S';
        public const char thirdLivingroomDoor = 'T';

        public const char interactiveA = 'a'; // 1
        public const char interactiveB = 'b'; // 2
        public const char interactiveC = 'c'; // 3

    }
    static class GameSystem
    {

        public const int MAP_OFFSET_X = 10;
        public const int MAP_OFFSET_Y = 3;
        public const int MAP_MAX_X = 24;
        public const int MAP_MAX_Y = 12;

        public const int CRIME_EVIDENCE = 9;
        public const int MURDERER_COUNT = 18;
        public const int WEAPON_COUNT = 10;
        public const int MOTIVE_COUNT = 9;
        public const int ANSWER_LIST = 3;
        public const int ADD_HINT_LIST = 2;

        public static MapIcon[,] mapMetaData = new MapIcon[(MAP_OFFSET_X * 2) + MAP_MAX_X,
            (MAP_OFFSET_Y * 2) + MAP_MAX_Y];
        public static InteractionObject[,] mapInteractionData = new InteractionObject[(MAP_OFFSET_X * 2) + MAP_MAX_X,
            (MAP_OFFSET_Y * 2) + MAP_MAX_Y];

        public static int[] murdererList = new int[CRIME_EVIDENCE];
        public static int[] weaponList = new int[CRIME_EVIDENCE];
        public static int[] motiveList = new int[CRIME_EVIDENCE];

        public static string[] LoadPrologue(int prologueNumber)
        {
            string motiveFilePath = Path.Combine("..\\..\\..\\Assets", "MessageData", $"GamePrologue{prologueNumber:D2}.txt");

            if (false == File.Exists(motiveFilePath))
            {
                Console.WriteLine($"파일이 없습니다. GamePrologue{prologueNumber:D2}.txt");
            }

            return File.ReadAllLines(motiveFilePath);
        }

        public static string[] LoadMurderer(int suspectNumber)
        {
            string suspectFilePath = Path.Combine("..\\..\\..\\Assets", "CandidateList", "Suspect", $"Suspect{suspectNumber:D2}.txt");

            if (false == File.Exists(suspectFilePath))
            {
                Console.WriteLine($"용의자 파일이 없습니다. 파일 번호{suspectNumber}.txt");
            }

            return File.ReadAllLines(suspectFilePath);
        }
        public static string[] LoadWeapon(int weaponNumber)
        {
            string weaponFilePath = Path.Combine("..\\..\\..\\Assets", "CandidateList", "Weapon", $"Weapon{weaponNumber:D2}.txt");

            if (false == File.Exists(weaponFilePath))
            {
                Console.WriteLine($"살해 도구 파일이 없습니다. 파일 번호{weaponNumber}.txt");
            }

            return File.ReadAllLines(weaponFilePath);
        }
        public static string[] LoadMotive(int motiveNumber)
        {
            string motiveFilePath = Path.Combine("..\\..\\..\\Assets", "CandidateList", "Motive", $"Motive{motiveNumber:D2}.txt");

            if (false == File.Exists(motiveFilePath))
            {
                Console.WriteLine($"살해 동기 파일이 없습니다. 파일 번호{motiveNumber}.txt");
            }

            return File.ReadAllLines(motiveFilePath);
        }


        public static string OutputTextToFirstLine(string[] fileText)
        {
            string outputText = fileText[0];
            return outputText;
        }
        public static string OutputTextToSecondLine(string[] fileText)
        {
            string outputText = fileText[1];
            return outputText;
        }
        public static string OutputTextToThirdLine(string[] fileText)
        {
            string outputText = fileText[2];
            return outputText;
        }

        public static int[] RandomCrimePick(int pickCount, int firstReapeat)
        {
            Random random = new Random();
            int[] outputArray = new int[pickCount];

            for (int checkJob = 0; checkJob < pickCount;)
            {
                bool checkNum = true;
                int randomNumber = random.Next(1, firstReapeat + 1);

                for (int alreadyInputCheck = 0; alreadyInputCheck < pickCount; ++alreadyInputCheck)
                {
                    if (outputArray[alreadyInputCheck] == randomNumber)
                    {
                        checkNum = false;
                        break;
                    }
                }

                if (checkNum)
                {
                    outputArray[checkJob] = randomNumber;
                    ++checkJob;
                }
            }

            return outputArray;
        }



        public static int correctAnswer(int[] inputArray)
        {
            Random random = new Random();
            int randomNumber = random.Next(9);

            int returnNumber = inputArray[randomNumber];

            return returnNumber;
        }

        public static void BeforeRender(Stage currentScene)
        {
            string[] temp = default;

            Console.ForegroundColor = ConsoleColor.White;
            temp = StageFormat.LoadDrawStage((int)currentScene);
            for (int i = 0; i < temp.Length; ++i)
            {
                Console.WriteLine(temp[i]);
            }


        }

        public static void Render(Stage currentScene, Player player, Wall[] walls, Utilityroom[] utilityroomDoor,
            Toilet[] toiletDoor, Bedroom[] bedroomDoor, Frontdoor[] frontDoor,
            LivingroomDoor_First[] firstLRDoor, LivingroomDoor_Second[] secondLRDoor,
            LivingroomDoor_Third[] thirdLRDoor, bool[] alreadySearchHint,
            string[] mixedHintString, bool[] answerOpportunity,
            string[] addHintString)
        {
            if (currentScene == Stage.Livingroom)
            {
                

                Console.ForegroundColor = ConsoleColor.White;
                ObjectRender(MAP_OFFSET_X, 1, "거실");

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                ObjectRender(bedroomDoor[0].X, bedroomDoor[0].Y, "B");
                ObjectRender(bedroomDoor[1].X, bedroomDoor[1].Y, "B");

                ObjectRender(toiletDoor[0].X, toiletDoor[0].Y, "T");
                ObjectRender(toiletDoor[1].X, toiletDoor[1].Y, "T");

                ObjectRender(utilityroomDoor[0].X, utilityroomDoor[0].Y, "U");
                ObjectRender(utilityroomDoor[1].X, utilityroomDoor[1].Y, "U");

                ObjectRender(frontDoor[0].X, frontDoor[0].Y, "F");
                ObjectRender(frontDoor[1].X, frontDoor[1].Y, "F");
            }

            if (currentScene == Stage.Utilityroom)
            {

                Console.ForegroundColor = ConsoleColor.White;
                ObjectRender(MAP_OFFSET_X, 1, "다용도실");

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                ObjectRender(firstLRDoor[0].X, firstLRDoor[0].Y, "L");
                ObjectRender(firstLRDoor[1].X, firstLRDoor[1].Y, "L");
            }
            if (currentScene == Stage.Toilet)
            {
                Console.ForegroundColor = ConsoleColor.White;
                ObjectRender(MAP_OFFSET_X, 1, "화장실");

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                ObjectRender(secondLRDoor[0].X, secondLRDoor[0].Y, "L");
                ObjectRender(secondLRDoor[1].X, secondLRDoor[1].Y, "L");
            }
            if (currentScene == Stage.Bedroom)
            {
                Console.ForegroundColor = ConsoleColor.White;
                ObjectRender(MAP_OFFSET_X, 1, "침실");

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                ObjectRender(thirdLRDoor[0].X, thirdLRDoor[0].Y, "L");
                ObjectRender(thirdLRDoor[1].X, thirdLRDoor[1].Y, "L");
            }

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            for (int i = 0; i < walls.Length; ++i)
            {
                ObjectRender(walls[i].X, walls[i].Y, "#");
            }

            Console.ForegroundColor = ConsoleColor.White;
            if (player.IsOnInteraction)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                ObjectRender(player.X, player.Y, "P");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                ObjectRender(player.X, player.Y, "P");
            }

            if (alreadySearchHint[0] == true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                ObjectRender(MAP_MAX_X * 3 + 4, MAP_OFFSET_Y,
                    mixedHintString[0]);
            }
            if (alreadySearchHint[1] == true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                ObjectRender(MAP_MAX_X * 3 + 4, MAP_OFFSET_Y + 1,
                    mixedHintString[1]);
            }
            if (alreadySearchHint[2] == true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                ObjectRender(MAP_MAX_X * 3 + 4, MAP_OFFSET_Y + 2,
                    mixedHintString[2]);
            }

            Console.ForegroundColor = ConsoleColor.White;
            ObjectRender(MAP_OFFSET_X * 2, 1, "남은 기회");
            ObjectRender(MAP_OFFSET_X * 3, 1, "O O O O");

            if (answerOpportunity[0])
            {
                Console.ForegroundColor = ConsoleColor.White;
                ObjectRender(MAP_MAX_X * 3 + 4, MAP_OFFSET_Y + 4,
                    addHintString[0]);

                ObjectRender(MAP_OFFSET_X * 3, 1, "@");
            }
            if (answerOpportunity[1])
            {
                Console.ForegroundColor = ConsoleColor.White;
                ObjectRender(MAP_MAX_X * 3 + 4, MAP_OFFSET_Y + 5,
                    addHintString[1]);

                ObjectRender(MAP_OFFSET_X * 3 + 2, 1, "@");
            }
            if (answerOpportunity[2])
            {
                ObjectRender(MAP_OFFSET_X * 3 + 4, 1, "@");
            }

        }

        public static void ObjectRender(int x, int y, string icon)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(icon);
        }

        public static void MadeMapMetaData(MapIcon[,] mapMetaData,
            in Stage currentScene, Player player, Wall[] walls, Utilityroom[] utilityroomDoor,
            Toilet[] toiletDoor, Bedroom[] bedroomDoor, Frontdoor[] frontDoor,
            LivingroomDoor_First[] firstLRDoor, LivingroomDoor_Second[] secondLRDoor,
            LivingroomDoor_Third[] thirdLRDoor)
        {
            mapMetaData[player.X, player.Y] = MapIcon.Player;

            for (int i = 0; i < walls.Length; ++i)
            {
                mapMetaData[walls[i].X, walls[i].Y] = MapIcon.Wall;
            }

            if (currentScene == Stage.Livingroom)
            {
                mapMetaData[utilityroomDoor[0].X, utilityroomDoor[0].Y] = MapIcon.Utility;
                mapMetaData[utilityroomDoor[1].X, utilityroomDoor[1].Y] = MapIcon.Utility;

                mapMetaData[toiletDoor[0].X, toiletDoor[0].Y] = MapIcon.Toilet;
                mapMetaData[toiletDoor[1].X, toiletDoor[1].Y] = MapIcon.Toilet;

                mapMetaData[bedroomDoor[0].X, bedroomDoor[0].Y] = MapIcon.Bed;
                mapMetaData[bedroomDoor[1].X, bedroomDoor[1].Y] = MapIcon.Bed;

                mapMetaData[frontDoor[0].X, frontDoor[0].Y] = MapIcon.Front;
                mapMetaData[frontDoor[1].X, frontDoor[1].Y] = MapIcon.Front;
            }

            if (currentScene == Stage.Utilityroom)
            {
                mapMetaData[firstLRDoor[0].X, firstLRDoor[0].Y] = MapIcon.Living_1;
                mapMetaData[firstLRDoor[1].X, firstLRDoor[1].Y] = MapIcon.Living_1;
            }

            if (currentScene == Stage.Toilet)
            {
                mapMetaData[secondLRDoor[0].X, secondLRDoor[0].Y] = MapIcon.Living_2;
                mapMetaData[secondLRDoor[1].X, secondLRDoor[1].Y] = MapIcon.Living_2;
            }

            if (currentScene == Stage.Bedroom)
            {
                mapMetaData[thirdLRDoor[0].X, thirdLRDoor[0].Y] = MapIcon.Living_3;
                mapMetaData[thirdLRDoor[1].X, thirdLRDoor[1].Y] = MapIcon.Living_3;
            }
        }

        public static void MadeInteractionData(InteractionObject[,] mapInteractionData, 
                Stage currentScene, Interactive_A[] interactiveFieldA,
                Interactive_B[] interactiveFieldB, Interactive_C[] interactiveFieldC)
        {
            if (currentScene == Stage.Livingroom)
            {
                for (int i = 0; i < interactiveFieldA.Length; ++i)
                {
                    mapInteractionData[interactiveFieldA[i].X, interactiveFieldA[i].Y]
                        = InteractionObject.LivingroomTable;
                }

                for (int i = 0; i < interactiveFieldB.Length; ++i)
                {
                    mapInteractionData[interactiveFieldB[i].X, interactiveFieldB[i].Y]
                        = InteractionObject.LivingroomKitchen;
                }

                for (int i = 0; i < interactiveFieldC.Length; ++i)
                {
                    mapInteractionData[interactiveFieldC[i].X, interactiveFieldC[i].Y]
                        = InteractionObject.LivingroomCloset;
                }
            }

            if (currentScene == Stage.Utilityroom)
            {
                for (int i = 0; i < interactiveFieldA.Length; ++i)
                {
                    mapInteractionData[interactiveFieldA[i].X, interactiveFieldA[i].Y]
                        = InteractionObject.UtilityroomCloset;
                }

                for (int i = 0; i < interactiveFieldB.Length; ++i)
                {
                    mapInteractionData[interactiveFieldB[i].X, interactiveFieldB[i].Y]
                        = InteractionObject.UtilityroomSecretroom;
                }

                for (int i = 0; i < interactiveFieldC.Length; ++i)
                {
                    mapInteractionData[interactiveFieldC[i].X, interactiveFieldC[i].Y]
                        = InteractionObject.UtilityroomMirror;
                }
            }

            if (currentScene == Stage.Toilet)
            {
                for (int i = 0; i < interactiveFieldA.Length; ++i)
                {
                    mapInteractionData[interactiveFieldA[i].X, interactiveFieldA[i].Y]
                        = InteractionObject.ToiletBathtub;
                }

                for (int i = 0; i < interactiveFieldB.Length; ++i)
                {
                    mapInteractionData[interactiveFieldB[i].X, interactiveFieldB[i].Y]
                        = InteractionObject.ToiletSink;
                }

                for (int i = 0; i < interactiveFieldC.Length; ++i)
                {
                    mapInteractionData[interactiveFieldC[i].X, interactiveFieldC[i].Y]
                        = InteractionObject.ToiletLavatory;
                }
            }

            if (currentScene == Stage.Bedroom)
            {
                for (int i = 0; i < interactiveFieldA.Length; ++i)
                {
                    mapInteractionData[interactiveFieldA[i].X, interactiveFieldA[i].Y]
                        = InteractionObject.BedroomBookshelf;
                }

                for (int i = 0; i < interactiveFieldB.Length; ++i)
                {
                    mapInteractionData[interactiveFieldB[i].X, interactiveFieldB[i].Y]
                        = InteractionObject.BedroomPillow;
                }

                for (int i = 0; i < interactiveFieldC.Length; ++i)
                {
                    mapInteractionData[interactiveFieldC[i].X, interactiveFieldC[i].Y]
                        = InteractionObject.BedroomDesk;
                }
            }
        }
        
        public static void AfterUpdate(ref Stage currentScene, Player player, Wall[] walls, Utilityroom[] utilityroomDoor,
            Toilet[] toiletDoor, Bedroom[] bedroomDoor, 
            LivingroomDoor_First[] firstLRDoor, LivingroomDoor_Second[] secondLRDoor,
            LivingroomDoor_Third[] thirdLRDoor)
        {
            MapIcon checkData = mapMetaData[player.X, player.Y];

            switch (checkData)
            {
                case MapIcon.Default:

                    break;

                case MapIcon.Wall:
                    player.X = player.pastX;
                    player.Y = player.pastY;

                    break;

                case MapIcon.Utility:
                    currentScene = Stage.Utilityroom;
                    player.X = firstLRDoor[1].X + 1;
                    player.Y = firstLRDoor[1].Y;

                    break;

                case MapIcon.Toilet:
                    currentScene = Stage.Toilet;
                    player.X = secondLRDoor[0].X;
                    player.Y = secondLRDoor[0].Y + 1;

                    break;

                case MapIcon.Bed:
                    currentScene = Stage.Bedroom;
                    player.X = thirdLRDoor[0].X - 1;
                    player.Y = thirdLRDoor[0].Y;

                    break;

             

                case MapIcon.Living_1:
                    currentScene = Stage.Livingroom;
                    player.X = utilityroomDoor[1].X - 1;
                    player.Y = utilityroomDoor[1].Y;

                    break;

                case MapIcon.Living_2:
                    currentScene = Stage.Livingroom;
                    player.X = toiletDoor[0].X;
                    player.Y = toiletDoor[0].Y - 1;

                    break;

                case MapIcon.Living_3:
                    currentScene = Stage.Livingroom;
                    player.X = bedroomDoor[0].X + 1;
                    player.Y = bedroomDoor[0].Y;

                    break;

            }
        }
        public static void FindHintString(ref int appearHintStringCount, ref bool[] checkHint, in int a, ref bool[] alreadySearchHint)
        {
            if (appearHintStringCount < 3)
            {
                if (checkHint[a] == true)
                {
                    checkHint[a] = false;
                    alreadySearchHint[appearHintStringCount] = true;
                    ++appearHintStringCount;
                }
            }
        }

        public static void Interaction(Stage currentScene, Player player,
            ConsoleKey key, ref bool[] checkHint, ref int appearHintStringCount,
            ref bool[] alreadySearchHint)
        {
            InteractionObject checkData = mapInteractionData[player.X, player.Y];
            string[] outputMessage = default;
            Console.ForegroundColor = ConsoleColor.White;

            if (checkData == InteractionObject.Default)
                player.IsOnInteraction = false;

            if (currentScene == Stage.Livingroom)
            {
                switch (checkData)
                {
                    case InteractionObject.LivingroomTable:
                        AppearMessage(outputMessage, InteractionObject.LivingroomTable,
                            player, key);
                        if (key == ConsoleKey.R)
                        {
                            if (checkHint[0] == true)
                            {
                                FindHintString(ref appearHintStringCount, ref checkHint,
                                    0, ref alreadySearchHint);
                            }
                        }

                        break;

                    case InteractionObject.LivingroomKitchen:
                        AppearMessage(outputMessage, InteractionObject.LivingroomKitchen,
                            player, key);
                        if (key == ConsoleKey.R)
                        {
                            if (checkHint[1] == true)
                            {
                                FindHintString(ref appearHintStringCount, ref checkHint,
                                    1, ref alreadySearchHint);
                            }
                        }

                        break;

                    case InteractionObject.LivingroomCloset:
                        AppearMessage(outputMessage, InteractionObject.LivingroomCloset,
                            player, key);
                        if (key == ConsoleKey.R)
                        {
                            if (checkHint[2] == true)
                            {
                                FindHintString(ref appearHintStringCount, ref checkHint,
                                    2, ref alreadySearchHint);
                            }
                        }

                        break;
                }
            }

            if (currentScene == Stage.Utilityroom)
            {
                switch (checkData)
                {

                    case InteractionObject.UtilityroomCloset:
                        AppearMessage(outputMessage, InteractionObject.UtilityroomCloset,
                            player, key);
                        if (key == ConsoleKey.R)
                        {
                            if (checkHint[3] == true)
                            {
                                FindHintString(ref appearHintStringCount, ref checkHint,
                                    3, ref alreadySearchHint);
                            }
                        }

                        break;

                    case InteractionObject.UtilityroomSecretroom:
                        AppearMessage(outputMessage, InteractionObject.UtilityroomSecretroom,
                            player, key);
                        if (key == ConsoleKey.R)
                        {
                            if (checkHint[4] == true)
                            {
                                FindHintString(ref appearHintStringCount, ref checkHint,
                                    4, ref alreadySearchHint);
                            }
                        }

                        break;

                    case InteractionObject.UtilityroomMirror:
                        AppearMessage(outputMessage, InteractionObject.UtilityroomMirror,
                            player, key);
                        if (key == ConsoleKey.R)
                        {
                            if (checkHint[5] == true)
                            {
                                FindHintString(ref appearHintStringCount, ref checkHint,
                                    5, ref alreadySearchHint);
                            }
                        }

                        break;
                }
            }

            if (currentScene == Stage.Toilet)
            {
                switch (checkData)
                {
                    case InteractionObject.ToiletBathtub:
                        AppearMessage(outputMessage, InteractionObject.ToiletBathtub,
                            player, key);
                        if (key == ConsoleKey.R)
                        {
                            if (checkHint[6] == true)
                            {
                                FindHintString(ref appearHintStringCount, ref checkHint,
                                    6, ref alreadySearchHint);
                            }
                        }

                        break;

                    case InteractionObject.ToiletSink:
                        AppearMessage(outputMessage, InteractionObject.ToiletSink,
                            player, key);
                        if (key == ConsoleKey.R)
                        {
                            if (checkHint[7] == true)
                            {
                                FindHintString(ref appearHintStringCount, ref checkHint,
                                    7, ref alreadySearchHint);
                            }
                        }

                        break;

                    case InteractionObject.ToiletLavatory:
                        AppearMessage(outputMessage, InteractionObject.ToiletLavatory,
                            player, key);
                        if (key == ConsoleKey.R)
                        {
                            if (checkHint[8] == true)
                            {
                                FindHintString(ref appearHintStringCount, ref checkHint,
                                    8, ref alreadySearchHint);
                            }
                        }

                        break;
                }
            }

            if (currentScene == Stage.Bedroom)
            {
                switch (checkData)
                {
                    case InteractionObject.BedroomBookshelf:
                        AppearMessage(outputMessage, InteractionObject.BedroomBookshelf,
                            player, key);
                        if (key == ConsoleKey.R)
                        {
                            if (checkHint[9] == true)
                            {
                                FindHintString(ref appearHintStringCount, ref checkHint,
                                    9, ref alreadySearchHint);
                            }
                        }

                        break;

                    case InteractionObject.BedroomPillow:
                        AppearMessage(outputMessage, InteractionObject.BedroomPillow,
                            player, key);
                        if (key == ConsoleKey.R)
                        {
                            if (checkHint[10] == true)
                            {
                                FindHintString(ref appearHintStringCount, ref checkHint,
                                    10, ref alreadySearchHint);
                            }
                        }

                        break;

                    case InteractionObject.BedroomDesk:
                        AppearMessage(outputMessage, InteractionObject.BedroomDesk,
                            player, key);
                        if (key == ConsoleKey.R)
                        {
                            if (checkHint[11] == true)
                            {
                                FindHintString(ref appearHintStringCount, ref checkHint,
                                    11, ref alreadySearchHint);
                            }
                        }

                        break;
                }
            }
        }

        public static void AppearMessage(string[] str, InteractionObject someCoordinate,
            Player player, ConsoleKey inputkey)
        {
            str = Player.LoadMessage((int)someCoordinate);
            player.IsOnInteraction = true;
            if (inputkey == ConsoleKey.R && player.IsOnInteraction == true)
            {
                Console.SetCursorPosition(MAP_OFFSET_X, MAP_OFFSET_Y * 2 + MAP_MAX_Y - 1);
                Console.WriteLine(str[0]);
                Console.SetCursorPosition(MAP_OFFSET_X, MAP_OFFSET_Y * 2 + MAP_MAX_Y);
                Console.WriteLine(str[1]);

                inputkey = Console.ReadKey().Key;
            }
        }

        public static void MapMetaDataClear(MapIcon[,] mapMetaData,
            Player player, Wall[] walls, Utilityroom[] utilityroomDoor,
            Toilet[] toiletDoor, Bedroom[] bedroomDoor, Frontdoor[] frontDoor,
            LivingroomDoor_First[] firstLRDoor, LivingroomDoor_Second[] secondLRDoor,
            LivingroomDoor_Third[] thirdLRDoor
            )
        {
            mapMetaData[player.X, player.Y] = MapIcon.Default;

            for (int i = 0; i < walls.Length; ++i)
            {
                mapMetaData[walls[i].X, walls[i].Y] = MapIcon.Default;
            }

            mapMetaData[bedroomDoor[0].X, bedroomDoor[0].Y] = MapIcon.Default;
            mapMetaData[bedroomDoor[1].X, bedroomDoor[1].Y] = MapIcon.Default;

            mapMetaData[toiletDoor[0].X, toiletDoor[0].Y] = MapIcon.Default;
            mapMetaData[toiletDoor[1].X, toiletDoor[1].Y] = MapIcon.Default;

            mapMetaData[utilityroomDoor[0].X, utilityroomDoor[0].Y] = MapIcon.Default;
            mapMetaData[utilityroomDoor[1].X, utilityroomDoor[1].Y] = MapIcon.Default;

            mapMetaData[frontDoor[0].X, frontDoor[0].Y] = MapIcon.Default;
            mapMetaData[frontDoor[1].X, frontDoor[1].Y] = MapIcon.Default;

            mapMetaData[firstLRDoor[0].X, firstLRDoor[0].Y] = MapIcon.Default;
            mapMetaData[firstLRDoor[1].X, firstLRDoor[1].Y] = MapIcon.Default;

            mapMetaData[secondLRDoor[0].X, secondLRDoor[0].Y] = MapIcon.Default;
            mapMetaData[secondLRDoor[1].X, secondLRDoor[1].Y] = MapIcon.Default;

            mapMetaData[thirdLRDoor[0].X, thirdLRDoor[0].Y] = MapIcon.Default;
            mapMetaData[thirdLRDoor[1].X, thirdLRDoor[1].Y] = MapIcon.Default;
        }

        public static void MapInteractionDataClear(InteractionObject[,] mapInteractionData,
                Stage currentScene, Interactive_A[] interactiveFieldA,
                Interactive_B[] interactiveFieldB, Interactive_C[] interactiveFieldC)
        {
            if (currentScene != Stage.Livingroom)
            {
                for (int i = 0; i < interactiveFieldA.Length; ++i)
                {
                    mapInteractionData[interactiveFieldA[i].X, interactiveFieldA[i].Y]
                        = InteractionObject.Default;
                }

                for (int i = 0; i < interactiveFieldB.Length; ++i)
                {
                    mapInteractionData[interactiveFieldB[i].X, interactiveFieldB[i].Y]
                        = InteractionObject.Default;
                }

                for (int i = 0; i < interactiveFieldC.Length; ++i)
                {
                    mapInteractionData[interactiveFieldC[i].X, interactiveFieldC[i].Y]
                        = InteractionObject.Default;
                }
            }

            if (currentScene != Stage.Utilityroom)
            {
                for (int i = 0; i < interactiveFieldA.Length; ++i)
                {
                    mapInteractionData[interactiveFieldA[i].X, interactiveFieldA[i].Y]
                        = InteractionObject.Default;
                }

                for (int i = 0; i < interactiveFieldB.Length; ++i)
                {
                    mapInteractionData[interactiveFieldB[i].X, interactiveFieldB[i].Y]
                        = InteractionObject.Default;
                }

                for (int i = 0; i < interactiveFieldC.Length; ++i)
                {
                    mapInteractionData[interactiveFieldC[i].X, interactiveFieldC[i].Y]
                        = InteractionObject.Default;
                }
            }

            if (currentScene != Stage.Toilet)
            {
                for (int i = 0; i < interactiveFieldA.Length; ++i)
                {
                    mapInteractionData[interactiveFieldA[i].X, interactiveFieldA[i].Y]
                        = InteractionObject.Default;
                }

                for (int i = 0; i < interactiveFieldB.Length; ++i)
                {
                    mapInteractionData[interactiveFieldB[i].X, interactiveFieldB[i].Y]
                        = InteractionObject.Default;
                }

                for (int i = 0; i < interactiveFieldC.Length; ++i)
                {
                    mapInteractionData[interactiveFieldC[i].X, interactiveFieldC[i].Y]
                        = InteractionObject.Default;
                }
            }

            if (currentScene != Stage.Bedroom)
            {
                for (int i = 0; i < interactiveFieldA.Length; ++i)
                {
                    mapInteractionData[interactiveFieldA[i].X, interactiveFieldA[i].Y]
                        = InteractionObject.Default;
                }

                for (int i = 0; i < interactiveFieldB.Length; ++i)
                {
                    mapInteractionData[interactiveFieldB[i].X, interactiveFieldB[i].Y]
                        = InteractionObject.Default;
                }

                for (int i = 0; i < interactiveFieldC.Length; ++i)
                {
                    mapInteractionData[interactiveFieldC[i].X, interactiveFieldC[i].Y]
                        = InteractionObject.Default;
                }
            }


        }

        public static void StageStatus()
        {
            Console.ForegroundColor = ConsoleColor.White;
            ObjectRender(MAP_OFFSET_X * 2 + MAP_MAX_X, MAP_OFFSET_Y, "용의자");
            ObjectRender(MAP_OFFSET_X * 3 + MAP_MAX_X, MAP_OFFSET_Y, "살해도구");
            ObjectRender(MAP_OFFSET_X * 4 + MAP_MAX_X + 1, MAP_OFFSET_Y, "살해동기");

            Console.ForegroundColor = ConsoleColor.Red;
            for (int murdererListPick = 0; murdererListPick < CRIME_EVIDENCE; ++murdererListPick)
            {
                ObjectRender(MAP_OFFSET_X * 2 + MAP_MAX_X, MAP_OFFSET_Y + murdererListPick + 1,
                   OutputTextToFirstLine(LoadMurderer(murdererList[murdererListPick])));
            }

            Console.ForegroundColor = ConsoleColor.Green;
            for (int weaponListPick = 0; weaponListPick < CRIME_EVIDENCE; ++weaponListPick)
            {
                ObjectRender(MAP_OFFSET_X * 3 + MAP_MAX_X, MAP_OFFSET_Y + weaponListPick + 1,
                   OutputTextToFirstLine(LoadWeapon(weaponList[weaponListPick])));
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            for (int motiveListPick = 0; motiveListPick < CRIME_EVIDENCE; ++motiveListPick)
            {
                ObjectRender(MAP_OFFSET_X * 4 + MAP_MAX_X + 1, MAP_OFFSET_Y + motiveListPick + 1,
                   OutputTextToFirstLine(LoadMotive(motiveList[motiveListPick])));
            }
        }
    }
}
