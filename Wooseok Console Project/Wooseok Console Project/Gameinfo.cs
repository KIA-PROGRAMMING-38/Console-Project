using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wooseok_Console_Project
{
    internal class Gameinfo
    {
        public int min_x;
        public int min_y;
        public int max_x;
        public int max_y;
        public bool character_selection;
        public bool tetrismode;
        public bool collision;
        public bool battlemode;


        public Gameinfo()
        {
            min_x = 1;
            min_y = 1;
            max_x = 40;
            max_y = 20;
            character_selection = true;
            tetrismode = true;
            collision = true;
            battlemode = true;
        }

        public static string[] LoadStage(int stagenumber)
        {
            // 1. 경로를 정해준다
            string stagepath = Path.Combine("Assets", "Stages", $"Stage{stagenumber}.txt");

            // 2. 파일의 존재유무를 확인한다
            if (File.Exists(stagepath) == false)
            {
                Console.Clear();
                Console.WriteLine("파일이 존재하지 않습니다...");
            }

            // 3. 파일의 내용을 불러온다
            return File.ReadAllLines(stagepath);
        }


        //public static void ParseStage(string[] stage, out Player player, out Player line4, out Box box, out Goal goal )
        //{




        //    for ()
        //    {
        //        for ()
        //        {
        //            switch ()
        //            {
        //                case :
        //                    break;
        //                case :
        //                    break;
        //                case :
        //                    break;
        //                case :
        //                    break;
        //                case :
        //                    break;


        //            }
        //        }
        //    }
        //}



    }
}
