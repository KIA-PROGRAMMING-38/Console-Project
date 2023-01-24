using System.Diagnostics;

namespace SnakeGame
{
    public class Player : GameObject
    {
        private Renderer _renderer;
        private SnakeBody _head;
        private SnakeBody _tail;
        public  Vector2 PrevPos;

        
        public override void Start()
        {

            AddComponent(new PlayerMovement());
            AddComponent(new Collider());
            //◯☺ ▢
            AddComponent(new Renderer('▢', 5, ConsoleColor.Yellow));
            PrevPos = Position;
            _renderer = GetComponent<Renderer>();

            StartComponents();
        }

        /// <summary>
        /// 충돌했을 때 처리할 콜백 함수
        /// </summary>
        /// <param name="sender"></param>
        public override void OnCollision(object? sender)
        {
            GameObject? obj = sender as GameObject;
            Debug.Assert(obj != null, $"{GetType().Name}.OnCollision() => sender is null");

            switch(obj.Tag)
            {
                case "Wall":
                    SoundManager.Instance.Play("CollisionSound");
                    SceneManager.Instance.ChangeFlagOn("DeadScene");
                    MapShaker.Instance.SetShakeFlag(true, 1700, 3, 1);
                    break;
                case "Feed":
                    AddBody();
                    GameObjectManager.Instance.Destroy(obj);
                    if(obj is Feed feed)
                    {
                        feed.IsAlive = false;
                    }
                    GameDataManager.Instance.CurrentFeedCount += 1;
                    break;
                case "SnakeBody":
                    SoundManager.Instance.Play("CollisionSound");
                    SceneManager.Instance.ChangeFlagOn("DeadScene");
                    MapShaker.Instance.SetShakeFlag(true, 3000, 4, 1);
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
                _tail = _head;
                return;
            }

            SnakeBody newBody = new SnakeBody();
            newBody.SetPosition(AddPositionCalc(_tail.PrevPosition));
            _tail.Next = newBody;
            newBody.Parent = _tail;
            _tail = newBody;

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

        public void TailPrintRemove()
        {
            if (_head == null) return;
            Console.SetCursorPosition(_tail.PrevPosition.X, _tail.PrevPosition.Y);
            Console.Write(" ");
        }
        #endregion

        public override void Update()
        {
            PrevPos = Position;

            UpdateComponents();

            BodyUpdate();
        }

        public override void Render()
        {
            TailPrintRemove();
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
