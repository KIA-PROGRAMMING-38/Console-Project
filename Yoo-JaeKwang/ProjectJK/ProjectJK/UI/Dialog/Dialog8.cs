using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.UI.Dialog
{
    public class Dialog8
    {
        public int X;
        public int Y;
        public static class Function
        {
            public static void Render(Dialog8 dialog8)
            {
                Game.Function.ObjRender(dialog8.X, dialog8.Y, "┣", ConsoleColor.Black);
            }
        }
    }
}
