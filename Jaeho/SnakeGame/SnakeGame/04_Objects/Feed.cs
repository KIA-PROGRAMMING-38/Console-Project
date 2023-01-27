﻿namespace SnakeGame
{
    public class Feed : GameObject
    {
        private Renderer renderer;
        public bool IsAlive = true;

        public override void Start()
        {
            AddComponent(new Collider());
            AddComponent(new Renderer(GameDataManager.FEED_ICON, 1, ConsoleColor.DarkCyan));
            renderer = GetComponent<Renderer>();

            StartComponents();
        }

        public override void Update()
        {
            UpdateComponents();
        }

        public override void Render()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(renderer.OwnerIcon);
        }
    }
}
