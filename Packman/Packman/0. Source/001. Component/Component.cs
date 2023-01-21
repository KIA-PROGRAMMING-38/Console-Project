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
        protected GameObject? gameObject = null;

        // ===================================== Property.. ===================================== //
        public GameObject? GameObject
        {
            get { return gameObject; }
            set
            {
                if(null != value )
                {
                    gameObject = value;
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
