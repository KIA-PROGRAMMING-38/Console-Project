using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class PathFinder
    {        
        private class Node
        {
            public Tile _tile;
        
            public int _gCost;      // 부모와의 거리( 가로 1칸 or 세로 1칸만 떨어진 경우 10, 대각선은 14 )..
            public int _hCost;      // 현재 tile 과 목적지까지의 거리..
            public int _fCost;      // G + H..
        
            public Node _parent;
        
            public static Node CreateNode( Tile tile, int fCost = 0, int gCost = 0, int hCost = 0, Node parent = null )
            {
                Node newNode = new Node();
        
                newNode._tile = tile;
                newNode._fCost = fCost;
                newNode._gCost = gCost;
                newNode._hCost = hCost;
                newNode._parent = parent;
        
                return newNode;
            }
        }

        /// <summary>
        /// 길찾기 알고리즘을 사용해 길을 구해 Path를 계산합니다.
        /// </summary>
        /// <param name="map"> 게임 Map </param>
        /// <param name="startX"> 시작 지점 x </param>
        /// <param name="startY"> 시작 지점 y </param>
        /// <param name="endX"> 끝 지점 x </param>
        /// <param name="endY"> 끝 지점 y </param>
        /// <param name="paths"> 알고리즘 결과로 나온 Path를 받을 애 </param>
        /// <returns></returns>
        public static bool ComputePath( Map map, int startX, int startY, int endX, int endY, ref List<Point2D> paths )
        {
            Tile start = map.GetTile(startX, startY);
            Tile end = map.GetTile(endX, endY);

            Node lastPathNode = AStar( map, start, end );
            if( null == lastPathNode )
            {
                return false;
            }

            ReverseAdd( lastPathNode, ref paths );

            paths.RemoveRange( 0, 1 );


			return true;
        }

        private static void ReverseAdd( Node curNode, ref List<Point2D> paths)
        {
            if(null == curNode)
            {
                return;
            }

            ReverseAdd( curNode._parent, ref paths );

            paths.Add( new Point2D( curNode._tile.X, curNode._tile.Y ) );
        }

        /// <summary>
        /// 길찾기 작업..
        /// </summary>
        /// <param name="map"> 게임의 map Instance </param>
        /// <param name="start"> 시작 타일 </param>
        /// <param name="end"> 종료 타일 </param>
        /// <returns></returns>
        private static Node AStar( Map map, Tile start, Tile end )
        {
            PriorityQueue<Node, int> openList = new PriorityQueue<Node, int>();
            LinkedList<Node> closeList = new LinkedList<Node>();

            Node newNode = Node.CreateNode( start );

            openList.Enqueue( newNode, 0 );

            Node[] nearNodes = new Node[4];

            Node curNode = null;

            while ( openList.Count > 0 )
            {
                // 현재 가장 우선순위가 높은 노드를 가져온다..
                curNode = openList.Dequeue();

                if ( curNode._tile == end )
                {
                    return curNode;
                }

                // 이 노드가 이미 검사한 노드인지 체크..
                bool isContinue = false;
                foreach ( var node in closeList )
                {
                    if ( curNode._tile == node._tile )
                    {
                        isContinue = true;
                        break;
                    }
                }

                if ( isContinue )  // 검사했다면 굳이 또 할 필요없다 나감..
                {
                    continue;
                }

                closeList.AddLast( curNode );   // 검사한 리스트에 추가..

                int findNearNodeCount = ComputeNearNode( nearNodes, curNode, map.Width, map.Height);
                for ( int i = 0; i < findNearNodeCount; ++i )
                {
                    openList.Enqueue( nearNodes[i], nearNodes[i]._fCost );
                }
            }

            return null;

            int ComputeNearNode( Node[] nearNodes, Node parent, int width, int height )
            {
                int x = parent._tile.X;
                int y = parent._tile.Y;

                int G = 10;
                int H = 0;
                int F = 0;

                int nodeCount = 0;

                // 왼쪽 인접 노드..
                Tile curTile = null;

                if (x - 1 >= 0)
                {
                    curTile = map.GetTile(x - 1, y);
                    if ( null != curTile && true == curTile.IsCanPassTile() )
                    {
                        H = (int)(ComputeDistance( curTile.X, curTile.Y, end.X, end.Y ) * 10.0f + 0.1f);
                        F = G + H;

                        nearNodes[nodeCount++] = Node.CreateNode( curTile, F, G, H, parent );
                    }
                }

                // 오른쪽 인접 노드..
                if ( x + 1 < map.X + map.Width )
                {
                    curTile = map.GetTile( x + 1, y );
                    if ( null != curTile && true == curTile.IsCanPassTile() )
                    {
                        H = (int)(ComputeDistance( curTile.X, curTile.Y, end.X, end.Y ) * 10.0f + 0.1f);
                        F = G + H;

                        nearNodes[nodeCount++] = Node.CreateNode( curTile, F, G, H, parent );
                    }
                }

                // 위쪽 인접 노드..
                if ( y - 1 >= 0 )
                {
                    curTile = map.GetTile( x, y - 1 );
                    if ( null != curTile && true == curTile.IsCanPassTile() )
                    {
                        H = (int)(ComputeDistance( curTile.X, curTile.Y, end.X, end.Y ) * 10.0f + 0.1f);
                        F = G + H;

                        nearNodes[nodeCount++] = Node.CreateNode( curTile, F, G, H, parent );
                    }
                }

                // 아래쪽 인접 노드..
                if ( y + 1 < map.Y + map.Height )
                {
                    curTile = map.GetTile( x, y + 1 );
                    if ( null != curTile && true == curTile.IsCanPassTile() )
                    {
                        H = (int)(ComputeDistance( curTile.X, curTile.Y, end.X, end.Y ) * 10.0f + 0.1f);
                        F = G + H;

                        nearNodes[nodeCount++] = Node.CreateNode( curTile, F, G, H, parent );
                    }
                }

                return nodeCount;
            }
        }

        /// <summary>
        /// 두 지점 간의 거리를 계산..
        /// </summary>
        /// <param name="x1"> 지점1의 x 좌표 </param>
        /// <param name="y1"> 지점1의 y 좌표 </param>
        /// <param name="x2"> 지점2의 x 좌표 </param>
        /// <param name="y2"> 지점2의 y 좌표 </param>
        /// <returns></returns>
        private static float ComputeDistance( int x1, int y1, int x2, int y2 )
        {
            int xDist = Math.Abs(x1 - x2);
            int yDist = Math.Abs(y1 - y2);
        
            return MathF.Sqrt( (float)(xDist * xDist + yDist * yDist) );
        }
    }
}
