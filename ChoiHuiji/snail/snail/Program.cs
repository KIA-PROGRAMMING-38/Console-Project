using System;
using System.Threading;

namespace snail;
class Program
{
    static void Main(string[] args)
    {
        
        int y = 1;

        while(y < 5)
        {          
            for (int x = 1; x < 50; ++x)
            {
                Console.Clear();
                Console.SetCursorPosition(x, y);

                if (x % 3 == 0)
                {
                    Console.Write("__@");
                }

                else if (x % 3 == 1)
                {
                    Console.Write("_^@");
                }

                else
                {
                    Console.Write("^_@");
                }
                Thread.Sleep(100);
              
            }
        }
        
        
    }
}

