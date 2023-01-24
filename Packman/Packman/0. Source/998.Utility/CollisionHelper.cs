using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class CollisionHelper
    {
        public static bool CollisionObjectToObject(GameObject dstObject, GameObject srcObject)
        {
            // dstObject나 srcObject 가 null이거나 비활성화중이라면 검사안함..
            if ( null == dstObject || false == dstObject.IsActive || null == srcObject || false == srcObject.IsActive )
            {
                return false;
            }

            return IsSamePosition( dstObject, srcObject );
        }

        public static bool IsSamePosition( GameObject dstObject, GameObject srcObject )
        {
            Debug.Assert( null != dstObject && true == dstObject.IsActive );
            Debug.Assert( null != srcObject && true == srcObject.IsActive );

            // 현재 위치가 같은지 검사..
            if ( IsSamePosition( dstObject.X, dstObject.Y, srcObject.X, srcObject.Y ) )
                return true;

            return false;
        }

        public static bool IsSamePosition( int x1, int y1, int x2, int y2 )
        {
            if ( x1 == x2 && y1 == y2 )
            {
                return true;
            }

            return false;
        }
    }
}
