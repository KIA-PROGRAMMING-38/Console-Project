using Packman.Source;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Packman.PlayerSkill;

namespace Packman
{
    internal class Player : Character
    {
        // 많이 사용하는 싱글톤들은 미리 멤버로 받아두기..
        private EventManager _eventManager = EventManager.Instance;

        // 스킬들을 관리할 컴포넌트..
        PlayerSkill _skillComponent = null;

        // 키 관련 상수 정의..
        private const ConsoleKey moveRightKey = ConsoleKey.RightArrow;
        private const ConsoleKey moveLeftKey = ConsoleKey.LeftArrow;
        private const ConsoleKey moveUpKey = ConsoleKey.UpArrow;
        private const ConsoleKey moveDownKey = ConsoleKey.DownArrow;
        private const ConsoleKey moveStopKey = ConsoleKey.Spacebar;
        private const ConsoleKey fireStunGunKey = ConsoleKey.Q;
        private const ConsoleKey firePunchMissileKey = ConsoleKey.W;
        private const ConsoleKey fireCollectGoldBulletKey = ConsoleKey.E;
        private const ConsoleKey fireKillMonsterBulletKey = ConsoleKey.R;
        private const ConsoleKey stealthModeKey = ConsoleKey.G;

        // 플레이어의 움직임 방향..
        private int _moveDirX = 0;
        private int _moveDirY = 0;
        // 플레이어가 다음으로 움직일 방향..
        private int _nextMoveDirX = 0;
        private int _nextMoveDirY = 0;
        // 플레이어가 현재 바라보고 있는 방향..
        private int _lookDirX = 0;
        private int _lookDirY = 0;

        private int _curMP = 10;
        private int _maxMP = 10;

        private bool _isStealthMode = false;

        private bool _isPause = false;

        public int CurMP { get { return _curMP; } }
        public int MaxMP { get { return _maxMP; } }
        public int LookDirX { get { return _lookDirX; } }
        public int LookDirY { get { return _lookDirY; } }

        public bool IsStealthMode { get { return _isStealthMode; } }
        public PlayerSkill SkillComponent { get { return _skillComponent; } }

        public Player( int x, int y, Map map )
            : base( x, y, Constants.PLAYER_IMAGE, Constants.PLAYER_COLOR, Constants.PLAYER_RENDER_ORDER, map, Constants.PLAYER_MOVE_DELAY )
        {
            _skillComponent = new PlayerSkill();
            AddComponent( "Skill", _skillComponent );

            _skillComponent.SetSkillRemainUseCount( SkillKind.FireStungun, 100 );
            _skillComponent.SetSkillRemainUseCount( SkillKind.FirePunchMissile, 100 );
            _skillComponent.SetSkillRemainUseCount( SkillKind.FireCollectGoldBullet, 10 );
            _skillComponent.SetSkillRemainUseCount( SkillKind.FireKillMonsterBullet, 2 );
            //_skillComponent.SetSkillRemainUseCount( SkillKind.FireCollectGoldBullet, 100 );
            //_skillComponent.SetSkillRemainUseCount( SkillKind.FireKillMonsterBullet, 100 );

            _skillComponent.SetSkillRemainUseCount( SkillKind.Stealth, _curMP );

            _lookDirX = 1;
            _lookDirY = 0;
        }

        /// <summary>
        /// 활성화 시 호출되는 함수..
        /// </summary>
        public override void OnEnable()
        {
            base.OnEnable();

            AddKeyPressEvent();
        }

        /// <summary>
        /// 비활성화 시 호출되는 함수..
        /// </summary>
        public override void OnDisable()
        {
            base.OnDisable();

            RemoveKeyPressEvent();
        }

        /// <summary>
        /// 플레이어를 초기화합니다..
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
            Debug.Assert( base.Initialize() );

            OnCharacterMoveFailedEvent += OnFailedMove;

            return true;
        }

        /// <summary>
        /// Player Update..
        /// </summary>
        public override void Update()
        {
            base.Update();

            if ( true == _isPause )
            {
                return;
            }

            UpdateMovement();
        }

        /// <summary>
        /// 정리할 작업이 있다면 여기서 ㄱㄱ..
        /// </summary>
        public override void Release()
        {
            base.Release();

            RemoveKeyPressEvent();
        }

        /// <summary>
        /// 플레이어를 잠시 멈춥니다..
        /// </summary>
        /// <param name="isPause"> isPause 가 true 일 경우 잠시 멈춤 </param>
        public void Pause(bool isPause)
        {
            _isPause = isPause;

            if ( _isPause )
            {
                RemoveKeyPressEvent();
                _movementComponent.Reset();
                SetMoveDirection( 0, 0 );
            }
            else
            {
                AddKeyPressEvent();
            }
        }

        /// <summary>
        /// 스킬 사용 개수 추가..
        /// </summary>
        /// <param name="skillKind"> 추가할 스킬 종류 </param>
        public void AddSkill( SkillKind skillKind )
        {
            _skillComponent.AddSkill( skillKind );
        }

        /// <summary>
        /// 스텔스 모드 On/Off
        /// </summary>
        /// <param name="isStealthMode"> 스텔스 킬건지 끌건지 </param>
        public void SetStealthMode(bool isStealthMode)
        {
            _isStealthMode = isStealthMode;
            if ( true == _isStealthMode )
            {
                _image = "_";
                OnStartStealthMode();
            }
            else
            {
                _image = Constants.PLAYER_IMAGE;
                OnEndStealthMode();
            }
        }

        /// <summary>
        /// 스텔스 모드 On 시 실행되는 함수..
        /// </summary>
        private void OnStartStealthMode()
        {
            EventManager.Instance.SetTimeOut( () =>
            {
                if ( false == _isStealthMode )
                    return;

                --_curMP;
                if(_curMP <= 0)
                {
                    _curMP = 0;
                    SetStealthMode( false );
                }
                else
                {
                    OnStartStealthMode();
                }

                _skillComponent.SetSkillRemainUseCount( SkillKind.Stealth, CurMP );
            }, 1.0f );
        }

        /// <summary>
        /// 스텔스 모드 Off 시 실행되는 함수..
        /// </summary>
        private void OnEndStealthMode()
        {
            EventManager.Instance.SetTimeOut( () =>
            {
                if ( true == _isStealthMode )
                    return;

                ++_curMP;
                if ( _maxMP <= _curMP )
                {
                    _curMP = _maxMP;
                }
                else
                {
                    OnEndStealthMode();
                }

                _skillComponent.SetSkillRemainUseCount( SkillKind.Stealth, CurMP );
            }, 1.0f );
        }

        /// <summary>
        /// InputManager에게 키 입력 시 호출할 이벤트를 등록합니다..
        /// </summary>
        private void AddKeyPressEvent()
        {
            _eventManager.AddInputEvent( moveRightKey, OnMoveRightKeyPress );
            _eventManager.AddInputEvent( moveLeftKey, OnMoveLeftKeyPress );
            _eventManager.AddInputEvent( moveUpKey, OnMoveUpKeyPress );
            _eventManager.AddInputEvent( moveDownKey, OnMoveDownKeyPress );
            _eventManager.AddInputEvent( moveStopKey, OnMoveStopKeyPress );
            _eventManager.AddInputEvent( fireStunGunKey, OnPressFireStunGunKey );
            _eventManager.AddInputEvent( firePunchMissileKey, OnPressFirePunchMissileKey );
            _eventManager.AddInputEvent( fireCollectGoldBulletKey, OnPressFireCollectGoldBullet );
            _eventManager.AddInputEvent( fireKillMonsterBulletKey, OnPressFireKillMonsterBulletKey );
            _eventManager.AddInputEvent( stealthModeKey, OnPressStealthModeKey );
        }

        /// <summary>
        /// InputManager에게 키 입력 시 등록된 이벤트를 해제시킵니다..
        /// </summary>
        private void RemoveKeyPressEvent()
        {
            _eventManager.RemoveInputEvent( moveRightKey, OnMoveRightKeyPress );
            _eventManager.RemoveInputEvent( moveLeftKey, OnMoveLeftKeyPress );
            _eventManager.RemoveInputEvent( moveUpKey, OnMoveUpKeyPress );
            _eventManager.RemoveInputEvent( moveDownKey, OnMoveDownKeyPress );
            _eventManager.RemoveInputEvent( moveStopKey, OnMoveStopKeyPress );
            _eventManager.RemoveInputEvent( fireStunGunKey, OnPressFireStunGunKey );
            _eventManager.RemoveInputEvent( firePunchMissileKey, OnPressFirePunchMissileKey );
            _eventManager.RemoveInputEvent( fireCollectGoldBulletKey, OnPressFireCollectGoldBullet );
            _eventManager.RemoveInputEvent( fireKillMonsterBulletKey, OnPressFireKillMonsterBulletKey );
            _eventManager.RemoveInputEvent( stealthModeKey, OnPressStealthModeKey );
        }

        /// <summary>
        /// 오른쪽 움직임 키를 눌렀을 때 호출됩니다..
        /// </summary>
        private void OnMoveRightKeyPress()
        {
            //MoveDirection( 1, 0 );
            SetMoveDirection( 1, 0 );
        }

        /// <summary>
        /// 왼쪽 움직임 키를 눌렀을 때 호출됩니다..
        /// </summary>
        private void OnMoveLeftKeyPress()
        {
            //MoveDirection( -1, 0 );
            SetMoveDirection( -1, 0 );
        }

        /// <summary>
        /// 위쪽 움직임 키를 눌렀을 때 호출됩니다..
        /// </summary>
        private void OnMoveUpKeyPress()
        {
            //MoveDirection( 0, -1 );
            SetMoveDirection( 0, -1 );
        }

        /// <summary>
        /// 아래쪽 움직임 키를 눌렀을 때 호출됩니다..
        /// </summary>
        private void OnMoveDownKeyPress()
        {
            //MoveDirection( 0, 1 );
            SetMoveDirection( 0, 1 );
        }

        /// <summary>
        /// 움직임 멈춤 키를 눌렀을 때 호출됩니다..
        /// </summary>
        private void OnMoveStopKeyPress()
        {
            SetMoveDirection( 0, 0 );
        }

        /// <summary>
        /// 스턴 총알 발사 키를 눌렀을 때 호출됩니다..
        /// </summary>
        private void OnPressFireStunGunKey()
        {
            _skillComponent.UseSkill( SkillKind.FireStungun );
        }

        /// <summary>
        /// 펀치 미사일 발사 키를 눌렀을 때 호출됩니다..
        /// </summary>
        private void OnPressFirePunchMissileKey()
        {
            _skillComponent.UseSkill( SkillKind.FirePunchMissile );
        }

        /// <summary>
        /// 골드 수집기 발사 키를 눌렀을 때 호출됩니다..
        /// </summary>
        private void OnPressFireCollectGoldBullet()
        {
            _skillComponent.UseSkill( SkillKind.FireCollectGoldBullet );
        }

        /// <summary>
        /// 몬스터 죽이는 총알 발사 키를 눌렀을 때 호출됩니다..
        /// </summary>
        private void OnPressFireKillMonsterBulletKey()
        {
            _skillComponent.UseSkill( SkillKind.FireKillMonsterBullet );
        }

        /// <summary>
        /// 스텔스 모드 On/Off 발사 키를 눌렀을 때 호출됩니다..
        /// </summary>
        private void OnPressStealthModeKey()
        {
            _skillComponent.UseSkill( SkillKind.Stealth );
        }

        /// <summary>
        /// 움직이는 방향 변경 시 호출됩니다..
        /// </summary>
        private void SetMoveDirection(int dirX, int dirY)
        {
            if(_moveDirX != dirX || _moveDirY != dirY )
            {
                _movementComponent.Reset();
            }

            if ( IsCanGoPosition( _x + dirX, _y + dirY ) )
            {
                _moveDirX = dirX;
                _moveDirY = dirY;

                if(0 != _moveDirX || 0 != _moveDirY)
                {
                    _lookDirX = _moveDirX;
                    _lookDirY = _moveDirY;
                }
            }
            else
            {
                if ( 0 == _nextMoveDirX && 0 == _nextMoveDirY )
                {
                    _nextMoveDirX = dirX;
                    _nextMoveDirY = dirY;
                }
            }

            OnMoveCharacterEvent += OnSuccessMove;
        }

        /// <summary>
        /// 플레이어가 방향을 전환하고 첫 움직임에 성공할 때 호출됩니다..
        /// </summary>
        private void OnSuccessMove( Character character )
        {
            // 또 호출될 필요 없기 때문에 콜백 해제..
            OnMoveCharacterEvent -= OnSuccessMove;
        }

        /// <summary>
        /// 플레이어가 움직이는데 실패할 때 호출됩니다..
        /// </summary>
        private void OnFailedMove( Character character )
        {
            _moveDirX = 0;
            _moveDirY = 0;

            _nextMoveDirX = 0;
            _nextMoveDirY = 0;
        }

        /// <summary>
        /// 충돌 시 호출됩니다..
        /// </summary>
        /// <param name="collisionObjectInst"> 충돌된 상대 GameObject </param>
        public override void OnCollision( GameObject collisionObjectInst )
        {
            base.OnCollision( collisionObjectInst );

            // 충돌한 타입이 몬스터다..
            if ( typeof( Monster ) == collisionObjectInst.GetType() )
            {
                StageManager.Instance.OnPlayerDead();
            }
        }

        /// <summary>
        /// 방향이 있는지 검사합니다.
        /// </summary>
        /// <param name="dirX"> x 방향 </param>
        /// <param name="dirY"> y 방향 </param>
        /// <returns></returns>
        private bool IsDirNotZero(int dirX, int dirY)
        {
            return (0 != dirX || 0 != dirY);
        }

        /// <summary>
        /// 움직임 업데이트..
        /// </summary>
        private void UpdateMovement()
        {
            if ( IsDirNotZero(_moveDirX, _moveDirY) )
            {
                CheckNextDirCondition();

                SendMoveDirOrder( _moveDirX, _moveDirY );
            }
        }

        /// <summary>
        /// 다음 방향으로 변경해야하는지 검사..
        /// </summary>
        private void CheckNextDirCondition()
        {
            if ( IsDirNotZero( _nextMoveDirX, _nextMoveDirY ) )
            {
                if ( true == IsCanGoPosition( _x + _nextMoveDirX, _y + _nextMoveDirY ) )
                {
                    _moveDirX = _nextMoveDirX;
                    _moveDirY = _nextMoveDirY;

                    _lookDirX = _moveDirX;
                    _lookDirY = _moveDirY;

                    _nextMoveDirX = 0;
                    _nextMoveDirY = 0;
                }
            }
        }
    }
}
