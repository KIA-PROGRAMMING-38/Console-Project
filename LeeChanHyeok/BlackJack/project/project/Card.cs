using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    class Card
    {
        public string[] value = new string[3];
        public int cardIndex = 0;
    }
    class CardInfo
    {
        public string[] pattern = { "♠", "◆", "♥", "♣" };
        public string[] number = { "", "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    }
}
