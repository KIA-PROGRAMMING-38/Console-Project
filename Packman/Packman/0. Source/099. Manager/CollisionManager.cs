using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class CollisionManager : SingletonBase<CollisionManager>
    {
        Player _player;
        Monster[] _monsters;
        Item[] _items;
        Projectile[] _projectiles;

        public void RenewObjectInstance()
        {
            _player = ObjectManager.Instance.GetGameObject<Player>();
            RenewMonsterInstance();
            RenewItemInstance();
            RenewProjectileInstance();
        }

        public void RenewMonsterInstance()
        {
            _monsters = ObjectManager.Instance.GetAllGameObject<Monster>();
        }

        public void RenewItemInstance()
        {
            _items = ObjectManager.Instance.GetAllGameObject<Item>();
        }

        public void RenewProjectileInstance()
        {
            _projectiles = ObjectManager.Instance.GetAllGameObject<Projectile>();
        }

        public void Update()
        {
            RenewObjectInstance();

			// 각각의 오브젝트들끼리 충돌 검사..
			UpdateCollisionPlayerToMonster();
            UpdateCollisionPlayerToItem();
			UpdateCollisionProjectileToMonster();
        }

        private void UpdateCollisionPlayerToMonster()
        {
			if ( null != _monsters )
			{
				if ( !_player.IsStealthMode )
				{
					foreach ( Monster monster in _monsters )
					{
						if ( true == monster.IsDead )
						{
							continue;
						}

						if ( true == CollisionHelper.CollisionObjectToObject( _player, monster ) )
						{
							_player.OnCollision( monster );
							monster.OnCollision( _player );

							break;
						}
					}
				}
			}
		}

        private void UpdateCollisionPlayerToItem()
        {
			if ( null != _items )
			{
				foreach ( Item item in _items )
				{
					if ( true == CollisionHelper.CollisionObjectToObject( _player, item ) )
					{
						_player.OnCollision( item );
						item.OnCollision( _player );
					}
				}
			}
		}

        private void UpdateCollisionProjectileToMonster()
        {
			if ( null != _projectiles && null != _monsters )
			{
				List<GameObject> collisionObjects = new List<GameObject>();

				foreach ( var projectile in _projectiles )
				{
					foreach ( var monster in _monsters )
					{
						if ( true == monster.IsDead )
						{
							continue;
						}

						// 현재 위치가 같은지 검사..
						bool isSamePosition = CollisionHelper.CollisionObjectToObject(projectile, monster);

						// projectile 이전위치 == monster 현재위치 && projectile 현재위치 == monster 이전 위치
						// 위의 조건이 성립하는 경우는 projectile 과 monster 가 자리가 뒤바뀐 것..
						bool isCheckPrevPosition = CollisionHelper.IsSamePosition(projectile.PrevX, projectile.PrevY, monster.X, monster.Y) &&
												   CollisionHelper.IsSamePosition(projectile.X, projectile.Y, monster.PrevX, monster.PrevY);

						if ( isSamePosition || isCheckPrevPosition )
						{
							collisionObjects.Add( projectile );
							collisionObjects.Add( monster );
						}
					}

					for ( int index = 0; index < collisionObjects.Count; index += 2 )
					{
						collisionObjects[index].OnCollision( collisionObjects[index + 1] );
						collisionObjects[index + 1].OnCollision( collisionObjects[index] );
					}
				}
			}
		}
    }
}
