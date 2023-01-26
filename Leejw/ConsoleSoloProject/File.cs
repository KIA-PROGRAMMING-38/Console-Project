
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Numerics;


namespace ConsoleSoloProject
{
    class File
    {

        public static string[] LoadStage()
        {
            //1. 경로를 구성한다.
            string stageFilePath = Path.Combine("Assets", "Stage", "MapStage.txt");

            if (false == System.IO.File.Exists(stageFilePath))
            {
                Console.WriteLine("스테이지없다");
            }

            return System.IO.File.ReadAllLines(stageFilePath);


        }

      

    }
}


