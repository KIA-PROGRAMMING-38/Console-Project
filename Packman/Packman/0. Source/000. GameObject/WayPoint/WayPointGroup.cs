using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class WayPointGroup : GameObject
    {
        private List<WayPoint> _wayPoints;
        private int _wayPointCount = 0;

        public WayPointGroup()
            : base( 0, 0, 0 )
        {
            _wayPoints = null;
        }

        /// <summary>
        /// 초기화..
        /// </summary>
        /// <param name="wayPoints"> 모든 WayPoint 들 </param>
        public void Initialize( List<WayPoint> wayPoints )
        {
            // WayPoint 들 받는다..
            _wayPointCount = wayPoints.Count;
            _wayPoints = wayPoints;

            // WayPoint와 인접한 WayPoint들 연결시켜준다..
            ConnectWayPoint();
        }

        public override void Update()
        {
            //base.Update();
        }

        public override void Render()
        {
            //base.Render();

            foreach ( WayPoint wayPoint in _wayPoints )
            {
                wayPoint.Render();
            }
        }

        public WayPoint FindNearWayPoint( in GameObject gameobject )
        {
            Debug.Assert( null != gameobject );

            int objectPosX = gameobject.X;
            int objectPosY = gameobject.Y;

            int mostSmallerDist = int.MaxValue;
            WayPoint mostNearWayPoint = null;

            foreach(WayPoint wayPoint in _wayPoints)
            {
                int wayPointX = wayPoint.X;
                int wayPointY = wayPoint.Y;

                int dist = Math.Abs(wayPointX - objectPosX) + Math.Abs(wayPointY - objectPosY);
                if(dist < mostSmallerDist)
                {
                    mostSmallerDist = dist;
                    mostNearWayPoint = wayPoint;
                }
            }

            return mostNearWayPoint;
        }

        /// <summary>
        /// 일직선 상에 있는 WayPoint 들끼리 연결시켜준다..
        /// </summary>
        private void ConnectWayPoint()
        {
            for( int dstIndex = 0; dstIndex < _wayPointCount; ++dstIndex )
            {
                for ( int srcIndex = 0; srcIndex < _wayPointCount; ++srcIndex )
                {
                    // 인덱스가 같다 == 같은 녀석이다 == 검사할 필요 없다..
                    if(dstIndex == srcIndex)
                    {
                        continue;
                    }

                    // 가로, 세로 길이 구하기..
                    //int xDist = Math.Abs(_wayPoints[dstIndex].X - _wayPoints[srcIndex].X);
                    //int yDist = Math.Abs(_wayPoints[dstIndex].Y - _wayPoints[srcIndex].Y);
                    //
                    //// 일직선상에 있다 == 가로, 세로 둘중 하나만 같아야한다..
                    //if ( (0 == xDist && 0 != yDist) ||  (0 == yDist && 0 != xDist) )
                    //{
                    //    // 일직선상에 있다면 연결시켜준다..
                    //    _wayPoints[dstIndex].Connect( _wayPoints[srcIndex] );
                    //    _wayPoints[srcIndex].Connect( _wayPoints[dstIndex] );
                    //}

                    _wayPoints[dstIndex].Connect( _wayPoints[srcIndex] );
                    _wayPoints[srcIndex].Connect( _wayPoints[dstIndex] );
                }
            }
        }
    }
}
