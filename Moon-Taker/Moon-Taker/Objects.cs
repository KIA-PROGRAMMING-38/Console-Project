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
    public class PreviousPlayer
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
        public bool isOnTrap = false;
    }
    public class PreviousEnemy
    {
        public int X;
        public int Y;
    }
    public class Block
    {
        public int X;
        public int Y;
        public bool isOnTrap = false;
    }
    public class PreviousBlock
    {
        public int X;
        public int Y;
    }
    public class Trap
    {
        public int X;
        public int Y;
        public bool IsActivated = true;
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
    public class Advice
    {
        public string name;
        public string advice;
        public int weight;
    }
}
