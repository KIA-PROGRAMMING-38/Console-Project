using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class StageManager : SingletonBase<StageManager>
    {
        private GoldGroup _goldGroup;

        private bool _isPauseGame = false;

        private LinkedList<GameObject> _ui = new LinkedList<GameObject>();

        private SelectUI _selectUI = null;
        private TitleText _titleText = null;

        private int _endingKind = 0;

        public int EndingKind { get { return _endingKind; } }
        public bool IsPauseGame { get { return _isPauseGame; } }

        public StageManager() 
        {
            _goldGroup = null;

            EventManager.Instance.AddInputEvent( ConsoleKey.Escape, OnPressEscapeKey );

            int uiPosX = 85;
            int uiPosY = 0;

            string[] titleText =
            {
                @"###############################",
                @"#___  ___ _____  _   _  _   _ #",
                @"#|  \/  ||  ___|| \ | || | | |#",
                @"#| .  . || |__  |  \| || | | |#",
                @"#| |\/| ||  __| | . ` || | | |#",
                @"#| |  | || |___ | |\  || |_| |#",
                @"#\_|  |_/\____/ \_| \_/ \___/ #",
                @"###############################",
            };
            _titleText = new TitleText( titleText, uiPosX, uiPosY );
            _titleText.Initialize();

            _selectUI = new SelectUI( uiPosX + 8, uiPosY + titleText.Length + 2 );
            _selectUI.Initialize();

            _selectUI.AddSelectList( "Resume", ResumeGame );
            _selectUI.AddSelectList( "Go Title Scene", GoTitleScene );
        }

        public void SetPauseGame(bool isPauseGame)
        {
            _isPauseGame = isPauseGame;

            if(_isPauseGame)
            {

            }
            else
            {
                _ui.Clear();

                Console.Clear();

                Map map = ObjectManager.Instance.GetGameObject<Map>();
                if ( null != map )
                {
                    map.Render();
                }

                GoldGroup goldGroup = ObjectManager.Instance.GetGameObject<GoldGroup>();
                if ( null != goldGroup )
                {
                    goldGroup.Render();
                }
            }
        }

        public void Update()
        {
            if ( false == _isPauseGame )
            {
                if ( null == _goldGroup )
                {
                    _goldGroup = ObjectManager.Instance.GetGameObject<GoldGroup>();

                    if ( null == _goldGroup )
                    {
                        return;
                    }
                }

                if ( 0 >= _goldGroup.RemainGoldCount )
                {
                    ClearStage();
                }
            }
            else
            {
                foreach ( var ui in _ui )
                {
                    ui.Update();
                    ui.Render();
                }
            }
        }

        public void AddProcessUI( GameObject ui )
        {
            _ui.AddLast( ui );
        }

        public void OnPlayerDead()
        {
            _endingKind = 1;
            SceneManager.Instance.ChangeScene( SceneManager.SceneKind.Ending );
        }

        public void ClearStage()
        {
            _endingKind = 0;
            SceneManager.Instance.ChangeScene( SceneManager.SceneKind.Ending );
        }

        private void OnPressEscapeKey()
        {
            SetPauseGame( true );

            AddProcessUI( _selectUI );
            AddProcessUI( _titleText );
        }

        private void GoTitleScene()
        {
            SceneManager.Instance.ChangeScene( SceneManager.SceneKind.Title );
        }

        private void ResumeGame()
        {
            SetPauseGame( false );
        }
    }
}
