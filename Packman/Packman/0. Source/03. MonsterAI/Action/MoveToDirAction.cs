using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class MoveToDirAction : ActionBase
    {
        public MoveToDirAction( Monster monster, MonsterAI monsterAI ) 
            : base( monster, monsterAI )
        {

        }
    }
}
