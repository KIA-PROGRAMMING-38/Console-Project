using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoloProject
{
    internal class Bullet
    {
        public int X;
        public int Y;
        public int Damage;
       


        public void BulletMove(int X, int Y, string Icon)
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(Icon);

        }

        public void BulletEffect(int X, int Y, string Icon)
        {
            Console.ForegroundColor = ConsoleColor.White;
            BulletMove(X, Y, Icon);
            Console.ForegroundColor = ConsoleColor.Blue;

        }





    }
}

