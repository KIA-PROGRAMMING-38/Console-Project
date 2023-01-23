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

        private SelectUI _selectUI = null;
        private TitleText _titleText = null;

        public bool IsPauseGame { get { return _isPauseGame; } }

        public StageManager() 
        {
            _goldGroup = null;

            EventManager.Instance.AddInputEvent( ConsoleKey.Escape, OnPressEscapeKey );

            int uiPosX = 30;
            int uiPosY = 2;

            string[] titleText =
            {
                @"___  ___ _____  _   _  _   _ ",
                @"|  \/  ||  ___|| \ | || | | |",
                @"| .  . || |__  |  \| || | | |",
                @"| |\/| ||  __| | . ` || | | |",
                @"| |  | || |___ | |\  || |_| |",
                @"\_|  |_/\____/ \_| \_/ \___/ ",
            };
            _titleText = new TitleText( titleText, uiPosX, uiPosY );
            _titleText.Initialize();

            _selectUI = new SelectUI( uiPosX + 5, uiPosY + titleText.Length + 2 );
            _selectUI.Initialize();

            _selectUI.AddSelectList( "Resume", ResumeGame );
            _selectUI.AddSelectList( "Go Title Scene", GoTitleScene );
        }

        public void Update()
        {
            if( false == _isPauseGame)
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
                _selectUI.Update();
                _titleText.Update();

                _selectUI.Render();
                _titleText.Render();
            }
        }

        public void OnPlayerDead()
        {
            Game.Instance.Exit( 0 );
        }

        private void ClearStage()
        {
            Game.Instance.Exit( 0 );
        }

        private void OnPressEscapeKey()
        {
            _isPauseGame = true;
        }

        private void GoTitleScene()
        {
            SceneManager.Instance.ChangeScene( SceneManager.SceneKind.Title );
        }

        private void ResumeGame()
        {
            _isPauseGame = false;

            Console.Clear();

            Map map = ObjectManager.Instance.GetGameObject<Map>();
            if ( null != map )
            {
                map.Render();
            }

            GoldGroup goldGroup = ObjectManager.Instance.GetGameObject<GoldGroup>();
            if( null != goldGroup)
            {
                goldGroup.Render();
            }
        }
    }
}
