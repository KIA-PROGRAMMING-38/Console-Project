namespace SnakeGame
{
    public class Collider : Component
    {
        public Collider()
        {
           
        }

        private bool _collisionCheck;
        private Action<object>? _onCollisionAction = null;
        public  Action<object>? CollisionAction { get { return _onCollisionAction; } }

        public void SetCollisionCheck(bool isCollisionCheck) => _collisionCheck = isCollisionCheck;

        public override void Start()
        {
            ColliderManager.Instance.AddColliderObjectList(this);
            _collisionCheck = true;
            _onCollisionAction = Owner.OnCollision;
        }

        public override void Update()
        {
            if (_collisionCheck == false) return;

            if(ColliderManager.Instance.GetCollidedGameObject(this) is GameObject obj)
            {
                _onCollisionAction?.Invoke(obj);
            }
        }
    }
}
