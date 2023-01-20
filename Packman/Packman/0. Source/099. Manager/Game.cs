using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Game : SingletonBase<Game>
    {
        // 시간과 관련된 상수들..
        public const int FRAME_PER_SECOND = 60;
        // 맵과 관련된 상수들..
        public const int MAP_RANGE_MIN_X = 1;
        public const int MAP_RANGE_MIN_Y = 1;
        public const int MAP_RANGE_MAX_X = 60;
        public const int MAP_RANGE_MAX_Y = 25;

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

        public void Run()
        {
            TimeManager timeManagerInstance = TimeManager.Instance;
            InputManager inputManagerInstance = InputManager.Instance;

            float elaspedTime = 0.0f;

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

        public void Release()
        {
            TimeManager.Instance.Release();
        }

        public void Exit(int exitCode)
        {
            Environment.Exit( exitCode );
        }

        private bool InitConsoleSetting()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Title = "Packman";
            Console.CursorVisible = false;

            return true;
        }

        private bool InitializeSingletons()
        {
            // Scene Manager 초기화..
            SceneManager.Instance.ChangeScene( SceneManager.SceneKind.Title );

            // Time Manager 초기화..
            if( false == TimeManager.Instance.Initialize( FRAME_PER_SECOND ) )
            {
                return false;
            }

            return true;
        }
    }
}
