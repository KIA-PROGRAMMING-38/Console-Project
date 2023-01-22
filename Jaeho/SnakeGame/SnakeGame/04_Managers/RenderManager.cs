using SnakeGame;
using System.Data;
using System.Diagnostics;

namespace SnakeGame
{
    public class RenderManager : Singleton<RenderManager>
    {
        public RenderManager()
        {
            _renderers = new Dictionary<Renderer, int>();
        }

        private Dictionary<Renderer, int> _renderers = new Dictionary<Renderer, int>();

        public void Clear()
        {
            _renderers.Clear();
        }

        public void RemoveRenderer(Renderer renderer)
        {
            _renderers.Remove(renderer);
        }

        public void AddRenderer(Renderer renderer)
        {
            bool isSuccess = _renderers.TryAdd(renderer, renderer.Order);
            Debug.Assert(isSuccess, "Have Same Renderer");
            _renderers = _renderers.OrderBy((num) => num.Value).ToDictionary(x=>x.Key,x=>x.Value);
        }

        public void Render()
        {   
            foreach(Renderer renderer in _renderers.Keys)
            {
                renderer.Render();
            }
        }

        public void Release()
        {
            _renderers.Clear();
        }
    }
}
