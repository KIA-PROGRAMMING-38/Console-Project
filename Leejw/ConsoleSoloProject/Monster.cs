using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleSoloProject
{
    internal class Monster
    {
        public int X = 13;
        public int Y = 3;
        public int Hp = 5;
        public int count = 0;
        public int deathCount = 0;
        public int unit = 15;
        public string Icon = "o";



        public void MonsterMove()
        {
            if (X == 13 && Y <= 9 && Y != 3)
            {
                Console.SetCursorPosition(X, Y--);
                Console.Write(Icon);
            }
            else if (Y == 9 && X <= 42)
            {
                Console.SetCursorPosition(X--, Y);
                Console.Write(Icon);
            }
            else if (X == 42)
            {
                Console.SetCursorPosition(X, Y++);
                Console.Write(Icon);

            }
            else if (X <= 42 && Y == 3)
            {
                Console.SetCursorPosition(X++, Y);
                Console.Write(Icon);

            }

        }
    }

}
