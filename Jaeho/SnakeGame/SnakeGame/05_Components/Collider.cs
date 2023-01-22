namespace SnakeGame
{
    public class Collider : Component
    {
        public Collider()
        {
           
        }

        private Action<object>? _onCollisionAction = null;
        public  Action<object>? CollisionAction { get { return _onCollisionAction; } }

        public void SetCollisionAction(Action<object> onCollisionAction)
        {
            _onCollisionAction = onCollisionAction;
        }

        public override void Start()
        {
            ColliderManager.Instance.AddColliderObjectList(this);
            _onCollisionAction = Owner.OnCollision;
        }

        public override void Update()
        {
            ColliderManager.Instance.IsCollided(this);
        }
    }
}
