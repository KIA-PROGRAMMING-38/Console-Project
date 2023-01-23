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
            _monsters = ObjectManager.Instance.GetAllGameObject<Monster>();
            RenewItemInstance();
            RenewProjectileInstance();
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
            // Collision Player To Monster..
            if(null != _monsters )
            {
                foreach ( Monster monster in _monsters )
                {
                    if ( true == CollisionHelper.CollisionObjectToObject( _player, monster ) )
                    {
                        _player.OnCollision( monster );
                        monster.OnCollision( _player );

                        break;
                    }
                }
            }

            // Collision Player To Item..
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
    }
}
