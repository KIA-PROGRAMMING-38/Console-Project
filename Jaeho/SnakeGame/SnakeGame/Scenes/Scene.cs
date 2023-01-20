using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class Scene
    {
        public Scene() 
        { 
            _nextSceneName = string.Empty;
        }
        public void ClearScene()
        {
            GameObjectManager.Instance.Clear();
        }

        protected string _nextSceneName;

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Render()
        {

        }

    }
}
