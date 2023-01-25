using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.Objects
{
    public class VillageRecoveringMerchant
    {
        public int X;
        public int Y;
        public static void Render(VillageRecoveringMerchant villageRecoveringMerchant)
        {
            Game.Function.ObjRender(villageRecoveringMerchant.X, villageRecoveringMerchant.Y, "1", ConsoleColor.Black);
        }
        public static void Buy(Player player)
        {
            if (player.Money >= 10 && player.CurrentHP != player.MaxHP)
            {
                player.Money -= 10;
                player.CurrentHP += 100;
                if (player.CurrentHP > player.MaxHP)
                {
                    player.CurrentHP = player.MaxHP;
                }
            }
        }
    }
}
