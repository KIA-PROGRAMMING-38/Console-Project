using System.Diagnostics;

namespace SnakeGame
{
    public class ColliderManager : LazySingleton<ColliderManager>
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

        /// <summary>
        /// 콜라이더 리스트에 있는 콜라이더를 삭제
        /// </summary>
        /// <param name="col"></param>
        public void RemoveCollider(Collider col)
        {
            bool isContains = _colliders.Contains(col);
            Debug.Assert(isContains, "삭제할 콜라이더가 리스트에 없음");

            _colliders.Remove(col);
        }

        /// <summary>
        /// 콜라이더리스트에 콜라이더를 추가해준다.
        /// </summary>
        /// <param name="col"></param>
        public void AddColliderObjectList(Collider col)
        {
            _colliders.Add(col);
        }


        /// <summary>
        /// 충돌했는지 체크
        /// </summary>
        /// <param name="callerCollider">함수를 호출한 게임오브젝트의 콜라이더</param>
        /// <returns></returns>
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

        /// <summary>
        /// 충돌된 게임오브젝트를 반환합니다.
        /// </summary>
        /// <param name="callerCollider">함수를 호출한 게임오브젝트의 콜라이더</param>
        /// <returns></returns>
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

        public List<Collider> GetColliderObjectList() => _colliders;

        public void Release()
        {
            _colliders.Clear();
            _colliders = null;
        }
    }
}
