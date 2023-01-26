using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class FastClickUI : GameObject
    {
        public Action OnFillMaxGauge;

        private string[] _text =
        {
            @"===================================",
            @"|  ___  _ __    __ _   ___   ___  |",
            @"| / __|| '_ \  / _` | / __| / _ \ |",
            @"| \__ \| |_) || (_| || (__ |  __/ |",
            @"| |___/| .__/  \__,_| \___| \___| |",
            @"|      | |                        |",
            @"|      |_|                        |",
            @"===================================",
        };

        // 프로그레스 바 관련 변수들..
        private string[] _progressBar = 
        {
            "       Gauge Bar        ",
            "========================",
            "|                      |",
            "========================",
        };

        // 프로그레바 게이지 시작 위치..
        private const int GAUGE_BAR_START_X = 1;
        private const int GAUGE_BAR_START_Y = 2;
        private const int GAUGE_BAR_END_X = 23;
        private const int GAUGE_BAR_WIDTH = GAUGE_BAR_END_X - GAUGE_BAR_START_X;

        private int _gaugeBarOffsetX = 3;
        private int _gaugeBarOffsetY = 2;

        private string[] _gaugeBar = null;

        float _curGauge = 0.0f;
        float _gaugePower = 0.1f;
        bool _isFullGauge = false;

        ConsoleColor[] _colors = { ConsoleColor.Gray, ConsoleColor.DarkGray };
        int _curColorIndex = 0;

        public FastClickUI( int x, int y )
            : base( x, y, 2100 )
        {
            _gaugeBar = new string[GAUGE_BAR_WIDTH + 1];
            string image = "▮";
            string accImage = "▮";
            for ( int i = 1; i < _gaugeBar.Length; ++i )
            {
                _gaugeBar[i] = accImage;
                accImage = accImage + image;
            }
        }

        public void Initialize()
        {
            base.Initialize();

            OnUpdateColor();

            EventManager.Instance.AddInputEvent( ConsoleKey.Spacebar, OnPressSpacebarKey );
        }

        public override void OnEnable()
        {
            base.OnEnable();

            _curGauge = 0.0f;
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Release()
        {
            base.Release();

            EventManager.Instance.RemoveInputEvent( ConsoleKey.Spacebar, OnPressSpacebarKey );
        }

        public override void Render()
        {
            base.Render();

            ConsoleColor tempColor = Console.ForegroundColor;

            int textLineCount = _text.Length;

            Console.ForegroundColor = _colors[_curColorIndex];
            for ( int i = 0; i <textLineCount; ++i )
            {
                Console.SetCursorPosition( _x, _y + i );
                Console.Write( _text[i] );
            }

            Console.ForegroundColor = tempColor;

            int gaugeBarIndex = (int)(GAUGE_BAR_WIDTH * (_curGauge + 0.0001f));

            int gaugeBarX = _x + _gaugeBarOffsetX;
            int gaugeBarY = Console.CursorTop + _gaugeBarOffsetY;

            for ( int i = 0; i < _progressBar.Length; ++i )
            {
                Console.SetCursorPosition( gaugeBarX, gaugeBarY + i );
                Console.Write( _progressBar[i] );
            }

            gaugeBarX += GAUGE_BAR_START_X;
            gaugeBarY = gaugeBarY + GAUGE_BAR_START_Y;

            Console.SetCursorPosition( gaugeBarX, gaugeBarY );
            Console.Write( _gaugeBar[gaugeBarIndex] );
        }

        private void OnUpdateColor()
        {
            _curColorIndex = (_curColorIndex + 1) % _colors.Length;

            EventManager.Instance.SetTimeOut( OnUpdateColor, 0.1f );
        }

        private void OnPressSpacebarKey()
        {
            _curGauge = Math.Min( _curGauge + _gaugePower, 1.0f );
            if( _curGauge >= 0.999f )
            {
                OnFillMaxGauge?.Invoke();
            }
        }
    }
}
