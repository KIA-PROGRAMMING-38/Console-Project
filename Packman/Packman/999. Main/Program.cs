using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Packman
{
    internal class Program
    {
        static void Main()
        {
            //string path = Path.Combine( "../../../Resources", "Data", "Stage1.txt" );
            //
            //string[] lines = File.ReadAllLines( path );
            //
            //for( int i = 0; i < lines.Length; ++i )
            //{
            //    char[] charArray = lines[i].ToCharArray();
            //    for( int j = 0; j < charArray.Length; ++j )
            //    {
            //        if( 'w' == charArray[j] )
            //            charArray[j] = 'W';
            //    }
            //
            //    lines[i] = new string( charArray );
            //}
            //
            //File.WriteAllLines( path, lines );

            string asd = "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";
            Console.WriteLine( asd.Length );

            //if ( true == Game.Instance.Initialize() )
            //{
            //    Game.Instance.Run();
            //
            //    Game.Instance.Release();
            //}
        }

        //static void Main( string[] args )
        //{
        //    Console.BackgroundColor = ConsoleColor.Black;
        //    
        //    ConsoleKey key = ConsoleKey.NoName;
        //    
        //    Thread thread = new Thread(() =>
        //    {
        //        key = Console.ReadKey().Key;
        //    });
        //    thread.Start();
        //    
        //    while ( true )
        //    {
        //        TileMap tileMap = new TileMap();
        //        tileMap._width = 60;
        //        tileMap._height = 25;
        //        tileMap._tiles = new Tile[tileMap._height, tileMap._width];
        //        for ( int i = 0; i < tileMap._height; ++i )
        //        {
        //            for ( int j = 0; j < tileMap._width; ++j )
        //            {
        //                tileMap._tiles[i, j] = new Tile( j, i, "□", ConsoleColor.White );
        //            }
        //        }
        //    
        //        Random random = new Random();
        //        int startX = 0;
        //        int startY = 0;
        //    
        //        int endX = 0;
        //        int endY = 0;
        //    
        //        do
        //        {
        //            startX = random.Next( tileMap._width );
        //            startY = random.Next( tileMap._height );
        //    
        //            endX = random.Next( tileMap._width );
        //            endY = random.Next( tileMap._height );
        //        } while ( ComputeDistance( startX, startY, endX, endY ) * 10.0f <= 20.0f );
        //    
        //        Console.Clear();
        //    
        //        Console.WriteLine( $"Start : {startX}, {startY}" );
        //        Console.WriteLine( $"End : {endX}, {endY}" );
        //    
        //        AStar( tileMap, tileMap._tiles[startY, startX], tileMap._tiles[endY, endX] );
        //    
        //        tileMap.Render();
        //    
        //        Thread.Sleep( 1000 );
        //    
        //        if ( key == ConsoleKey.Escape )
        //            break;
        //    }
        //}
        //
        //public class TileMap
        //{
        //    public Tile[,] _tiles = null;
        //    public int _width;
        //    public int _height;
        //
        //    public TileMap()
        //    {
        //        _tiles = null;
        //        _width = 0;
        //        _height = 0;
        //    }
        //
        //    public void Render()
        //    {
        //        for ( int i = 0; i < _height; ++i )
        //        {
        //            for ( int j = 0; j < _width; ++j )
        //            {
        //                _tiles[i, j].Render();
        //            }
        //        }
        //    }
        //}
        //
        //public class Tile
        //{
        //    public int _x;
        //    public int _y;
        //    public string _icon;
        //    public ConsoleColor _color;
        //
        //    public Tile( int x, int y, string icon, ConsoleColor color )
        //    {
        //        _x = x;
        //        _y = y;
        //        _icon = icon;
        //        _color = color;
        //    }
        //
        //    public void Render()
        //    {
        //        Console.SetCursorPosition( _x * 2, _y + 2 );
        //        Console.ForegroundColor = _color;
        //        Console.Write( _icon );
        //    }
        //}
        //
        //public class Node
        //{
        //    public Tile _tile;
        //
        //    public int _gCost;      // 부모와의 거리( 가로 1칸 or 세로 1칸만 떨어진 경우 10, 대각선은 14 )..
        //    public int _hCost;      // 현재 tile 과 목적지까지의 거리..
        //    public int _fCost;      // G + H..
        //
        //    public Node _parent;
        //
        //    public static Node CreateNode( Tile tile, int fCost = 0, int gCost = 0, int hCost = 0, Node parent = null )
        //    {
        //        Node newNode = new Node();
        //
        //        newNode._tile = tile;
        //        newNode._fCost = fCost;
        //        newNode._gCost = gCost;
        //        newNode._hCost = hCost;
        //        newNode._parent = parent;
        //
        //        return newNode;
        //    }
        //}
        //
        //public static void AStar( TileMap tileMap, Tile start, Tile end )
        //{
        //    PriorityQueue<Node, int> openList = new PriorityQueue<Node, int>();
        //    LinkedList<Node> closeList = new LinkedList<Node>();
        //
        //    Node newNode = Node.CreateNode( start );
        //
        //    openList.Enqueue( newNode, 0 );
        //
        //    Node[] nearNodes = new Node[8];
        //
        //    Node curNode = null;
        //
        //    while ( openList.Count > 0 )
        //    {
        //        // 현재 가장 우선순위가 높은 노드를 가져온다..
        //        curNode = openList.Dequeue();
        //
        //        if ( curNode._tile == end )
        //        {
        //            break;
        //        }
        //
        //        // 이 노드가 이미 검사한 노드인지 체크..
        //        bool isContinue = false;
        //        foreach ( var node in closeList )
        //        {
        //            if ( curNode._tile == node._tile )
        //            {
        //                isContinue = true;
        //                break;
        //            }
        //        }
        //
        //        if ( isContinue )  // 검사했다면 굳이 또 할 필요없다 나감..
        //        {
        //            continue;
        //        }
        //
        //        closeList.AddLast( curNode );   // 검사한 리스트에 추가..
        //
        //        int findNearNodeCount = ComputeNearNode(nearNodes, curNode, tileMap._width, tileMap._height);
        //        for ( int i = 0; i < findNearNodeCount; ++i )
        //        {
        //            openList.Enqueue( nearNodes[i], nearNodes[i]._fCost );
        //        }
        //    }
        //
        //    while( null != curNode )
        //    {
        //        curNode._tile._color = ConsoleColor.Magenta;
        //        curNode = curNode._parent;
        //    }
        //
        //    start._color = ConsoleColor.Red;
        //    end._color = ConsoleColor.Blue;
        //
        //    int ComputeNearNode( Node[] nearNodes, Node parent, int width, int height )
        //    {
        //        int x = parent._tile._x;
        //        int y = parent._tile._y;
        //
        //        int minX = Math.Max(x - 1, 0);
        //        int maxX = Math.Min(x + 1, width - 1);
        //        int minY = Math.Max(y - 1, 0);
        //        int maxY = Math.Min(y + 1, height - 1);
        //
        //        int count = 0;
        //        for ( int curY = minY; curY <= maxY; ++curY )
        //        {
        //            for( int curX = minX; curX <= maxX; ++curX )
        //            {
        //                Tile curTile = tileMap._tiles[curY, curX];
        //
        //                if ( curTile == parent._tile )
        //                {
        //                    continue;
        //                }
        //
        //                int G = (int)(ComputeDistance(curTile._x, curTile._y, parent._tile._x, parent._tile._y) * 10.0f + 0.1f);
        //                int H = (int)(ComputeDistance(curTile._x, curTile._y, end._x, end._y) * 10.0f + 0.1f);
        //                int F = G + H;
        //
        //                nearNodes[count++] = Node.CreateNode( tileMap._tiles[curY, curX], F, G, H, parent );
        //            }
        //        }
        //
        //        return count;
        //    }
        //}
        //
        //public static float ComputeDistance( int x1, int y1, int x2, int y2 )
        //{
        //    int xDist = Math.Abs(x1 - x2);
        //    int yDist = Math.Abs(y1 - y2);
        //
        //    return MathF.Sqrt( (float)(xDist * xDist + yDist * yDist) );
        //}
    }
}