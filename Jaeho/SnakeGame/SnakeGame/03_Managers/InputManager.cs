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
            ProcessInputAsync();
        }

        /// <summary>
        /// 비동기 키 처리
        /// </summary>
        public void ProcessInputAsync()
        {
            //Task.Factory.StartNew(() =>
            //{
            //    while (true)
            //    {
            //        _key = Console.ReadKey().Key;
            //    }
            //}, TaskCreationOptions.LongRunning);
        }

        public void Update()
        {
            if(Console.KeyAvailable)
            {
                _key = Console.ReadKey().Key;
            }
        }

        /// <summary>
        /// key가 눌렸는지 검사
        /// </summary>
        /// <param name="key">검사할 키</param>
        /// <returns>눌렸는지 반환</returns>
        public bool IsKeyDown(ConsoleKey key) => _key == key;

        public void ResetKey() { _key = default; }
        private ConsoleKey _key;
    }
}
