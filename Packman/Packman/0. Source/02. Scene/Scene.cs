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

        /// <summary>
        /// 씬 초기화..
        /// </summary>
        /// <returns></returns>
        public virtual bool Initialize()
        {
            return true;
        }

        /// <summary>
        /// 씬을 갱신시킵니다.
        /// </summary>
        public virtual void Update()
        {
            _objectManager.Update();
        }

        /// <summary>
        /// 씬을 그립니다.
        /// </summary>
        public virtual void Render()
        {
            _objectManager.Render();
        }

        /// <summary>
        /// 씬이 제거될 때 호출됩니다.
        /// </summary>
        public virtual void Release()
        {
            _objectManager.RemoveAll();

            Console.Clear();
        }
    }
}
