using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.UI.Dialog
{
    public class Dialog10
    {
        public int X;
        public int Y;
        public static class Function
        {
            public static void Render(Dialog10 dialog10)
            {
                Game.Function.ObjRender(dialog10.X, dialog10.Y, "┻", ConsoleColor.Black);
            }
        }
    }
}
