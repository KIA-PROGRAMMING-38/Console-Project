using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class TitleScene : Scene
    {
        public TitleScene()
        {

        }

        public override bool Initialize()
        {
            Debug.Assert( true == InitializeTitleText() );
            Debug.Assert( true == InitializeSelectUI() );

            return true;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Render()
        {
            base.Render();
        }

        private bool InitializeTitleText()
        {
            string[] titleTextString =
            {
                @"@@@@@@@@@@@@ @@ @@                   @@  @@",
                @"@@@@@@@@@@@@ @@ @@       @@@@@@@@@@@ @@  @@",
                @"   @@  @@    @@ @@       @@@@$$$@@@@ @@  @@",
                @"   @@  @@    @@ @@       @@       @@ @@  @@",
                @"   @@  @@    @@@@@       @@       @@ @@  @@",
                @"   @@  @@    @@ @@       @@       @@ @@@@@@",
                @"   @@  @@    @@ @@       @@       @@ @@  @@",
                @"@@@@@@@@@@@@ @@ @@       @@@@@@@@@@@ @@  @@",
                @"@@@@@@@@@@@@ @@ @@       @@@@@@@@@@@ @@  @@",
                @"             @@ @@                   @@  @@",
                @"                            @@@      @@  @@",
                @"@@@@@@@@@@@@@@@@@@          @@@      @@  @@",
                @"@@@@@@@@@@@@@@@@@@          @@@            ",
                @"                @@          @@@            ",
                @"                @@          @@@            ",
                @"                @@          @@@@@@@@@@@@@@@",
                @"                @@          @@@@@@@@@@@@@@@"
            };

            TitleText titleText = new TitleText(titleTextString, 30, 0);
            titleText.Initialize();
            _objectManager.AddGameObject( "TitleText", titleText );

            return true;
        }

        private bool InitializeSelectUI()
        {
            SelectUI selectUI = new SelectUI( 50, 20 );
            selectUI.Initialize();

            selectUI.AddSelectList( "Game Start", GoStageScene );
            selectUI.AddSelectList( "Exit", ExitGame );

            _objectManager.AddGameObject( "Select UI", selectUI );

            return true;
        }

        private void GoStageScene()
        {
            SceneManager.Instance.ChangeScene( SceneManager.SceneKind.Stage );
        }

        private void ExitGame()
        {
            Game.Instance.Exit( 0 );
        }
    }
}
