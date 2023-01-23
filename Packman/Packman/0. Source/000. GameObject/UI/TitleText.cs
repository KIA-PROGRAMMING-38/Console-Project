using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class TitleText : GameObject
    {
        private string[] _titleText;

        int _titleStartX = 30;
        int _titleStartY = 0;

        public TitleText( string[] titleText, int titleStartX, int titleStartY )
            : base( 0 )
        {
            _titleText = titleText;
            _titleStartX = titleStartX;
            _titleStartY = titleStartY;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Render()
        {
            base.Render();

            for( int i = 0; i < _titleText.Length; ++i)
            {
                Console.SetCursorPosition( _titleStartX, _titleStartY + i );
                Console.Write( _titleText[i] );
            }
        }
    }
}
