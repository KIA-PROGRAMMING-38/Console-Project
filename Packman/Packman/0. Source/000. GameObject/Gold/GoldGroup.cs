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
        private Player _player = null;
        private List<Character> _characters = null;
        private CollectGoldBullet[] _collectGoldBullets = null;

        // 룩업테이블로 사용할 예정( y, x 좌표를 넣으면 Gold 인스턴스를 뱉게끔 )
        private Gold[,] _goldTable;
        private int _remainGoldCount = 0;  // 남은 골드 개수..

        private int _goldRowCount = 0;
        private int _goldColumnCount = 0;

        public int RemainGoldCount { get { return _remainGoldCount; } }
        

        public GoldGroup( int mapWidth, int mapHeight )
            : base( 0 )
        {
            _player = null;
            _characters = new List<Character>();

            _goldRowCount = mapWidth + 1;
            _goldColumnCount = mapHeight + 1;

            _goldTable = new Gold[_goldColumnCount, _goldRowCount];
            _remainGoldCount = 0;
        }

        public void Initialize( LinkedList<Gold> golds )
        {
            Debug.Assert( base.Initialize() );

            // 인자로 들어온 gold들 정보 저장..
            foreach ( Gold gold in golds )
            {
                _goldTable[gold.Y, gold.X] = gold;
            }

            Render();

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

            //base.Update();

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

            // 현재 골드 수집기가 밟고 있는 타일에 있는 골드 제거..
            _collectGoldBullets = _objectManager.GetAllGameObject<CollectGoldBullet>();
            if ( null != _collectGoldBullets )
            {
                foreach ( var collectGoldBullet in _collectGoldBullets )
                {
                    Gold findGold = GetGold( collectGoldBullet.X, collectGoldBullet.Y );

                    if(null != findGold )
                    {
                        _objectManager.RemoveObject( findGold );

                        _goldTable[collectGoldBullet.Y, collectGoldBullet.X] = null;

                        --_remainGoldCount;
                    }
                }
            }
        }

        public override void Render()
        {
            base.Render();

            for( int column = 0; column < _goldColumnCount; ++column )
            {
                for( int row = 0; row < _goldRowCount; ++row )
                {
                    Gold gold = _goldTable[column, row];
                    if( null != gold)
                    {
                        gold.Render();
                    }
                }
            }
        }

        private Gold GetGold(int x, int y)
        {
            return _goldTable[y, x];
        }

        private void OnCharacterMove( Character character )
        {
            Debug.Assert( null != character );

            Gold gold = GetGold( character.PrevX, character.PrevY );
            if(null != gold )
            {
                gold.Update();
            }
        }

        public void OnProjectileMove( Projectile projectile )
        {
            Debug.Assert( null != projectile );

            Gold gold = GetGold( projectile.PrevX, projectile.PrevY );
            if ( null != gold )
            {
                gold.Update();
            }
        }
    }
}
