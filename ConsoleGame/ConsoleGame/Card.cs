using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    enum CardType
    {
        Mino,
        Choi,
        YJK,
        Galaxy,
        Eom,
        HSJ,
        Doik,
        MAX
    }

    internal class Card
    {
        private CardType _type;
        private int _weight;

        public CardType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public int Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                _weight = value;
            }
        }
    }
}
