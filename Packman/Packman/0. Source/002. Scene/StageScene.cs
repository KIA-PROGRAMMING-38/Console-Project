using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class StageScene : Scene
    {
        // 많이 사용하는 싱글톤들은 미리 멤버로 받아두기..
        private CollisionManager _collisionManager = CollisionManager.Instance;
        private StageManager _stageManager = StageManager.Instance;

        int _stageNum = 0;

        public StageScene( int stageNum )
        {
            _stageNum = stageNum;
        }

        public override bool Initialize()
        {
            FileLoader.Load( $"Stage{_stageNum:D2}", "txt" );

            InitStageUI( 0, 2 );

            CollisionManager.Instance.RenewObjectInstance();

            return true;
        }

        public override void Update()
        {
            if ( false ==  StageManager.Instance.IsPauseGame)
            {
                base.Update();

                _collisionManager.Update();
            }
            _stageManager.Update();
        }

        public override void Render()
        {
            base.Render();
        }

        public override void Release()
        {
            base.Release();

            _stageManager.SetPauseGame( false );
        }

        private void InitStageUI(int x, int y)
        {
            StageUI ui = new StageUI(x, y);
            ui.Initialize();

            _objectManager.AddGameObject( "StageUI", ui );
        }
    }
}
