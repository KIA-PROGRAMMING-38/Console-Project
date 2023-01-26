using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.Objects
{
    public class StageUpPortal
    {
        public int X;
        public int Y;
        public static class Function
        {
            public static void Render(StageUpPortal stageUpPortal)
            {
                Game.Function.ObjRender(stageUpPortal.X, stageUpPortal.Y, "Ω", ConsoleColor.Black);
            }
            
        }
    }
}
