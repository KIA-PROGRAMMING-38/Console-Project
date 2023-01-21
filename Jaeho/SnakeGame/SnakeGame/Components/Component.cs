namespace ConsoleGame
{
    public class Component
    {
        public Component() { }

        protected string        _name;
        protected GameObject    _owner;

        public string       Name { get { return _name; } set { _name = value; } }
        public GameObject   Owner { get { return _owner; }set { _owner = value; } }

        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void Render() { }
    }
}
