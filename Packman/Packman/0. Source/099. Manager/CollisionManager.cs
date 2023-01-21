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
        GoldGroup _goldGroup;

        public void RenewObjectInstance()
        {
            _player = ObjectManager.Instance.GetGameObject<Player>();
            _monsters = ObjectManager.Instance.GetAllGameObject<Monster>();
            _items = ObjectManager.Instance.GetAllGameObject<Item>();
            _goldGroup = ObjectManager.Instance.GetGameObject<GoldGroup>();
        }

        public void Update()
        {

        }
    }
}
