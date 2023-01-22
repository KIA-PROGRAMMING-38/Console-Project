using System.Diagnostics;

namespace SnakeGame
{
    public class Player : GameObject
    {
        private Renderer _renderer;
        private SnakeBody _head;
        public  Vector2 PrevPos;

        
        public override void Start()
        {
            AddComponent(new InputKeyComponent());
            AddComponent(new PlayerMovement());
            AddComponent(new Collider());
            AddComponent(new Renderer("P", 5));
            PrevPos = Position;
            _renderer = GetComponent<Renderer>();
            
            base.Start();
        }

        public override void OnCollision(object? sender)
        {
            GameObject? obj = sender as GameObject;
            Debug.Assert(obj != null, $"{GetType().Name}.OnCollision() => sender is null");

            switch(obj.Tag)
            {
                case "Wall":
                    SceneManager.Instance.ChangeFlagOn("DeadScene");
                    MapShaker.Instance.SetShakeFlag(true, 700, 4, 1);
                    break;
                case "Feed":
                    AddBody();
                    GameObjectManager.Instance.Destroy(obj);
                    if(obj is Feed)
                    {
                        ((Feed)obj).IsAlive = false;
                    }
                    GameDataManager.Instance.CurrentFeedCount += 1;
                    break;
                case "SnakeBody":
                    SceneManager.Instance.ChangeFlagOn("DeadScene");
                    MapShaker.Instance.SetShakeFlag(true, 700, 4, 1);
                    break;
                default:
                    break;
            }
            
        }

        public Vector2 AddPositionCalc(Vector2 pos)
        {
            Vector2 result = pos;

            switch (GetComponent<PlayerMovement>().MoveDirection)
            {
                case PlayerMovement.Direction.Left:
                    result.X += 1;
                    break;
                case PlayerMovement.Direction.Right:
                    result.X -= 1;
                    break;
                case PlayerMovement.Direction.Up:
                    result.Y += 1;
                    break;
                case PlayerMovement.Direction.Down:
                    result.Y -= 1;
                    break;
            }

            return result;
        }
        #region body 관련
        public void AddBody()
        {
            if(_head == null)
            {
                _head = new SnakeBody();
                _head.SetPosition(AddPositionCalc(PrevPos));
                return;
            }
            SnakeBody iter = _head;

            while(iter.Next != null)
            {
                iter = iter.Next;
            }
            SnakeBody newBody = new SnakeBody();
            newBody.SetPosition(AddPositionCalc(iter.PrevPosition));
            iter.Next = newBody;
            newBody.Parent = iter;

        }

        public void BodyUpdate()
        {
            if (_head == null) return;

            SnakeBody iter = _head;
            iter.PositionUpdate(PrevPos);

            iter = iter.Next;

            while(iter != null) 
            {
                iter.PositionUpdate(iter.Parent.PrevPosition);
                iter = iter.Next;
            }
        }

        public void BodyRender()
        {
            if (_head == null) return;

            SnakeBody iter = _head;

            while (iter.Next != null)
            {
                iter.Render();
                iter = iter.Next;
            }
            Console.SetCursorPosition(iter.PrevPosition.X, iter.PrevPosition.Y);
            Console.Write(" ");
        }
        #endregion

        public override void Update()
        {
            PrevPos = Position;
            foreach (var pair in _components)
            {
                pair.Value.Update();
            }
            BodyUpdate();


        }

        public override void Render()
        {
            BodyRender();
            if (_head == null)
            {
                Console.SetCursorPosition(PrevPos.X, PrevPos.Y);
                Console.Write(" ");
            }
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(_renderer.OwnerIcon);
       
        }
    }
}
