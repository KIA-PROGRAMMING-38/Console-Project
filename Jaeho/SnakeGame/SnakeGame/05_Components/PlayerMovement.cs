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

        private Direction moveDirection = Direction.None;
        public Direction MoveDirection { get { return moveDirection; } }

        public override void Start()
        {
        }
        public void DirectionUpdate()
        {
            if (InputManager.Instance.IsKeyDown(ConsoleKey.LeftArrow))
            {
                if (MoveDirection == Direction.Right) return;
                moveDirection = Direction.Left;
            }
            else if (InputManager.Instance.IsKeyDown(ConsoleKey.RightArrow))
            {
                if (moveDirection == Direction.Left) return;
                moveDirection = Direction.Right;
            }
            else if (InputManager.Instance.IsKeyDown(ConsoleKey.UpArrow))
            {
                if (moveDirection == Direction.Down) return;
                moveDirection = Direction.Up;
            }
            else if (InputManager.Instance.IsKeyDown(ConsoleKey.DownArrow))
            {
                if (moveDirection == Direction.Up) return;
                moveDirection = Direction.Down;
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
