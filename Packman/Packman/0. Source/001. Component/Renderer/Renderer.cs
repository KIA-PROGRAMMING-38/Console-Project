using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman.Source
{
    internal class Renderer : Component
    {
        RenderManager renderManager = RenderManager.Instance;

        private int _renderOrder;

        public int RenderOrder { get { return _renderOrder; } set { _renderOrder = value; } }

        public Renderer(int renderOrder)
        {
            _renderOrder = renderOrder;
        }

        public override void UpdateComponent()
        {
            base.UpdateComponent();

            renderManager.AddRenderer( this );
        }

        public void Render()
        {
            gameObject?.Render();
        }
    }
}
