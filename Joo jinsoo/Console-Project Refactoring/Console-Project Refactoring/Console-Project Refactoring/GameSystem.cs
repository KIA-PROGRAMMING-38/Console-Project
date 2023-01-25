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
        public const char exceptionObj_01 = '-';
        public const char exceptionObj_02 = '|';
        public const char exceptionObj_03 = '*';
        public const char exceptionObj_04 = '@';
        public const char exceptionObj_05 = '=';
        public const char exceptionObj_06 = '&';
        public const char exceptionObj_07 = '+';
        public const char exceptionObj_08 = '/';
        public const char exceptionObj_09 = '\\';
        public const char exceptionObj_10 = '_';

        public const char utilityroomDoor = 'U';
        public const char bedroomDoor = 'B';
        public const char toiletDoor = 'T';
        public const char frontDoor = 'F';

        public const char firstLivingroomDoor = '1';
        public const char secondLivingroomDoor = '2';
        public const char thirdLivingroomDoor = '3';

        public const char interactiveA = 'a'; // 1
        public const char interactiveB = 'b'; // 2
        public const char interactiveC = 'c'; // 3
        public const char interactiveD = 'd'; // 4
        public const char interactiveE = 'e'; // 5
        public const char interactiveF = 'f'; // 6
        public const char interactiveG = 'g'; // 7
        public const char interactiveH = 'h'; // 8
        public const char interactiveI = 'i'; // 9
        public const char interactiveJ = 'j'; // 10 
        public const char interactiveK = 'k'; // 11
        public const char interactiveL = 'l'; // 12
    }
    static class GameSystem
    {
        public const int MAP_OFFSET_X = 10;
        public const int MAP_OFFSET_Y = 3;
        public const int MAP_MAX_X = 24;
        public const int MAP_MAX_Y = 12;

        public static MapIcon[,] mapMetaData = new MapIcon[(MAP_OFFSET_X * 2) + MAP_MAX_X,
            (MAP_OFFSET_Y * 2) + MAP_MAX_Y];
        public static InteractionObject[,] mapInteractionData = new InteractionObject[(MAP_OFFSET_X * 2) + MAP_MAX_X,
            (MAP_OFFSET_Y * 2) + MAP_MAX_Y];

        public static void Render(Stage currentScene, Player player, Wall[] walls, Utilityroom[] utilityroomDoor,
            Toilet[] toiletDoor, Bedroom[] bedroomDoor, Frontdoor[] frontDoor,
            LivingroomDoor_First[] firstLRDoor, LivingroomDoor_Second[] secondLRDoor,
            LivingroomDoor_Third[] thirdLRDoor)
        {
            

            if (currentScene == Stage.Livingroom)
            {
                Console.ForegroundColor = ConsoleColor.White;
                ObjectRender(MAP_OFFSET_X, 1, "거실");

                
                string[] stage = StageFormat.LoadStageFormat((int)Stage.Livingroom);
                for (int i = 2; i < stage.Length; ++i)
                {
                    Console.WriteLine(stage[i]);
                }

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

                string[] stage = StageFormat.LoadStageFormat((int)Stage.Utilityroom);
                for (int i = 2; i < stage.Length; ++i)
                {
                    Console.WriteLine(stage[i]);
                }

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                ObjectRender(firstLRDoor[0].X, firstLRDoor[0].Y, "L");
                ObjectRender(firstLRDoor[1].X, firstLRDoor[1].Y, "L");
            }
            if (currentScene == Stage.Toilet)
            {
                Console.ForegroundColor = ConsoleColor.White;
                ObjectRender(MAP_OFFSET_X, 1, "화장실");

                string[] stage = StageFormat.LoadStageFormat((int)Stage.Toilet);
                for (int i = 2; i < stage.Length; ++i)
                {
                    Console.WriteLine(stage[i]);
                }

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                ObjectRender(secondLRDoor[0].X, secondLRDoor[0].Y, "L");
                ObjectRender(secondLRDoor[1].X, secondLRDoor[1].Y, "L");
            }
            if (currentScene == Stage.Bedroom)
            {
                Console.ForegroundColor = ConsoleColor.White;
                ObjectRender(MAP_OFFSET_X, 1, "침실");

                string[] stage = StageFormat.LoadStageFormat((int)Stage.Bedroom);
                for (int i = 2; i < stage.Length; ++i)
                {
                    Console.WriteLine(stage[i]);
                }

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
                Interactive_B[] interactiveFieldB, Interactive_C[] interactiveFieldC,
                Interactive_D[] interactiveFieldD, Interactive_E[] interactiveFieldE,
                Interactive_F[] interactiveFieldF, Interactive_G[] interactiveFieldG,
                Interactive_H[] interactiveFieldH, Interactive_I[] interactiveFieldI,
                Interactive_J[] interactiveFieldJ, Interactive_K[] interactiveFieldK,
                Interactive_L[] interactiveFieldL)
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

            if (currentScene == Stage.Toilet)
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

            if (currentScene == Stage.Bedroom)
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
    }
}
