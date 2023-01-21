using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Moon_Taker
{
    public class Player
    {
        public int X;
        public int Y;
    }
    public class Wall
    {
        public int X;
        public int Y;
    }
    public class Enemy
    {
        public int X;
        public int Y;
        public bool IsAlive;
    }
    public class Block
    {
        public int X;
        public int Y;
    }
    public class Moon
    {
        public int X;
        public int Y;
    }
    public class Key
    {
        public int X;
        public int Y;
    }
    public class Door
    {
        public int X;
        public int Y;
    }
    public class MapSize
    {
        public int X;
        public int Y;
    }
}
