using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.UI.Dialog
{
    public class Dialog9
    {
        public int X;
        public int Y;
        public static class Function
        {
            public static void Render(Dialog9 dialog9)
            {
                Game.Function.ObjRender(dialog9.X, dialog9.Y, "┫", ConsoleColor.Black);
            }
        }
    }
}
