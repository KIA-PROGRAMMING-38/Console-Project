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

        private int renderOrder;

        public int RenderOrder { get { return renderOrder; } set { renderOrder = value; } }

        public override void UpdateComponent()
        {
            base.UpdateComponent();

            renderManager.AddRenderer( this );
        }

        public void Render()
        {
            gameobject.Render();
        }
    }
}
