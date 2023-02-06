using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.Objects
{
    public class Wall
    {
        public int X;
        public int Y;
        public static void Render(Wall[] walls)
        {
            for (int i = 0; i < walls.Length; ++i)
            {
                Game.ObjRender(walls[i].X, walls[i].Y, "#", ConsoleColor.DarkMagenta);
            }
        }
    }
}