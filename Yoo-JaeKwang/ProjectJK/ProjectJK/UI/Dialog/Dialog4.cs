using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.UI.Dialog
{
    public class Dialog4
    {
        public int X;
        public int Y;
        public static class Function
        {
            public static void Render(Dialog4[] dialog4)
            {
                for (int i = 0; i < dialog4.Length; ++i)
                {
                    Game.Function.ObjRender(dialog4[i].X, dialog4[i].Y, "┓", ConsoleColor.Black);
                }
            }
        }
    }
}
