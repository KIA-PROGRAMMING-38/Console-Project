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

        public static void ParseStage(string[] stage, out Wall[] walls)
        {
            string[] stageMetaData = stage[0].Split(" ");
            walls = new Wall[int.Parse(stageMetaData[0])];

            int wallIndex = 0;

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
