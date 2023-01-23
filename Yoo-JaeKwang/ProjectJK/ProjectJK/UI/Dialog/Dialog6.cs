using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.UI.Dialog
{
    public class Dialog6
    {
        public int X;
        public int Y;
        public static class Function
        {
            public static void Render(Dialog6[] dialog6)
            {
                for (int i = 0; i < dialog6.Length; ++i)
                {
                    Game.Function.ObjRender(dialog6[i].X, dialog6[i].Y, "┛", ConsoleColor.Black);
                }
            }
        }
    }
}
