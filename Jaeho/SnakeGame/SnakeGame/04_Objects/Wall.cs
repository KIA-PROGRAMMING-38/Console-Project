namespace SnakeGame
{
    public class Wall : GameObject
    {
        private Renderer renderer;

        public override void Start()
        {
            AddComponent(new Collider());
            AddComponent(new Renderer(GameDataManager.WALL_ICON, 1, ConsoleColor.DarkGray));
            renderer = GetComponent<Renderer>();

            StartComponents();

            GetComponent<Collider>().SetCollisionCheck(false);
        }

        public override void Render()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(renderer.OwnerIcon);
        }
    }
}
