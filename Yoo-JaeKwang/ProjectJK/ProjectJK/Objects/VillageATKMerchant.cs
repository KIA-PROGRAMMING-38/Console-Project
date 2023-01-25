using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.Objects
{
    public class VillageATKMerchant
    {
        public int X;
        public int Y;
        public static void Render(VillageATKMerchant villageATKMerchant)
        {
            Game.Function.ObjRender(villageATKMerchant.X, villageATKMerchant.Y, "3", ConsoleColor.Black);
        }
        public static void Buy(Player player)
        {
            if (player.Money >= 50)
            {
                player.Money -= 50;
                player.ATK += 1;
                if (player.ATK > 999)
                {
                    player.ATK = 999;
                }
            }
        }
    }
}