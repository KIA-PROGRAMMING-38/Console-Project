using System;
using System.Collections.Generic;
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
            : base( x, y, "A", ConsoleColor.Magenta, 2 )
        {
            _clickUI = new FastClickUI( 0, 0 );
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
            if(true == _isTrap)
            {
                return;
            }

            _isTrap = true;

            StageManager.Instance.SetPauseGame( true );
            StageManager.Instance.AddProcessUI( _clickUI );

            _clickUI.Initialize();
            _clickUI.OnFillMaxGauge += OnClickUIFillMaxGauge;

            base.OnCollision( collisionObjectInst );
        }

        private void OnClickUIFillMaxGauge()
        {
            _clickUI.Release();
            _objectManager.RemoveObject( this );

            StageManager.Instance.SetPauseGame( false );
        }
    }
}
