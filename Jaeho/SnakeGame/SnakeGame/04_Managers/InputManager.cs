using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class InputManager : Singleton<InputManager>
    {
        public InputManager()
        {
            Task.Factory.StartNew(() => 
            { 
                while(true)
                {
                    _key = Console.ReadKey().Key;
                }
            });
        }
        private ConsoleKey _key;
        public  ConsoleKey Key { get { return _key; } }
    }
}
