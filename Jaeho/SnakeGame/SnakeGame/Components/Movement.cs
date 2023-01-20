using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class Movement : Component
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
                    moveDirection = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    moveDirection = Direction.Right;
                    break;
                case ConsoleKey.UpArrow:
                    moveDirection = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
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
