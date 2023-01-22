using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class ActionBase
    {
        protected Monster _monster;
        protected MonsterAI _monsterAI;

        public ActionBase( Monster monster, MonsterAI monsterAI )
        {
            Debug.Assert( null != monster );

            _monster = monster;
            _monsterAI = monsterAI;
        }

        public virtual void Action()
        {

        }
    }
}
