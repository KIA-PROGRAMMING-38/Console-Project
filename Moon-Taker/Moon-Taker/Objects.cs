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
        public ConsoleColor Color = ConsoleColor.White;
    }
    public class Wall
    {
        public int X;
        public int Y;
        public ConsoleColor Color = ConsoleColor.Gray;
    }
    public class Enemy
    {
        public int X;
        public int Y;
        public bool IsAlive;
        public ConsoleColor Color = ConsoleColor.Blue;
    }
    public class Block
    {
        public int X;
        public int Y;
        public ConsoleColor Color = ConsoleColor.Cyan;
    }
    public class Trap
    {
        public int X;
        public int Y;
        public ConsoleColor Color = ConsoleColor.Red;
    }
    public class Moon
    {
        public int X;
        public int Y;
        public ConsoleColor Color = Functions.RandomColor();
    }
    public class Key
    {
        public int X;
        public int Y;
        public ConsoleColor Color = ConsoleColor.Yellow;
    }
    public class Door
    {
        public int X;
        public int Y;
        public ConsoleColor Color = ConsoleColor.DarkYellow;
    }
    public class MapSize
    {
        public int X;
        public int Y;
    }
    public class Advice
    {
        public string name;
        public string advice;
        public int weight;
    }
}
