using SnakeGame;

namespace SnakeGame
{
    public class PlayerMovement : Component
    {
        public enum Direction
        {
            None,
            Left,
            Right,
            Up,
            Down,
        }

        private Direction moveDirection;
        public Direction MoveDirection { get { return moveDirection; } }

        public override void Start()
        {
            moveDirection = Direction.None;
        }
        public void DirectionUpdate()
        {
            if (InputManager.Instance.IsKeyDown(ConsoleKey.LeftArrow))
            {
                if (MoveDirection != Direction.Right)
                {
                    moveDirection = Direction.Left;
                }
            }
            if (InputManager.Instance.IsKeyDown(ConsoleKey.RightArrow))
            {
                if (moveDirection != Direction.Left)
                {
                    moveDirection = Direction.Right;
                }
            }
            if (InputManager.Instance.IsKeyDown(ConsoleKey.UpArrow))
            {
                if (moveDirection != Direction.Down)
                {
                    moveDirection = Direction.Up;
                }
            }
            if (InputManager.Instance.IsKeyDown(ConsoleKey.DownArrow))
            {
                if (moveDirection != Direction.Up)
                {
                    moveDirection = Direction.Down;
                }
            }
        }

        public void MoveObject()
        {
            Vector2 position = Owner.Position;
            switch (moveDirection)
            {
                case Direction.Left:
                    position.X = Math.Max(GameDataManager.MAP_MIN_X, position.X - 1);
                    break;
                case Direction.Right:
                    position.X = Math.Min(GameDataManager.MAP_MAX_X, position.X + 1);
                    break;
                case Direction.Up:
                    position.Y = Math.Max(GameDataManager.MAP_MIN_Y, position.Y - 1);
                    break;
                case Direction.Down:
                    position.Y = Math.Min(GameDataManager.MAP_MAX_Y, position.Y + 1);
                    break;
            }
            Owner.Position = position;
        }

        public override void Update()
        {
            DirectionUpdate();
            MoveObject();
        }
    }
}
