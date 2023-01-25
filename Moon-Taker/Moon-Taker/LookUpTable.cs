using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon_Taker
{
    public class LookUpTable
    {
        static public ConsoleColor[] allColor =
        {
            ConsoleColor.DarkRed,
            ConsoleColor.DarkBlue,
            ConsoleColor.DarkGreen,
            ConsoleColor.DarkCyan,
            ConsoleColor.DarkGray,
            ConsoleColor.DarkMagenta,
            ConsoleColor.Red,
            ConsoleColor.Blue,
            ConsoleColor.Cyan,
            ConsoleColor.Green,
            ConsoleColor.Gray,
            ConsoleColor.Magenta,
            ConsoleColor.DarkYellow,
            ConsoleColor.Yellow,
            ConsoleColor.White,
        };

        static public string[] controlHelp =
        {
            "방향키로 움직이세요!",
            "A키를 눌러 동기들의 응원을 확인하세요!",
            "막혔을 땐 R키로 재시작해봐요!",
        };

        static public string[] objectDescription =
        {
            $"{Constants.player} : 유재광   ",
            $"{Constants.wall} : 벽",
            $"{Constants.enemy} : 적       ",
            $"{Constants.block} : 블록",
            $"{Constants.activatedTrap} : 가시함정 ",
            $"{Constants.key} : 열쇠     ",
            $"{Constants.door} : 문       ",
            $"{Constants.moon} : 교수님의 흔적",
        };

        static public ConsoleColor[] objectColor =
        {
            Constants.playerColor,
            Constants.wallColor,
            Constants.enemyColor,
            Constants.blockColor,
            Constants.trapColor,
            Constants.keyColor,
            Constants.doorColor,
            ObjectStatus.moonColor,
        };
    }
}
