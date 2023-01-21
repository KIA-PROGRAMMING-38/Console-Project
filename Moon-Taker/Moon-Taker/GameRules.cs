using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon_Taker
{
    internal class GameRules
    {
        public static bool isGameStarted = false;
        public static int stageNum = 0;
        public static int stageSettingNum;

        public static Enum.Direction playerMoveDirection;
        public static bool enemyAlive = true;
    }
}
