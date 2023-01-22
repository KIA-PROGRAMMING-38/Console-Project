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
        InputKeyComponent InputKey;

        public override void Start()
        {
            InputKey = Owner.GetComponent<InputKeyComponent>();
        }
        public void DirectionUpdate()
        {
            switch (InputKey.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (moveDirection == Direction.Right) break;
                    moveDirection = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    if (moveDirection == Direction.Left) break;
                    moveDirection = Direction.Right;
                    break;
                case ConsoleKey.UpArrow:
                    if (moveDirection == Direction.Down) break;
                    moveDirection = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    if (moveDirection == Direction.Up) break;
                    moveDirection = Direction.Down;
                    break;
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
