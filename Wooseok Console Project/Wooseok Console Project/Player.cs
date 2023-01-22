using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wooseok_Console_Project
{
    internal class Player
    {
        public int x;
        public int y;

        public string symbol;

        public int prex;
        public int prey;

        public Player()
        {
            x = 2;
            y = 2;
            symbol = "P";
            prex = 2;
            prey = 2;
        }


        public class Soldier : Player
        {

        }



    }
}
