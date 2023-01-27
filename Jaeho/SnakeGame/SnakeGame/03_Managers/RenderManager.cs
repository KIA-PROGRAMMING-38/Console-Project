using SnakeGame;
using System.Data;
using System.Diagnostics;

namespace SnakeGame
{
    public class RenderManager : LazySingleton<RenderManager>
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

        /// <summary>
        /// 렌더러 리스트에서 삭제
        /// </summary>
        /// <param name="renderer">삭제할 렌더러</param>
        public void RemoveRenderer(Renderer renderer)
        {
            _renderers.Remove(renderer);
        }

        /// <summary>
        /// 렌더러 리스트에 렌더러를 추가해줍니다.
        /// </summary>
        /// <param name="renderer">추가할 렌더러</param>
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
