using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Project_Refactoring.Assets.StageData
{
    class Utilityroom
    {
        public int X;
        public int Y;
    }

    class Toilet
    {
        public int X;
        public int Y;
    }

    class Bedroom
    {
        public int X;
        public int Y;
    }

    class Frontdoor
    {
        public int X;
        public int Y;
    }

    class LivingroomDoor_First
    {
        public int X;
        public int Y;
    }

    class LivingroomDoor_Second
    {
        public int X;
        public int Y;
    }

    class LivingroomDoor_Third
    {
        public int X;
        public int Y;
    }

    class Interactive_A
    {
        public int X;
        public int Y;
    }

    class Interactive_B
    {
        public int X;
        public int Y;
    }

    class Interactive_C
    {
        public int X;
        public int Y;
    }

    class Interactive_D
    {
        public int X;
        public int Y;
    }

    class Interactive_E
    {
        public int X;
        public int Y;
    }

    class Interactive_F
    {
        public int X;
        public int Y;
    }

    class Interactive_G
    {
        public int X;
        public int Y;
    }

    class Interactive_H
    {
        public int X;
        public int Y;
    }

    class Interactive_I
    {
        public int X;
        public int Y;
    }

    class Interactive_J
    {
        public int X;
        public int Y;
    }

    class Interactive_K
    {
        public int X;
        public int Y;
    }

    class Interactive_L
    {
        public int X;
        public int Y;
    }





    class InteractedObject
    {
        public static string[] LoadDividedroomDoor()
        {
            string stageFilePath = Path.Combine("..\\..\\..\\Assets", "StageData", $"DividedRoomCoordinate.txt");

            if (false == File.Exists(stageFilePath))
            {
                Console.WriteLine($"스테이지 상호작용 파일이 없습니다. DividedRoomCoordinate.txt");
            }

            return File.ReadAllLines(stageFilePath);
        }

        public static void ParseStageDoorID(string[] stage, out Bedroom[] bedroomDoor,
            out Toilet[] toiletDoor, out Utilityroom[] utilityroomDoor,
            out Frontdoor[] frontDoor)
        {
            string[] stageMetaData = stage[0].Split(" ");
            utilityroomDoor = new Utilityroom[int.Parse(stageMetaData[0])];
            toiletDoor = new Toilet[int.Parse(stageMetaData[0])];
            bedroomDoor = new Bedroom[int.Parse(stageMetaData[0])];
            frontDoor = new Frontdoor[int.Parse(stageMetaData[0])];

            int utilityroomDoorIndex = 0;
            int toiletDoorIndex = 0;
            int bedroomDoorIndex = 0;
            int frontDoorIndex = 0;

            for (int y = 1; y < stage.Length; ++y)
            {
                for (int x = 0; x < stage[y].Length; ++x)
                {
                    switch (stage[y][x])
                    {
                        case MapSymbol.utilityroomDoor :
                            utilityroomDoor[utilityroomDoorIndex] = new Utilityroom { X = x, Y = y - 1 };
                            ++utilityroomDoorIndex;

                            break;

                        case MapSymbol.toiletDoor :
                            toiletDoor[toiletDoorIndex] = new Toilet { X = x, Y = y - 1 };
                            ++toiletDoorIndex;

                            break;

                        case MapSymbol.bedroomDoor :
                            bedroomDoor[bedroomDoorIndex] = new Bedroom { X = x, Y = y - 1 };
                            ++bedroomDoorIndex;

                            break;

                        case MapSymbol.frontDoor :
                            frontDoor[frontDoorIndex] = new Frontdoor { X = x, Y = y - 1 };
                            ++frontDoorIndex;

                            break;
                    }
                }
            }
        }

        public static string[] LoadDLivingroomDoor()
        {
            string stageFilePath = Path.Combine("..\\..\\..\\Assets", "StageData", $"LivingroomCoordinate.txt");

            if (false == File.Exists(stageFilePath))
            {
                Console.WriteLine($"스테이지 상호작용 파일이 없습니다. LivingroomCoordinate.txt");
            }

            return File.ReadAllLines(stageFilePath);
        }

        public static void ParseLivingroomDoorID(string[] stage, out LivingroomDoor_First[] firstLRDoor,
            out LivingroomDoor_Second[] secondLRDoor, out LivingroomDoor_Third[] thirdLRDoor)
        {
            string[] stageMetaData = stage[0].Split(" ");
            firstLRDoor = new LivingroomDoor_First[int.Parse(stage[0])];
            secondLRDoor = new LivingroomDoor_Second[int.Parse(stage[0])];
            thirdLRDoor = new LivingroomDoor_Third[int.Parse(stage[0])];

            int firstLRDoorIndex = 0;
            int secondLRDoorIndex = 0;
            int thirdLRDoorIndex = 0;

            for (int y = 1; y < stage.Length; ++y)
            {
                for (int x = 0; x < stage[y].Length; ++x)
                {
                    switch (stage[y][x])
                    {
                        case MapSymbol.firstLivingroomDoor :
                            firstLRDoor[firstLRDoorIndex] = new LivingroomDoor_First { X = x, Y = y - 1 };
                            ++firstLRDoorIndex;

                            break;

                        case MapSymbol.secondLivingroomDoor :
                            secondLRDoor[secondLRDoorIndex] = new LivingroomDoor_Second { X = x, Y = y - 1 };
                            ++secondLRDoorIndex;

                            break;

                        case MapSymbol.thirdLivingroomDoor :
                            thirdLRDoor[thirdLRDoorIndex] = new LivingroomDoor_Third { X = x, Y = y - 1 };
                            ++thirdLRDoorIndex;

                            break;
                    }
                }
            }
        }

        public static string[] LoadInteractionStage(int stageNumber)
        {
            string stageFilePath = Path.Combine("..\\..\\..\\Assets", "StageData", $"InteractOnStage{stageNumber:D2}.txt");

            if (false == File.Exists(stageFilePath))
            {
                Console.WriteLine($"상호작용 스테이지 파일이 없습니다. 스테이지 번호{stageNumber}.txt");
            }

            return File.ReadAllLines(stageFilePath);
        }

        public static void ParseInteractionID(string[] stage, out Interactive_A[] interactiveFieldA,
    out Interactive_B[] interactiveFieldB, out Interactive_C[] interactiveFieldC,
    out Interactive_D[] interactiveFieldD, out Interactive_E[] interactiveFieldE,
    out Interactive_F[] interactiveFieldF, out Interactive_G[] interactiveFieldG,
    out Interactive_H[] interactiveFieldH, out Interactive_I[] interactiveFieldI,
    out Interactive_J[] interactiveFieldJ, out Interactive_K[] interactiveFieldK,
    out Interactive_L[] interactiveFieldL)
        {
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
                        case MapSymbol.interactiveA:
                            interactiveFieldA[interactiveFieldAIndex] = new Interactive_A { X = x, Y = y - 1 };
                            ++interactiveFieldAIndex;

                            break;

                        case MapSymbol.interactiveB:
                            interactiveFieldB[interactiveFieldBIndex] = new Interactive_B { X = x, Y = y - 1 };
                            ++interactiveFieldBIndex;

                            break;
                        case MapSymbol.interactiveC:
                            interactiveFieldC[interactiveFieldCIndex] = new Interactive_C { X = x, Y = y - 1 };
                            ++interactiveFieldCIndex;

                            break;
                        case MapSymbol.interactiveD:
                            interactiveFieldD[interactiveFieldDIndex] = new Interactive_D { X = x, Y = y - 1 };
                            ++interactiveFieldDIndex;

                            break;
                        case MapSymbol.interactiveE:
                            interactiveFieldE[interactiveFieldEIndex] = new Interactive_E { X = x, Y = y - 1 };
                            ++interactiveFieldEIndex;

                            break;
                        case MapSymbol.interactiveF:
                            interactiveFieldF[interactiveFieldFIndex] = new Interactive_F { X = x, Y = y - 1 };
                            ++interactiveFieldFIndex;

                            break;
                        case MapSymbol.interactiveG:
                            interactiveFieldG[interactiveFieldGIndex] = new Interactive_G { X = x, Y = y - 1 };
                            ++interactiveFieldGIndex;

                            break;
                        case MapSymbol.interactiveH:
                            interactiveFieldH[interactiveFieldHIndex] = new Interactive_H { X = x, Y = y - 1 };
                            ++interactiveFieldHIndex;

                            break;
                        case MapSymbol.interactiveI:
                            interactiveFieldI[interactiveFieldIIndex] = new Interactive_I { X = x, Y = y - 1 };
                            ++interactiveFieldIIndex;

                            break;
                        case MapSymbol.interactiveJ:
                            interactiveFieldJ[interactiveFieldJIndex] = new Interactive_J { X = x, Y = y - 1 };
                            ++interactiveFieldJIndex;

                            break;
                        case MapSymbol.interactiveK:
                            interactiveFieldK[interactiveFieldKIndex] = new Interactive_K { X = x, Y = y - 1 };
                            ++interactiveFieldKIndex;

                            break;
                        case MapSymbol.interactiveL:
                            interactiveFieldL[interactiveFieldLIndex] = new Interactive_L { X = x, Y = y - 1 };
                            ++interactiveFieldLIndex;

                            break;

                    }
                }
            }
        }

        
    }
}
