using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class EventManager : SingletonBase<EventManager>
    {
        private const int PRESS_EVENT_DEFAULT = 0;
        private const int PRESS_EVENT_ALTPRESS = 1;
        private const int PRESS_EVENT_SHIFTPRESS = 2;
        private const int PRESS_EVENT_CTRLPRESS = 4;

        private const int PRESS_EVENT_TYPE_COUNT = 5;

        public EventManager()
        {
            for ( int i = 0; i < InputManager.TOTAL_KEY_COUNT; ++i )
            {
                _pressKeyEvents[i] = new PressKeyEventInfo();
            }

            InputManager.Instance.OnPressInput += OnPressInputEvent;
        }

        /// <summary>
        /// 키 입력 이벤트 관련 정보를 저장할 구조체..
        /// </summary>
        private struct PressKeyEventInfo
        {
            public Action[] Events;

            public PressKeyEventInfo()
            {
                Events = new Action[PRESS_EVENT_TYPE_COUNT];
            }
        }

        /// <summary>
        /// Timeout 이벤트 관련 정보를 저장할 구조체..
        /// </summary>
        private class TimeoutEventInfo
        {
            public Action Action;
            public float RemainTime;
        }

        // 많이 사용하는 싱글톤들은 미리 멤버로 받아두기..
        TimeManager _timeManager = TimeManager.Instance;

        // 키 입력 이벤트들을 저장할 객체..
        private PressKeyEventInfo[] _pressKeyEvents = new PressKeyEventInfo[InputManager.TOTAL_KEY_COUNT];

        // TimeOut 이벤트들을 저장할 객체..
        LinkedList<TimeoutEventInfo> _timeoutEventsInfo = new LinkedList<TimeoutEventInfo>();
        LinkedList<TimeoutEventInfo> _removeTimeoutEventsInfo = new LinkedList<TimeoutEventInfo>();

        public void Update()
        {
            // 현재 흐른 시간을 가져옵니다..
            float elaspedTime = _timeManager.ElaspedTime;

            // Timeout 이벤트들을 순회하면서 시간 갱신..
            foreach( TimeoutEventInfo timeoutEventInfo in _timeoutEventsInfo )
            {
                timeoutEventInfo.RemainTime -= elaspedTime;

                // 이벤트를 실행해야 할 시간이 왔다..
                if(timeoutEventInfo.RemainTime <= 0.0f)
                {
                    _removeTimeoutEventsInfo.AddLast( timeoutEventInfo );
                    timeoutEventInfo.Action?.Invoke();
                }
            }

            foreach ( TimeoutEventInfo timeoutEventInfo in _removeTimeoutEventsInfo )
            {
                _timeoutEventsInfo.Remove( timeoutEventInfo );
            }
        }

        /// <summary>
        /// Key가 눌렸을 때 실행될 콜백 함수..
        /// </summary>
        /// <param name="key"> 입력된 Key </param>
        /// <param name="modifier"> 입력될 때 Ctrl, Atl, Shift 를 눌렀는지 여부 </param>
        public void OnPressInputEvent(ConsoleKey key, ConsoleModifiers modifier)
        {
            _pressKeyEvents[(int)key].Events[(int)modifier]?.Invoke();
        }

        /// <summary>
        /// 키 눌렸을 때 실행되길 원하는 콜백함수들을 저장..
        /// </summary>
        /// <param name="key"> 눌린 Key </param>
        /// <param name="action"> 실행될 함수들 </param>
        public void AddInputEvent( ConsoleKey key, Action action )
        {
            _pressKeyEvents[(int)key].Events[PRESS_EVENT_DEFAULT] += action;
        }

        /// <summary>
        /// 키 눌렸을 때 실행되길 원하는 콜백함수들 속에서 제거한다..
        /// </summary>
        /// <param name="key"> 제거할 Key 종류 </param>
        /// <param name="action"> 제거할 함수들 </param>
        public void RemoveInputEvent( ConsoleKey key, Action action )
        {
            _pressKeyEvents[(int)key].Events[PRESS_EVENT_DEFAULT] -= action;
        }

        /// <summary>
        /// 몇초 뒤에 실행될 함수를 저장합니다.
        /// </summary>
        /// <param name="action"> 실행될 함수(들) </param>
        /// <param name="delay"> 몇 초 뒤에 실행할건지 </param>
        public void SetTimeOut(Action action, float delay)
        {
            _timeoutEventsInfo.AddLast( new TimeoutEventInfo { Action = action, RemainTime = delay } );
        }
    }
}
