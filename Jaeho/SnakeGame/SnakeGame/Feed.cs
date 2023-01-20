using ConsoleGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class Feed : GameObject
    {
        public Feed()
        {
            AddComponent(new Collider());
            AddComponent(new Renderer("F",1));
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
