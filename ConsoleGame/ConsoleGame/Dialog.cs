using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    internal class Dialog
    {
        private int _x;
        private int _y;
        private char _shape;
        

        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public char Shape
        {
            get
            {
                return _shape;
            }
            set
            {
                _shape = value;
            }
        }

    }
}
