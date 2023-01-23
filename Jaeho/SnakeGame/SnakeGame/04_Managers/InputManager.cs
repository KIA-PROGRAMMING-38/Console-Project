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
            }, TaskCreationOptions.LongRunning);
        }

        public bool IsKeyDown(ConsoleKey key) => _key == key;

        public void ResetKey() { _key = default; }
        private ConsoleKey _key;
    }
}
