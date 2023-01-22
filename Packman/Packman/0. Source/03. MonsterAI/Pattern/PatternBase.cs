using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class PatternBase
    {
        private Monster _monsterInstance;

        public PatternBase( Monster monsterInstance )
        {
            _monsterInstance = monsterInstance;
        }
    }
}
