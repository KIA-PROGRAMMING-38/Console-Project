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
            AddComponent(new Renderer(GameDataManager.PLAYER_ICON, 5, ConsoleColor.Yellow));
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
                    MapShaker.Instance.SetShakeFlag(true, 700, 3, 1);
                    break;
                case "Feed":
                    AddBody();
                    GameObjectManager.Instance.Destroy(obj);
                    if(obj is Feed feed)
                    {
                        feed.IsAlive = false;
                    }
                    GameDataManager.Instance.CurrentFeedCount += 1;
                    if(RandomManager.Instance.GetRandomRangeInt(1, 10) <= 5)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            long time = 1000;
                            int speed = RandomManager.Instance.GetRandomRangeInt(2,5);
                            int prevTimeScale = TimeManager.TimeScale;

                            if (TimeManager.TimeScale == speed) return;
                            

                            TimeManager.TimeScale *= speed;

                            string playSpeed = $"배속 : {speed}";
                            Console.SetCursorPosition(GameDataManager.MAP_MIN_X + GameDataManager.MAP_WIDTH / 2 - playSpeed.Length/2, 8);
                            Console.Write(playSpeed);
                            while (time > 0 || SceneManager.Instance.ChangeFlag )
                            {
                                time -= TimeManager.Instance.ElapsedMs;
                                Thread.Sleep((int)TimeManager.Instance.ElapsedMs);
                            }
                            Console.SetCursorPosition(GameDataManager.MAP_MIN_X + GameDataManager.MAP_WIDTH / 2 - playSpeed.Length / 2, 8);
                            Console.Write($"               ");
                            TimeManager.TimeScale = prevTimeScale;
                        });
                    }
                    break;
                case "SnakeBody":
                    SoundManager.Instance.Play("CollisionSound");
                    SceneManager.Instance.ChangeFlagOn("DeadScene");
                    MapShaker.Instance.SetShakeFlag(true, 700, 3, 1);
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
                    result.X = Math.Min(GameDataManager.MAP_MAX_X - 1, result.X + 1);
                    break;
                case PlayerMovement.Direction.Right:
                    result.X = Math.Max(GameDataManager.MAP_MIN_X + 1, result.X - 1);
                    break;
                case PlayerMovement.Direction.Up:
                    result.Y = Math.Min(GameDataManager.MAP_MAX_Y - 1, result.Y + 1);
                    break;
                case PlayerMovement.Direction.Down:
                    result.Y = Math.Max(GameDataManager.MAP_MIN_Y + 1, result.Y - 1);
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
