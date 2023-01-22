using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class TimeManager : SingletonBase<TimeManager>
    {
        // 타이머 관련..
        private Stopwatch stopwatch = null;

        // 시간 계산에 관련된 변수들..
        private float frameInterval = 0.0f;
        private float prevRunTime = 0.0f;
        // 시간과 관련된 값들을 실질적으로 들고있는 변수들..
        private float deltaTime = 0.0f;
        private float runTime = 0.0f;

        public float ElaspedTime { get { return frameInterval; } }

        public float RunTime { get { return runTime; } }

        public bool Initialize( int framePerSecond )
        {
            stopwatch = new Stopwatch();

            stopwatch.Start();

            if ( framePerSecond <= 0 )
            {
                return false;
            }

            frameInterval = 1.0f / framePerSecond;
            deltaTime = 0.0f;
            runTime = prevRunTime = MilliSecondToSecond( stopwatch.ElapsedMilliseconds );

            return true;
        }

        public void Update()
        {
            prevRunTime = runTime;
            runTime = MilliSecondToSecond( stopwatch.ElapsedMilliseconds );

            deltaTime += runTime - prevRunTime;
        }

        public void Release()
        {
            stopwatch?.Stop();
        }

        /// <summary>
        /// 이전 프레임 종료부터 현재까지 흘러간 시간이 프레임 간격보다 큰지 여부를 갱신..
        /// </summary>
        /// <returns></returns>
        public bool UpdatePassFrameInterval()
        {
            // 현재 흘러간 시간이 프레임 간격보다 크다면..
            if ( frameInterval <= deltaTime )
            {
                deltaTime -= frameInterval;
                //deltaTime = 0.0f;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 밀리세컨드 단위를 초 단위로 바꿔줍니다..
        /// <para></para>
        /// </summary>
        /// <param name="millisecond"> 밀리세컨드 </param>
        /// <returns> 초 단위로 변환한 결과값 </returns>
        private float MilliSecondToSecond(long millisecond)
        {
            return millisecond * 0.001f;
        }
    }
}
