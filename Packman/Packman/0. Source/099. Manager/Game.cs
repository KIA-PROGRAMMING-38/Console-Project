using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Game : SingletonBase<Game>
    {
        /// <summary>
        /// 전체적인 게임 초기화..
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
            if ( false == InitConsoleSetting() )
            {
                return false;
            }

            if ( false == InitializeSingletons() )
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 게임 루프 부분..
        /// </summary>
        public void Run()
        {
            TimeManager timeManagerInstance = TimeManager.Instance;
            InputManager inputManagerInstance = InputManager.Instance;

            while ( true )
            {
                timeManagerInstance.Update();

                if ( true == timeManagerInstance.UpdatePassFrameInterval() )
                {
                    inputManagerInstance.Update();

                    SceneManager.Instance.Update();
                    SceneManager.Instance.Render();
                }
            }
        }

        /// <summary>
        /// 게임 종료 시 호출됩니다.
        /// </summary>
        public void Release()
        {
            TimeManager.Instance.Release();
        }

        /// <summary>
        /// 게임을 강제 종료시킵니다.
        /// </summary>
        /// <param name="exitCode"></param>
        public void Exit(int exitCode)
        {
            Environment.Exit( exitCode );
        }

        /// <summary>
        /// 콘솔 세팅 초기화..
        /// </summary>
        /// <returns></returns>
        private bool InitConsoleSetting()
        {
            Console.Clear();
            Console.BackgroundColor = Constants.DEFAULT_BACKGROUND_COLOR;
            Console.ForegroundColor = Constants.DEFAULT_FOREGROUND_COLOR;
            Console.Title = "Packman";
            Console.CursorVisible = false;

            // 인코딩은 UTF8로 설정..
            //Console.OutputEncoding = Encoding.UTF8;

            return true;
        }

        /// <summary>
        /// 싱글톤 인스턴스들 초기화..
        /// </summary>
        /// <returns></returns>
        private bool InitializeSingletons()
        {
            // Scene Manager 초기화..
            SceneManager.Instance.ChangeScene( SceneManager.SceneKind.Title );

            // Time Manager 초기화..
            if( false == TimeManager.Instance.Initialize( Constants.FRAME_PER_SECOND ) )
            {
                return false;
            }

            return true;
        }
    }
}
