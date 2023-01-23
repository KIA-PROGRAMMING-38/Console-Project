using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.UI.Dialog
{
    public class Dialog7
    {
        public int X;
        public int Y;
        public static class Function
        {
            public static void Render(Dialog7 dialog7)
            {
                Game.Function.ObjRender(dialog7.X, dialog7.Y, "┳", ConsoleColor.Black);
            }
        }
    }
}
