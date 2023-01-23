using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.UI.Dialog
{
    public class Dialog3
    {
        public int X;
        public int Y;
        public static class Function
        {
            public static void Render(Dialog3[] dialog3)
            {
                for (int i = 0; i < dialog3.Length; ++i)
                {
                    Game.Function.ObjRender(dialog3[i].X, dialog3[i].Y, "┏", ConsoleColor.Black);
                }
            }
        }

    }
}
