using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class StageUI : GameObject
    {
        private struct TextInfo
        {
            public int LineAlignmentCount;
            public string Text;
        }

        TextInfo[] _texts = null;
        int _textCount = 0;

        public StageUI( int x, int y )
            : base( x, y, 1000 )
        {

        }

        public void Initialize()
        {
            Debug.Assert( base.Initialize() );

            InitText();
        }

        public override void Update()
        {
            base.Update();

            GoldGroup goldGroup = _objectManager.GetGameObject<GoldGroup>();
            Player player = _objectManager.GetGameObject<Player>();
            _texts[2].Text = $"현재 남은 골드 개수 : {goldGroup.RemainGoldCount}개";
            _texts[4].Text = $"Player MP : {player.CurMP} / {player.MaxMP}";
        }

        public override void Render()
        {
            int yOffset = 0;
            for( int index = 0; index < _textCount; ++index )
            {
                Console.SetCursorPosition( _x, _y + yOffset );
                Console.Write( _texts[index].Text );

                yOffset += _texts[index].LineAlignmentCount;
            }
        }

        private void InitText()
        {
            _texts = new TextInfo[]
            {
                new TextInfo { Text = "====== Stage UI ======", LineAlignmentCount = 1 },
                new TextInfo { Text = "                                  ", LineAlignmentCount = 0 },
                new TextInfo { Text = "현재 남은 골드 개수 : ", LineAlignmentCount = 2 },

                new TextInfo { Text = "====== Game Info ======", LineAlignmentCount = 1 },
                new TextInfo { Text = "Player MP : ", LineAlignmentCount = 1 },
            };

            _textCount = _texts.Length;
        }
    }
}
