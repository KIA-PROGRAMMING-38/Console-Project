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
        private GameObject gameObject;

        // ===================================== Property.. ===================================== //
        public GameObject gameobject { get { return gameObject; } set { gameObject = value; } }

        // ===================================== Function.. ===================================== //
        public bool Initialize()
        {
            return false;
        }

        public virtual void UpdateComponent()
        {

        }
    }
}
