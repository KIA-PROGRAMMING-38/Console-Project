using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.Objects
{
    public class VillageDEFMerchant
    {
        public int X;
        public int Y;
        public static void Render(VillageDEFMerchant villageDEFMerchant)
        {
            Game.Function.ObjRender(villageDEFMerchant.X, villageDEFMerchant.Y, "4", ConsoleColor.Black);
        }
        public static void Buy(Player player)
        {
            if (player.Money >= 50)
            {
                player.Money -= 50;
                player.DEF += 1;
                if (player.DEF > 999)
                {
                    player.DEF = 999;
                }
            }
        }
    }
}