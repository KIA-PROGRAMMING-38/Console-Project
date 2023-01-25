using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Monster : Character
    {
        public enum State
        {
            Default,        // 기본( 패턴 진행중 )..
            Stun,           // 스턴..
            ForcedToPush,   // 강제로 밀리는중..
        }

        public enum PatternKind
        {
			Begin,
			Wait,
			Idle,
            Move,
            ChaseTarget,
		}

        // 플레이어 인스턴스..
        private Player _player = null;

		// 경로 관련..
		private List<Point2D> _paths = new List<Point2D>();
        private int _curPathIndex = 0;

        // 현재 몬스터의 상태..
        private State _curState = State.Default;
        // 상태와 관련된 정보들..
        private float _stunEffectiveTime = 0.0f;    // 몇 초 동안 스턴 걸릴것인가..
        private float _forcedPushPower = 0.0f;    // 얼마나 날라갈 것인가..
        private float _forcedPushPowerMinus = 0.0f;    // 얼마나 날라갈 것인가..
        private float _forcedPushX = 0.0f;
        private float _forcedPushY = 0.0f;
        private int _forcedPushStartX = 0;
        private int _forcedPushStartY = 0;
        // AI 와 관련된 정보들..
        private PatternKind _prevPatternKind = PatternKind.Begin;
        private PatternKind _patternKind = PatternKind.Begin;
        // 패턴을 진행할 수 없는가..
        private bool _isCanActPattern = false;
        // WayPointGroup..
        private  WayPointGroup _wayPointGroup = null;
        // 현재 찾아가야할 WayPoint..
        private WayPoint _curDestinationWayPoint = null;

        // 타겟 인식 범위..
        private int _targetRecognizeRange = 5;

        public List<Point2D> Paths { get { return _paths; } }
        

        public Monster( int x, int y, Map map )
            : base( x, y, Constants.MONSTER_IMAGE, Constants.MONSTER_COLOR, Constants.MONSTER_RENDER_ORDER, map, Constants.MONSTER_MOVE_DELAY )
        {
            _player = _objectManager.GetGameObject<Player>( "Player" );
        }

        public void Initialize()
        {
            Debug.Assert( base.Initialize() );

            OnMoveCharacterEvent += OnSuccessMove;
        }

        public override void Update()
        {
            base.Update();

            return;

            switch( _curState )
            {
                case State.Default:
                    UpdateDefaultState();

                    break;
                case State.Stun:
                    UpdateStunState();

                    break;
                case State.ForcedToPush:
                    UpdateForcedToPushPower();

                    break;
            }
        }

		public override void Render()
		{
			base.Render();
		}

        public void OnDead()
        {

        }

        public void OnChangedStunState( float stunEffectiveTime )
        {
            ChangeState( State.Stun );
            _stunEffectiveTime = stunEffectiveTime;
        }

        public void OnCHangedForcedToPushState( int dirX, int dirY, float forcedPushPower )
        {
            _forcedPushPower = forcedPushPower;
            _forcedPushPowerMinus = forcedPushPower * 4.0f;

            _dirX = dirX;
            _dirY = dirY;

            if ( 0 != _dirX )
            {
                _dirX /= Math.Abs( _dirX );
            }
            if ( 0 != _dirY )
            {
                _dirY /= Math.Abs( _dirY );
            }

            ChangeState( State.ForcedToPush );
        }

        private void ChangeState( State _newState )
        {
            _curState = _newState;

            switch ( _curState )
            {
                case State.Default:
                    SetPatternKind( _patternKind );
                    _image = Constants.MONSTER_IMAGE;
                    _color = Constants.MONSTER_COLOR;
                    break;
                case State.Stun:
                    _image = Constants.MONSTER_STUN_STATE_IMAGE;
                    _color = Constants.MONSTER_STUN_STATE_COLOR;
                    break;
                case State.ForcedToPush:
                    _image = Constants.MONSTER_STUN_STATE_IMAGE;
                    _color = Constants.MONSTER_FORCED_PUSH_STATE_COLOR;
                    _forcedPushX = _forcedPushY = 0.0f;
                    _movementComponent.Reset();

                    _forcedPushStartX = _x;
                    _forcedPushStartY = _y;
                    break;
            }
        }

        /// <summary>
        /// 기본 상태일 때 Update 에서 실행될 함수..
        /// </summary>
        private void UpdateDefaultState()
        {
            switch ( _patternKind )
            {
                case PatternKind.Begin:
                    ActBeginPattern();
                    _color = ConsoleColor.Green;

                    break;
                case PatternKind.Wait:
                    ActWaitPattern();

                    break;
                case PatternKind.Idle:
                    ActIdlePattern();

                    _color = ConsoleColor.Blue;

                    break;
                case PatternKind.ChaseTarget:
                    ActChaseTargetPattern();

                    _color = ConsoleColor.Red;

                    break;
            }
        }

        /// <summary>
        /// Stun 상태일 때 Update 에서 실행될 함수..
        /// </summary>
        private void UpdateStunState()
        {
            _stunEffectiveTime -= TimeManager.Instance.ElaspedTime;
            if ( _stunEffectiveTime <= 0.0f )
            {
                ChangeState( State.Default );
            }
        }

        /// <summary>
        /// ForcedToPush 상태일 때 Update 에서 실행될 함수..
        /// </summary>
        private void UpdateForcedToPushPower()
        {
            if ( _forcedPushPower <= 0.0f )
            {
                ChangeState( State.Default );

                return;
            }

            _forcedPushX += _dirX * _forcedPushPower * TimeManager.Instance.ElaspedTime;
            _forcedPushY += _dirY * _forcedPushPower * TimeManager.Instance.ElaspedTime;

            _forcedPushPower -= TimeManager.Instance.ElaspedTime * _forcedPushPowerMinus;

            int moveDestinationX = _forcedPushStartX + (int)_forcedPushX;
            int moveDestinationY = _forcedPushStartY + (int)_forcedPushY;

            if ( IsCanGoPosition( moveDestinationX, moveDestinationY ) )
            {
                _renderManager.ReserveRenderRemove( _x, _y, 1 );

                _prevX = _x;
                _prevY = _y;

                _x = moveDestinationX;
                _y = moveDestinationY;

                OnMoveCharacterEvent?.Invoke( this );
            }
            else
            {
                ChangeState( State.Default );
            }
        }
        

		// ====================================================================================================================================
		// ================================================= AI Pattern 별로 실행되는 함수들..  =================================================
		// ====================================================================================================================================
		private void ActBeginPattern()
        {
            float waitTime = 1.0f;// (float)RandomManager.Instance.GetRandomDouble(2.0, 3.0);

            EventManager.Instance.SetTimeOut( OnEnablePattern, waitTime );

            if ( null == _wayPointGroup )
            {
                _wayPointGroup = _objectManager.GetGameObject<WayPointGroup>();
            }

            _curDestinationWayPoint = _wayPointGroup.FindNearWayPoint( this );
            ActionFindPath( _curDestinationWayPoint.X, _curDestinationWayPoint.Y );

            SetPatternKind( PatternKind.Wait );
        }

        private void ActWaitPattern()
        {
			if ( true == _isCanActPattern )
			{
				SetPatternKind( PatternKind.Idle );
			}
		}

        private void ActIdlePattern()
        {
			if ( _paths.Count > _curPathIndex )
			{
                StartMoveAction();
			}
			else
			{
				int findIndex = RandomManager.Instance.GetRandomNumber( _curDestinationWayPoint.NearPointCount - 1 );
				_curDestinationWayPoint = _curDestinationWayPoint.GetNearPoint( findIndex );
				ActionFindPath( _curDestinationWayPoint.X, _curDestinationWayPoint.Y );
			}

            FindTarget();
		}

        private void ActChaseTargetPattern()
        {
			ActionFindPath( _player.X, _player.Y );
			StartMoveAction();
		}

        private void StartMoveAction()
        {
            MoveToNextPath();
        }

		// ====================================================================================================================================
		// ============================================= AI Pattern 에서 필요로 하는 함수들..  ==================================================
		// ====================================================================================================================================
		private void ActionFindPath(int destinationX, int destinationY)
        {
            _paths.Clear();

            if( true == PathFinder.ComputePath( _map, _x, _y, destinationX, destinationY, ref _paths ) )
            {
                _curPathIndex = 0;
            }
        }

        private void MoveToNextPath()
        {
            if( _curPathIndex >= _paths.Count )
            {
                return;
            }

            int dirX = _paths[_curPathIndex].X - _x;
            int dirY = _paths[_curPathIndex].Y - _y;

            SendMoveDirOrder( dirX, dirY );
        }

		private void FindTarget()
        {
            // 몬스터가 플레이어를 바라보는 방향 구하기..
            int monsterLookPlayerDirX = _player.X - _x;
            int monsterLookPlayerDirY = _player.Y - _y;

            int monsterLookPlayerDirDist = Math.Abs(monsterLookPlayerDirX) + Math.Abs(monsterLookPlayerDirY);

            if(_targetRecognizeRange >= monsterLookPlayerDirDist)
            {
                SetPatternKind( PatternKind.ChaseTarget );
            }

			//int monsterLookPlayerDirDist = 0;
			//
			//// 방향 크기 1로 조절..
			//if (0 != monsterLookPlayerDirX )
			//{
			//    monsterLookPlayerDirDist = Math.Abs( monsterLookPlayerDirX );
			//	monsterLookPlayerDirX /= monsterLookPlayerDirDist;
			//}
			//if ( 0 != monsterLookPlayerDirY )
			//{
			//	monsterLookPlayerDirDist = Math.Abs( monsterLookPlayerDirY );
			//	monsterLookPlayerDirY /= monsterLookPlayerDirDist;
			//}
			//
			//// 몬스터가 플레이어를 바라보는 방향과 현재 몬스터가 움직이는 방향이 같다면 그리고 인식 범위라면..
			//if( _dirX == monsterLookPlayerDirX && _dirY == monsterLookPlayerDirY && _targetRecognizeRange >= monsterLookPlayerDirDist )
			//{
			//    
			//}
		}

        private void OnSuccessMove( Character character )
        {
            ++_curPathIndex;
        }

		private void OnEnablePattern()
        {
            _isCanActPattern = true;
        }

		private void OnDisablePattern()
		{
            _isCanActPattern = false;
		}

        private void SetPatternKind( PatternKind newPatternKind )
        {
            _prevPatternKind = _patternKind;
			_patternKind = newPatternKind;

            switch( _patternKind)
            {
				case PatternKind.Begin:
					break;

				case PatternKind.Wait:
					break;

				case PatternKind.Idle:
                    _movementComponent.Reset();
                    break;

				case PatternKind.Move:
					break;

				case PatternKind.ChaseTarget:
                    _movementComponent.Reset();
                    if ( true == _movementComponent.IsWaitMove )
                        ActionFindPath( _player.X, _player.Y );
					break;
			}
        }
	}
}
