using Console_ReProject.Map;
using System.Numerics;
using Console_ReProject.Object;
using Console_ReProject.StageRender;
using Console_ReProject.Object.Door;
using Console_ReProject.Object.MapExceptionObject;
using Microsoft.VisualBasic;
using System.IO.IsolatedStorage;

namespace Console_ReProject
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
            // X = 5 24 5 / Y = 3 12 3
            const int MAP_OFFSET_X = 10;
            const int MAP_OFFSET_Y = 3;
            const int MAP_MAX_X = 24;
            const int MAP_MAX_Y = 12;

            bool[] answerOpportunity = new bool[4];
            int opportunityCount = 0;

            MapIcon[,] mapMetaData = new MapIcon[(MAP_OFFSET_X * 2) + MAP_MAX_X, (MAP_OFFSET_Y * 2) + MAP_MAX_Y];
            MapIcon checkData = MapIcon.Default;
            Stage currentScene = Stage.Title;
            Stage captureScene = Stage.Title;
            InteractionObject[,] mapInteractionData = new InteractionObject[(MAP_OFFSET_X * 2) + MAP_MAX_X, (MAP_OFFSET_Y * 2) + MAP_MAX_Y];

            // 정답지 선정
            int[] murdererList = new int[Game.CRIME_EVIDENCE];
            int[] weaponList = new int[Game.CRIME_EVIDENCE];
            int[] motiveList = new int[Game.CRIME_EVIDENCE];
            murdererList = Game.RandomPickJob(Game.CRIME_EVIDENCE);
            weaponList = Game.RandomPickWeapon(Game.CRIME_EVIDENCE);
            motiveList = Game.RandomPickMotive(Game.CRIME_EVIDENCE);

            int[] answerList = new int[3];
            answerList[0] = Game.correctAnswer(murdererList);
            answerList[1] = Game.correctAnswer(weaponList);
            answerList[2] = Game.correctAnswer(motiveList);
            
            string[] corretAnswerMurderer = Game.LoadJob(answerList[0]);
            string[] corretAnswerWeapon = Game.LoadWeapon(answerList[1]);
            string[] corretAnswerMotive = Game.LoadMotive(answerList[2]);
            int answerCount = 0;
            string[] hintString = new string[answerList.Length];
            hintString[0] = Game.OutputTextToSecondLine(Game.LoadJob(answerList[0]));
            hintString[1] = Game.OutputTextToSecondLine(Game.LoadWeapon(answerList[1]));
            hintString[2] = Game.OutputTextToSecondLine(Game.LoadMotive(answerList[2]));
            string[] addHintString = new string[2];
            addHintString[0] = Game.OutputTextToThirdLine(Game.LoadJob(answerList[0]));
            addHintString[1] = Game.OutputTextToThirdLine(Game.LoadMotive(answerList[2]));


            int[] mixedHintString = new int[answerList.Length];
            mixedHintString = Game.MixedHintString(answerList.Length);
            string[] mixedHintStrings = new string[answerList.Length];
            mixedHintStrings[0] = hintString[mixedHintString[0] - 1];
            mixedHintStrings[1] = hintString[mixedHintString[1] - 1];
            mixedHintStrings[2] = hintString[mixedHintString[2] - 1];

            int hintStringCount = 0;

            bool[] checkAnswer = new bool[3];
            int checkAnswerCount = 0;


            int[] initialHint = new int[ObjectCoordinate.INITIAL_HINT];
            initialHint = ObjectCoordinate.InitialRandomHint();
            InteractionObject[] existHint = Game.RandomExistHint(answerList);
            bool[] checkHint = new bool[12];
            bool[] alreadySearchHint = new bool[3];
            bool[] afterAppearHint = new bool[3];

            for (int i = 0; i - 1 < checkHint.Length; ++i)
            {
                if (existHint[i] != InteractionObject.Default)
                {
                    checkHint[i] = true;
                }
            }

            void FindHintString(in int a)
            {
                if (hintStringCount < 3)
                {
                    if (checkHint[a] == true)
                    {
                        checkHint[a] = false;
                        alreadySearchHint[hintStringCount] = true;
                        ++hintStringCount;

                    }
                }
            }

            #region 클래스 선언
            Player player = null;
            Wall[] walls;
            Bedroom[] bedroomDoor;
            Toilet[] toiletDoor;
            Utilityroom[] utilityroomDoor;
            Frontdoor[] frontDoor;
            FirstLivingroomDoor[] firstLivingroomDoor;
            SecondLivingroomDoor[] secondLivingroomDoor;
            ThirdLivingroomDoor[] thirdLivingroomDoor;
            ExceptionObj_0[] exceptionObj_0;
            ExceptionObj_1[] exceptionObj_1;
            ExceptionObj_2[] exceptionObj_2;
            ExceptionObj_3[] exceptionObj_3;
            ExceptionObj_4[] exceptionObj_4;
            ExceptionObj_5[] exceptionObj_5;
            ExceptionObj_6[] exceptionObj_6;
            ExceptionObj_7[] exceptionObj_7;
            ExceptionObj_8[] exceptionObj_8;
            ExceptionObj_9[] exceptionObj_9;
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
            string[] playerExist = ObjectCoordinate.LoadPlayer();
            #endregion

            // 타이틀 재생
            string[] lines = DrewStage.LoadStage((int)currentScene);
            for (int i = 0; i < lines.Length; ++i)
            {
                Console.WriteLine(lines[i]);
            }
            
            ConsoleKey key = Console.ReadKey().Key;
            
            // 올바른 키 입력하지 않을 시 진행 불가
            // 추가 안내 메시지 삽입 필요
            while (key != ConsoleKey.D)
            {
                Console.Clear();
            
                for (int i = 0; i < lines.Length; ++i)
                {
                    Console.WriteLine(lines[i]);
                }
            
                key = Console.ReadKey().Key;
            }
             
            Console.Clear();
             
            //// 프롤로그 진입
            //string[] victimPicture = Game.LoadMessage((int)Message.VictimPicture);
            //for (int i = 0; i < victimPicture.Length; ++i)
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine(victimPicture[i]);
            //}
            //
            //// 메시지 재생
            //string[] prologue = Game.LoadMessage((int)Message.Prologue);
            //for (int i = 0; i < prologue.Length; ++i)
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine(prologue[i]);
            //
            //    Thread.Sleep(5000);
            //}
            
            Console.Clear();

            // 씬 초기설정
            currentScene = Stage.Stage01;
            lines = DrewStage.LoadStage((int)currentScene);
            DrewStage.ParseStage(lines, out walls, out exceptionObj_1, out exceptionObj_2,
                out exceptionObj_3, out exceptionObj_4, out exceptionObj_5, out exceptionObj_6,
                out exceptionObj_7, out exceptionObj_8, out exceptionObj_9, out exceptionObj_0);
            ObjectCoordinate.ParsePlayer(playerExist, out player);
            string[] doorExist = DrewStage.LoadDividedroomDoor();
            DrewStage.ParseStageDoorID(doorExist, out bedroomDoor, out toiletDoor,
                out utilityroomDoor, out frontDoor);
            string[] livingroomDoorExist = DrewStage.LoadLivingroomDoorNumber();
            DrewStage.ParseLivingRoomDoorID(livingroomDoorExist, out firstLivingroomDoor,
                out secondLivingroomDoor, out thirdLivingroomDoor);
            string[] interactivedStage = DrewStage.LoadInteractiveStage((int)currentScene);
            DrewStage.ParseInteractiveStage(interactivedStage, out interactiveFieldA,
                out interactiveFieldB, out interactiveFieldC, out interactiveFieldD,
                out interactiveFieldE, out interactiveFieldF, out interactiveFieldG,
                out interactiveFieldH, out interactiveFieldI, out interactiveFieldJ,
                out interactiveFieldK, out interactiveFieldL);
            MadeMapMataData();
            MadeInteractionData();


            // 게임루프 돌입
            while (true)
            {
                if (captureScene != currentScene)
                {
                    lines = DrewStage.LoadStage((int)currentScene);
                    DrewStage.ParseStage(lines, out walls, out exceptionObj_1, out exceptionObj_2,
                out exceptionObj_3, out exceptionObj_4, out exceptionObj_5, out exceptionObj_6,
                out exceptionObj_7, out exceptionObj_8, out exceptionObj_9, out exceptionObj_0);

                    interactivedStage = DrewStage.LoadInteractiveStage((int)currentScene);
                    DrewStage.ParseInteractiveStage(interactivedStage, out interactiveFieldA,
                out interactiveFieldB, out interactiveFieldC, out interactiveFieldD,
                out interactiveFieldE, out interactiveFieldF, out interactiveFieldG,
                out interactiveFieldH, out interactiveFieldI, out interactiveFieldJ,
                out interactiveFieldK, out interactiveFieldL);
                    

                }

                answerCount = 0;
                captureScene = currentScene;

                Console.Clear();

                // 플레이어 과거 좌표 저장
                player.pastX = player.X;
                player.pastY = player.Y;

                MadeMapMataData();
                MadeInteractionData();


                Render();
                
                

                StageStatus();


                key = Console.ReadKey().Key;

                Player.MovePlayer(key, ref player.X, ref player.Y, player.X, player.Y);

                Interaction(player, player.X, player.Y, mapInteractionData);

                AfterUpdate(player, player.X, player.Y);

                


            }


            void Render()
            #region Render
            {
                // 스테이지 구성요소 렌더링
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < exceptionObj_1.Length; ++i)
                {
                    Game.ObjectRender(exceptionObj_1[i].X, exceptionObj_1[i].Y, "-");
                }
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < exceptionObj_2.Length; ++i)
                {
                    Game.ObjectRender(exceptionObj_2[i].X, exceptionObj_2[i].Y, "|");
                }
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < exceptionObj_3.Length; ++i)
                {
                    Game.ObjectRender(exceptionObj_3[i].X, exceptionObj_3[i].Y, "*");
                }
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < exceptionObj_4.Length; ++i)
                {
                    Game.ObjectRender(exceptionObj_4[i].X, exceptionObj_4[i].Y, "@");
                }
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < exceptionObj_5.Length; ++i)
                {
                    Game.ObjectRender(exceptionObj_5[i].X, exceptionObj_5[i].Y, "=");
                }
                Console.ForegroundColor = ConsoleColor.Red;
                for (int i = 0; i < exceptionObj_6.Length; ++i)
                {
                    Game.ObjectRender(exceptionObj_6[i].X, exceptionObj_6[i].Y, "&");
                }
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < exceptionObj_7.Length; ++i)
                {
                    Game.ObjectRender(exceptionObj_7[i].X, exceptionObj_7[i].Y, "+");
                }
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < exceptionObj_8.Length; ++i)
                {
                    Game.ObjectRender(exceptionObj_8[i].X, exceptionObj_8[i].Y, "/");
                }
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < exceptionObj_9.Length; ++i)
                {
                    Game.ObjectRender(exceptionObj_9[i].X, exceptionObj_9[i].Y, "\\");
                }
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < exceptionObj_0.Length; ++i)
                {
                    Game.ObjectRender(exceptionObj_0[i].X, exceptionObj_0[i].Y, "_");
                }

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                for (int i = 0; i < walls.Length; ++i)
                {
                    Game.ObjectRender(walls[i].X, walls[i].Y, "#");
                }

                // 거실일 때 출력
                
                if (captureScene == Stage.Stage01)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Game.ObjectRender(MAP_OFFSET_X, 1, "거실");

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Game.ObjectRender(bedroomDoor[0].X, bedroomDoor[0].Y, "B");
                    Game.ObjectRender(bedroomDoor[1].X, bedroomDoor[1].Y, "B");

                    Game.ObjectRender(toiletDoor[0].X, toiletDoor[0].Y, "T");
                    Game.ObjectRender(toiletDoor[1].X, toiletDoor[1].Y, "T");

                    Game.ObjectRender(utilityroomDoor[0].X, utilityroomDoor[0].Y, "U");
                    Game.ObjectRender(utilityroomDoor[1].X, utilityroomDoor[1].Y, "U");

                    Game.ObjectRender(frontDoor[0].X, frontDoor[0].Y, "F");
                    Game.ObjectRender(frontDoor[1].X, frontDoor[1].Y, "F");
                }

                // 그 외 방일 때 출력
                
                if (captureScene == Stage.Stage02)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Game.ObjectRender(MAP_OFFSET_X, 1, "다용도실");

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Game.ObjectRender(firstLivingroomDoor[0].X, firstLivingroomDoor[0].Y, "L");
                    Game.ObjectRender(firstLivingroomDoor[1].X, firstLivingroomDoor[1].Y, "L");
                }
                if (captureScene == Stage.Stage03)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Game.ObjectRender(MAP_OFFSET_X, 1, "화장실");

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Game.ObjectRender(secondLivingroomDoor[0].X, secondLivingroomDoor[0].Y, "L");
                    Game.ObjectRender(secondLivingroomDoor[1].X, secondLivingroomDoor[1].Y, "L");
                }
                if (captureScene == Stage.Stage04)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Game.ObjectRender(MAP_OFFSET_X, 1, "침실");

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Game.ObjectRender(thirdLivingroomDoor[0].X, thirdLivingroomDoor[0].Y, "L");
                    Game.ObjectRender(thirdLivingroomDoor[1].X, thirdLivingroomDoor[1].Y, "L");
                }

                Console.ForegroundColor = ConsoleColor.White;
                if (player.IsOnInteraction)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Game.ObjectRender(player.X, player.Y, "P");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Game.ObjectRender(player.X, player.Y, "P");
                }

                if (answerOpportunity[0])
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Game.ObjectRender(MAP_MAX_X * 3 + 4, MAP_OFFSET_Y + 4,
                        addHintString[0]);
                }
                if (answerOpportunity[1])
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Game.ObjectRender(MAP_MAX_X * 3 + 4, MAP_OFFSET_Y + 5,
                        addHintString[1]);
                }

                if (alreadySearchHint[0] == true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Game.ObjectRender(MAP_MAX_X * 3 + 4, MAP_OFFSET_Y,
                        mixedHintStrings[0]);
                }
                if (alreadySearchHint[1] == true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Game.ObjectRender(MAP_MAX_X * 3 + 4, MAP_OFFSET_Y + 1,
                        mixedHintStrings[1]);
                }
                if (alreadySearchHint[2] == true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Game.ObjectRender(MAP_MAX_X * 3 + 4, MAP_OFFSET_Y + 2,
                        mixedHintStrings[2]);
                }
            }
            #endregion

            void AfterUpdate(Player player, int x, int y)
            #region AfterUpdate
            {
                MapIcon checkData = mapMetaData[x, y];

                switch (checkData)
                {
                    case MapIcon.Default:

                        break;

                    case MapIcon.Wall:
                        player.X = player.pastX;
                        player.Y = player.pastY;

                        break;

                    case MapIcon.Utility:
                        currentScene = Stage.Stage02;
                        player.X = firstLivingroomDoor[1].X + 1;
                        player.Y = firstLivingroomDoor[1].Y;
                        MapClear();
                        MapClear2();

                        break;

                    case MapIcon.Toilet:
                        currentScene = Stage.Stage03;
                        player.X = secondLivingroomDoor[0].X;
                        player.Y = secondLivingroomDoor[0].Y + 1;
                        MapClear();
                        MapClear2();

                        break;

                    case MapIcon.Bed:
                        currentScene = Stage.Stage04;
                        player.X = thirdLivingroomDoor[0].X - 1;
                        player.Y = thirdLivingroomDoor[0].Y;
                        MapClear();
                        MapClear2();

                        break;

                    case MapIcon.FrontDoor:
                        Console.Clear();
                        player.X = player.pastX;
                        player.Y = player.pastY;

                        string[] readResultMessage = Game.LoadMessage(2);
                        for (int i = 0; i < readResultMessage.Length; ++i)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine(readResultMessage[i]);
                        }

                        string readAnswer1 = Console.ReadLine();
                        string readAnswer2 = Console.ReadLine();
                        string readAnswer3 = Console.ReadLine();

                        if (readAnswer1 == corretAnswerMurderer[0])
                        {
                            ++answerCount;
                        }
                        if (readAnswer2 == corretAnswerWeapon[0])
                        {
                            ++answerCount;
                        }
                        if (readAnswer3 == corretAnswerMotive[0])
                        {
                            ++answerCount;
                        }
                        if (answerCount == 3)
                        {
                            Console.Clear();

                            Console.ForegroundColor = ConsoleColor.Cyan;
                            string[] endString = Game.LoadGoodEndMessage();
                            for (int i = 0; i < endString.Length; ++i)
                            {
                                Console.WriteLine(endString[i]);
                            }

                            key = Console.ReadKey().Key;
                            Environment.Exit(1);
                        }
                        else
                        {
                            answerOpportunity[opportunityCount] = true;
                            ++opportunityCount;

                            if (opportunityCount == 4)
                            {
                                Console.Clear();

                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                string[] endString = Game.LoadBadEndMessage();
                                for (int i = 0; i < endString.Length; ++i)
                                {
                                    Console.WriteLine(endString[i]);
                                }

                                key = Console.ReadKey().Key;
                                Environment.Exit(2);
                            }
                            else
                                Console.WriteLine("잘못된 추리입니다");
                            
                            key = Console.ReadKey().Key;
                        }

                        break;

                    case MapIcon.Living01:
                        currentScene = Stage.Stage01;
                        player.X = utilityroomDoor[1].X - 1;
                        player.Y = utilityroomDoor[1].Y;
                        MapClear();
                        MapClear2();

                        break;

                    case MapIcon.Living02:
                        currentScene = Stage.Stage01;
                        player.X = toiletDoor[0].X;
                        player.Y = toiletDoor[0].Y - 1;
                        MapClear();
                        MapClear2();

                        break;

                    case MapIcon.Living03:
                        currentScene = Stage.Stage01;
                        player.X = bedroomDoor[0].X + 1;
                        player.Y = bedroomDoor[0].Y;
                        MapClear();
                        MapClear2();

                        break;

                }
            }
            #endregion

            void MadeInteractionData()
            #region MadeInteractionData
            {
                if (currentScene == Stage.Stage01)
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
                
                if (currentScene == Stage.Stage02)
                {
                    for (int i = 0; i < interactiveFieldD.Length; ++i)
                    {
                        mapInteractionData[interactiveFieldD[i].X, interactiveFieldD[i].Y]
                            = InteractionObject.UtilityroomCloset;
                    }

                    for (int i = 0; i < interactiveFieldE.Length; ++i)
                    {
                        mapInteractionData[interactiveFieldE[i].X, interactiveFieldE[i].Y]
                            = InteractionObject.UtilityroomSecretroom;
                    }

                    for (int i = 0; i < interactiveFieldF.Length; ++i)
                    {
                        mapInteractionData[interactiveFieldF[i].X, interactiveFieldF[i].Y]
                            = InteractionObject.UtilityroomMirror;
                    }
                }

                if (currentScene == Stage.Stage03)
                {
                    for (int i = 0; i < interactiveFieldG.Length; ++i)
                    {
                        mapInteractionData[interactiveFieldG[i].X, interactiveFieldG[i].Y]
                            = InteractionObject.ToiletBathtub;
                    }

                    for (int i = 0; i < interactiveFieldH.Length; ++i)
                    {
                        mapInteractionData[interactiveFieldH[i].X, interactiveFieldH[i].Y]
                            = InteractionObject.ToiletSink;
                    }

                    for (int i = 0; i < interactiveFieldI.Length; ++i)
                    {
                        mapInteractionData[interactiveFieldI[i].X, interactiveFieldI[i].Y]
                            = InteractionObject.ToiletLavatory;
                    }
                }

                if (currentScene == Stage.Stage04)
                {
                    for (int i = 0; i < interactiveFieldJ.Length; ++i)
                    {
                        mapInteractionData[interactiveFieldJ[i].X, interactiveFieldJ[i].Y]
                            = InteractionObject.BedroomBookshelf;
                    }

                    for (int i = 0; i < interactiveFieldK.Length; ++i)
                    {
                        mapInteractionData[interactiveFieldK[i].X, interactiveFieldK[i].Y]
                            = InteractionObject.BedroomPillow;
                    }

                    for (int i = 0; i < interactiveFieldL.Length; ++i)
                    {
                        mapInteractionData[interactiveFieldL[i].X, interactiveFieldL[i].Y]
                            = InteractionObject.BedroomDesk;
                    }
                }
            }
            #endregion

            void MadeMapMataData()
            {
                mapMetaData[player.X, player.Y] = MapIcon.Player;

                for (int i = 0; i < walls.Length; ++i)
                {
                    mapMetaData[walls[i].X, walls[i].Y] = MapIcon.Wall;
                }

                if (currentScene == Stage.Stage01)
                {
                    mapMetaData[bedroomDoor[0].X, bedroomDoor[0].Y] = MapIcon.Bed;
                    mapMetaData[bedroomDoor[1].X, bedroomDoor[1].Y] = MapIcon.Bed;
                }
                
                if (currentScene == Stage.Stage01)
                {
                    mapMetaData[toiletDoor[0].X, toiletDoor[0].Y] = MapIcon.Toilet;
                    mapMetaData[toiletDoor[1].X, toiletDoor[1].Y] = MapIcon.Toilet;
                }
                
                if (currentScene == Stage.Stage01)
                {
                    mapMetaData[utilityroomDoor[0].X, utilityroomDoor[0].Y] = MapIcon.Utility;
                    mapMetaData[utilityroomDoor[1].X, utilityroomDoor[1].Y] = MapIcon.Utility;
                }
                
                if (currentScene == Stage.Stage01)
                {
                    mapMetaData[frontDoor[0].X, frontDoor[0].Y] = MapIcon.FrontDoor;
                    mapMetaData[frontDoor[1].X, frontDoor[1].Y] = MapIcon.FrontDoor;
                }

                if (currentScene == Stage.Stage02)
                {
                    mapMetaData[firstLivingroomDoor[0].X, firstLivingroomDoor[0].Y] = MapIcon.Living01;
                    mapMetaData[firstLivingroomDoor[1].X, firstLivingroomDoor[1].Y] = MapIcon.Living01;
                }

                if (currentScene == Stage.Stage03)
                {
                    mapMetaData[secondLivingroomDoor[0].X, secondLivingroomDoor[0].Y] = MapIcon.Living02;
                    mapMetaData[secondLivingroomDoor[1].X, secondLivingroomDoor[1].Y] = MapIcon.Living02;
                }

                if (currentScene == Stage.Stage04)
                {
                    mapMetaData[thirdLivingroomDoor[0].X, thirdLivingroomDoor[0].Y] = MapIcon.Living03;
                    mapMetaData[thirdLivingroomDoor[1].X, thirdLivingroomDoor[1].Y] = MapIcon.Living03;
                }
            }

            void MapClear()
            {
                mapMetaData[player.X, player.Y] = MapIcon.Default;

                for (int i = 0; i < walls.Length; ++i)
                {
                    mapMetaData[walls[i].X, walls[i].Y] = MapIcon.Default;
                }

                //for (int i = 0; i < interactiveFields.Length; ++i)
                //{
                //    mapMetaData[interactiveFields[i].X, interactiveFields[i].Y] = MapIcon.Default;
                //}

                if (mapMetaData[bedroomDoor[0].X, bedroomDoor[0].Y] == MapIcon.Bed)
                {
                    mapMetaData[bedroomDoor[0].X, bedroomDoor[0].Y] = MapIcon.Default;
                    mapMetaData[bedroomDoor[1].X, bedroomDoor[1].Y] = MapIcon.Default;
                }
                
                if (mapMetaData[toiletDoor[0].X, toiletDoor[0].Y] == MapIcon.Toilet)
                {
                    mapMetaData[toiletDoor[0].X, toiletDoor[0].Y] = MapIcon.Default;
                    mapMetaData[toiletDoor[1].X, toiletDoor[1].Y] = MapIcon.Default;
                }

                if (mapMetaData[utilityroomDoor[0].X, utilityroomDoor[0].Y] == MapIcon.Utility)
                {
                    mapMetaData[utilityroomDoor[0].X, utilityroomDoor[0].Y] = MapIcon.Default;
                    mapMetaData[utilityroomDoor[1].X, utilityroomDoor[1].Y] = MapIcon.Default;
                }

                if (mapMetaData[frontDoor[0].X, frontDoor[0].Y] == MapIcon.FrontDoor)
                {
                    mapMetaData[frontDoor[0].X, frontDoor[0].Y] = MapIcon.Default;
                    mapMetaData[frontDoor[1].X, frontDoor[1].Y] = MapIcon.Default;
                }

                if (mapMetaData[firstLivingroomDoor[0].X, firstLivingroomDoor[0].Y] == MapIcon.Living01)
                {
                    mapMetaData[firstLivingroomDoor[0].X, firstLivingroomDoor[0].Y] = MapIcon.Default;
                    mapMetaData[firstLivingroomDoor[1].X, firstLivingroomDoor[1].Y] = MapIcon.Default;
                }

            }

            void MapClear2()
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

                for (int i = 0; i < interactiveFieldD.Length; ++i)
                {
                    mapInteractionData[interactiveFieldD[i].X, interactiveFieldD[i].Y]
                        = InteractionObject.Default;
                }

                for (int i = 0; i < interactiveFieldE.Length; ++i)
                {
                    mapInteractionData[interactiveFieldE[i].X, interactiveFieldE[i].Y]
                        = InteractionObject.Default;
                }

                for (int i = 0; i < interactiveFieldF.Length; ++i)
                {
                    mapInteractionData[interactiveFieldF[i].X, interactiveFieldF[i].Y]
                        = InteractionObject.Default;
                }

                for (int i = 0; i < interactiveFieldG.Length; ++i)
                {
                    mapInteractionData[interactiveFieldG[i].X, interactiveFieldG[i].Y]
                        = InteractionObject.Default;
                }

                for (int i = 0; i < interactiveFieldH.Length; ++i)
                {
                    mapInteractionData[interactiveFieldH[i].X, interactiveFieldH[i].Y]
                        = InteractionObject.Default;
                }

                for (int i = 0; i < interactiveFieldI.Length; ++i)
                {
                    mapInteractionData[interactiveFieldI[i].X, interactiveFieldI[i].Y]
                        = InteractionObject.Default;
                }

                for (int i = 0; i < interactiveFieldJ.Length; ++i)
                {
                    mapInteractionData[interactiveFieldJ[i].X, interactiveFieldJ[i].Y]
                        = InteractionObject.Default;
                }

                for (int i = 0; i < interactiveFieldK.Length; ++i)
                {
                    mapInteractionData[interactiveFieldK[i].X, interactiveFieldK[i].Y]
                        = InteractionObject.Default;
                }

                for (int i = 0; i < interactiveFieldL.Length; ++i)
                {
                    mapInteractionData[interactiveFieldL[i].X, interactiveFieldL[i].Y]
                        = InteractionObject.Default;
                }
            }

            void StageStatus()
            {
                Console.ForegroundColor = ConsoleColor.White;
                Game.ObjectRender(MAP_OFFSET_X * 2 + MAP_MAX_X, MAP_OFFSET_Y, "용의자");
                Game.ObjectRender(MAP_OFFSET_X * 3 + MAP_MAX_X, MAP_OFFSET_Y, "살해도구");
                Game.ObjectRender(MAP_OFFSET_X * 4 + MAP_MAX_X + 1, MAP_OFFSET_Y, "살해동기");

                Console.ForegroundColor = ConsoleColor.Red;
                for (int murdererListPick = 0; murdererListPick < Game.CRIME_EVIDENCE; ++murdererListPick)
                {
                    Game.ObjectRender(MAP_OFFSET_X * 2 + MAP_MAX_X, MAP_OFFSET_Y + murdererListPick + 1,
                       Game.OutputTextToFirstLine(Game.LoadJob(murdererList[murdererListPick])));
                }

                Console.ForegroundColor = ConsoleColor.Green;
                for (int weaponListPick = 0; weaponListPick < Game.CRIME_EVIDENCE; ++weaponListPick)
                {
                    Game.ObjectRender(MAP_OFFSET_X * 3 + MAP_MAX_X, MAP_OFFSET_Y + weaponListPick + 1,
                       Game.OutputTextToFirstLine(Game.LoadWeapon(weaponList[weaponListPick])));
                }

                Console.ForegroundColor = ConsoleColor.Blue;
                for (int motiveListPick = 0; motiveListPick < Game.CRIME_EVIDENCE; ++motiveListPick)
                {
                    Game.ObjectRender(MAP_OFFSET_X * 4 + MAP_MAX_X + 1, MAP_OFFSET_Y + motiveListPick + 1,
                       Game.OutputTextToFirstLine(Game.LoadMotive(motiveList[motiveListPick])));
                }
            }

            void Interaction(Player player, int x, int y, InteractionObject[,] mapData)
            {
                InteractionObject checkData = mapData[x, y];
                string[] outputMessage = default;
                if (checkData == InteractionObject.Default)
                    player.IsOnInteraction = false;

                if (currentScene == Stage.Stage01)
                {
                    switch (checkData)
                    {
                        case InteractionObject.LivingroomTable:
                            AppearMessage(outputMessage, InteractionObject.LivingroomTable);
                            if (key == ConsoleKey.R)
                            {
                                if (checkHint[0] == true)
                                {
                                    FindHintString(0);
                                }
                            }

                            break;

                        case InteractionObject.LivingroomKitchen:
                            AppearMessage(outputMessage, InteractionObject.LivingroomKitchen);
                            if (key == ConsoleKey.R)
                            {
                                if (checkHint[1] == true)
                                {
                                    FindHintString(1);
                                }
                            }

                            break;

                        case InteractionObject.LivingroomCloset:
                            AppearMessage(outputMessage, InteractionObject.LivingroomCloset);
                            if (key == ConsoleKey.R)
                            {
                                if (checkHint[2] == true)
                                {
                                    FindHintString(2);
                                }
                            }

                            break;
                    }
                }

                if (currentScene == Stage.Stage02)
                {
                    switch (checkData)
                    {

                        case InteractionObject.UtilityroomCloset:
                            AppearMessage(outputMessage, InteractionObject.UtilityroomCloset);
                            if (key == ConsoleKey.R)
                            {
                                if (checkHint[3] == true)
                                {
                                    FindHintString(3);
                                }
                            }

                            break;

                        case InteractionObject.UtilityroomSecretroom:
                            AppearMessage(outputMessage, InteractionObject.UtilityroomSecretroom);
                            if (key == ConsoleKey.R)
                            {
                                if (checkHint[4] == true)
                                {
                                    FindHintString(4);
                                }
                            }

                            break;

                        case InteractionObject.UtilityroomMirror:
                            AppearMessage(outputMessage, InteractionObject.UtilityroomMirror);
                            if (key == ConsoleKey.R)
                            {
                                if (checkHint[5] == true)
                                {
                                    FindHintString(5);
                                }
                            }

                            break;
                    }
                }

                if (currentScene == Stage.Stage03)
                {
                    switch (checkData)
                    {
                        case InteractionObject.ToiletBathtub:
                            AppearMessage(outputMessage, InteractionObject.ToiletBathtub);
                            if (key == ConsoleKey.R)
                            {
                                if (checkHint[6] == true)
                                {
                                    FindHintString(6);
                                }
                            }

                            break;

                        case InteractionObject.ToiletSink:
                            AppearMessage(outputMessage, InteractionObject.ToiletSink);
                            if (key == ConsoleKey.R)
                            {
                                if (checkHint[7] == true)
                                {
                                    FindHintString(7);
                                }
                            }

                            break;

                        case InteractionObject.ToiletLavatory:
                            AppearMessage(outputMessage, InteractionObject.ToiletLavatory);
                            if (key == ConsoleKey.R)
                            {
                                if (checkHint[8] == true)
                                {
                                    FindHintString(8);
                                }
                            }

                            break;
                    }
                }

                if (currentScene == Stage.Stage04)
                {
                    switch (checkData)
                    {
                        case InteractionObject.BedroomBookshelf:
                            AppearMessage(outputMessage, InteractionObject.BedroomBookshelf);
                            if (key == ConsoleKey.R)
                            {
                                if (checkHint[9] == true)
                                {
                                    FindHintString(9);
                                }
                            }

                            break;

                        case InteractionObject.BedroomPillow:
                            AppearMessage(outputMessage, InteractionObject.BedroomPillow);
                            if (key == ConsoleKey.R)
                            {
                                if (checkHint[10] == true)
                                {
                                    FindHintString(10);
                                }
                            }

                            break;

                        case InteractionObject.BedroomDesk:
                            AppearMessage(outputMessage, InteractionObject.BedroomDesk);
                            {
                                if (checkHint[11] == true)
                                {
                                    FindHintString(11);
                                }
                            }

                            break;
                    }
                }
                    
                
            }

            void AppearMessage(string[] str, InteractionObject someCoordinate)
            {
                str = Player.LoadMessage((int)someCoordinate);
                player.IsOnInteraction = true;
                if (key == ConsoleKey.R && player.IsOnInteraction == true)
                {
                    Console.SetCursorPosition(MAP_OFFSET_X, MAP_OFFSET_Y * 2 + MAP_MAX_Y - 1);
                    Console.WriteLine(str[0]);
                    Console.SetCursorPosition(MAP_OFFSET_X, MAP_OFFSET_Y * 2 + MAP_MAX_Y);
                    Console.WriteLine(str[1]);

                    key = Console.ReadKey().Key;
                }
                
            }
        }
    }
}