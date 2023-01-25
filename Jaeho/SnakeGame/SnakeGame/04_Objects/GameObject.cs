using System.Diagnostics;

namespace SnakeGame
{
    public class GameObject
    {
        public GameObject() 
        {
            _name = GetType().Name;
            _tag = GetType().Name;
            _position = new Vector2(0, 0);
            _components = new Dictionary<string, Component>();

            // 생성과 동시에 게임오브젝트매니저에 등록
            GameObjectManager.Instance.AddGameObject(this);
        }

        public GameObject(string name, string tag)
        {
            _name = name;
            _tag = tag;
            _position = new Vector2(0, 0);
            _components = new Dictionary<string, Component>();
            GameObjectManager.Instance.AddGameObject(this);
        }

        protected string    _tag;
        protected string    _name;
        protected Vector2   _position;
        protected Dictionary<string, Component> _components;

        #region Properties
        public string Tag   { get { return _tag; } set { _tag = value; } }
        public string Name  { get { return _name; } set { _name = value; } }
        public Vector2 Position { get { return _position; } set { _position = value; } }
        #endregion

        /// <summary>
        /// 컴포넌트를 추가해줍니다.
        /// </summary>
        /// <param name="component">추가할 컴포넌트</param>
        public void AddComponent(Component component)
        {
            string componentTypeName = component.GetType().Name;

            bool isSuccess = _components.TryAdd(componentTypeName, component);
            Debug.Assert(isSuccess, "Cant Add Component");

            component.Name= componentTypeName;
            component.Owner = this;
        }

        /// <summary>
        /// 가지고 있는 컴포넌트 중 이름이 같은 컴포넌트를 반환
        /// </summary>
        /// <typeparam name="T">Component클래스를 상속받은 클래스만 가능</typeparam>
        /// <param name="name">가져올 컴포넌트 이름</param>
        /// <returns>컴포넌트</returns>
        public T GetComponent<T>(string name) where T : Component
        {
            Component component;
            bool isSuccess = _components.TryGetValue(name, out component);
            
            Debug.Assert(isSuccess, "Cant Add GetComponent");

            T result = (T)component;
            return result;
        }

        /// <summary>
        /// T타입과 같은 컴포넌트를 반환
        /// </summary>
        /// <typeparam name="T">Component클래스를 상속받은 클래스만 가능</typeparam>
        /// <returns>컴포넌트</returns>
        public T GetComponent<T>() where T : Component
        {
            Component component;
            bool isSuccess = _components.TryGetValue(typeof(T).Name, out component);

            Debug.Assert(isSuccess, "Cant Add GetComponent");

            T result = (T)component;
            
            return result;
        }

        protected void StartComponents()
        {
            foreach (KeyValuePair<string, Component> comp in _components)
            {
                comp.Value.Start();
            }
        }
        protected void UpdateComponents()
        {
            foreach (KeyValuePair<string, Component> comp in _components)
            {
                comp.Value.Update();
            }
        }

        /// <summary>
        /// 충돌했을 때 호출될 함수
        /// </summary>
        /// <param name="sender"></param>
        public virtual void OnCollision(object? sender = null)
        {

        }

        public virtual void Start() 
        {
        }

        public virtual void Update() 
        {

        }

        public virtual void Render()
        {

        }
    }
}
