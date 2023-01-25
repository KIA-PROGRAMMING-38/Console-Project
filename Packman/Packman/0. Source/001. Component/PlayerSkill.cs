using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class PlayerSkill : Component
    {
        public enum SkillKind
        {
            FireStungun,
            FirePunchMissile,
            FireCollectGoldBullet,
            FireKillMonsterBullet,
            End,
        }

        private struct SkillInfo
        {
            public SkillKind Kind;
            public int RemainUseCount;
        }

        Player _player = null;

        SkillInfo[] _skillInfoes = null;

        private int _stunBulletID = 0;
        private int _punchMissileID = 0;
        private int _collectGoldBulletID = 0;
        private int _monsterKillBulletID = 0;

        public PlayerSkill()
        {
            _skillInfoes = new SkillInfo[(int)SkillKind.End];
            for(int index = 0; index < _skillInfoes.Length; ++index ) 
            {
                _skillInfoes[index].Kind = (SkillKind)index;
                _skillInfoes[index].RemainUseCount = 0;
            }
        }

        public void AddSkill( SkillKind skillKind )
        {
            ++_skillInfoes[(int)skillKind].RemainUseCount;
        }

        public void SetSkillRemainUseCount(SkillKind skillKind, int remainUseCount ) 
        {
            _skillInfoes[(int)skillKind].RemainUseCount = remainUseCount;
        }

        public void UseSkill( SkillKind skillKind )
        {
            if ( 0 >= _skillInfoes[(int)skillKind].RemainUseCount )
            {
                return;
            }

            if(null == _player )
            {
                _player = (Player)_gameObject;
                Debug.Assert( null != _player );
            }

            switch ( skillKind )
            {
                case SkillKind.FireStungun:
                    OnUseFireStungunSkill();

                    break;
                case SkillKind.FirePunchMissile:
                    OnUsePunchSkill();

                    break;

                case SkillKind.FireCollectGoldBullet:
                    OnUseFireCollectGoldBulletSkill();
                    break;

                case SkillKind.FireKillMonsterBullet:
                    OnUseFireKillMonsterBulletSKill();
                    break;
                default:
                    return;
            }

            --_skillInfoes[(int)skillKind].RemainUseCount;
        }

        /// <summary>
        /// FireStungun 스킬을 사용 시 호출됩니다..
        /// </summary>
        private void OnUseFireStungunSkill()
        {
            Projectile projectile = CreateStunBullet();
            Debug.Assert( null != projectile );
            Debug.Assert( ObjectManager.Instance.AddGameObject( $"Bullet_{_stunBulletID:D2}", projectile ) );
            ++_stunBulletID;

            CollisionManager.Instance.RenewProjectileInstance();
        }

        /// <summary>
        /// Punch 스킬을 사용 시 호출됩니다..
        /// </summary>
        private void OnUsePunchSkill()
        {
            Projectile projectile = CreatePunchMissile();
            Debug.Assert( null != projectile );
            Debug.Assert( ObjectManager.Instance.AddGameObject( $"PunchMissile_{_punchMissileID:D2}", projectile ) );
            ++_punchMissileID;

            CollisionManager.Instance.RenewProjectileInstance();
        }

        private void OnUseFireCollectGoldBulletSkill()
        {
            Projectile projectile = CreateCollectGoldBullet();
            Debug.Assert( null != projectile );
            Debug.Assert( ObjectManager.Instance.AddGameObject( $"CollectGoldBullet_{_collectGoldBulletID:D2}", projectile ) );
            ++_collectGoldBulletID;

            CollisionManager.Instance.RenewProjectileInstance();
        }

        private void OnUseFireKillMonsterBulletSKill()
        {
            Projectile projectile = CreateMonsterKillBullet();
            Debug.Assert( null != projectile );
            Debug.Assert( ObjectManager.Instance.AddGameObject( $"MonsterKillBullet_{_monsterKillBulletID:D2}", projectile ) );
            ++_monsterKillBulletID;

            CollisionManager.Instance.RenewProjectileInstance();
        }

        /// <summary>
        /// StunBullet 을 생성한 다음 초기화한 뒤 반환합니다..
        /// </summary>
        /// <returns> 생성한 StunBullet 인스턴스 </returns>
        private StunBullet CreateStunBullet()
        {
            StunBullet projectile = new StunBullet(_player.X, _player.Y, _player.LookDirX, _player.LookDirY);
            Debug.Assert( projectile.Initialize() );

            return projectile;
        }
        
        /// <summary>
        /// PunchMissile 을 생성한 다음 초기화한 뒤 반환합니다..
        /// </summary>
        /// <returns> 생성한 PunchMissile 인스턴스 </returns>
        private PunchMissile CreatePunchMissile()
        {
            PunchMissile projectile = new PunchMissile(_player.X, _player.Y, _player.LookDirX, _player.LookDirY);
            Debug.Assert( projectile.Initialize() );

            return projectile;
        }

        /// <summary>
        /// CollectGoldBullet 을 생성한 다음 초기화한 뒤 반환합니다..
        /// </summary>
        /// <returns> 생성한 CollectGoldBullet 인스턴스 </returns>
        private CollectGoldBullet CreateCollectGoldBullet()
        {
            CollectGoldBullet projectile = new CollectGoldBullet(_player.X, _player.Y, _player.LookDirX, _player.LookDirY);
            Debug.Assert( projectile.Initialize() );

            return projectile;
        }

        /// <summary>
        /// MonsterKillBullet 을 생성한 다음 초기화한 뒤 반환합니다..
        /// </summary>
        /// <returns> 생성한 MonsterKillBullet 인스턴스 </returns>
        private MonsterKillBullet CreateMonsterKillBullet()
        {
            MonsterKillBullet projectile = new MonsterKillBullet(_player.X, _player.Y, _player.LookDirX, _player.LookDirY);
            Debug.Assert( projectile.Initialize() );

            return projectile;
        }
    }
}
