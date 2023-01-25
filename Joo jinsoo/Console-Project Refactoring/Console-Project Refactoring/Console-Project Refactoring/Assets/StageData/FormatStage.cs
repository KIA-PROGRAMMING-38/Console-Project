using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Project_Refactoring.Assets.StageData
{
    

    class StageFormat
    {
        public static string[] LoadStageFormat(int stageNumber)
        {
            string stageFilePath = Path.Combine("..\\..\\..\\Assets", "StageData", $"FormatStage{stageNumber:D2}.txt");

            if (false == File.Exists(stageFilePath))
            {
                Console.WriteLine($"스테이지 포맷 파일이 없습니다. 스테이지 번호{stageNumber}.txt");
            }

            return File.ReadAllLines(stageFilePath);
        }

        public static void ParseStage(string[] stage, out Wall[] walls,
            out ExceptionObj_01[] exceptionObj_01, out ExceptionObj_02[] exceptionObj_02,
            out ExceptionObj_03[] exceptionObj_03, out ExceptionObj_04[] exceptionObj_04,
            out ExceptionObj_05[] exceptionObj_05, out ExceptionObj_06[] exceptionObj_06,
            out ExceptionObj_07[] exceptionObj_07, out ExceptionObj_08[] exceptionObj_08,
            out ExceptionObj_09[] exceptionObj_09, out ExceptionObj_10[] exceptionObj_10)
        {
            string[] stageMetaData = stage[0].Split(" ");
            walls = new Wall[int.Parse(stageMetaData[0])];
            exceptionObj_01 = new ExceptionObj_01[int.Parse(stageMetaData[1])];
            exceptionObj_02 = new ExceptionObj_02[int.Parse(stageMetaData[2])];
            exceptionObj_03 = new ExceptionObj_03[int.Parse(stageMetaData[3])];
            exceptionObj_04 = new ExceptionObj_04[int.Parse(stageMetaData[4])];
            exceptionObj_05 = new ExceptionObj_05[int.Parse(stageMetaData[5])];
            exceptionObj_06 = new ExceptionObj_06[int.Parse(stageMetaData[6])];
            exceptionObj_07 = new ExceptionObj_07[int.Parse(stageMetaData[7])];
            exceptionObj_08 = new ExceptionObj_08[int.Parse(stageMetaData[8])];
            exceptionObj_09 = new ExceptionObj_09[int.Parse(stageMetaData[9])];
            exceptionObj_10 = new ExceptionObj_10[int.Parse(stageMetaData[10])];

            int wallIndex = 0;
            int exceptionObj_01_Index = 0;
            int exceptionObj_02_Index = 0;
            int exceptionObj_03_Index = 0;
            int exceptionObj_04_Index = 0;
            int exceptionObj_05_Index = 0;
            int exceptionObj_06_Index = 0;
            int exceptionObj_07_Index = 0;
            int exceptionObj_08_Index = 0;
            int exceptionObj_09_Index = 0;
            int exceptionObj_10_Index = 0;

            for (int y = 1; y < stage.Length; ++y)
            {
                for (int x = 0; x < stage[y].Length; ++x)
                {
                    switch (stage[y][x]) 
                    {
                        case MapSymbol.wall:
                            walls[wallIndex] = new Wall { X = x, Y = y - 1 };
                            ++wallIndex;

                            break;

                        case MapSymbol.exceptionObj_01:
                            exceptionObj_01[exceptionObj_01_Index] = new ExceptionObj_01 { X = x, Y = y - 1 };
                            ++exceptionObj_01_Index;

                            break;

                        case MapSymbol.exceptionObj_02:
                            exceptionObj_02[exceptionObj_02_Index] = new ExceptionObj_02 { X = x, Y = y - 1 };
                            ++exceptionObj_02_Index;

                            break;

                        case MapSymbol.exceptionObj_03:
                            exceptionObj_03[exceptionObj_03_Index] = new ExceptionObj_03 { X = x, Y = y - 1 };
                            ++exceptionObj_03_Index;

                            break;

                        case MapSymbol.exceptionObj_04:
                            exceptionObj_04[exceptionObj_04_Index] = new ExceptionObj_04 { X = x, Y = y - 1 };
                            ++exceptionObj_04_Index;

                            break;

                        case MapSymbol.exceptionObj_05:
                            exceptionObj_05[exceptionObj_05_Index] = new ExceptionObj_05 { X = x, Y = y - 1 };
                            ++exceptionObj_05_Index;

                            break;

                        case MapSymbol.exceptionObj_06:
                            exceptionObj_06[exceptionObj_06_Index] = new ExceptionObj_06 { X = x, Y = y - 1 };
                            ++exceptionObj_06_Index;

                            break;

                        case MapSymbol.exceptionObj_07:
                            exceptionObj_07[exceptionObj_07_Index] = new ExceptionObj_07 { X = x, Y = y - 1 };
                            ++exceptionObj_07_Index;

                            break;

                        case MapSymbol.exceptionObj_08:
                            exceptionObj_08[exceptionObj_08_Index] = new ExceptionObj_08 { X = x, Y = y - 1 };
                            ++exceptionObj_08_Index;

                            break;

                        case MapSymbol.exceptionObj_09:
                            exceptionObj_09[exceptionObj_09_Index] = new ExceptionObj_09 { X = x, Y = y - 1 };
                            ++exceptionObj_09_Index;

                            break;

                        case MapSymbol.exceptionObj_10:
                            exceptionObj_10[exceptionObj_10_Index] = new ExceptionObj_10 { X = x, Y = y - 1 };
                            ++exceptionObj_10_Index;

                            break;

                    }
                }
            }
        }
        
    }

    class Wall
    {
        public int X;
        public int Y;
    }

    class ExceptionObj_01
    {
        public int X;
        public int Y;
    }

    class ExceptionObj_02
    {
        public int X;
        public int Y;
    }

    class ExceptionObj_03
    {
        public int X;
        public int Y;
    }

    class ExceptionObj_04
    {
        public int X;
        public int Y;
    }

    class ExceptionObj_05
    {
        public int X;
        public int Y;
    }

    class ExceptionObj_06
    {
        public int X;
        public int Y;
    }

    class ExceptionObj_07
    {
        public int X;
        public int Y;
    }

    class ExceptionObj_08
    {
        public int X;
        public int Y;
    }

    class ExceptionObj_09
    {
        public int X;
        public int Y;
    }

    class ExceptionObj_10
    {
        public int X;
        public int Y;
    }
}
