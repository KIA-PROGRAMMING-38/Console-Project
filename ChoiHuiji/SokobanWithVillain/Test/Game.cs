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
            out Box[] boxes, out Wall[] walls, out Goal[] goals, out Villain villain)
        {
            Debug.Assert(stage != null);

            // stage에 배열 10개가 들어있다고 했을때 마지막 인덱스를 불러와야하니 0,1,2...9
            // 즉 10개 - 1 이 마지막 인덱스.
            string[] stageMetadata = stage[stage.Length - 1].Split(" ");
            player = null;
            villain = null;
            walls = new Wall[100];
            boxes = new Box[int.Parse(stageMetadata[1])];
            goals = new Goal[int.Parse(stageMetadata[2])];

            //wall의 개수를 세기 위한 변수
            int wallIndex = 0;
            int boxIndex = 0;
            int goalIndex = 0;

            // y는 stage.Length - 1 전에 끝난다는것을 잊지말자
            for (int y = 0; y < stage.Length - 1; ++y)
            {
                for(int x = 0; x < stage[y].Length; ++x)
                {
                    switch (stage[y][x])
                    {
                        case '☻':
                            player = new Player { X = x, Y = y};

                            break;
                        case '⎕':
                            walls[wallIndex] = new Wall { X = x, Y = y};
                            ++wallIndex;

                            break;
                        case '✩':
                            boxes[boxIndex] = new Box { X = x, Y = y};
                            ++boxIndex;

                            break;
                        case '✪':
                            goals[goalIndex] = new Goal { X = x, Y = y};
                            ++goalIndex;

                            break;
                        case '✦':
                            villain = new Villain { X = x, Y = y };

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

