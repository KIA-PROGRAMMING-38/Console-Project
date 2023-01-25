namespace SnakeGame
{
    public class Wall : GameObject
    {
        private bool _isRendered = false;
        private Renderer renderer;
        public override void Start()
        {
            AddComponent(new Collider());
            AddComponent(new Renderer(GameDataManager.WALL_ICON, 1));
            renderer = GetComponent<Renderer>();

            StartComponents();

            GetComponent<Collider>().SetCollisionCheck(false);
        }

        public override void Render()
        {
            //if (_isRendered) return;

            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(renderer.OwnerIcon);

            _isRendered = true;
        }
    }
}
