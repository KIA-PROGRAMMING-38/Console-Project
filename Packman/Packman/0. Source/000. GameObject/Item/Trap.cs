using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Trap : Item
    {
        private bool _isTrap = false;

        private FastClickUI _clickUI = null;

        public Trap( int x, int y )
            : base( x, y, Constants.TRAP_IMAGE, Constants.TRAP_COLOR, 2 )
        {
            _clickUI = new FastClickUI( 0, 16 );
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Render()
        {
            base.Render();
        }

        public override void OnCollision( GameObject collisionObjectInst )
        {
            if ( true == _isTrap )  // 발동 중일라면 리턴..
            {
                return;
            }

            _isTrap = true;

            // Click UI 키고 다 채울 때 호출될 함수를 설정..
            _clickUI.Initialize();
            _clickUI.OnFillMaxGauge += OnClickUIFillMaxGauge;
            Debug.Assert( _objectManager.AddGameObject( "ClickUI", _clickUI ) );

            // 플레이어 기능 정지..
            Player player = _objectManager.GetGameObject<Player>();
            player.Pause( true );

            base.OnCollision( collisionObjectInst );
        }

        private void OnClickUIFillMaxGauge()
        {
            // 오브젝트 매니저에 있는 자기 자신 instance 제거 및 ClickUI 도 제거..
            _objectManager.RemoveObject( this );
            _objectManager.RemoveObject( _clickUI );

            // 플레이어 멈춤 해제..
            Player player = _objectManager.GetGameObject<Player>();
            player.Pause( false );

            // 화면 클리어(Console.Clear())를 위해 일시정지 키고 끄기..
            StageManager.Instance.SetPauseGame( true );
            StageManager.Instance.SetPauseGame( false );
        }
    }
}
