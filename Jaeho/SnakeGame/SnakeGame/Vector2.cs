﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public struct Vector2
    {
        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X;
        public int Y;

        public static bool operator ==(Vector2 a, Vector2 b) => (a.X == b.X && a.Y == b.Y);
        public static bool operator !=(Vector2 a, Vector2 b) => !(a==b);
    }
}
