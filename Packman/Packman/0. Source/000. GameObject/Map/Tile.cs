using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Tile : GameObject
    {
        public enum Kind
        {
            Empty,
            Block
        }

        private Kind _kind;
        
        public Kind MyKind { get { return _kind; } }

        public Tile( int x, int y, Kind kind, ConsoleColor color )
            : base( x, y, Constants.TILE_RENDER_ORDER )
        {
            _kind = kind;
            _color = color;

            switch ( _kind )
            {
                case Kind.Empty:
                    _image = " ";
                    break;

                case Kind.Block:
                    _image = "A";
                    break;
            }
        }

        public override void Update()
        {
            if ( _kind == Kind.Empty )
                return;

            base.Update();
        }

        public override void Render()
        {
            base.Render();
        }

        public bool IsCanPassTile()
        {
            return _kind != Kind.Block;
        }
    }
}
