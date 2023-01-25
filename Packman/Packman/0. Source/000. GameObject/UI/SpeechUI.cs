using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class SpeechUI : GameObject
    {
        private struct SpeechInfo
        {
            public string Text;
            public int LineCount;

            public SpeechInfo(string text)
            {
                Text = text;
                LineCount = text.Length;
            }
        }

        private SpeechInfo[] _speechs = null;
        private int _curSpeechIndex = 0;

        private int _speechSpaceWidth = 80;
        private int _speechSpaceHeight = 5;

        private string[] _outSide =
        {
            "",
            "",
            "",
        };

        private int _renderTextOffsetX = 2;
        private int _renderTextOffsetY = 1;

        private string _curText = "";
        private int _curRenderTextIndex = 0;
        private bool _isAllRenderText = false;

        private float _deltaTime = 0.0f;

        private bool _isEndSpeech = false;
        public bool IsEndSpeech { get { return _isEndSpeech; } }

        public SpeechUI( int x, int y, string[] speechs )
            : base( x, y, 1500 )
        {
            EventManager.Instance.AddInputEvent( ConsoleKey.Spacebar, OnPressEnterKey );

            _speechs = new SpeechInfo[speechs.Length];
            for ( int i = 0; i < speechs.Length; ++i )
            {
                _speechs[i] = new SpeechInfo( speechs[i] );
            }

            for( int i = 0; i < _speechSpaceWidth; ++i )
            {
                _outSide[0] += "=";
                _outSide[2] += "=";
            }

            _outSide[1] = "|";
            for ( int i = 1; i < _speechSpaceWidth - 1; ++i )
            {
                _outSide[1] += " ";
            }

            _outSide[1] += "|";
        }

        public override void Release()
        {
            base.Release();

            EventManager.Instance.RemoveInputEvent( ConsoleKey.Spacebar, OnPressEnterKey );
        }

        public override void Update()
        {
            base.Update();

            if( _isEndSpeech )
            {
                return;
            }

            _deltaTime += TimeManager.Instance.ElaspedTime;
            if ( _deltaTime >= 0.05f )
            {
                UpdateRenderText();

                _deltaTime = 0.0f;
            }
        }

        public override void Render()
        {
            base.Render();

            int textX = _x + _renderTextOffsetX;
            int textY = _y + _renderTextOffsetY;

            Console.SetCursorPosition( textX, textY );
            Console.Write( _curText );
        }

        public void RenderOutside()
        {
            for ( int index = 0; index < _outSide.Length; ++index ) 
            {
                Console.SetCursorPosition( _x, _y + index );
                Console.Write( _outSide[index] );
            }
        }

        private void OnPressEnterKey()
        {
            if ( true == _isAllRenderText )
            {
                ++_curSpeechIndex;

                if(_curSpeechIndex >= _speechs.Length)
                {
                    _isEndSpeech = true;
                    _curSpeechIndex = 0;

                    return;
                }

                _curText = "";
                _curRenderTextIndex = 0;

                RenderOutside();

                _isAllRenderText = false;
            }
            else
            {
                while ( false == _isAllRenderText )
                {
                    UpdateRenderText();
                }
            }
        }

        private void UpdateRenderText()
        {
            if ( _curRenderTextIndex >= _speechs[_curSpeechIndex].LineCount )
            {
                _isAllRenderText = true;

                return;
            }

            _curText += _speechs[_curSpeechIndex].Text[_curRenderTextIndex];
            ++_curRenderTextIndex;
        }
    }
}
