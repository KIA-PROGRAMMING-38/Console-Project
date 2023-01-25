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
        public int x;
        public int y;
    }
    public class PreviousPlayer
    {
        public int x;
        public int y;
    }
    public class Wall
    {
        public int x;
        public int y;
    }
    public class Enemy
    {
        public int x;
        public int y;
        public bool isAlive;
        public bool isOnTrap = false;
    }
    public class PreviousEnemy
    {
        public int x;
        public int y;
    }
    public class Block
    {
        public int x;
        public int y;
        public bool isOnTrap = false;
    }
    public class PreviousBlock
    {
        public int x;
        public int y;
    }
    public class Trap
    {
        public int x;
        public int y;
        public bool isActivated = true;
    }
    public class Moon
    {
        public int x;
        public int y;
    }
    public class Key
    {
        public int x;
        public int y;
    }
    public class Door
    {
        public int x;
        public int y;
    }
    public class MapSize
    {
        public int x;
        public int y;
    }
    public class Advice
    {
        public string name;
        public string advice;
        public int weight;
    }
    public class Trace
    {
        public string name;
        public string description;
        public bool isUseful;
        public string SuccessString;
        public string FailureString;
    }
}
