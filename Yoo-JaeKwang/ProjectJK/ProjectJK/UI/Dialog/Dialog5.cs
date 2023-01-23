using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.UI.Dialog
{
    public class Dialog5
    {
        public int X;
        public int Y;
        public static class Function
        {
            public static void Render(Dialog5[] dialog5)
            {
                for (int i = 0; i < dialog5.Length; ++i)
                {
                    Game.Function.ObjRender(dialog5[i].X, dialog5[i].Y, "┗", ConsoleColor.Black);
                }
            }
        }
    }
}
