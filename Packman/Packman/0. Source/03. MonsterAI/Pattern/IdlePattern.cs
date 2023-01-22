using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class IdlePattern : PatternBase
    {
        FindWayPointAction findWayPointAction = null;

        public IdlePattern( Monster monster, int actPriority )
            : base( monster, actPriority )
        {

        }

        public override void OnEnable()
        {
            base.OnEnable();
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }

        public override void Update()
        {
            
        }
    }
}
