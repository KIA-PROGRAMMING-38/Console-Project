using System.Diagnostics;

namespace SnakeGame
{
    public class GameObjectManager : LazySingleton<GameObjectManager>
    {
        public GameObjectManager() 
        {
            _gameObjects = new List<GameObject>();
            _removePendingList = new HashSet<GameObject>();
        }

        public int GameObjectCount { get { return _gameObjects.Count; } }

        private List<GameObject> _gameObjects = new List<GameObject>();
        private HashSet<GameObject> _removePendingList;
        
        /// <summary>
        /// 게임오브젝트리스트에 게임오브젝트를 추가해줍니다.
        /// </summary>
        /// <param name="gameObject"></param>
        public void AddGameObject(GameObject gameObject)
        {
            Debug.Assert(gameObject != null, $"Already include same GameObject // ObjectName : {gameObject.Name}");
            _gameObjects.Add(gameObject);

            // 게임오브젝트를 추가하면 Start 해줌
            gameObject.Start();
        }

        public void Clear()
        {
            ColliderManager.Instance.Clear();
            RenderManager.Instance.Clear();
            _gameObjects.Clear();
        }

        public void Start()
        {
            foreach (var objects in _gameObjects)
            {
                objects.Start();
            }
        }

        /// <summary>
        /// 게임오브젝트를 파괴 시킵니다.
        /// </summary>
        /// <param name="gameObject">파괴시킬 오브젝트</param>
        public void Destroy(GameObject gameObject)
        {
            ColliderManager.Instance.RemoveCollider(gameObject.GetComponent<Collider>());
            RenderManager.Instance.RemoveRenderer(gameObject.GetComponent<Renderer>());
            _removePendingList.Add(gameObject);
        }

        public void Update()
        {
            for(int i = 0;i <_gameObjects.Count; ++i)
            {
                _gameObjects[i].Update();
            }
            _gameObjects.RemoveAll(_removePendingList.Contains);
            _removePendingList.Clear();
        }

        /// <summary>
        /// 이름이 같은 게임오브젝트 반환
        /// </summary>
        /// <param name="name">찾을 게임오브젝트의 이름</param>
        /// <returns>이름이 같은 게임오브젝트 반환 없으면 null 반환</returns>
        public GameObject? FindGameObjectWithName(string name)
        {
            GameObject? gameObject = null;
            gameObject = _gameObjects.Find(x => x.Name == name);
            return gameObject;
        }

        /// <summary>
        /// 게임오브젝트의 이름과 같은 게임오브젝트들을 배열로 반환
        /// </summary>
        /// <param name="name">찾을 게임오브젝트의 이름</param>
        /// <returns>이름이 같은 게임오브젝트들 반환 없으면 null반환</returns>
        public GameObject[]? FindAllGameObjectWithName(string name)
        {
            return _gameObjects.Where(x => x.Name == name).ToArray();
        }

        /// <summary>
        /// 태그가 같은 게임오브젝트를 반환
        /// </summary>
        /// <param name="name">찾을 게임오브젝트의 태그</param>
        /// <returns>태그가 같은 게임오브젝트 반환 없으면 null 반환</returns>
        public GameObject? FindGameObjectWithTag(string tag)
        {
            GameObject? gameObject = null;
            gameObject = _gameObjects.Find(x => x.Tag == tag);
            return gameObject;
        }

        /// <summary>
        /// 게임오브젝트의 태그와 같은 게임오브젝트들을 배열로 반환
        /// </summary>
        /// <param name="name">찾을 게임오브젝트의 태그</param>
        /// <returns>태그가 같은 게임오브젝트들</returns>
        public GameObject[]? FindAllGameObjectWithTag(string tag)
        {
            return _gameObjects.Where(x => x.Tag == tag).ToArray();
        }


        public void Release()
        {
            _gameObjects.Clear();
            _gameObjects = null;
        }
    
    }
}
