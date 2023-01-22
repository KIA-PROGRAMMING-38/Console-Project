namespace SnakeGame
{
    public class Wall : GameObject
    {
        //private bool _isRendered = false;
        private Renderer renderer;
        public override void Start()
        {
            AddComponent(new Collider());
            AddComponent(new Renderer('▒', 1));
            renderer = GetComponent<Renderer>();

            base.Start();
        }

        public override void Render()
        {
            //if (_isRendered) return;

            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(renderer.OwnerIcon);

            //_isRendered = true;
        }
    }
}
