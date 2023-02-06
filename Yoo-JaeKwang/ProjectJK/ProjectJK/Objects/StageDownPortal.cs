using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.Objects
{
    public class StageDownPortal
    {
        public int X;
        public int Y;
        public static void Render(StageDownPortal stageDownPortal)
        {
            Game.ObjRender(stageDownPortal.X, stageDownPortal.Y, "℧", ConsoleColor.Black);
        }
    }
}