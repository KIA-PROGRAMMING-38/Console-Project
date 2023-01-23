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

        private LinkedList<KeyValuePair<string, GameObject>> _removeObjects = new LinkedList<KeyValuePair<string, GameObject>>();

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

        /// <summary>
        /// 등록된 오브젝트들 갱신..
        /// </summary>
        public void Update()
        {
            // 삭제할 오브젝트들 제거작업..
            foreach ( var gameobject in _removeObjects )
            {
                gameobject.Value.Release();
                _gameObjects.Remove( gameobject );
            }
            _removeObjects.Clear();

            // 현재 오브젝트들 갱신..
            foreach ( var gameobject in _gameObjects )
            {
                if( gameobject.Value.IsActive )
                {
                    gameobject.Value.Update();
                }
            }
        }

        /// <summary>
        /// 그려야 할 오브젝트들 Render 작업 개시..
        /// </summary>
        public void Render()
        {
            RenderManager.Instance.Render();
        }

        /// <summary>
        /// 등록된 모든 오브젝트 제거..
        /// </summary>
        public void RemoveAll()
        {
            foreach ( var gameobject in _gameObjects )
            {
                gameobject.Value.Release();
            }
            _gameObjects.Clear();
        }

        /// <summary>
        /// objectID 인 오브젝트 검사 후 제거
        /// </summary>
        /// <param name="objectID"> 제거할 object의 ID </param>
        public void RemoveObject( string objectID )
        {
            foreach ( var gameobject in _gameObjects )
            {
                if( gameobject.Key == objectID )
                {
                    _removeObjects.AddLast( gameobject );
                    //_gameObjects.Remove( gameobject );

                    return;
                }
            }
        }

        /// <summary>
        /// removeGameObject 와 같은 오브젝트 검사 후 제거..
        /// </summary>
        /// <param name="removeGameObject"> 제거할 오브젝트 인스턴스 </param>
        public void RemoveObject(GameObject removeGameObject)
        {
            foreach ( var gameobject in _gameObjects )
            {
                if ( gameobject.Value == removeGameObject )
                {
                    _removeObjects.AddLast( gameobject );
                    //_gameObjects.Remove( gameobject );

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

        /// <summary>
        /// 모든 T 타입인 GameObject를 찾습니다.
        /// </summary>
        /// <typeparam name="T"> 찾으려는 타입 </typeparam>
        /// <returns></returns>
        public T[] GetAllGameObject<T>() where T : GameObject
        {
            Type findType = typeof(T);  // 찾으려는 타입..
            LinkedList<T> findTypeInstanceList = new LinkedList<T>();

            // 모든 오브젝트들 순회하면서 값 담아둠..
            foreach ( var gameobject in _gameObjects )
            {
                if ( gameobject.Value.GetType() == findType )
                {
                    findTypeInstanceList.AddLast( (T)gameobject.Value );
                }
            }

            // 만약 찾은게 없다면..
            if(findTypeInstanceList.Count <= 0)
            {
                return null;
            }

            // 찾은게 있다면 배열에 담아서 리턴..
            T[] returnArray = new T[findTypeInstanceList.Count];
            int arrayIndex = 0;
            foreach(T instance in findTypeInstanceList )
            {
                returnArray[arrayIndex++] = instance;
            }

            return returnArray;
        }
    }
}
