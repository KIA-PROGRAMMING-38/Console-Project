using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.UI.Dialog
{
    public class Dialog1
    {
        public int X;
        public int Y;
        public static class Function
        {
            public static void Render(Dialog1[] dialog1)
            {
                for (int i = 0; i < dialog1.Length; ++i)
                {
                    Game.Function.ObjRender(dialog1[i].X, dialog1[i].Y, "┃", ConsoleColor.Black);
                }
            }
        }
    }
}
