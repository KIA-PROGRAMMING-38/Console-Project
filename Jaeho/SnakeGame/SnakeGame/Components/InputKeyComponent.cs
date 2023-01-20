﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class InputKeyComponent : Component
    {

        public InputKeyComponent() { }
        private ConsoleKey _key;
        public  ConsoleKey Key { get { return _key; } }

        public override void Start()
        {
            _key = ConsoleKey.NoName;
        }

        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                _key = Console.ReadKey().Key;
            }
        }
    }
}
