namespace SnakeGame
{
    public class InputKeyComponent : Component
    {

        public InputKeyComponent() { }
        private ConsoleKey _key;
        public  ConsoleKey Key { get { return _key; } set { _key = value; } }

        public override void Start()
        {
            _key = ConsoleKey.NoName;
        }

        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                Console.Read();
                _key = Console.ReadKey().Key;
            }
        }
    }
}
