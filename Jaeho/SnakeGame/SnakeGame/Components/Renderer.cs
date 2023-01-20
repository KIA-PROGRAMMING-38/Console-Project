using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class Renderer : Component
    {
        public Renderer(string icon, int order) 
        { 
            _ownerIcon = icon;
            _order = order;

            RenderManager.Instance.AddRenderer(this);
        }

        private int     _order;
        private string  _ownerIcon;
        
        public int      Order { get { return _order; } set{ _order = value; } }
        public string   OwnerIcon { get { return _ownerIcon; } set { _ownerIcon = value; } }

        public override void Render()
        {
            Owner.Render();
        }
    }
}
