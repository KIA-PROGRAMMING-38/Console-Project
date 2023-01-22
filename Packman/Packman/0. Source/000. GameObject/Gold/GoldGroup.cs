using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class GoldGroup : GameObject
    {
        private Player _player;
        private List<Character> _characters;

        // 룩업테이블로 사용할 예정( y, x 좌표를 넣으면 Gold 인스턴스를 뱉게끔 )
        private Gold[,] _goldTable;
        private int _remainGoldCount = 0;  // 남은 골드 개수..

        public int RemainGoldCount { get { return _remainGoldCount; } }
        

        public GoldGroup( int mapWidth, int mapHeight )
            : base( 0 )
        {
            _player = null;
            _characters = new List<Character>();

            _goldTable = new Gold[mapHeight + 1, mapWidth + 1];
            _remainGoldCount = 0;
        }

        public void Initialize( LinkedList<Gold> golds )
        {
            Debug.Assert( base.Initialize() );

            // 인자로 들어온 gold들 정보 저장..
            foreach ( Gold gold in golds )
            {
                _goldTable[gold.Y, gold.X] = gold;
                gold.Update();
            }

            // 총 Gold 개수 저장( 현재 남은 Gold 개수는 총 Gold 개수와 같으니까 )..
            _remainGoldCount = golds.Count;
        }

        public override void Update()
        {
            if ( null == _player )
            {
                _player = _objectManager.GetGameObject<Player>();
                Character[] allCharacters = _objectManager.GetAllGameObject<Monster>();

                if ( null != allCharacters )
                {
                    foreach ( var character in allCharacters )
                    {
                        if ( _player.GetType() == character.GetType() )
                        {
                            continue;
                        }

                        character.OnMoveCharacterEvent += OnCharacterMove;
                    }
                }
            }

            base.Update();

            // 현재 플레이어가 밟고 있는 타일에 있는 골드 제거..
            if ( null != _player )
            {
                Gold findGold = GetGold( _player.X, _player.Y );
                if ( null != findGold )
                {
                    _objectManager.RemoveObject( findGold );

                    _goldTable[_player.Y, _player.X] = null;

                    --_remainGoldCount;
                }
            }
        }

        private Gold GetGold(int x, int y)
        {
            return _goldTable[y, x];
        }

        public void OnCharacterMove( Character character )
        {
            Debug.Assert( null != character );

            Gold gold = GetGold( character.PrevX, character.PrevY );
            if(null != gold )
            {
                gold.Update();
            }
        }
    }
}
