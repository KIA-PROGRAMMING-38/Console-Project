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
        private HashSet<GameObject> _removePendingList = new HashSet<GameObject>();
        

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
                _gameObjects[i]?.Update();
            }
            _gameObjects.RemoveAll(_removePendingList.Contains);
            _removePendingList.Clear();
        }


        public GameObject? FindGameObjectWithName(string name)
        {
            GameObject? gameObject = null;
            gameObject = _gameObjects.Find(x => x.Name == name);
            return gameObject;
        }

        public GameObject[]? FindAllGameObjectWithName(string name)
        {
            return _gameObjects.Where(x => x.Name == name).ToArray();
        }

        public GameObject[]? FindAllGameObjectWithTag(string tag)
        {
            return _gameObjects.Where(x => x.Tag == tag).ToArray();
        }

        public GameObject? FindGameObjectWithTag(string tag)
        {
            GameObject? gameObject = null;
            gameObject = _gameObjects.Find(x => x.Tag == tag);
            return gameObject;
        }

        public void Release()
        {
            _gameObjects.Clear();
        }
    
    }
}
