using System.Drawing;

namespace SnakeGame
{
    public class SnakeBody : GameObject
    {
        public SnakeBody Parent;
        public SnakeBody Next;
        public Vector2 PrevPosition;
        private Renderer _renderer;

        public void SetPosition(Vector2 pos)
        {
            Position = pos;
            PrevPosition = pos;
        }

        public void PositionUpdate(Vector2 pos)
        {
            PrevPosition = Position;
            Position = pos;
        }
        public override void Start()
        {
            AddComponent(new Collider());
            _renderer = new Renderer(GameDataManager.SNAKEBODY_ICON, 2, ConsoleColor.Gray);
            AddComponent(_renderer);
            Parent = null;
            Next = null;

            StartComponents();
        }

        public override void Render()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(_renderer.OwnerIcon);
        }
    }
}
