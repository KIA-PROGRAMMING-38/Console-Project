using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class Wall : GameObject
    {
        public Wall()
        {
            AddComponent(new Collider());
            AddComponent(new Renderer("▒", 1));
            renderer = GetComponent<Renderer>();
        }
        private Renderer renderer;
        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
        }
        public override void Render()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(renderer.OwnerIcon);
        }
    }
}
