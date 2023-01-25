using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.Objects
{
    public class VillageMaxHPMerchant
    {
        public int X;
        public int Y;
        public static void Render(VillageMaxHPMerchant villageMaxHPMerchant)
        {
            Game.Function.ObjRender(villageMaxHPMerchant.X, villageMaxHPMerchant.Y, "2", ConsoleColor.Black);
        }
        public static void Buy(Player player)
        {
            if (player.Money >= 50)
            {
                player.Money -= 50;
                player.MaxHP += 10;
                if (player.MaxHP > 999)
                {
                    player.MaxHP = 999;
                }
            }
        }
    }
}