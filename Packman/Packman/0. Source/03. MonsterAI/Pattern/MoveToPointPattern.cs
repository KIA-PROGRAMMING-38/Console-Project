using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class MoveToPointPattern : PatternBase
    {
        private Point2D _destinationPoint;

        public MoveToPointPattern( Monster monsterInstance, int actPriority )
            : base( monsterInstance, actPriority )
        {

        }
    }
}
