using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    internal class Horse
    {
        private int _x;
        private int _y;
        private int _rank = 1;
        private int _horseSpeed;
        private int _id;
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

        public int Rank
        {
            get
            {
                return _rank;
            }
            set
            {
                _rank = value;
            }
        }

        public int HorseSpeed
        {
            get
            {
                return _horseSpeed;
            }
            set
            {
                _horseSpeed = value;
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
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
