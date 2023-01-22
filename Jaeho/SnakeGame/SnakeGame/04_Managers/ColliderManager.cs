using System.Diagnostics;

namespace SnakeGame
{
    public class ColliderManager : Singleton<ColliderManager>
    {
        public ColliderManager() 
        {
            _colliders = new List<Collider>();
        }

        private List<Collider> _colliders;

        public void Clear()
        {
            _colliders.Clear();
        }
        public void RemoveCollider(Collider col)
        {
            _colliders.Remove(col);
        }

        public void AddColliderObjectList(Collider col)
        {
            _colliders.Add(col);
        }

        public bool IsCollided(Collider callerCollider)
        {
            bool isCollided = false;

            foreach(var col in _colliders)
            {
                if(col == callerCollider) continue;

                if(col.Owner == null)
                {
                    continue;
                }

                if (col.Owner.Position == callerCollider.Owner.Position)
                {
                    callerCollider?.CollisionAction?.Invoke(col.Owner);
                    isCollided = true;
                    return isCollided;
                }
            }
            return isCollided;
        }

        public GameObject? GetCollidedGameObject(Collider callerCollider)
        {
            GameObject? collidedObject = null;
            foreach (var col in _colliders)
            {
                if (col == callerCollider) continue;

                Debug.Assert(col.Owner != null);

                if (col.Owner.Position == callerCollider.Owner.Position)
                {
                    collidedObject = col.Owner;
                    break;
                }
            }

            return collidedObject;
        }

        public List<Collider> GetColliderObjectList() { return _colliders; }

        public void Release()
        {
            _colliders.Clear();
        }
    }
}
