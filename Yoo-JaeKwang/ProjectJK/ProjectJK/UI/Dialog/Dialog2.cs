using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.UI.Dialog
{
    public class Dialog2
    {
        public int X;
        public int Y;
        public static class Function
        {
            public static void Render(Dialog2[] dialog2)
            {
                for (int i = 0; i < dialog2.Length; ++i)
                {
                    Game.Function.ObjRender(dialog2[i].X, dialog2[i].Y, "━", ConsoleColor.Black);
                }
            }
        }
    }
}
