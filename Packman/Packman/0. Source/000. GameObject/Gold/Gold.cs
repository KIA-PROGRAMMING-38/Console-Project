using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Gold : GameObject
    {
        public Gold(int x, int y)
            : base( x, y, Constants.GOLD_IMAGE, Constants.GOLD_COLOR, Constants.GOLD_RENDER_ORDER )
        {

        }

        public override void Update()
        {
            base.Update();
        }

        public override void Render()
        {
            base.Render();
        }
    }
}
