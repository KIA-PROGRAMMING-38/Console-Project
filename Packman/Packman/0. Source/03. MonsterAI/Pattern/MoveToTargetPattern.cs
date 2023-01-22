using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class MoveToTargetPattern : PatternBase
    {
        private GameObject _targetObject;

        public MoveToTargetPattern( Monster monsterInstance, int actPriority )
            : base( monsterInstance, actPriority )
        {

        }
    }
}
