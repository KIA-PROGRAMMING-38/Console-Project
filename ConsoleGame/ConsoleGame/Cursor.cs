using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    internal class Cursor
    {
        private int _x;
        private int _y;
        private int _prevX;
        private int _prevY;

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

        public int PrevX
        {
            get
            {
                return _prevX;
            }
            set
            {
                _prevX = value;
            }
        }

        public int PrevY
        {
            get
            {
                return _prevY;
            }
            set
            {
                _prevY = value;
            }
        }
    }
}
