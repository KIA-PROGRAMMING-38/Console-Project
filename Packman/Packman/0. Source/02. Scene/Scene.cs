using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class Scene
    {
        // 필요한 싱글톤 인스턴스들 미리 멤버로 저장..
        protected ObjectManager _objectManager;

        public Scene()
        {
            _objectManager = ObjectManager.Instance;
        }

        public virtual bool Initialize()
        {
            return true;
        }

        public virtual void Update()
        {
            _objectManager.Update();
        }

        public virtual void Render()
        {
            _objectManager.Render();
        }

        public virtual void Release()
        {
            _objectManager.RemoveAll();
        }
    }
}
