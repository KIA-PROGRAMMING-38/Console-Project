using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class InputManager : SingletonBase<InputManager>
    {
        // ConsoleKey enum 값에 있는 기호상수중 가장 큰 값인 OemClear가 254니까 +1해서 255개 생성..
        public const int TOTAL_KEY_COUNT = 255;

        // 키 입력 관련 데이터들..
        private bool[] pressKeyState = new bool[TOTAL_KEY_COUNT];
        private ConsoleModifiers[] pressKeyModifiers = new ConsoleModifiers[InputManager.TOTAL_KEY_COUNT];
        // 키 입력 시 실행될 콜백함수를 저장할 녀석..
        public Action<ConsoleKey, ConsoleModifiers> OnPressInput;

        public InputManager()
        {

        }

        public void Update()
        {
            while ( Console.KeyAvailable )
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                int index = (int)keyInfo.Key;

                pressKeyState[index] = true;
                pressKeyModifiers[index] = keyInfo.Modifiers;
            }

            for( int index = 0; index < TOTAL_KEY_COUNT; ++index )
            {
                if( false == pressKeyState[index] )
                {
                    continue;
                }

                pressKeyState[index] = false;

                OnPressInput?.Invoke( (ConsoleKey)index, pressKeyModifiers[index] );
            }
        }
    }
}
