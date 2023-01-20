using Packman.Source;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class RenderManager : SingletonBase<RenderManager>
    {
        private struct RemoveRenderInfo
        {
            public int X;
            public int Y;
            public int Size;
        }
            

        List<Renderer> _renderers = new List<Renderer>(50);
        LinkedList<RemoveRenderInfo> _removeRenderInfoes = new LinkedList<RemoveRenderInfo>();
        string[] emptyStrings = new string[11];
        //PriorityQueue<Renderer, int> _renderers = new PriorityQueue<Renderer, int>();

        public RenderManager()
        {
            string emptyStr = " ";
            int loopCount = emptyStrings.Length;
            for ( int i = 1; i < loopCount; ++i )
            {
                emptyStrings[i] = emptyStr;
                emptyStr += " ";
            }
        }

        public void AddRenderer( Renderer renderer )
        {
            Debug.Assert( null != renderer );

            _renderers.Add( renderer );
        }

        public void ReserveRenderRemove( int x, int y, int size )
        {
            _removeRenderInfoes.AddLast( new RemoveRenderInfo { X = x, Y = y, Size = size } );
        }

        public void Render()
        {
            foreach( RemoveRenderInfo removeRenderInfo in _removeRenderInfoes)
            {
                Console.SetCursorPosition( removeRenderInfo.X, removeRenderInfo.Y );
                Console.Write( emptyStrings[removeRenderInfo.Size] );
            }
            _removeRenderInfoes.Clear();

            // 각 Renderer 인스턴스들의 renderOrder 를 기준으로 정렬합니다( 내림차순 )..
            _renderers.Sort( delegate ( Renderer a, Renderer b ) { return a.RenderOrder.CompareTo( b.RenderOrder ); } );

            foreach ( Renderer renderer in _renderers )
            {
                renderer.Render();
            }

            _renderers.Clear();
        }
    }
}
