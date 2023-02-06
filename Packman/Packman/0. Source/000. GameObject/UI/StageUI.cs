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

        string[] _backgroundImage = null;

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
            PlayerSkill playerSkillComponent = player.SkillComponent;
            int num = 2;
            _texts[num].Text = $"현재 남은 골드 개수 : {goldGroup.RemainGoldCount}개";
            num += 4;

            _texts[num].Text = $"Player MP : {player.CurMP} / {player.MaxMP}";
            num += 2;

            _texts[num].Text = $"스턴 총알 발사 : Q 키( {playerSkillComponent.GetSkillRemainCount(PlayerSkill.SkillKind.FireStungun):D2} 개)";
            num += 2;

            _texts[num].Text = $"주먹 날리기 : W 키( {playerSkillComponent.GetSkillRemainCount( PlayerSkill.SkillKind.FirePunchMissile ):D2} 개)";
            num += 2;

            _texts[num].Text = $"골드 수집기 발사 : E 키( {playerSkillComponent.GetSkillRemainCount( PlayerSkill.SkillKind.FireCollectGoldBullet ):D2} 개)";
            num += 2;

            _texts[num].Text = $"몬스터Kill 총알 : R 키( {playerSkillComponent.GetSkillRemainCount( PlayerSkill.SkillKind.FireKillMonsterBullet ):D2} 개)";
        }

        public override void Render()
        {
			Console.SetCursorPosition( _x - 2, _y - 2 );
            Console.Write( _backgroundImage[0] );

			Console.SetCursorPosition( _x - 2, _y + (_backgroundImage.Length - 1) - 2 );
			Console.Write( _backgroundImage[_backgroundImage.Length - 1] );

			for ( int i = 1; i < _backgroundImage.Length - 1; ++i )
            {
                Console.SetCursorPosition( _x - 2, _y - 2 + i );
                Console.Write( _backgroundImage[i] );
				Console.SetCursorPosition( _x - 2 + _backgroundImage[0].Length - 1, _y - 2 + i );
				Console.Write( _backgroundImage[i] );
			}

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

                new TextInfo { Text = "====== Player Info ======", LineAlignmentCount = 1 },
                new TextInfo { Text = "이동 : 화살표키, 이동 정지 : Spacebar", LineAlignmentCount = 1 },
                new TextInfo { Text = "                     ", LineAlignmentCount = 0 },
                new TextInfo { Text = "Player MP : ", LineAlignmentCount = 1 },
                new TextInfo { Text = "                                     ", LineAlignmentCount = 0 },
                new TextInfo { Text = "스턴 총알 발사 : Q 키( 남은 개수 : )", LineAlignmentCount = 1 },
                new TextInfo { Text = "                                       ", LineAlignmentCount = 0 },
                new TextInfo { Text = "주먹 날리기 : W 키( 남은 개수 : )", LineAlignmentCount = 1 },
                new TextInfo { Text = "                                      ", LineAlignmentCount = 0 },
                new TextInfo { Text = "골드 수집기 발사 : E 키( 남은 개수 : )", LineAlignmentCount = 1 },
                new TextInfo { Text = "                                       ", LineAlignmentCount = 0 },
                new TextInfo { Text = "몬스터Kill 총알 : R 키( 남은 개수 : )", LineAlignmentCount = 1 },
                new TextInfo { Text = "스텔스 모드(Toggle) : G 키( 마나를 사용 )", LineAlignmentCount = 1 },


                new TextInfo { Text = "   ", LineAlignmentCount = 0 },
                new TextInfo { Text = "", LineAlignmentCount = 0 },
            };

            _textCount = _texts.Length;

            _backgroundImage = new string[_texts.Length + 4];
            _backgroundImage[0] = "=============================================";
            for( int i = 1; i < _texts.Length + 4 - 1; ++i )
            {
                _backgroundImage[i] = "|";
            }
			_backgroundImage[_backgroundImage.Length - 1] = "=============================================";
        }
    }
}
