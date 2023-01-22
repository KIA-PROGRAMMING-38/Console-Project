using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class WayPoint : GameObject
    {
        private List<WayPoint> _nearPoints = new List<WayPoint>();
        private int _nearPointCount = 0;

        public int NearPointCount { get { return _nearPointCount; } }

        public WayPoint GetNearPoint(int index)
        {
            return _nearPoints[index];
        }

        public WayPoint( int x, int y, int renderOrder )
            : base( x, y, renderOrder )
        {

        }

        /// <summary>
        /// 인접한 WayPoint 를 연결한다..
        /// <para></para>
        /// 인접한 WayPoint는 WayPointGroup 에서 연산해 보내준다..
        /// </summary>
        /// <param name="nearPoint"> 인접한 WayPoint </param>
        public void Connect(WayPoint nearPoint)
        {
            Debug.Assert( null != nearPoint );

            _nearPoints.Add( nearPoint );
            _nearPointCount = _nearPoints.Count;
		}
    }
}
