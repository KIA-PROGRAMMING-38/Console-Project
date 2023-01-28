using System;
using System.Diagnostics;
namespace Sokoban_Huiji
{
	public class Game
	{
        //Load 함수
        public static string[] LoadStage(int stageNumber)
        {
            //경로 구성하기
            string stageFilePath = Path.Combine("Assets", "Stage", $"Stage{stageNumber:D2}.tt");            

            //파일 있는지 확인하기
            if(false == File.Exists(stageFilePath))
            {
                Console.WriteLine($"스테이지 파일이 없습니다. 스테이지 번호 : {stageNumber}");
            }

            return File.ReadAllLines(stageFilePath);
        }

        //Parse 함수
        public static void ParseStage(string[] stage, out Player player,
            out Box[] boxes, out Wall[] walls, out Goal[] goals)
        {
            Debug.Assert(stage != null);
            string[] stageMetadata = stage[0].Split(" ");
            player = null;
            walls = new Wall[int.Parse(stageMetadata[0])];
            boxes = new Box[int.Parse(stageMetadata[1])];
            goals = new Goal[int.Parse(stageMetadata[2])];

            //wall의 개수를 세기 위한 변수
            int wallIndex = 0;
            int boxIndex = 0;
            int goalIndex = 0;

            for (int y = 1; y < stage.Length; ++y)
            {
                for(int x = 0; x < stage[y].Length; ++x)
                {
                    switch (stage[y][x])
                    {
                        case '☻':
                            player = new Player { X = x, Y = y - 1};

                            break;
                        case '⎕':
                            walls[wallIndex] = new Wall { X = x, Y = y - 1};
                            ++wallIndex;

                            break;
                        case '✩':
                            boxes[boxIndex] = new Box { X = x, Y = y - 1};
                            ++boxIndex;

                            break;
                        case '✪':
                            goals[goalIndex] = new Goal { X = x, Y = y - 1};
                            ++goalIndex;

                            break;
                        case ' ':

                            break;
                        default:
                            Console.WriteLine("스테이지 파일이 잘못되었습니다.");

                            break;

                    }
                }
            }
        }
        
    }
}

