using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class EndingScene : Scene
    {
        private int _endingKind = 0;

        private TitleText _titleText = null;
        private SpeechUI _speechUI = null;

        private string[] _endingWordText = null;
        private bool _isRenderEndingWord = false;

        public EndingScene()
        {
            _endingKind = StageManager.Instance.EndingKind;
        }

        public override bool Initialize()
        {
            string filePath = "";
            string speechFilePath = "";
            string endingWordFilePath = "";
            string[] endingImage = null;
            string[] speechs = null;

            switch (_endingKind)
            {
                case 0:
                    {
                        filePath = FileLoader.MakePath("Ending_Success", "txt");
                        speechFilePath = FileLoader.MakePath( "Ending_Success_Speech", "txt" );
                        endingWordFilePath = FileLoader.MakePath( "GoodEnding", "txt" );
                    }
                    break;
                case 1:
                    {
                        filePath = FileLoader.MakePath("Ending_Failed", "txt");
                        speechFilePath = FileLoader.MakePath( "Ending_Failed_Speech", "txt" );
                        endingWordFilePath = FileLoader.MakePath( "BadEnding", "txt" );
                    }
                    break;
            }

            endingImage = FileLoader.ReadFile( filePath );
            speechs = FileLoader.ReadFile( speechFilePath );
            _endingWordText = FileLoader.ReadFile( endingWordFilePath );
            _titleText = new TitleText( endingImage, 30, 0 );
            _titleText.Initialize();

            _speechUI = new SpeechUI(25, endingImage.Length + 2, speechs);
            _speechUI.Initialize();

            _titleText.Update();
            _speechUI.RenderOutside();

            return true;
        }

        public override void Update()
        {
            base.Update();

            _speechUI.Update();

            if(_speechUI.IsEndSpeech && InputManager.Instance.IsKeyDown(ConsoleKey.Spacebar))
            {
                if ( false == _isRenderEndingWord )
                {
                    _isRenderEndingWord = true;
                }
            }
        }

        public override void Render()
        {
            base.Render();

            if(true == _isRenderEndingWord)
            {
                for( int i = 0; i < _endingWordText.Length; ++i )
                {
                    Console.SetCursorPosition( 30, 4 + i );
                    Console.Write( _endingWordText[i] );
                }

                Console.ReadKey();

                SceneManager.Instance.ChangeScene( SceneManager.SceneKind.Title );
            }
        }

        public override void Release()
        {
            base.Release();

            _titleText.Release();
            _speechUI.Release();
        }
    }
}
