using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.Objects
{
    public class VillageChief
    {
        public int X;
        public int Y;
        public bool BeginnerSupport;
        public static class Function
        {
            public static void Render(VillageChief villageChief)
            {
                Game.Function.ObjRender(villageChief.X, villageChief.Y, "C", ConsoleColor.Black);
            }
            public static void GetBeginnerSupport(Player player)
            {
                player.MaxHP += 90;
                player.CurrentHP += 90;
                player.ATK += 9;
                player.DEF += 9;
                player.Money += 100;
            }
        }
    }
}
