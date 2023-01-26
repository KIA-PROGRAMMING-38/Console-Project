using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Game : SingletonBase<Game>
    {
        int _realFPS = 0;
        int _runFrameCount = 0;
        int _prevSecFrameCount = 0;

        /// <summary>
        /// 전체적인 게임 초기화..
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
			Debug.Assert( true == InitConsoleSetting() );
			Debug.Assert( true == InitializeSingletons() );

            return true;
        }

        /// <summary>
        /// 게임 루프 부분..
        /// </summary>
        public void Run()
        {
            // 쓰는 싱긍톤 인스턴스들은 미리 변수에 저장..
            TimeManager timeManagerInstance = TimeManager.Instance;
            InputManager inputManagerInstance = InputManager.Instance;
            EventManager eventManagerInstance = EventManager.Instance;
            SceneManager sceneManagerInstance = SceneManager.Instance;

            while ( true )
            {
                // 실행 시간 계속 누적..
                timeManagerInstance.Update();

                // 현재 프레임을 실행할 시간이라면..
				if ( true == timeManagerInstance.UpdatePassFrameInterval() )
                {
                    // Win10 에서 전체화면 하면 커서 계속 보여서 계속 없애줌..
                    Console.CursorVisible = false;

                    // 입력과 이벤트 관련 처리 먼저..
                    inputManagerInstance.Update();
                    eventManagerInstance.Update();

                    // 실제 씬 Update Render 하기..
                    sceneManagerInstance.Update();
                    sceneManagerInstance.Render();
                }
            }
        }

        /// <summary>
        /// 게임 종료 시 호출됩니다.
        /// </summary>
        public void Release()
        {
            TimeManager.Instance.Release();
            SoundManager.Instance.Release();
        }

        /// <summary>
        /// 게임을 강제 종료시킵니다.
        /// </summary>
        /// <param name="exitCode"></param>
        public void Exit(int exitCode)
        {
            Console.Clear();
            Environment.Exit( exitCode );
        }

        /// <summary>
        /// 콘솔 세팅 초기화..
        /// </summary>
        /// <returns> 성공 실패 여부 </returns>
        private bool InitConsoleSetting()
        {
            Console.Clear();
            Console.BackgroundColor = Constants.DEFAULT_BACKGROUND_COLOR;
            Console.ForegroundColor = Constants.DEFAULT_FOREGROUND_COLOR;
            Console.Title = "Packman";
            Console.CursorVisible = false;

            // 인코딩은 UTF8로 설정..
            Console.OutputEncoding = Encoding.UTF8;

            //Console.SetWindowSize( 1920, 1080 );
            //Console.SetBufferSize( 1920, 1080 );

            //Console.SetWindowSize( 1920, 1080 );
            //Console.SetBufferSize( 1920, 1080 );

            return true;
        }

        /// <summary>
        /// 싱글톤 인스턴스들 초기화..
        /// </summary>
        /// <returns> 성공 실패 여부 </returns>
        private bool InitializeSingletons()
        {
            // Scene Manager 초기화..
            SceneManager.Instance.ChangeScene( SceneManager.SceneKind.Title );

            // Time Manager 초기화..
            if( false == TimeManager.Instance.Initialize( Constants.FRAME_PER_SECOND ) )
            {
                return false;
            }

            InitializeSoundManager();

			return true;
        }

        private void InitializeSoundManager()
        {
            string path = Path.Combine( "..\\..\\..\\Assets", "Sound", "TitleBackground" + "." + "wav" );
			SoundManager.Instance.AddSound( "Title Background", path );

			path = Path.Combine( "..\\..\\..\\Assets", "Sound", "StageBackground" + "." + "wav" );
			SoundManager.Instance.AddSound( "Stage Background", path );

			path = Path.Combine( "..\\..\\..\\Assets", "Sound", "EndingSuccess_00" + "." + "wav" );
			SoundManager.Instance.AddSound( "Ending Success 00", path );

			path = Path.Combine( "..\\..\\..\\Assets", "Sound", "EndingFailedBackground" + "." + "wav" );
			SoundManager.Instance.AddSound( "Ending Failed Background", path );

			path = Path.Combine( "..\\..\\..\\Assets", "Sound", "EndingSuccessBackground" + "." + "wav" );
			SoundManager.Instance.AddSound( "Ending Success Background", path );
		}
    }
}
