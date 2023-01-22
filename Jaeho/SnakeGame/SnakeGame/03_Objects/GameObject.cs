using System.Diagnostics;

namespace SnakeGame
{
    public class GameObject
    {
        public GameObject() 
        {
            _name = GetType().Name;
            _tag = GetType().Name;
            //_tag = string.Empty;
            _position = new Vector2(0, 0);
            _components = new Dictionary<string, Component>();
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

        public void AddComponent(Component component)
        {
            string componentTypeName = component.GetType().Name;

            bool isSuccess = _components.TryAdd(componentTypeName, component);
            Debug.Assert(isSuccess, "Cant Add Component");

            component.Name= componentTypeName;
            component.Owner = this;
        }

        public T GetComponent<T>(string name) where T : Component
        {
            Component component;
            bool isSuccess = _components.TryGetValue(name, out component);
            
            Debug.Assert(isSuccess, "Cant Add GetComponent");

            T result = (T)component;
            return result;
        }

        public T GetComponent<T>() where T : Component
        {
            Component component;
            bool isSuccess = _components.TryGetValue(typeof(T).Name, out component);

            Debug.Assert(isSuccess, "Cant Add GetComponent");

            T result = (T)component;
            
            return result;
        }

        public virtual void OnCollision(object? sender = null)
        {

        }


        public virtual void Start() 
        {
            foreach (var comp in _components)
            {
                comp.Value.Start();
            }
        }

        public virtual void Update() 
        {
            foreach (var comp in _components)
            {
                comp.Value.Update();
            }
        }

        public virtual void Render()
        {

        }
        //public virtual void Render() 
        //{
        //    foreach (var comp in _components)
        //    {
        //        comp.Value.Render();
        //    }
        //}
    }
}
