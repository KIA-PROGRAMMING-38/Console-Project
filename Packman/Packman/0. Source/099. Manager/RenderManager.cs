using Packman.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class RenderManager : SingletonBase<RenderManager>
    {
        List<Renderer> _renderers = new List<Renderer>(50);
        //PriorityQueue<Renderer, int> _renderers = new PriorityQueue<Renderer, int>();

        public bool AddRenderer( Renderer renderer )
        {
            if ( null == renderer )
            {
                return false;
            }

            _renderers.Add( renderer );

            return true;
        }

        public void Render()
        {
            Console.Clear();

            // 각 Renderer 인스턴스들의 renderOrder 를 기준으로 정렬합니다( 내림차순 )..
            _renderers.Sort( delegate ( Renderer a, Renderer b ) { return a.RenderOrder.CompareTo( b.RenderOrder ); } );

            foreach ( var renderer in _renderers )
            {
                renderer.Render();
            }

            _renderers.Clear();
        }
    }
}
