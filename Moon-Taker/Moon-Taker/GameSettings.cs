using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Moon_Taker
{
    internal class GameSettings
    {
        public static bool isGameStarted = false;
        public static int stageNumber = Functions.CheckStageNumber();

        public static string[] stageFilePath = new string[stageNumber + 1];
        public static string adviceFilePath = @"Assets\Advice\Advice.txt";
    }
}
