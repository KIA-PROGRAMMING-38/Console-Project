using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon_Taker
{
    internal class StageSettings

    {
        public static bool isGameStarted = false;
        public static bool isStageReseted = true;
        public static int currentStage = 1;
        public static int stageNumber = 3;

        public static int[] stageMovePoint = { 0, 23, 18, 33 };
        public static bool[] isStageKeyNull = { true, true, true, false};
        public static bool isKeyNull = true;
        public static bool isBlessed = true;
    }
}
