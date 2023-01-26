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
        private Trap[] _traps = null;

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

        /// <summary>
        /// Gold Group 초기화..
        /// </summary>
        /// <param name="golds"> 필드에 있는 모든 골드들 </param>
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

        /// <summary>
        /// Gold Group 갱신..
        /// </summary>
        public override void Update()
        {
            // 필요한 정보들을 갱신..
            UpdateNecessaryData();

            // 현재 플레이어가 밟고 있는 타일에 있는 골드 제거..
            RemovePlayerOnGold();

            // 현재 골드 수집기가 밟고 있는 타일에 있는 골드 제거..
            RemoveCollectGoldBulletOnGold();

            // 현재 함정이 있는 타일에 있는 골드 제거..
            RemoveTrapOnGold();

            // 모든 골드를 먹었다면(필드에 남아있는 골드가 0개라면)..
            CheckIsRemainGoldIsZero();
        }

        /// <summary>
        /// 필요한 정보들을 가져온다..
        /// </summary>
        private void UpdateNecessaryData()
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

            _traps = _objectManager.GetAllGameObject<Trap>();
            _collectGoldBullets = _objectManager.GetAllGameObject<CollectGoldBullet>();
        }

        /// <summary>
        /// 현재 필드에 남아 있는 모든 골드들 그린다..
        /// </summary>
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

        /// <summary>
        /// 플레이어가 있는 위치에 골드 제거..
        /// </summary>
        private void RemovePlayerOnGold()
        {
            if ( null != _player )
            {
                RemoveGold( _player.X, _player.Y );
            }
        }

        /// <summary>
        /// 골드 수집기가 있는 위치에 골드 제거..
        /// </summary>
        private void RemoveCollectGoldBulletOnGold()
        {
            if ( null != _collectGoldBullets )
            {
                foreach ( var collectGoldBullet in _collectGoldBullets )
                {
                    RemoveGold( collectGoldBullet.X, collectGoldBullet.Y );
                }
            }
        }

        /// <summary>
        /// Trap 이 있는 위치에 골드 제거..
        /// </summary>
        private void RemoveTrapOnGold()
        {
            if ( null != _traps )
            {
                foreach ( var trap in _traps )
                {
                    RemoveGold( trap.X, trap.Y );
                }
            }
        }

        /// <summary>
        /// 현재 필드에 남은 골드가 0개인지 체크..
        /// </summary>
        private void CheckIsRemainGoldIsZero()
        {
            if ( 0 >= _remainGoldCount )
            {
                StageManager.Instance.ClearStage();
            }
        }

        /// <summary>
        /// x, y 위치에 있는 골드를 제거한다..
        /// </summary>
        /// <param name="x"> x 위치 </param>
        /// <param name="y"> y 위치 </param>
        private void RemoveGold(int x, int y)
        {
            Gold findGold = GetGold( x, y );
            if ( null != findGold )
            {
                _objectManager.RemoveObject( findGold );

                _goldTable[y, x] = null;

                --_remainGoldCount;
            }
        }

        /// <summary>
        /// x, y 위치에 골드를 반환한다..
        /// </summary>
        /// <param name="x"> x 위치 </param>
        /// <param name="y"> y 위치 </param>
        /// <returns></returns>
        private Gold GetGold(int x, int y)
        {
            return _goldTable[y, x];
        }

        /// <summary>
        /// 캐릭터가 움직일 때 호출되는 함수..
        /// </summary>
        /// <param name="character"> 움직인 Character Instance </param>
        private void OnCharacterMove( Character character )
        {
            Debug.Assert( null != character );

            Gold gold = GetGold( character.PrevX, character.PrevY );
            if(null != gold )
            {
                gold.Update();
            }
        }

        /// <summary>
        /// Projectile 이 움직일 때 호출되는 함수..
        /// </summary>
        /// <param name="projectile"> 움직인 Projectile Instance </param>
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
