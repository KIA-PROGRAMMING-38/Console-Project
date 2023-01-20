using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class SnakeBody
    {
        public class Tail
        {
            public Tail(Vector2 pos) 
            {
                Next = null;
                Parent = null;
                Pos = pos;
                PrevPos = Pos;
            }
            public Tail Parent;
            public Tail Next;
            public Vector2 PrevPos;
            public Vector2 Pos;
        }
        public SnakeBody(Player owner)
        {
            Owner = owner;
            _position = owner.PrevPos;
        }
        private Tail? _head = null;
        public Player Owner;
        private Vector2 _position;   

        public void Start(Player owner)
        {
            Owner = owner;
            _position = owner.PrevPos;
        }

 
        public void AddTail(Movement.Direction dir)
        {
            if(_head == null )
            {
                Vector2 p = Owner.PrevPos;
                switch (dir)
                {
                    case Movement.Direction.Left:
                        p.X += 1;
                        break;
                    case Movement.Direction.Right:
                        p.X -= 1;
                        break;
                    case Movement.Direction.Up:
                        p.Y -= 1;
                        break;
                    case Movement.Direction.Down:
                        p.Y += 1;
                        break;
                }
                _head = new Tail(p);
                return;
            }

            Tail temp = _head;
            while(temp.Next != null )
            {
                temp = temp.Next;
            }

            Tail newTail = new Tail(temp.PrevPos);
            temp.Next = newTail;
            newTail.Parent = temp;
        }

        public void UpdatePosAllTail()
        {
            if(_head == null) { return; }

            _head.PrevPos = _head.Pos;
            _head.Pos = Owner.PrevPos;

            Tail temp = _head.Next;
            while( temp != null )
            {
                temp.PrevPos = temp.Pos;
                temp.Pos = temp.Parent.PrevPos;
                temp = temp.Next;
            }
        }
        public void Update()
        {
            UpdatePosAllTail();
        }

        public void Render()
        {
            if (_head == null) { return; }
            Tail temp = _head;
            while (temp != null)
            { 
                Console.SetCursorPosition(temp.Pos.X, temp.Pos.Y);
                Console.WriteLine("O");
                temp = temp.Next;
            }
        }
    }
}
