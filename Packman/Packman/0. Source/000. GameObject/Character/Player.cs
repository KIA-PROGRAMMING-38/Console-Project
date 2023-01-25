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
        private const ConsoleKey fireStunGunKey = ConsoleKey.Q;
        private const ConsoleKey firePunchMissileKey = ConsoleKey.W;
        private const ConsoleKey fireCollectGoldBulletKey = ConsoleKey.E;
        private const ConsoleKey fireKillMonsterBulletKey = ConsoleKey.R;

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

        public int CurMP { get { return _curMP; } }
        public int MaxMP { get { return _maxMP; } }
        public int LookDirX { get { return _lookDirX; } }
        public int LookDirY { get { return _lookDirY; } }

        public Player( int x, int y, Map map )
            : base( x, y, Constants.PLAYER_IMAGE, Constants.PLAYER_COLOR, Constants.PLAYER_RENDER_ORDER, map, Constants.PLAYER_MOVE_DELAY )
        {
            _skillComponent = new PlayerSkill();
            AddComponent( "Skill", _skillComponent );

            for( int i = 0; i < 100; ++i )
            {
                _skillComponent.AddSkill( SkillKind.FireStungun );
                _skillComponent.AddSkill( SkillKind.FirePunchMissile );
                _skillComponent.AddSkill( SkillKind.FireKillMonsterBullet );
                _skillComponent.AddSkill( SkillKind.FireCollectGoldBullet );
            }
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

            UpdateMovement();
        }

        public override void Release()
        {
            base.Release();

            RemoveKeyPressEvent();
        }

        public void AddSkill( SkillKind skillKind )
        {
            _skillComponent.AddSkill( skillKind );
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
            _eventManager.AddInputEvent( fireStunGunKey, OnPressFireStunGunKey );
            _eventManager.AddInputEvent( firePunchMissileKey, OnPressFirePunchMissileKey );
            _eventManager.AddInputEvent( fireCollectGoldBulletKey, OnPressFireCollectGoldBullet );
            _eventManager.AddInputEvent( fireKillMonsterBulletKey, OnPressFireKillMonsterBulletKey );
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
            _eventManager.RemoveInputEvent( fireStunGunKey, OnPressFireStunGunKey );
            _eventManager.RemoveInputEvent( firePunchMissileKey, OnPressFirePunchMissileKey );
            _eventManager.RemoveInputEvent( fireCollectGoldBulletKey, OnPressFireCollectGoldBullet );
            _eventManager.RemoveInputEvent( fireKillMonsterBulletKey, OnPressFireKillMonsterBulletKey );
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

        private void OnPressFireStunGunKey()
        {
            _skillComponent.UseSkill( SkillKind.FireStungun );
        }

        private void OnPressFirePunchMissileKey()
        {
            _skillComponent.UseSkill( SkillKind.FirePunchMissile );
        }

        private void OnPressFireCollectGoldBullet()
        {
            _skillComponent.UseSkill( SkillKind.FireCollectGoldBullet );
        }

        private void OnPressFireKillMonsterBulletKey()
        {
            _skillComponent.UseSkill( SkillKind.FireKillMonsterBullet );
        }

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

                _lookDirX = _moveDirX;
                _lookDirY = _moveDirY;
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

        private void OnFailedMove( Character character )
        {
            _moveDirX = 0;
            _moveDirY = 0;

            _nextMoveDirX = 0;
            _nextMoveDirY = 0;
        }

        public override void OnCollision( GameObject collisionObjectInst )
        {
            base.OnCollision( collisionObjectInst );

            // 충돌한 타입이 몬스터다..
            if ( typeof( Monster ) == collisionObjectInst.GetType() )
            {
                StageManager.Instance.OnPlayerDead();
            }
        }

        private bool IsDirNotZero(int dirX, int dirY)
        {
            return (0 != dirX || 0 != dirY);
        }

        private void UpdateMovement()
        {
            if ( IsDirNotZero(_moveDirX, _moveDirY) )
            {
                CheckNextDirCondition();

                SendMoveDirOrder( _moveDirX, _moveDirY );
            }
        }

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
