using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class PatternBase
    {
        protected Monster _monsterInstance = null;
        private int _actPriority = 0;

        public int ActPriority { get { return _actPriority; } }

        public PatternBase( Monster monsterInstance, int actPriority )
        {
            Debug.Assert( null != monsterInstance );

            _monsterInstance = monsterInstance;
            _actPriority = actPriority;
        }

        public virtual void OnEnable()
        {

        }

        public virtual void OnDisable()
        {

        }

        public virtual void Update()
        {

        }

        public bool ChecClearActionCondition()
        {
            return true;
        }
    }
}
