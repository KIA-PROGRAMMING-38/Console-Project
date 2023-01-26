using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    class Input
    {
        public static ConsoleKey key;
        public void Process()
        {
            
            key = default;

            if (Console.KeyAvailable)
            {
                key = Console.ReadKey().Key;
            }
           
        }

    }
}
