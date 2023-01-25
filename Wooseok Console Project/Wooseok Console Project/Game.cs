using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wooseok_Console_Project
{
    internal class Game
    {
        public int minx = 1;
        public int miny = 1;
        public int maxx = 55;
        public int maxy = 18;

        public bool Stage1Ongoing = true;
        public bool Stage2Ongoing = false;    
        public bool Stage3Ongoing = false;
        public bool Stage4Ongoing = false;

        public static string[] LoadStage(int stagenumber)
        {
            // 길 뚫어주기
            string stagepath = Path.Combine("Assets", "Stages", $"Stage{stagenumber}.txt");

            // 확신해버리기
            if (File.Exists(stagepath) == false)
            {
                Console.Clear();
                Console.WriteLine("잘못된 형식의 파일이거나 경로가 이상하다 이놈아");
            }

            // 파일 내용 불러오기
            return File.ReadAllLines(stagepath);

        }

        public static void RenderStage(string[] path)
        {
            for (int i = 1; i < path.Length ;++i)
            {
                Console.WriteLine(path[i]);
            }
        }




        public static void ParseStage(string[] stage, out Player Player, out Wall[] wall, out Goal[] goal, out Box[] box, out Line4[] line4 )
        {
            string[] counts = stage[0].Split(" "); // 0번째 행에 오브젝트의 숫자를 써놨다
            Player = null;
            
            wall = new Wall[int.Parse(counts[0])];
            goal = new Goal[int.Parse(counts[1])];
            box = new Box[int.Parse(counts[2])];
            line4 = new Line4[int.Parse(counts[3])];

            int WallIndex = 0;
            int GoalIndex = 0;
            int BoxIndex = 0;
            int Line4Index = 0;


            for (int y = 1; y < stage.Length ;++y)
            {
                for (int x = 0; x < stage[y].Length ;++x)
                {
                    switch (stage[y][x])
                    {
                        case 'P':
                            Player = new Player {x = x, y = y-1 };
                            break;
                        case 'W':
                            wall[WallIndex] = new Wall { x = x, y = y - 1 };
                            WallIndex++;
                            break;
                        case '*':
                            goal[GoalIndex] = new Goal { x = x, y = y - 1 };
                            GoalIndex++;
                            break;
                        case 'B':
                            box[BoxIndex] = new Box { x = x, y = y - 1 };
                            ++BoxIndex;
                            break;
                        case '4':
                            line4[Line4Index] = new Line4 { x = x, y = y - 1 };
                            ++Line4Index;
                            break;
                        
                        case ' ':
                            // 아무것도 안함
                            break;

                    }
                }
            }


        }








    }
}
