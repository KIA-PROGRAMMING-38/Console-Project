using Console_ReProject.Object;
using Console_ReProject.Object.Door;
using Console_ReProject.Object.MapExceptionObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_ReProject.StageRender
{
    enum Stage
    {
        Title,
        Stage01,
        Stage02, // Utilityroom
        Stage03, // Toilet
        Stage04  // Bedroom
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


    class DrewStage
    {
        #region 상호작용 스테이지 저장
        public static string[] LoadInteractiveStage(int stageNumber)
        {
            string stageFilePath = Path.Combine("Assets", "Stage", $"InteractivedStage{stageNumber:D2}.txt");

            if (false == File.Exists(stageFilePath))
            {
                Console.WriteLine($"상호작용 스테이지 파일이 없습니다. 스테이지 번호{stageNumber}.txt");
            }

            return File.ReadAllLines(stageFilePath);
        }

        public static void ParseInteractiveStage(string[] stage, out Interactive_A[] interactiveFieldA,
           out Interactive_B[] interactiveFieldB, out Interactive_C[] interactiveFieldC,
           out Interactive_D[] interactiveFieldD, out Interactive_E[] interactiveFieldE,
           out Interactive_F[] interactiveFieldF, out Interactive_G[] interactiveFieldG,
           out Interactive_H[] interactiveFieldH, out Interactive_I[] interactiveFieldI,
           out Interactive_J[] interactiveFieldJ, out Interactive_K[] interactiveFieldK,
           out Interactive_L[] interactiveFieldL)
        {
            // 첫 줄에서 메타데이터를 파싱한다
            string[] stageMetadata = stage[0].Split(" ");
            interactiveFieldA = new Interactive_A[int.Parse(stageMetadata[0])];
            interactiveFieldB = new Interactive_B[int.Parse(stageMetadata[1])];
            interactiveFieldC = new Interactive_C[int.Parse(stageMetadata[2])];
            interactiveFieldD = new Interactive_D[int.Parse(stageMetadata[3])];
            interactiveFieldE = new Interactive_E[int.Parse(stageMetadata[4])];
            interactiveFieldF = new Interactive_F[int.Parse(stageMetadata[5])];
            interactiveFieldG = new Interactive_G[int.Parse(stageMetadata[6])];
            interactiveFieldH = new Interactive_H[int.Parse(stageMetadata[7])];
            interactiveFieldI = new Interactive_I[int.Parse(stageMetadata[8])];
            interactiveFieldJ = new Interactive_J[int.Parse(stageMetadata[9])];
            interactiveFieldK = new Interactive_K[int.Parse(stageMetadata[10])];
            interactiveFieldL = new Interactive_L[int.Parse(stageMetadata[11])];

            int interactiveFieldAIndex = 0;
            int interactiveFieldBIndex = 0;
            int interactiveFieldCIndex = 0;
            int interactiveFieldDIndex = 0;
            int interactiveFieldEIndex = 0;
            int interactiveFieldFIndex = 0;
            int interactiveFieldGIndex = 0;
            int interactiveFieldHIndex = 0;
            int interactiveFieldIIndex = 0;
            int interactiveFieldJIndex = 0;
            int interactiveFieldKIndex = 0;
            int interactiveFieldLIndex = 0;

            for (int y = 1; y < stage.Length; ++y)
            {
                for (int x = 0; x < stage[y].Length; ++x)
                {
                    switch (stage[y][x])
                    {
                        case ObjectSymbol.interactiveA:
                            interactiveFieldA[interactiveFieldAIndex] = new Interactive_A { X = x, Y = y - 1 };
                            ++interactiveFieldAIndex;

                            break;

                        case ObjectSymbol.interactiveB:
                            interactiveFieldB[interactiveFieldBIndex] = new Interactive_B { X = x, Y = y - 1 };
                            ++interactiveFieldBIndex;

                            break;
                        case ObjectSymbol.interactiveC:
                            interactiveFieldC[interactiveFieldCIndex] = new Interactive_C { X = x, Y = y - 1 };
                            ++interactiveFieldCIndex;

                            break;
                        case ObjectSymbol.interactiveD:
                            interactiveFieldD[interactiveFieldDIndex] = new Interactive_D { X = x, Y = y - 1 };
                            ++interactiveFieldDIndex;

                            break;
                        case ObjectSymbol.interactiveE:
                            interactiveFieldE[interactiveFieldEIndex] = new Interactive_E { X = x, Y = y - 1 };
                            ++interactiveFieldEIndex;

                            break;
                        case ObjectSymbol.interactiveF:
                            interactiveFieldF[interactiveFieldFIndex] = new Interactive_F { X = x, Y = y - 1 };
                            ++interactiveFieldFIndex;

                            break;
                        case ObjectSymbol.interactiveG:
                            interactiveFieldG[interactiveFieldGIndex] = new Interactive_G { X = x, Y = y - 1 };
                            ++interactiveFieldGIndex;

                            break;
                        case ObjectSymbol.interactiveH:
                            interactiveFieldH[interactiveFieldHIndex] = new Interactive_H { X = x, Y = y - 1 };
                            ++interactiveFieldHIndex;

                            break;
                        case ObjectSymbol.interactiveI:
                            interactiveFieldI[interactiveFieldIIndex] = new Interactive_I { X = x, Y = y - 1 };
                            ++interactiveFieldIIndex;

                            break;
                        case ObjectSymbol.interactiveJ:
                            interactiveFieldJ[interactiveFieldJIndex] = new Interactive_J { X = x, Y = y - 1 };
                            ++interactiveFieldJIndex;

                            break;
                        case ObjectSymbol.interactiveK:
                            interactiveFieldK[interactiveFieldKIndex] = new Interactive_K { X = x, Y = y - 1 };
                            ++interactiveFieldKIndex;

                            break;
                        case ObjectSymbol.interactiveL:
                            interactiveFieldL[interactiveFieldLIndex] = new Interactive_L { X = x, Y = y - 1 };
                            ++interactiveFieldLIndex;

                            break;


                        case ' ':

                            break;
                    }

                }
            }
        }
        #endregion

        #region 스테이지 오브젝트 저장
        public static string[] LoadStage(int stageNumber)
        {
            string stageFilePath = Path.Combine("Assets", "Stage", $"Stage{stageNumber:D2}.txt");

            if (false == File.Exists(stageFilePath))
            {
                Console.WriteLine($"스테이지 파일이 없습니다. 스테이지 번호{stageNumber}.txt");
            }

            return File.ReadAllLines(stageFilePath);
        }

        public static void ParseStage(string[] stage, out Wall[] walls,
            out ExceptionObj_1[] exceptionObj_1, out ExceptionObj_2[] exceptionObj_2,
            out ExceptionObj_3[] exceptionObj_3, out ExceptionObj_4[] exceptionObj_4,
            out ExceptionObj_5[] exceptionObj_5, out ExceptionObj_6[] exceptionObj_6,
            out ExceptionObj_7[] exceptionObj_7, out ExceptionObj_8[] exceptionObj_8,
            out ExceptionObj_9[] exceptionObj_9, out ExceptionObj_0[] exceptionObj_10)
        {
            // 첫 줄에서 메타데이터를 파싱한다
            string[] stageMetadata = stage[0].Split(" ");
            walls = new Wall[int.Parse(stageMetadata[0])];
            exceptionObj_1 = new ExceptionObj_1[int.Parse(stageMetadata[1])]; // [1] = -
            exceptionObj_2 = new ExceptionObj_2[int.Parse(stageMetadata[2])]; // [2] = |
            exceptionObj_3 = new ExceptionObj_3[int.Parse(stageMetadata[3])]; // [3] = *
            exceptionObj_4 = new ExceptionObj_4[int.Parse(stageMetadata[4])]; // [4] = @
            exceptionObj_5 = new ExceptionObj_5[int.Parse(stageMetadata[5])]; // [5] = =
            exceptionObj_6 = new ExceptionObj_6[int.Parse(stageMetadata[6])]; // [6] = &
            exceptionObj_7 = new ExceptionObj_7[int.Parse(stageMetadata[7])]; // [7] = +
            exceptionObj_8 = new ExceptionObj_8[int.Parse(stageMetadata[8])]; // [8] = /
            exceptionObj_9 = new ExceptionObj_9[int.Parse(stageMetadata[9])]; // [9] = \
            exceptionObj_10 = new ExceptionObj_0[int.Parse(stageMetadata[10])]; // [0] = _
            
            int wallIndex = 0;
            int exceptionObj_10_Index = 0;
            int exceptionObj_1_Index = 0;
            int exceptionObj_2_Index = 0;
            int exceptionObj_3_Index = 0;
            int exceptionObj_4_Index = 0;
            int exceptionObj_5_Index = 0;
            int exceptionObj_6_Index = 0;
            int exceptionObj_7_Index = 0;
            int exceptionObj_8_Index = 0;
            int exceptionObj_9_Index = 0;

            for (int y = 1; y < stage.Length; ++y)
            {
                for (int x = 0; x < stage[y].Length; ++x)
                {
                    switch (stage[y][x])
                    {
                        case ObjectSymbol.wall:
                            walls[wallIndex] = new Wall { X = x, Y = y - 1 };
                            ++wallIndex;

                            break;

                        case ObjectSymbol.exceptionObj_1:
                            exceptionObj_1[exceptionObj_1_Index] = new ExceptionObj_1 { X = x, Y = y - 1 };
                            ++exceptionObj_1_Index;

                            break;

                        case ObjectSymbol.exceptionObj_2:
                            exceptionObj_2[exceptionObj_2_Index] = new ExceptionObj_2 { X = x, Y = y - 1 };
                            ++exceptionObj_2_Index;

                            break;

                        case ObjectSymbol.exceptionObj_3:
                            exceptionObj_3[exceptionObj_3_Index] = new ExceptionObj_3 { X = x, Y = y - 1 };
                            ++exceptionObj_3_Index;

                            break;

                        case ObjectSymbol.exceptionObj_4:
                            exceptionObj_4[exceptionObj_4_Index] = new ExceptionObj_4 { X = x, Y = y - 1 };
                            ++exceptionObj_4_Index;

                            break;

                        case ObjectSymbol.exceptionObj_5:
                            exceptionObj_5[exceptionObj_5_Index] = new ExceptionObj_5 { X = x, Y = y - 1 };
                            ++exceptionObj_5_Index;

                            break;

                        case ObjectSymbol.exceptionObj_6:
                            exceptionObj_6[exceptionObj_6_Index] = new ExceptionObj_6 { X = x, Y = y - 1 };
                            ++exceptionObj_6_Index;

                            break;

                        case ObjectSymbol.exceptionObj_7:
                            exceptionObj_7[exceptionObj_7_Index] = new ExceptionObj_7 { X = x, Y = y - 1 };
                            ++exceptionObj_7_Index;

                            break;

                        case ObjectSymbol.exceptionObj_8:
                            exceptionObj_8[exceptionObj_8_Index] = new ExceptionObj_8 { X = x, Y = y - 1 };
                            ++exceptionObj_8_Index;

                            break;

                        case ObjectSymbol.exceptionObj_9:
                            exceptionObj_9[exceptionObj_9_Index] = new ExceptionObj_9 { X = x, Y = y - 1 };
                            ++exceptionObj_9_Index;

                            break;

                        case ObjectSymbol.exceptionObj_0:
                            exceptionObj_10[exceptionObj_10_Index] = new ExceptionObj_0 { X = x, Y = y - 1 };
                            ++exceptionObj_10_Index;

                            break;

                        case ' ':

                            break;
                    }

                }
            }
        }
        #endregion

        #region 거실에 존재하는 문 좌표 저장
        public static string[] LoadDividedroomDoor()
        {
            string stageFilePath = Path.Combine("Assets", "Coordinate", "DividedroomCoordinate.txt");

            if (false == File.Exists(stageFilePath))
            {
                Console.WriteLine($"파일이 없습니다. DividedroomCoordinate.txt");
            }

            return File.ReadAllLines(stageFilePath);
        }

        public static void ParseStageDoorID(string[] stage, out Bedroom[] bedroomDoor, out Toilet[] toiletDoor,
    out Utilityroom[] utilityroomDoor, out Frontdoor[] frontDoor)
        {
            // 첫 줄에서 메타데이터를 파싱한다
            string[] stageMetadata = stage[0].Split(" ");
            bedroomDoor = new Bedroom[int.Parse(stageMetadata[0])];
            toiletDoor = new Toilet[int.Parse(stageMetadata[0])];
            utilityroomDoor = new Utilityroom[int.Parse(stageMetadata[0])];
            frontDoor = new Frontdoor[int.Parse(stageMetadata[0])];

            int bedroomDoorIndex = 0;
            int toiletDoorIndex = 0;
            int utilityroomDoorIndex = 0;
            int frontDoorIndex = 0;

            for (int y = 1; y < stage.Length; ++y)
            {
                for (int x = 0; x < stage[y].Length; ++x)
                {
                    switch (stage[y][x])
                    {
                        case ObjectSymbol.bedroom:
                            bedroomDoor[bedroomDoorIndex] = new Bedroom { X = x, Y = y - 1 };
                            ++bedroomDoorIndex;

                            break;

                        case ObjectSymbol.toilet:
                            toiletDoor[toiletDoorIndex] = new Toilet { X = x, Y = y - 1 };
                            ++toiletDoorIndex;

                            break;

                        case ObjectSymbol.utilityroom:
                            utilityroomDoor[utilityroomDoorIndex] = new Utilityroom { X = x, Y = y - 1 };
                            ++utilityroomDoorIndex;

                            break;

                        case ObjectSymbol.frontDoor:
                            frontDoor[frontDoorIndex] = new Frontdoor { X = x, Y = y - 1 };
                            ++frontDoorIndex;

                            break;

                        case ' ':

                            break;
                    }

                }
            }
        }
        #endregion

        #region 거실로 향하는 문 정보 저장
        public static string[] LoadLivingroomDoorNumber()
        {
            string stageFilePath = Path.Combine("Assets", "Coordinate", "LivingroomCoordinate.txt");

            if (false == File.Exists(stageFilePath))
            {
                Console.WriteLine($"파일이 없습니다. LivingroomCoordinate.txt");
            }

            return File.ReadAllLines(stageFilePath);
        }

        public static void ParseLivingRoomDoorID(string[] playerExist,
           out FirstLivingroomDoor[] firstLivingroomDoor, out SecondLivingroomDoor[] secondLivingroomDoor,
           out ThirdLivingroomDoor[] thirdLivingroomDoor)
        {

            string[] stageMetadata = playerExist[0].Split(" ");
            firstLivingroomDoor = new FirstLivingroomDoor[int.Parse(playerExist[0])];
            secondLivingroomDoor = new SecondLivingroomDoor[int.Parse(playerExist[0])];
            thirdLivingroomDoor = new ThirdLivingroomDoor[int.Parse(playerExist[0])];

            int firstLivingroomDoorIndex = 0;
            int secondLivingroomDoorIndex = 0;
            int thirdLivingroomDoorIndex = 0;

            for (int y = 1; y < playerExist.Length; ++y)
            {
                for (int x = 0; x < playerExist[y].Length; ++x)
                {
                    switch (playerExist[y][x])
                    {
                        case 'F':
                            firstLivingroomDoor[firstLivingroomDoorIndex] = new FirstLivingroomDoor { X = x, Y = y - 1 };
                            ++firstLivingroomDoorIndex;

                            break;

                        case 'S':
                            secondLivingroomDoor[secondLivingroomDoorIndex] = new SecondLivingroomDoor { X = x, Y = y - 1 };
                            ++secondLivingroomDoorIndex;

                            break;

                        case 'T':
                            thirdLivingroomDoor[thirdLivingroomDoorIndex] = new ThirdLivingroomDoor { X = x, Y = y - 1 };
                            ++thirdLivingroomDoorIndex;

                            break;

                        case ' ':

                            break;
                    }
                }
            }
        }
        #endregion
    }
}