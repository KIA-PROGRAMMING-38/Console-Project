using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Component
    {
        // ===================================== Field.. ===================================== //
        protected GameObject? _gameObject = null;

        // ===================================== Property.. ===================================== //
        public GameObject? GameObject
        {
            get { return _gameObject; }
            set
            {
                if(null != value )
                {
                    _gameObject = value;
                }
            }
        }

        // ===================================== Function.. ===================================== //
        public virtual void Initialize()
        {

        }

        public virtual void UpdateComponent()
        {

        }
    }
}
