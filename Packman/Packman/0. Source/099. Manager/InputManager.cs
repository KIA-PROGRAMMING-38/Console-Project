using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class InputManager : SingletonBase<InputManager>
    {
        private const int PRESS_EVENT_DEFAULT = 0;
        private const int PRESS_EVENT_ALTPRESS = 1;
        private const int PRESS_EVENT_SHIFTPRESS = 2;
        private const int PRESS_EVENT_CTRLPRESS = 3;

        private const int EVENT_TYPE_COUNT = 4;

        private struct PressKeyEventInfo
        {
            public Action[] events;

            public PressKeyEventInfo()
            {
                events = new Action[EVENT_TYPE_COUNT];
            }
        }

        // ConsoleKey enum 값에 있는 기호상수중 가장 큰 값인 OemClear가 254니까 +1해서 255개 생성..
        const int totalKeyCount = 255;

        private PressKeyEventInfo[] pressKeyEvents = new PressKeyEventInfo[totalKeyCount];
        private bool[] pressKeyState = new bool[totalKeyCount];
        private ConsoleModifiers[] pressKeyModifiers = new ConsoleModifiers[totalKeyCount];

        Stack<int> pressKeyIndices = new Stack<int>();

        public InputManager()
        {
            for( int i = 0; i < totalKeyCount; ++i )
            {
                pressKeyEvents[i] = new PressKeyEventInfo();
            }
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

            for( int index = 0; index < totalKeyCount; ++index )
            {
                if( false == pressKeyState[index] )
                {
                    continue;
                }

                pressKeyState[index] = false;

                pressKeyEvents[index].events[PRESS_EVENT_DEFAULT]?.Invoke();
            }
        }

        public void AddEvent(ConsoleKey key, Action action)
        {
            pressKeyEvents[(int)key].events[PRESS_EVENT_DEFAULT] += action;
        }

        public void RemoveEvent(ConsoleKey key, Action action)
        {
            pressKeyEvents[(int)key].events[PRESS_EVENT_DEFAULT] -= action;
        }
    }
}
