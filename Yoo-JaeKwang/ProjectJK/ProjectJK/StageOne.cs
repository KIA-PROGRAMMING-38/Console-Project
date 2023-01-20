using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK
{
    internal class StageOne
    {





        public static class Function
        {
            public static void OutRender()
            {

            }

            public static void InRender(Player player)
            {
                Player.Function.Render(player);
            }

            public static void Update(ConsoleKey key, Player player)
            {
                Player.Function.Move(key, player);
            }





        }

    }
}
