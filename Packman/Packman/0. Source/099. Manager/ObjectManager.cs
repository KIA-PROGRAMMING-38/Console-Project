using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class ObjectManager : SingletonBase<ObjectManager>
    {
        private List<KeyValuePair<string, GameObject>> _gameObjects = new List<KeyValuePair<string, GameObject>>();

        public bool AddGameObject( string objectId, GameObject objectInstance )
        {
            // 이미 objectId를 사용하는 GameObject 인스턴스가 있는지 검사..
            if( null != GetGameObject<GameObject>( objectId ) )
            {
                return false;
            }

            _gameObjects.Add( new KeyValuePair<string, GameObject>( objectId, objectInstance ) );

            return true;
        }

        public void Update()
        {
            foreach ( var gameobject in _gameObjects )
            {
                gameobject.Value.Update();
            }
        }

        public void Render()
        {
            RenderManager.Instance.Render();
        }

        public void RemoveAll()
        {
            _gameObjects.Clear();
        }

        public void RemoveObject( string objectID )
        {
            foreach ( var gameobject in _gameObjects )
            {
                if( gameobject.Key == objectID )
                {
                    _gameObjects.Remove( gameobject );

                    return;
                }
            }
        }

        public GameObject GetGameObject(string objectId)
        {
            foreach ( var gameobject in _gameObjects )
            {
                if ( gameobject.Key == objectId )
                {
                    return gameobject.Value;
                }
            }

            return null;
        }

        public T GetGameObject<T>( string objectId ) where T : GameObject
        {
            return (T)GetGameObject( objectId );
        }
    }
}
