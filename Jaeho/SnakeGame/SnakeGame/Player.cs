using ConsoleGame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class Player : GameObject
    {
        public Player()
        {
            AddComponent(new InputKeyComponent());
            AddComponent(new Movement());
            AddComponent(new Collider());            
            AddComponent(new Renderer("P", 2));
            PrevPos = Position;
            movement = GetComponent<Movement>();
            renderer = GetComponent<Renderer>();
            _head = new SnakeBody(this);
        }

        public Movement movement;
        public Renderer renderer;
        private SnakeBody _head;
        public  Vector2 PrevPos;

        

        public override void OnCollision(object? sender)
        {
            GameObject? obj = sender as GameObject;

            Debug.Assert(obj != null, "ddddd");

            switch(obj.Tag)
            {
                case "Wall":
                    SceneManager.Instance.ChangeFlagOn("EndingScene");
                    break;
                case "Feed":
                    _head.AddTail(movement.MoveDirection);
                    GameObjectManager.Instance.Destroy(obj);
                    GameDataManager.Instance.CurrentFeedCount += 1;
                    break;
                default:
                    break;
            }
            
        }

        public override void Start()
        {
            foreach (var pair in _components)
            {
                pair.Value.Start();
            }
            //if(_tail == null)
            //    _tail = new Tail(this);
        }

        public override void Update()
        {
            PrevPos = Position;
            foreach (var pair in _components)
            {
                pair.Value.Update();
            }
   
            _head.UpdatePosAllTail();
        }

        public override void Render()
        {
            _head.Render();
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(renderer.OwnerIcon);
        }
    }
}
