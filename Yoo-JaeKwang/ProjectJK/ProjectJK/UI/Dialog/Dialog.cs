using ProjectJK.UI.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.UI.Dialog
{
    public class Dialog
    {
        public int X;
        public int Y;
        public static void Render(Dialog1[] dialog1, Dialog2[] dialog2, Dialog3[] dialog3, Dialog4[] dialog4, Dialog5[] dialog5, Dialog6[] dialog6, Dialog7 dialog7, Dialog8 dialog8, Dialog9 dialog9, Dialog10 dialog10)
        {
            Dialog1.Function.Render(dialog1);
            Dialog2.Function.Render(dialog2);
            Dialog3.Function.Render(dialog3);
            Dialog4.Function.Render(dialog4);
            Dialog5.Function.Render(dialog5);
            Dialog6.Function.Render(dialog6);
            Dialog7.Function.Render(dialog7);
            Dialog8.Function.Render(dialog8);
            Dialog9.Function.Render(dialog9);
            Dialog10.Function.Render(dialog10);
        }
    }
}