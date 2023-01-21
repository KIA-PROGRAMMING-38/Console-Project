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

        /// <summary>
        /// ObjectID를 가지고 GameObject를 찾아서 반환합니다..
        /// </summary>
        /// <param name="objectId"> 찾으려는 오브젝트의 ID </param>
        /// <returns> 찾은 경우 GameObject, 못 찾은 경우 null </returns>
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

        /// <summary>
        /// ObjectID를 가지고 GameObject를 찾고 T 타입으로 형변한하고 반환합니다..
        /// </summary>
        /// <typeparam name="T"> 형변환할 타입 </typeparam>
        /// <param name="objectId"> 찾으려는 오브젝트의 ID </param>
        /// <returns> 찾은 경우 GameObject를 T 타입으로 형변환, 못 찾은 경우 null </returns>
        public T GetGameObject<T>( string objectId ) where T : GameObject
        {
            return (T)GetGameObject( objectId );
        }

        /// <summary>
        /// 가장 먼저 들어온 T 타입인 GameObject를 찾습니다.
        /// </summary>
        /// <typeparam name="T"> 찾으려는 타입 </typeparam>
        /// <returns></returns>
        public T GetGameObject<T>() where T : GameObject
        {
            Type findType = typeof(T);  // 찾으려는 타입..

            foreach ( var gameobject in _gameObjects )
            {
                if( gameobject.Value.GetType() == findType )
                {
                    return (T)gameobject.Value;
                }
            }

            return null;
        }
    }
}
